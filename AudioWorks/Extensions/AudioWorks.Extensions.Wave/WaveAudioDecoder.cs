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
        byte[] _buffer;

        public bool Finished => _framesRemaining == 0;

        public void Initialize(FileStream fileStream)
        {
            _audioInfo = new WaveAudioInfoDecoder().ReadAudioInfo(fileStream);
            _reader = new RiffReader(fileStream);
            _bitsPerSample = _audioInfo.BitsPerSample;
            _bytesPerSample = (int) Math.Ceiling(_audioInfo.BitsPerSample / 8.0);
            _framesRemaining = _audioInfo.SampleCount;

            _reader.Initialize();
            _reader.SeekToChunk("data");
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public SampleBuffer DecodeSamples()
        {
            var length = _audioInfo.Channels * (int) Math.Min(_framesRemaining, _defaultFrameCount) * _bytesPerSample;

            if (_buffer == null)
                _buffer = ArrayPool<byte>.Shared.Rent(length);

            _reader.Read(_buffer, 0, length);
            //TODO Throw if count is less than length

            var result = new SampleBuffer(
                _buffer.AsReadOnlySpan().Slice(0, length),
                _audioInfo.Channels,
                _bitsPerSample);

            _framesRemaining -= result.Frames;
            return result;
        }

        public void Dispose()
        {
            _reader?.Dispose();
            if (_buffer != null)
                ArrayPool<byte>.Shared.Return(_buffer);
        }
    }
}
