using AudioWorks.Common;
using System.IO;

namespace AudioWorks.Extensions.Flac
{
    [AudioMetadataDecoderExport(".flac")]
    public sealed class FlacAudioMetadataDecoder : IAudioMetadataDecoder
    {
        public AudioMetadata ReadMetadata(FileStream stream)
        {
            using (var decoder = new MetadataDecoder(stream))
            {
                decoder.SetMetadataRespond(MetadataType.VorbisComment);

                decoder.Initialize();
                if (!decoder.ProcessMetadata())
                    throw new AudioInvalidException(
                        $"libFLAC was unable to read the audio metadata: {decoder.GetState()}.", stream.Name);
                decoder.Finish();

                return decoder.AudioMetadata;
            }
        }
    }
}
