using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Wave
{
    [AudioDecoderExport(".wav")]
    public sealed class WaveAudioDecoder : IAudioDecoder, IDisposable
    {
        const int _defaultFrameCount = 4096;

        [CanBeNull] AudioInfo _audioInfo;
        [CanBeNull] RiffReader _reader;
        int _bitsPerSample;
        int _bytesPerSample;
        long _framesRemaining;

        public bool Finished => _framesRemaining == 0;

        public void Initialize(FileStream fileStream)
        {
            _audioInfo = new WaveAudioInfoDecoder().ReadAudioInfo(fileStream);
            _reader = new RiffReader(fileStream);
            _bitsPerSample = _audioInfo.BitsPerSample;
            _bytesPerSample = (int) Math.Ceiling(_audioInfo.BitsPerSample / 8.0);
            _framesRemaining = _audioInfo.FrameCount;

            _reader.Initialize();
            _reader.SeekToChunk("data");
        }

        public SampleBuffer DecodeSamples()
        {
#if NETCOREAPP2_1
            // ReSharper disable once PossibleNullReferenceException
            Span<byte> buffer = stackalloc byte[_audioInfo.Channels *
                                                (int) Math.Min(_framesRemaining, _defaultFrameCount)
                                                * _bytesPerSample];
            // ReSharper disable once PossibleNullReferenceException
            _reader.Read(buffer);
            //TODO Throw if read count is less than buffer size

            var result = new SampleBuffer(buffer, _audioInfo.Channels, _bitsPerSample);
            _framesRemaining -= result.Frames;
            return result;
#else
            // ReSharper disable once PossibleNullReferenceException
            var length = _audioInfo.Channels * (int) Math.Min(_framesRemaining, _defaultFrameCount) * _bytesPerSample;

            var buffer = ArrayPool<byte>.Shared.Rent(length);
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                _reader.Read(buffer, 0, length);
                //TODO Throw if read count is less than length

                var result = new SampleBuffer(
                    buffer.AsSpan().Slice(0, length),
                    _audioInfo.Channels,
                    _bitsPerSample);
                _framesRemaining -= result.Frames;
                return result;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
#endif
        }

        public void Dispose()
        {
            _reader?.Dispose();
        }
    }
}
