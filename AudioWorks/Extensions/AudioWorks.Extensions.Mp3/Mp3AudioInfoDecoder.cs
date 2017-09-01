using AudioWorks.Common;
using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Extensions.Mp3
{
    [AudioInfoDecoderExport(".mp3")]
    class Mp3AudioInfoDecoder : IAudioInfoDecoder
    {
        public AudioInfo ReadAudioInfo(FileStream stream)
        {
            using (var reader = new FrameReader(stream))
            {
                try
                {
                    var frameHeader = ReadFrameHeader(reader);

                    // Frame count is found in the optional Xing or VBRI header
                    var frameCount = ReadFrameCountFromXing(reader, frameHeader);
                    if (frameCount == 0)
                        frameCount = ReadFrameCountFromVbri(reader);

                    return new AudioInfo("MP3", frameHeader.Channels, 0, frameHeader.SampleRate,
                        frameCount * frameHeader.SamplesPerFrame);
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

        static uint ReadFrameCountFromXing([NotNull] FrameReader reader, [NotNull] FrameHeader header)
        {
            // Xing header (if present) is located after the side info
            reader.BaseStream.Seek(reader.FrameStart + 4 + header.SideInfoLength, SeekOrigin.Begin);

            var headerId = new string(reader.ReadChars(4));
            if (headerId == "Xing" || headerId == "Info")
            {
                // The flags DWORD indicates whether the frame count is included in the header
                var flags = reader.ReadUInt32BigEndian();
                if ((flags & 0b00000001) == 0b00000001)
                    return reader.ReadUInt32BigEndian();
            }

            return 0u;
        }

        static uint ReadFrameCountFromVbri([NotNull] FrameReader reader)
        {
            // VBRI header (if present) is located 32 bytes past the frame header
            reader.BaseStream.Seek(reader.FrameStart + 36, SeekOrigin.Begin);

            var headerId = new string(reader.ReadChars(4));
            if (headerId != "VBRI") return 0u;
            reader.BaseStream.Seek(10, SeekOrigin.Current);
            return reader.ReadUInt32BigEndian();
        }
    }
}
