using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Apple
{
    [AudioDecoderExport(".m4a")]
    public sealed class AlacAudioDecoder : IAudioDecoder, IDisposable
    {
        const uint _defaultFrameCount = 4096;

        [CanBeNull] AudioFile _audioFile;
        AudioStreamBasicDescription _inputDescription;
        [CanBeNull] AudioConverter _converter;
        IntPtr _magicCookie;
        [CanBeNull] int[] _buffer;

        public bool Finished { get; private set; }

        public void Initialize(FileStream fileStream)
        {
            _audioFile = new AudioFile(AudioFileType.M4A, fileStream);

            _inputDescription = _audioFile.GetProperty<AudioStreamBasicDescription>(AudioFilePropertyId.DataFormat);
            if (_inputDescription.AudioFormat != AudioFormat.AppleLossless)
                throw new AudioUnsupportedException($"{fileStream.Name} is not an Apple Lossless file.", fileStream.Name);
            var outputDescription = GetOutputDescription(_inputDescription);

            _converter = new AudioConverter(ref _inputDescription, ref outputDescription, _audioFile);
            _magicCookie = GetMagicCookie(_audioFile, _converter);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public SampleBuffer DecodeSamples()
        {
            if (_buffer == null)
                _buffer = new int[_defaultFrameCount * _inputDescription.ChannelsPerFrame];

            var bufferHandle = GCHandle.Alloc(_buffer, GCHandleType.Pinned);

            try
            {
                var bufferList = new AudioBufferList
                {
                    NumberBuffers = 1,
                    Buffers = new AudioBuffer[1]
                };
                bufferList.Buffers[0].NumberChannels = _inputDescription.ChannelsPerFrame;
                bufferList.Buffers[0].DataByteSize = (uint) _buffer.Length;
                bufferList.Buffers[0].Data = bufferHandle.AddrOfPinnedObject();

                var frameCount = _defaultFrameCount;
                _converter.FillBuffer(ref frameCount, ref bufferList, null);

                if (frameCount == 0)
                    Finished = true;

                var result = new SampleBuffer((int) _inputDescription.ChannelsPerFrame, (int) frameCount);
                result.CopyFromInterleaved(_buffer);

                return result;
            }
            finally
            {
                bufferHandle.Free();
            }
        }

        public void Dispose()
        {
            _converter?.Dispose();
            _audioFile?.Dispose();
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void FreeUnmanaged()
        {
            Marshal.FreeHGlobal(_magicCookie);
        }

        static AudioStreamBasicDescription GetOutputDescription(AudioStreamBasicDescription inputDescription)
        {
            uint bitsPerSample;
            switch (inputDescription.Flags)
            {
                case AudioFormatFlags.Alac16BitSourceData:
                    bitsPerSample = 16;
                    break;
                case AudioFormatFlags.Alac20BitSourceData:
                    bitsPerSample = 20;
                    break;
                case AudioFormatFlags.Alac24BitSourceData:
                    bitsPerSample = 24;
                    break;
                case AudioFormatFlags.Alac32BitSourceData:
                    bitsPerSample = 32;
                    break;
                default:
                    throw new AudioUnsupportedException("Unknown audio format.");
            }

            return new AudioStreamBasicDescription
            {
                AudioFormat = AudioFormat.LinearPcm,
                Flags = AudioFormatFlags.PcmIsSignedInteger | AudioFormatFlags.PcmIsAlignedHigh,
                BytesPerPacket = sizeof(int) * inputDescription.ChannelsPerFrame,
                FramesPerPacket = 1,
                BytesPerFrame = sizeof(int) * inputDescription.ChannelsPerFrame,
                ChannelsPerFrame = inputDescription.ChannelsPerFrame,
                BitsPerChannel = bitsPerSample,
                SampleRate = inputDescription.SampleRate
            };
        }

        static IntPtr GetMagicCookie([NotNull] AudioFile audioFile, [NotNull] AudioConverter converter)
        {
            audioFile.GetPropertyInfo(AudioFilePropertyId.MagicCookieData, out var dataSize, out _);
            var cookie = audioFile.GetProperty(AudioFilePropertyId.MagicCookieData, dataSize);
            converter.SetProperty(AudioConverterPropertyId.DecompressionMagicCookie, dataSize, cookie);
            return cookie;
        }

        ~AlacAudioDecoder()
        {
            FreeUnmanaged();
        }
    }
}
