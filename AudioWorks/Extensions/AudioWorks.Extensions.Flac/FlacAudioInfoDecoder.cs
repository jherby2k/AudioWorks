using AudioWorks.Common;
using System.IO;

namespace AudioWorks.Extensions.Flac
{
    [AudioInfoDecoderExport(".flac")]
    sealed class FlacAudioInfoDecoder : IAudioInfoDecoder
    {
        public AudioInfo ReadAudioInfo(FileStream stream)
        {
            using (var decoder = new NativeAudioInfoDecoder(stream))
            {
                decoder.Initialize();
                decoder.ProcessMetadata();
                decoder.Finish();

                return decoder.AudioInfo ?? throw new AudioInvalidException("Audio info could not be read.", stream.Name);
            }
        }
    }
}
