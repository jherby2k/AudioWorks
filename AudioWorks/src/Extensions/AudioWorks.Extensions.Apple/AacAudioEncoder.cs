/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Composition;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Apple
{
    [AudioEncoderExport("AppleAAC", "MP4 Advanced Audio Codec via Apple")]
    public sealed class AacAudioEncoder : IAudioEncoder, IDisposable
    {
        static readonly uint[] _vbrQualities = { 0, 9, 18, 27, 36, 45, 54, 63, 73, 82, 91, 100, 109, 118, 127 };

        [CanBeNull] FileStream _fileStream;
        [CanBeNull] AudioMetadata _metadata;
        [CanBeNull] SettingDictionary _settings;
        [CanBeNull] ExtendedAudioFile _audioFile;
        [CanBeNull] Export<IAudioFilter> _replayGainExport;

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

        public string FileExtension { get; } = ".m4a";

        public void Initialize(FileStream fileStream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            _fileStream = fileStream;
            _metadata = metadata;
            _settings = settings;

            InitializeReplayGainFilter(info, metadata, settings);

            var inputDescription = GetInputDescription(info);
            _audioFile = new ExtendedAudioFile(GetOutputDescription(inputDescription), AudioFileType.M4A, fileStream);
            _audioFile.SetProperty(ExtendedAudioFilePropertyId.ClientDataFormat, inputDescription);

            // Configure the audio converter
            var converter = _audioFile.GetProperty<IntPtr>(ExtendedAudioFilePropertyId.AudioConverter);

            // Enable high quality (defaults to medium, 0x40)
            SetConverterProperty(converter, AudioConverterPropertyId.CodecQuality, 0x60);

            if (settings.TryGetValue<int>("BitRate", out var bitRate))
            {
                // Stereo has a minimum of 64, Mono has a maximum of 256
                bitRate = info.Channels == 1 ? Math.Min(bitRate, 256) : Math.Max(bitRate, 64);

                SetConverterProperty(converter, AudioConverterPropertyId.BitRate, bitRate * 1000);

                // Set the control mode (constrained is the default)
                var controlMode = BitrateControlMode.VariableConstrained;
                if (settings.TryGetValue<string>("ControlMode", out var controlModeValue))
                    if (controlModeValue.Equals("Average", StringComparison.OrdinalIgnoreCase))
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
                    settings.TryGetValue<int>("VBRQuality", out var quality)
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
            bufferList.Buffers[0].DataByteSize = (uint) (buffer.Length * Marshal.SizeOf<float>());
            bufferList.Buffers[0].Data = new IntPtr(Unsafe.AsPointer(ref buffer.GetPinnableReference()));

            // ReSharper disable once PossibleNullReferenceException
            var status = _audioFile.Write(bufferList, (uint)samples.Frames);
            if (status != ExtendedAudioFileStatus.Ok)
                throw new AudioEncodingException($"Apple AAC encoder encountered error '{status}'.");
        }

        public void Finish()
        {
            // ReSharper disable once PossibleNullReferenceException
            _audioFile.Dispose();
            _audioFile = null;

            // ReSharper disable once PossibleNullReferenceException
            _fileStream.Position = 0;

            // Call the external MP4 encoder for writing iTunes-compatible atoms
            var metadataEncoderFactory =
                ExtensionProvider.GetFactories<IAudioMetadataEncoder>("Extension", FileExtension).FirstOrDefault();
            if (metadataEncoderFactory == null) return;
            using (var export = metadataEncoderFactory.CreateExport())
                // ReSharper disable twice AssignNullToNotNullAttribute
                export.Value.WriteMetadata(_fileStream, _metadata, _settings);
        }

        public void Dispose()
        {
            _audioFile?.Dispose();
            _replayGainExport?.Dispose();
        }

        [Pure]
        static AudioStreamBasicDescription GetInputDescription([NotNull] AudioInfo info)
        {
            return new AudioStreamBasicDescription
            {
                SampleRate = info.SampleRate,
                AudioFormat = AudioFormat.LinearPcm,
                Flags = AudioFormatFlags.PcmIsFloat,
                BytesPerPacket = (uint) (Marshal.SizeOf<float>() * info.Channels),
                FramesPerPacket = 1,
                BytesPerFrame = (uint) (Marshal.SizeOf<float>() * info.Channels),
                ChannelsPerFrame = (uint) info.Channels,
                BitsPerChannel = 32
            };
        }

        [Pure]
        static AudioStreamBasicDescription GetOutputDescription(AudioStreamBasicDescription inputDescription)
        {
            var result = new AudioStreamBasicDescription
            {
                SampleRate = inputDescription.SampleRate,
                FramesPerPacket = 1024,
                AudioFormat = AudioFormat.AacLowComplexity,
                ChannelsPerFrame = inputDescription.ChannelsPerFrame
            };

            switch (inputDescription.SampleRate)
            {
                case 192000:
                case 144000:
                case 128000: // conversion required
                case 96000:
                case 64000:  // conversion required
                case 48000:
                    result.SampleRate = 48000;
                    break;

                case 176400:
                case 88200:
                case 44100:
                case 37800:  // conversion required
                case 36000:  // conversion required
                    result.SampleRate = 44100;
                    break;

                case 32000:
                case 28000:  // conversion required
                    result.SampleRate = 32000;
                    break;

                case 22050:
                case 18900:  // conversion required
                    result.SampleRate = 22050;
                    break;
                default:
                    throw new AudioUnsupportedException(
                        $"Apple AAC does not support a {inputDescription.SampleRate} Hz sample rate.");
            }

            return result;
        }

        void InitializeReplayGainFilter(
            [NotNull] AudioInfo info,
            [NotNull] AudioMetadata metadata,
            [NotNull] SettingDictionary settings)
        {
            var filterFactory =
                ExtensionProvider.GetFactories<IAudioFilter>("Name", "ReplayGain").FirstOrDefault();
            if (filterFactory == null) return;

            _replayGainExport = filterFactory.CreateExport();
            // ReSharper disable once PossibleNullReferenceException
            _replayGainExport.Value.Initialize(info, metadata, settings);
        }

        static void SetConverterProperty<T>(IntPtr converter, AudioConverterPropertyId propertyId, T value)
            where T : struct
        {
            var unmanagedValueSize = Marshal.SizeOf(typeof(T));
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
