using System.IO;
using AudioWorks.Common;
using Id3Lib;
using Id3Lib.Exceptions;

namespace AudioWorks.Extensions.Id3
{
    [AudioMetadataDecoderExport(".mp3")]
    public sealed class Id3AudioMetadataDecoder : IAudioMetadataDecoder
    {
        public AudioMetadata ReadMetadata(FileStream stream)
        {
            TagModel tagModel;
            try
            {
                tagModel = TagManager.Deserialize(stream);
            }
            catch (TagNotFoundException)
            {
                try
                {
                    // If no ID3v2 tag was found, check for ID3v1
                    var v1Tag = new ID3v1();
                    v1Tag.Deserialize(stream);
                    tagModel = v1Tag.FrameModel;
                }
                catch (TagNotFoundException e)
                {
                    throw new AudioUnsupportedException(e.Message, stream.Name);
                }
            }

            return new TagModelToMetadataAdapter(tagModel);
        }
    }
}
