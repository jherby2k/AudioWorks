using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using AudioWorks.Common;
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

                return result;
            }
        }

        public string FileExtension { get; } = ".m4a";

        public void Initialize(FileStream fileStream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            _fileStream = fileStream;
            _metadata = metadata;
            _settings = settings;

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

            var bufferSize = samples.Frames * samples.Channels;
            Span<int> buffer = stackalloc int[bufferSize];
            samples.CopyToInterleaved(buffer, 32);

            fixed (int* bufferPointer = &MemoryMarshal.GetReference(buffer))
            {
                var bufferList = new AudioBufferList
                {
                    NumberBuffers = 1,
                    Buffers = new AudioBuffer[1]
                };
                bufferList.Buffers[0].NumberChannels = (uint) samples.Channels;
                bufferList.Buffers[0].DataByteSize = (uint) (bufferSize * Marshal.SizeOf<int>());
                bufferList.Buffers[0].Data = new IntPtr(bufferPointer);

                // ReSharper disable once PossibleNullReferenceException
                _audioFile.Write(bufferList, (uint) samples.Frames);
            }
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void Finish()
        {
            _audioFile.Dispose();
            _audioFile = null;

            _fileStream.Position = 0;

            // Call the external MP4 encoder for writing iTunes-compatible atoms
            var metadataEncoderFactory =
                ExtensionProvider.GetFactories<IAudioMetadataEncoder>("Extension", FileExtension).FirstOrDefault();
            if (metadataEncoderFactory == null) return;
            using (var export = metadataEncoderFactory.CreateExport())
                export.Value.WriteMetadata(_fileStream, _metadata, _settings);
        }

        public void Dispose()
        {
            _audioFile?.Dispose();
        }

        [Pure]
        static AudioStreamBasicDescription GetInputDescription([NotNull] AudioInfo info)
        {
            return new AudioStreamBasicDescription
            {
                SampleRate = info.SampleRate,
                AudioFormat = AudioFormat.LinearPcm,
                Flags = AudioFormatFlags.PcmIsSignedInteger | AudioFormatFlags.PcmIsPacked,
                BytesPerPacket = 4 * (uint) info.Channels,
                FramesPerPacket = 1,
                BytesPerFrame = 4 * (uint) info.Channels,
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
