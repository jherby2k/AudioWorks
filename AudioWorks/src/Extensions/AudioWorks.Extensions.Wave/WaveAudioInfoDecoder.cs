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
using System.Buffers.Binary;
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Wave
{
    [AudioInfoDecoderExport(".wav", "Waveform Audio")]
    sealed class WaveAudioInfoDecoder : IAudioInfoDecoder
    {
        static readonly Guid _lpcmGuid = new("00000001-0000-0010-8000-00aa00389b71");
        static readonly Guid _aLawGuid = new("00000006-0000-0010-8000-00aa00389b71");
        static readonly Guid _µLawGuid = new("00000007-0000-0010-8000-00aa00389b71");

        const string _format = "Waveform Audio File Format (WAVE)";

        public string Format => _format;

        public AudioInfo ReadAudioInfo(Stream stream)
        {
            using (var reader = new RiffReader(stream))
                try
                {
                    reader.Initialize();

                    if (stream.Length != reader.RiffChunkSize + 8)
                        throw new AudioInvalidException("Stream is unexpectedly truncated.");

                    reader.BaseStream.Position = 8;
                    if (!reader.ReadFourCc().Equals("WAVE", StringComparison.Ordinal))
                        throw new AudioInvalidException("Not a Wave stream.");

                    var fmtChunkSize = reader.SeekToChunk("fmt ");
                    if (fmtChunkSize == 0)
                        throw new AudioInvalidException("Missing 'fmt' chunk.");

                    Span<byte> fmtData = stackalloc byte[(int) fmtChunkSize];
                    stream.ReadExactly(fmtData);

                    var dataSize = reader.SeekToChunk("data");

                    switch (BinaryPrimitives.ReadUInt16LittleEndian(fmtData))
                    {
                        case 1:
                            return ParseLpcm(fmtData, dataSize);

                        case 6:
                            return ParseG711("A-law", fmtData, dataSize);

                        case 7:
                            return ParseG711("µ-law", fmtData, dataSize);

                        // WAVE_FORMAT_EXTENSIBLE
                        case 0xFFFE:
                            var guid = new Guid(fmtData.Slice(24, 16));
                            if (guid == _lpcmGuid) return ParseLpcm(fmtData, dataSize);
                            if (guid == _aLawGuid) return ParseG711("A-law", fmtData, dataSize);
                            if (guid == _µLawGuid) return ParseG711("µ-law", fmtData, dataSize);
                            throw new AudioUnsupportedException("Only PCM, A-law and µ-law streams are currently supported.");

                        default:
                            throw new AudioUnsupportedException("Only PCM, A-law and µ-law streams are currently supported.");
                    }
                }
                catch (EndOfStreamException e)
                {
                    // The end of the stream was unexpectedly reached
                    throw new AudioInvalidException(e.Message);
                }
        }

        static AudioInfo ParseLpcm(ReadOnlySpan<byte> fmtData, uint dataSize) =>
            AudioInfo.CreateForLossless(
                "LPCM",
                BinaryPrimitives.ReadUInt16LittleEndian(fmtData[2..]),
                BinaryPrimitives.ReadUInt16LittleEndian(fmtData[14..]),
                (int) BinaryPrimitives.ReadUInt32LittleEndian(fmtData[4..]),
                dataSize / BinaryPrimitives.ReadUInt16LittleEndian(fmtData[12..]));

        static AudioInfo ParseG711(string format, ReadOnlySpan<byte> fmtData, uint dataSize)
        {
            var sampleRate = (int) BinaryPrimitives.ReadUInt32LittleEndian(fmtData[4..]);
            var blockSize = BinaryPrimitives.ReadUInt16LittleEndian(fmtData[12..]);
            return AudioInfo.CreateForLossy(format,
                BinaryPrimitives.ReadUInt16LittleEndian(fmtData[2..]),
                sampleRate,
                dataSize / blockSize,
                blockSize * 8 * sampleRate);
        }
    }
}
