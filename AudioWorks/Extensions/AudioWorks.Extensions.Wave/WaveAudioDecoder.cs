using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Composition;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace AudioWorks.Extensions.Wave
{
    [Shared]
    [AudioDecoderExport(".wav")]
    public sealed class WaveAudioDecoder : IAudioDecoder, IDisposable
    {
        [NotNull] readonly byte[] _buffer = new byte[4];
        [CanBeNull] AudioInfo _audioInfo;
        [CanBeNull] RiffReader _reader;
        int _bytesPerSample;
        float _divisor;
        long _framesRemaining;

        public bool Finished => _framesRemaining == 0;

        public void Initialize(FileStream fileStream)
        {
            _audioInfo = new WaveAudioInfoDecoder().ReadAudioInfo(fileStream);
            _reader = new RiffReader(fileStream);
            _bytesPerSample = (int) Math.Ceiling(_audioInfo.BitsPerSample / 8.0);
            _divisor = (float) Math.Pow(2, _audioInfo.BitsPerSample - 1);
            _framesRemaining = _audioInfo.SampleCount;

            _reader.Initialize();
            _reader.SeekToChunk("data");
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public SampleCollection DecodeSamples()
        {
            var result = new SampleCollection(
                _audioInfo.Channels,
                (int) Math.Min(_framesRemaining, SampleCollection.MaxFrames));

            // 1-8 bit samples are unsigned:
            if (_bytesPerSample == 1)
                for (var frame = 0; frame < result.Frames; frame++)
                for (var channel = 0; channel < result.Channels; channel++)
                    result[channel][frame] = (_reader.ReadByte() - 128) / _divisor;
            else
                for (var sample = 0; sample < result.Frames; sample++)
                for (var channel = 0; channel < result.Channels; channel++)
                {
                    _reader.Read(_buffer, 4 - _bytesPerSample, _bytesPerSample);
                    result[channel][sample] =
                        (BitConverter.ToInt32(_buffer, 0) >> (4 - _bytesPerSample) * 8) / _divisor;
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
