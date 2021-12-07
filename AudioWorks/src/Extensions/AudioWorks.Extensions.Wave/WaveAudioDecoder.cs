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
#if NETSTANDARD2_0
using System.Buffers;
#endif
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Wave
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioDecoderExport(".wav")]
    sealed class WaveAudioDecoder : IAudioDecoder, IDisposable
    {
        enum DataFormat
        {
            Lpcm,
            ALaw,
            ΜLaw
        }

        const int _defaultFrameCount = 4096;

        static readonly short[] _aLawDecodeValues = {
            -5504, -5248, -6016, -5760, -4480, -4224, -4992, -4736,
            -7552, -7296, -8064, -7808, -6528, -6272, -7040, -6784,
            -2752, -2624, -3008, -2880, -2240, -2112, -2496, -2368,
            -3776, -3648, -4032, -3904, -3264, -3136, -3520, -3392,
            -22016, -20992, -24064, -23040, -17920, -16896, -19968, -18944,
            -30208, -29184, -32256, -31232, -26112, -25088, -28160, -27136,
            -11008, -10496, -12032, -11520, -8960, -8448, -9984, -9472,
            -15104, -14592, -16128, -15616, -13056, -12544, -14080, -13568,
            -344, -328, -376, -360, -280, -264, -312, -296,
            -472, -456, -504, -488, -408, -392, -440, -424,
            -88, -72, -120, -104, -24, -8, -56, -40,
            -216, -200, -248, -232, -152, -136, -184, -168,
            -1376, -1312, -1504, -1440, -1120, -1056, -1248, -1184,
            -1888, -1824, -2016, -1952, -1632, -1568, -1760, -1696,
            -688, -656, -752, -720, -560, -528, -624, -592,
            -944, -912, -1008, -976, -816, -784, -880, -848,
            5504, 5248, 6016, 5760, 4480, 4224, 4992, 4736,
            7552, 7296, 8064, 7808, 6528, 6272, 7040, 6784,
            2752, 2624, 3008, 2880, 2240, 2112, 2496, 2368,
            3776, 3648, 4032, 3904, 3264, 3136, 3520, 3392,
            22016, 20992, 24064, 23040, 17920, 16896, 19968, 18944,
            30208, 29184, 32256, 31232, 26112, 25088, 28160, 27136,
            11008, 10496, 12032, 11520, 8960, 8448, 9984, 9472,
            15104, 14592, 16128, 15616, 13056, 12544, 14080, 13568,
            344, 328, 376, 360, 280, 264, 312, 296,
            472, 456, 504, 488, 408, 392, 440, 424,
            88, 72, 120, 104, 24, 8, 56, 40,
            216, 200, 248, 232, 152, 136, 184, 168,
            1376, 1312, 1504, 1440, 1120, 1056, 1248, 1184,
            1888, 1824, 2016, 1952, 1632, 1568, 1760, 1696,
            688, 656, 752, 720, 560, 528, 624, 592,
            944, 912, 1008, 976, 816, 784, 880, 848
        };
        static readonly short[] _µLawDecodeValues = {
            -32124, -31100, -30076, -29052, -28028, -27004, -25980, -24956,
            -23932, -22908, -21884, -20860, -19836, -18812, -17788, -16764,
            -15996, -15484, -14972, -14460, -13948, -13436, -12924, -12412,
            -11900, -11388, -10876, -10364, -9852, -9340, -8828, -8316,
            -7932, -7676, -7420, -7164, -6908, -6652, -6396, -6140,
            -5884, -5628, -5372, -5116, -4860, -4604, -4348, -4092,
            -3900, -3772, -3644, -3516, -3388, -3260, -3132, -3004,
            -2876, -2748, -2620, -2492, -2364, -2236, -2108, -1980,
            -1884, -1820, -1756, -1692, -1628, -1564, -1500, -1436,
            -1372, -1308, -1244, -1180, -1116, -1052, -988, -924,
            -876, -844, -812, -780, -748, -716, -684, -652,
            -620, -588, -556, -524, -492, -460, -428, -396,
            -372, -356, -340, -324, -308, -292, -276, -260,
            -244, -228, -212, -196, -180, -164, -148, -132,
            -120, -112, -104, -96, -88, -80, -72, -64,
            -56, -48, -40, -32, -24, -16, -8, 0,
            32124, 31100, 30076, 29052, 28028, 27004, 25980, 24956,
            23932, 22908, 21884, 20860, 19836, 18812, 17788, 16764,
            15996, 15484, 14972, 14460, 13948, 13436, 12924, 12412,
            11900, 11388, 10876, 10364, 9852, 9340, 8828, 8316,
            7932, 7676, 7420, 7164, 6908, 6652, 6396, 6140,
            5884, 5628, 5372, 5116, 4860, 4604, 4348, 4092,
            3900, 3772, 3644, 3516, 3388, 3260, 3132, 3004,
            2876, 2748, 2620, 2492, 2364, 2236, 2108, 1980,
            1884, 1820, 1756, 1692, 1628, 1564, 1500, 1436,
            1372, 1308, 1244, 1180, 1116, 1052, 988, 924,
            876, 844, 812, 780, 748, 716, 684, 652,
            620, 588, 556, 524, 492, 460, 428, 396,
            372, 356, 340, 324, 308, 292, 276, 260,
            244, 228, 212, 196, 180, 164, 148, 132,
            120, 112, 104, 96, 88, 80, 72, 64,
            56, 48, 40, 32, 24, 16, 8, 0
        };

        AudioInfo? _audioInfo;
        RiffReader? _reader;
        int _bitsPerSample;
        int _bytesPerSample;
        long _framesRemaining;
        DataFormat _format;

        public bool Finished => _framesRemaining == 0;

        public void Initialize(Stream stream)
        {
            _audioInfo = new WaveAudioInfoDecoder().ReadAudioInfo(stream);
            _reader = new(stream);

            _bitsPerSample = _audioInfo.BitsPerSample > 0 ? _audioInfo.BitsPerSample : 8;
            _bytesPerSample = (int) Math.Ceiling(_bitsPerSample / 8.0);
            _framesRemaining = _audioInfo.FrameCount;
            _format = _audioInfo.Format switch
            {
                "LPCM" => DataFormat.Lpcm,
                "A-law" => DataFormat.ALaw,
                "µ-law" => DataFormat.ΜLaw,
                _ => throw new AudioUnsupportedException($"'{_audioInfo.Format}' is not supported in a Wave container.")
            };

            _reader.Initialize();
            _reader.SeekToChunk("data");
        }

        public SampleBuffer DecodeSamples()
        {
            SampleBuffer result;
#if NETSTANDARD2_0
            var length = _audioInfo!.Channels * (int) Math.Min(_framesRemaining, _defaultFrameCount) * _bytesPerSample;

            var buffer = ArrayPool<byte>.Shared.Rent(length);
            try
            {
                if (_reader!.Read(buffer, 0, length) < length)
                    throw new AudioInvalidException("Stream is unexpectedly truncated.");

                if (_format == DataFormat.Lpcm)
                    result = new(buffer.AsSpan()[..length], _audioInfo.Channels, _bitsPerSample);
                else
                {
                    var lpcmBuffer = ArrayPool<short>.Shared.Rent(length);
                    try
                    {
                        if (_format == DataFormat.ALaw)
                            for (var sampleIndex = 0; sampleIndex < buffer.Length; sampleIndex++)
                                lpcmBuffer[sampleIndex] = _aLawDecodeValues[buffer[sampleIndex]];
                        else
                            for (var sampleIndex = 0; sampleIndex < buffer.Length; sampleIndex++)
                                lpcmBuffer[sampleIndex] = _µLawDecodeValues[buffer[sampleIndex]];

                        // TODO SampleBuffer should probably accept 16-bit buffers directly
                        result = new(MemoryMarshal.Cast<short, byte>(lpcmBuffer.AsSpan()[..length]),
                            _audioInfo.Channels, 16);
                    }
                    finally
                    {
                        ArrayPool<short>.Shared.Return(lpcmBuffer);
                    }
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
#else
            Span<byte> buffer = stackalloc byte[_audioInfo!.Channels *
                                                (int) Math.Min(_framesRemaining, _defaultFrameCount)
                                                * _bytesPerSample];

            if (_reader!.Read(buffer) < buffer.Length)
                throw new AudioInvalidException("Stream is unexpectedly truncated.");

            if (_format == DataFormat.Lpcm)
                result = new(buffer, _audioInfo.Channels, _bitsPerSample);
            else
            {
                Span<short> lpcmBuffer =
                    stackalloc short[buffer.Length];

                if (_format == DataFormat.ALaw)
                    for (var sampleIndex = 0; sampleIndex < buffer.Length; sampleIndex++)
                        lpcmBuffer[sampleIndex] = _aLawDecodeValues[buffer[sampleIndex]];
                else
                    for (var sampleIndex = 0; sampleIndex < buffer.Length; sampleIndex++)
                        lpcmBuffer[sampleIndex] = _µLawDecodeValues[buffer[sampleIndex]];

                // TODO SampleBuffer should probably accept 16-bit buffers directly
                result = new(MemoryMarshal.Cast<short, byte>(lpcmBuffer), _audioInfo.Channels, 16);
            }
#endif

            _framesRemaining -= result.Frames;
            return result;
        }

        public void Dispose() => _reader?.Dispose();
    }
}
