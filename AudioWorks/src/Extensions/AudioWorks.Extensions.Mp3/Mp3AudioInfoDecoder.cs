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
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Mp3
{
    [AudioInfoDecoderExport(".mp3", "MPEG Audio Layer 3")]
    public sealed class Mp3AudioInfoDecoder : IAudioInfoDecoder
    {
        const string _format = "MPEG Audio Layer 3 (MP3)";

        public string Format => _format;

        public AudioInfo ReadAudioInfo(Stream stream)
        {
            using (var reader = new FrameReader(stream))
                try
                {
                    var frameHeader = ReadFrameHeader(reader);

                    // Frame count is found in the optional Xing (most common) or VBRI header
                    var optionalHeader = ReadXingHeader(reader, frameHeader);
                    if (optionalHeader.Incomplete)
                        optionalHeader = ReadVbriHeader(reader);

                    return AudioInfo.CreateForLossy(
                        "MP3",
                        frameHeader.Channels,
                        frameHeader.SampleRate,
                        optionalHeader.FrameCount * frameHeader.SamplesPerFrame,
                        DetermineBitRate(frameHeader, optionalHeader));
                }
                catch (EndOfStreamException e)
                {
                    // If a frame sync couldn't be located, this isn't an MP3
                    throw new AudioInvalidException(e.Message);
                }
        }

        static FrameHeader ReadFrameHeader(FrameReader reader)
        {
#if NETSTANDARD2_0
            var buffer = new byte[4];
#else
            Span<byte> buffer = stackalloc byte[4];
#endif

            // Seek to the first valid frame header
            FrameHeader? result = null;
            do
            {
                reader.SeekToNextFrame();
#if NETSTANDARD2_0
                if (reader.Read(buffer, 0, 4) < 4)
#else
                if (reader.Read(buffer) < 4)
#endif
                    throw new AudioInvalidException("Stream is unexpectedly truncated.");

                try
                {
                    result = new FrameHeader(buffer);
                }
                catch (AudioException)
                {
                    // If the frame header appears wrong, it is probably just a bad sync
                }
            } while (result == null || !reader.VerifyFrameSync(result));
            return result;
        }

        static OptionalHeader ReadXingHeader(FrameReader reader, FrameHeader header)
        {
            // Xing header (if present) is located after the side info
            reader.BaseStream.Position = reader.FrameStart + 4 + header.SideInfoLength;

            var result = new OptionalHeader();

            var headerId = reader.ReadHeaderId();
            if (!headerId.Equals("Xing", StringComparison.Ordinal) &&
                !headerId.Equals("Info", StringComparison.Ordinal))
                return result;

            // Both fields are optional, even if the header is present
            var flags = reader.ReadUInt32BigEndian();
            if ((flags & 1) == 1)
                result.FrameCount = reader.ReadUInt32BigEndian();
            if ((flags >> 1 & 1) == 1)
                result.ByteCount = reader.ReadUInt32BigEndian();

            return result;
        }

        static OptionalHeader ReadVbriHeader(FrameReader reader)
        {
            // VBRI header (if present) is located 32 bytes past the frame header
            reader.BaseStream.Position = reader.FrameStart + 36;

            var result = new OptionalHeader();

            if (!reader.ReadHeaderId().Equals("VBRI", StringComparison.Ordinal)) return result;

            reader.BaseStream.Seek(6, SeekOrigin.Current);
            result.ByteCount = reader.ReadUInt32BigEndian();
            result.FrameCount = reader.ReadUInt32BigEndian();

            return result;
        }

        static int DetermineBitRate(FrameHeader frameHeader, OptionalHeader optionalHeader)
        {
            // If the BitRate can't be calculated because of an incomplete or missing VBR header, assume CBR
            if (optionalHeader.Incomplete)
                return frameHeader.BitRate * 1000;

            return (int) Math.Round(
                optionalHeader.ByteCount * 8 /
                (optionalHeader.FrameCount * frameHeader.SamplesPerFrame / (double) frameHeader.SampleRate));
        }
    }
}
