using AudioWorks.Common;
using System;
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
                    reader.BaseStream.Seek(frameHeader.SideInfoLength, SeekOrigin.Current);
                    var frameCount = ReadFrameCount(reader);

                    return new AudioInfo("MP3", frameHeader.Channels, 0, frameHeader.SampleRate,
                        frameCount * frameHeader.SamplesPerFrame);
                }
                catch (EndOfStreamException e)
                {
                    // If a frame sync couldn't be located, this isn't an MP3
                    throw new InvalidFileException(e.Message, stream.Name);
                }
                catch (ArgumentException e)
                {
                    // If the frame header isn't valid for an MP3, this isn't an MP3
                    throw new InvalidFileException(e.Message, stream.Name);
                }
            }
        }

        static FrameHeader ReadFrameHeader(FrameReader reader)
        {
            // Seek to the first valid frame header:
            FrameHeader result;
            do
            {
                reader.SeekToNextFrame();
                result = new FrameHeader(reader.ReadBytes(4));
            } while (!reader.VerifyFrameSync(result));
            return result;
        }

        static uint ReadFrameCount(FrameReader reader)
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
    }
}
