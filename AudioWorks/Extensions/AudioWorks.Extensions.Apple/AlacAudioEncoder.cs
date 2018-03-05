using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Apple
{
    [AudioEncoderExport("ALAC", "Apple Lossless Audio Codec")]
    public sealed class AlacAudioEncoder : IAudioEncoder, IDisposable
    {
        [CanBeNull] FileStream _fileStream;
        [CanBeNull] AudioMetadata _metadata;
        [CanBeNull] SettingDictionary _settings;
        float _multiplier;
        [CanBeNull] ExtendedAudioFile _audioFile;

        public SettingInfoDictionary SettingInfo
        {
            get
            {
                // Use the external MP4 encoder's SettingInfo
                var metadataEncoderFactory =
                    ExtensionProvider.GetFactories<IAudioMetadataEncoder>("Extension", FileExtension).FirstOrDefault();
                if (metadataEncoderFactory == null) return new SettingInfoDictionary();
                using (var export = metadataEncoderFactory.CreateExport())
                    return export.Value.SettingInfo;
            }
        }

        public string FileExtension { get; } = ".m4a";

        public void Initialize(FileStream fileStream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            _fileStream = fileStream;
            _metadata = metadata;
            _settings = settings;
            _multiplier = (float) Math.Pow(2, info.BitsPerSample - 1);

            var inputDescription = GetInputDescription(info);
            _audioFile = new ExtendedAudioFile(GetOutputDescription(inputDescription), AudioFileType.M4A, fileStream);
            _audioFile.SetProperty(ExtendedAudioFilePropertyId.ClientDataFormat, inputDescription);
        }

        public unsafe void Submit(SampleCollection samples)
        {
            if (samples.Frames == 0) return;

            var bufferSize = samples.Frames * samples.Channels;
            var bufferAddress = Marshal.AllocHGlobal(bufferSize * Marshal.SizeOf<int>());
            try
            {
                var buffer = new Span<int>(bufferAddress.ToPointer(), bufferSize);

                // Interlace the samples in integer format, and store them in the unmanaged buffer
                var index = 0;
                for (var frameIndex = 0; frameIndex < samples.Frames; frameIndex++)
                for (var channelIndex = 0; channelIndex < samples.Channels; channelIndex++)
                    buffer[index++] = (int) Math.Round(samples[channelIndex][frameIndex] * _multiplier);

                var bufferList = new AudioBufferList
                {
                    NumberBuffers = 1,
                    Buffers = new AudioBuffer[1]
                };
                bufferList.Buffers[0].NumberChannels = (uint)samples.Channels;
                bufferList.Buffers[0].DataByteSize = (uint)(index * Marshal.SizeOf<int>());
                bufferList.Buffers[0].Data = bufferAddress;

                // ReSharper disable once PossibleNullReferenceException
                _audioFile.Write(bufferList, (uint) samples.Frames);
            }
            finally
            {
                Marshal.FreeHGlobal(bufferAddress);
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
                Flags = AudioFormatFlags.PcmIsSignedInteger,
                BytesPerPacket = 4 * (uint) info.Channels,
                FramesPerPacket = 1,
                BytesPerFrame = 4 * (uint) info.Channels,
                ChannelsPerFrame = (uint) info.Channels,
                BitsPerChannel = (uint) info.BitsPerSample
            };
        }

        [Pure]
        static AudioStreamBasicDescription GetOutputDescription(AudioStreamBasicDescription inputDescription)
        {
            var result = new AudioStreamBasicDescription
            {
                SampleRate = inputDescription.SampleRate,
                FramesPerPacket = 4096,
                AudioFormat = AudioFormat.AppleLossless,
                ChannelsPerFrame = inputDescription.ChannelsPerFrame
            };

            switch (inputDescription.BitsPerChannel)
            {
                case 16:
                    result.Flags = AudioFormatFlags.Alac16BitSourceData;
                    break;
                case 20:
                    result.Flags = AudioFormatFlags.Alac20BitSourceData;
                    break;
                case 24:
                    result.Flags = AudioFormatFlags.Alac24BitSourceData;
                    break;
                case 32:
                    result.Flags = AudioFormatFlags.Alac32BitSourceData;
                    break;
                default:
                    throw new AudioUnsupportedException(
                        $"ALAC does not support {inputDescription.BitsPerChannel}-bit audio.");
            }

            return result;
        }
    }
}
