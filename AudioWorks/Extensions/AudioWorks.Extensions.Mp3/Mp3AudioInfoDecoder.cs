using AudioWorks.Common;
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
                // Seek to the first valid frame header:
                FrameHeader frameHeader;
                do
                {
                    reader.SeekToNextFrame();
                    frameHeader = new FrameHeader(reader.ReadBytes(4));
                } while (!reader.VerifyFrameSync(frameHeader));

                return new AudioInfo("MP3", frameHeader.Channels, 0, frameHeader.SampleRate, 0);
            }
        }
    }
}
