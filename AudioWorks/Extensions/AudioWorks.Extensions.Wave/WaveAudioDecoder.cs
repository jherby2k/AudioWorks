using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace AudioWorks.Extensions.Wave
{
    [AudioDecoderExport(".wav")]
    public sealed class WaveAudioDecoder : IAudioDecoder, IDisposable
    {
        const int _defaultFrameCount = 4096;

        [NotNull] readonly byte[] _buffer = new byte[4];
        [CanBeNull] AudioInfo _audioInfo;
        [CanBeNull] RiffReader _reader;
        int _bytesPerSample;
        long _framesRemaining;

        public bool Finished => _framesRemaining == 0;

        public void Initialize(FileStream fileStream)
        {
            _audioInfo = new WaveAudioInfoDecoder().ReadAudioInfo(fileStream);
            _reader = new RiffReader(fileStream);
            _bytesPerSample = (int) Math.Ceiling(_audioInfo.BitsPerSample / 8.0);
            _framesRemaining = _audioInfo.SampleCount;

            _reader.Initialize();
            _reader.SeekToChunk("data");
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public SampleCollection DecodeSamples()
        {
            var result = new SampleCollection(
                _audioInfo.Channels,
                (int) Math.Min(_framesRemaining, _defaultFrameCount));

            // 1-8 bit samples are unsigned:
            if (_bytesPerSample == 1)
                for (var frameIndex = 0; frameIndex < result.Frames; frameIndex++)
                for (var channelIndex = 0; channelIndex < result.Channels; channelIndex++)
                    result[channelIndex][frameIndex] = (_reader.ReadByte() - 128) / (float) 128;
            else
                for (var frameIndex = 0; frameIndex < result.Frames; frameIndex++)
                for (var channelIndex = 0; channelIndex < result.Channels; channelIndex++)
                {
                    _reader.Read(_buffer, 4 - _bytesPerSample, _bytesPerSample);
                    result[channelIndex][frameIndex] = BitConverter.ToInt32(_buffer, 0) / (float) 0x8000_0000;
                }

            _framesRemaining -= result.Frames;
            return result;
        }

        public void Dispose()
        {
            _reader?.Dispose();
        }
    }
}
