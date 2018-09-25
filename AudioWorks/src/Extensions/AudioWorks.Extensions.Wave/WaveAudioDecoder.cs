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
#if !NETCOREAPP2_1
using System.Buffers;
#endif
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;
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
            if (_reader.Read(buffer) < buffer.Length)
                throw new AudioInvalidException("File is unexpectedly truncated.",
                    ((FileStream) _reader.BaseStream).Name);

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
                if (_reader.Read(buffer, 0, length) < length)
                    throw new AudioInvalidException("File is unexpectedly truncated.",
                        ((FileStream) _reader.BaseStream).Name);

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
