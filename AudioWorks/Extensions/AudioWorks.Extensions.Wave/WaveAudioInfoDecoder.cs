using AudioWorks.Common;
using System.IO;

namespace AudioWorks.Extensions.Wave
{
    [AudioInfoDecoderExport(".wav")]
    class WaveAudioInfoDecoder : IAudioInfoDecoder
    {
        public AudioInfo ReadAudioInfo(FileStream stream)
        {
            using (var reader = new RiffReader(stream))
            {
                try
                {
                    reader.Initialize();
                    return new AudioInfo();
                }
                catch (IOException e)
                {
                    throw new InvalidFileException(e.Message, stream.Name);
                }
            }
        }
    }
}
