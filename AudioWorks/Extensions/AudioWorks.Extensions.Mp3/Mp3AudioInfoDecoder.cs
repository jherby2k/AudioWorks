using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Composition;
using System.IO;

namespace AudioWorks.Extensions.Mp3
{
    [Shared]
    [AudioInfoDecoderExport(".mp3")]
    public sealed class Mp3AudioInfoDecoder : IAudioInfoDecoder
    {
        public AudioInfo ReadAudioInfo(FileStream stream)
        {
            using (var reader = new FrameReader(stream))
            {
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
                    throw new AudioInvalidException(e.Message, stream.Name);
                }
            }
        }

        [NotNull]
        static FrameHeader ReadFrameHeader([NotNull] FrameReader reader)
        {
            // Seek to the first valid frame header:
            FrameHeader result = null;
            do
            {
                try
                {
                    reader.SeekToNextFrame();
                    result = new FrameHeader(reader.ReadBytes(4));
                }
                catch (AudioException)
                {
                    // If the frame header appears wrong, its probably a bad sync
                }
            } while (result == null || !reader.VerifyFrameSync(result));
            return result;
        }

        static OptionalHeaderInfo ReadXingHeader([NotNull] FrameReader reader, [NotNull] FrameHeader header)
        {
            // Xing header (if present) is located after the side info
            reader.BaseStream.Seek(reader.FrameStart + 4 + header.SideInfoLength, SeekOrigin.Begin);

            var result = new OptionalHeaderInfo();

            var headerId = new string(reader.ReadChars(4));
            if (string.CompareOrdinal("Xing", headerId) != 0 && string.CompareOrdinal("Info", headerId) != 0) return result;

            // Both fields are optional, even if the header is present
            var flags = reader.ReadUInt32BigEndian();
            if ((flags & 0b00000001) == 0b00000001)
                result.FrameCount = reader.ReadUInt32BigEndian();
            if ((flags >> 1 & 0b00000001) == 0b00000001)
                result.ByteCount = reader.ReadUInt32BigEndian();

            return result;
        }

        static OptionalHeaderInfo ReadVbriHeader([NotNull] FrameReader reader)
        {
            // VBRI header (if present) is located 32 bytes past the frame header
            reader.BaseStream.Seek(reader.FrameStart + 36, SeekOrigin.Begin);

            var result = new OptionalHeaderInfo();

            var headerId = new string(reader.ReadChars(4));
            if (string.CompareOrdinal("VBRI", headerId) != 0) return result;

            reader.BaseStream.Seek(6, SeekOrigin.Current);
            result.ByteCount = reader.ReadUInt32BigEndian();
            result.FrameCount = reader.ReadUInt32BigEndian();

            return result;
        }

        static int DetermineBitRate([NotNull] FrameHeader frameHeader, OptionalHeaderInfo optionalHeader)
        {
            // If the BitRate can't be calculated because of an incomplete or missing VBR header, assume CBR
            if (optionalHeader.Incomplete)
                return frameHeader.BitRate * 1000;

            return (int) Math.Round(
                optionalHeader.ByteCount * 8 /
                (optionalHeader.FrameCount * frameHeader.SamplesPerFrame /
                 (double) frameHeader.SampleRate));
        }
    }
}
