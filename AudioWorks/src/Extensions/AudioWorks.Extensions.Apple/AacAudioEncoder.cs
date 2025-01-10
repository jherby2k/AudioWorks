/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Composition;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensions.Apple
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioEncoderExport("AppleAAC", "Apple MPEG-4 Advanced Audio Codec")]
    sealed class AacAudioEncoder : IAudioEncoder, IDisposable
    {
        static readonly uint[] _vbrQualities = [0, 9, 18, 27, 36, 45, 54, 63, 73, 82, 91, 100, 109, 118, 127];

        Stream? _stream;
        AudioMetadata? _metadata;
        SettingDictionary? _settings;
        ExtendedAudioFile? _audioFile;
        Export<IAudioFilter>? _replayGainExport;

        public SettingInfoDictionary SettingInfo
        {
            get
            {
                var result = new SettingInfoDictionary
                {
                    ["VBRQuality"] = new IntSettingInfo(0, 14),
                    ["BitRate"] = new IntSettingInfo(32, 320),
                    ["ControlMode"] = new StringSettingInfo("Constrained", "Average", "Constant")
                };

                // Merge the external MP4 encoder's SettingInfo
                var metadataEncoderFactory =
                    ExtensionProvider.GetFactories<IAudioMetadataEncoder>("Extension", FileExtension).FirstOrDefault();
                if (metadataEncoderFactory != null)
                    using (var export = metadataEncoderFactory.CreateExport())
                        foreach (var settingInfo in export.Value.SettingInfo)
                            result.Add(settingInfo.Key, settingInfo.Value);

                // Merge the external ReplayGain filter's SettingInfo
                var filterFactory =
                    ExtensionProvider.GetFactories<IAudioFilter>("Name", "ReplayGain").FirstOrDefault();
                // ReSharper disable once InvertIf
                if (filterFactory != null)
                    using (var export = filterFactory.CreateExport())
                        foreach (var settingInfo in export.Value.SettingInfo)
                            result.Add(settingInfo.Key, settingInfo.Value);

                return result;
            }
        }

        public string FileExtension => ".m4a";

        public void Initialize(Stream stream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            _stream = stream;
            _metadata = metadata;
            _settings = settings;

            InitializeReplayGainFilter(info, metadata, settings);

            var inputDescription = GetInputDescription(info);
            _audioFile = new(GetOutputDescription(inputDescription), AudioFileType.M4A, stream);
            _audioFile.SetProperty(ExtendedAudioFilePropertyId.ClientDataFormat, inputDescription);

            var converter = _audioFile.GetProperty<IntPtr>(ExtendedAudioFilePropertyId.AudioConverter);

            var logger = LoggerManager.LoggerFactory.CreateLogger<AacAudioEncoder>();

            // Enable high quality (defaults to medium, 0x40)
            SetConverterProperty(converter, AudioConverterPropertyId.CodecQuality, 0x60);

            if (settings.TryGetValue("BitRate", out int bitRate))
            {
                switch (bitRate)
                {
                    case > 256 when info.Channels == 1:
                        logger.LogWarning("The maximum bitrate for 1-channel audio is 256 kbps.");
                        bitRate = 256;
                        break;
                    case < 64 when info.Channels == 2:
                        logger.LogWarning("The minimum bitrate for 2-channel audio is 64 kbps.");
                        bitRate = 64;
                        break;
                }

                SetConverterProperty(converter, AudioConverterPropertyId.BitRate, bitRate * 1000);

                // Set the control mode (constrained is the default)
                var controlMode = BitrateControlMode.VariableConstrained;
                if (settings.TryGetValue("ControlMode", out string? controlModeValue))
                    if (controlModeValue!.Equals("Average", StringComparison.OrdinalIgnoreCase))
                        controlMode = BitrateControlMode.LongTermAverage;
                    else if (controlModeValue.Equals("Constant", StringComparison.OrdinalIgnoreCase))
                        controlMode = BitrateControlMode.Constant;
                SetConverterProperty(converter, AudioConverterPropertyId.BitRateControlMode, (uint) controlMode);
            }
            else
            {
                // Enable a true variable bitrate
                SetConverterProperty(converter, AudioConverterPropertyId.BitRateControlMode,
                    (uint) BitrateControlMode.Variable);

                // Use a default VBR quality index of 9
                SetConverterProperty(converter, AudioConverterPropertyId.VbrQuality,
                    settings.TryGetValue("VBRQuality", out int quality)
                        ? _vbrQualities[quality]
                        : _vbrQualities[9]);
            }

            // Setting the ConverterConfig property to null resynchronizes the converter settings
            _audioFile.SetProperty(ExtendedAudioFilePropertyId.ConverterConfig, IntPtr.Zero);
        }

        public unsafe void Submit(SampleBuffer samples)
        {
            if (samples.Frames == 0) return;

            if (_replayGainExport != null)
                samples = _replayGainExport.Value.Process(samples);

            Span<float> buffer = stackalloc float[samples.Frames * samples.Channels];
            samples.CopyToInterleaved(buffer);

            var bufferList = new AudioBufferList
            {
                NumberBuffers = 1,
                Buffers = new AudioBuffer[1]
            };
            bufferList.Buffers[0].NumberChannels = (uint) samples.Channels;
            bufferList.Buffers[0].DataByteSize = (uint) (buffer.Length * sizeof(float));
            bufferList.Buffers[0].Data = new(Unsafe.AsPointer(ref MemoryMarshal.GetReference(buffer)));

            var status = _audioFile!.Write(bufferList, (uint)samples.Frames);
            if (status != ExtendedAudioFileStatus.Ok)
                throw new AudioEncodingException($"Apple AAC encoder encountered error '{status}'.");
        }

        public void Finish()
        {
            _audioFile!.Dispose();
            _audioFile = null;

            _stream!.Position = 0;

            // Call the external MP4 encoder for writing iTunes-compatible atoms
            var metadataEncoderFactory =
                ExtensionProvider.GetFactories<IAudioMetadataEncoder>("Extension", FileExtension).FirstOrDefault();
            if (metadataEncoderFactory == null) return;
            using (var export = metadataEncoderFactory.CreateExport())
                export.Value.WriteMetadata(_stream, _metadata!, _settings!);
        }

        public void Dispose()
        {
            _audioFile?.Dispose();
            _replayGainExport?.Dispose();
        }

        static AudioStreamBasicDescription GetInputDescription(AudioInfo info) => new()
        {
            SampleRate = info.SampleRate,
            AudioFormat = AudioFormat.LinearPcm,
            Flags = AudioFormatFlags.PcmIsFloat,
            BytesPerPacket = (uint) (sizeof(float) * info.Channels),
            FramesPerPacket = 1,
            BytesPerFrame = (uint) (sizeof(float) * info.Channels),
            ChannelsPerFrame = (uint) info.Channels,
            BitsPerChannel = 32
        };

        static AudioStreamBasicDescription GetOutputDescription(AudioStreamBasicDescription inputDescription)
        {
            var result = new AudioStreamBasicDescription
            {
                SampleRate = inputDescription.SampleRate,
                FramesPerPacket = 1024,
                AudioFormat = AudioFormat.AacLowComplexity,
                ChannelsPerFrame = inputDescription.ChannelsPerFrame
            };

            result.SampleRate = inputDescription.SampleRate switch
            {
                192000 => 48000,
                144000 => 48000,
                128000 => 48000,
                96000 => 48000,
                64000 => 48000,
                48000 => 48000,
                176400 => 44100,
                88200 => 44100,
                44100 => 44100,
                37800 => 44100,
                36000 => 44100,
                32000 => 32000,
                28000 => 32000,
                22050 => 22050,
                18900 => 22050,
                _ => throw new AudioUnsupportedException(
                    $"Apple AAC does not support a {inputDescription.SampleRate} Hz sample rate.")
            };

            return result;
        }

        void InitializeReplayGainFilter(AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            var filterFactory =
                ExtensionProvider.GetFactories<IAudioFilter>("Name", "ReplayGain").FirstOrDefault();
            if (filterFactory == null) return;

            _replayGainExport = filterFactory.CreateExport();
            _replayGainExport.Value.Initialize(info, metadata, settings);
        }

        static void SetConverterProperty<T>(IntPtr converter, AudioConverterPropertyId propertyId, T value)
            where T : struct
        {
            var unmanagedValueSize = Marshal.SizeOf<T>();
            var unmanagedValue = Marshal.AllocHGlobal(unmanagedValueSize);
            try
            {
                Marshal.StructureToPtr(value, unmanagedValue, false);
                SafeNativeMethods.AudioConverterSetProperty(converter, propertyId, (uint) unmanagedValueSize,
                    unmanagedValue);
            }
            finally
            {
                Marshal.FreeHGlobal(unmanagedValue);
            }
        }
    }
}
