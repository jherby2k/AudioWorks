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
                    // Seek to the first valid frame header:
                    FrameHeader frameHeader;
                    do
                    {
                        reader.SeekToNextFrame();
                        frameHeader = new FrameHeader(reader.ReadBytes(4));
                    } while (!reader.VerifyFrameSync(frameHeader));

                    return new AudioInfo("MP3", frameHeader.Channels, 0, frameHeader.SampleRate, 0);
                }
                catch (ArgumentException e)
                {
                    throw new InvalidFileException(e.Message, stream.Name);
                }
            }
        }
    }
}
