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
                    var frameHeader = ReadFrameHeader(reader, out var framePosition);
                    reader.BaseStream.Seek(frameHeader.SideInfoLength, SeekOrigin.Current);
                    var frameCount = ReadFrameCountFromXing(reader);
                    if (frameCount == 0)
                    {
                        reader.BaseStream.Seek(framePosition + 36, SeekOrigin.Begin);
                        frameCount = ReadFrameCountFromVbri(reader);
                    }

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
        static FrameHeader ReadFrameHeader([NotNull] FrameReader reader, out long framePosition)
        {
            framePosition = 0;

            // Seek to the first valid frame header:
            FrameHeader result = null;
            do
            {
                try
                {
                    framePosition = reader.SeekToNextFrame();
                    result = new FrameHeader(reader.ReadBytes(4));
                }
                catch (AudioException)
                {
                    // If the frame header appears wrong, its probably a bad sync
                }
            } while (result == null || !reader.VerifyFrameSync(result));
            return result;
        }

        static uint ReadFrameCountFromXing([NotNull] FrameReader reader)
        {
            // Check for the optional Xing header
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
            var headerId = new string(reader.ReadChars(4));
            if (headerId != "VBRI") return 0u;
            reader.BaseStream.Seek(10, SeekOrigin.Current);
            return reader.ReadUInt32BigEndian();
        }
    }
}
