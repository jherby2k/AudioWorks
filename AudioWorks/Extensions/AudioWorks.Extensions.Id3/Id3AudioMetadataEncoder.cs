using AudioWorks.Common;
using Id3Lib;
using Id3Lib.Exceptions;
using System.IO;

namespace AudioWorks.Extensions.Id3
{
    [AudioMetadataEncoderExport(".mp3")]
    sealed class Id3AudioMetadataEncoder : IAudioMetadataEncoder
    {
        public void WriteMetadata(FileStream stream, AudioMetadata metadata)
        {
            // Determine how long the existing tag is, if present
            var existingTagLength = 0u;
            try
            {
                var existingTag = TagManager.Deserialize(stream);
                existingTagLength = existingTag.Header.TagSizeWithHeaderFooter + existingTag.Header.PaddingSize;
            }
            catch (TagNotFoundException)
            {
            }

            var tagModel = new MetadataToTagModelAdapter(metadata);
            tagModel.Header.Version = 3;
            tagModel.UpdateSize();

            // Copy the audio to memory, then rewrite the whole stream
            using (var tempStream = new MemoryStream())
            {
                // Seek just past the existing tag
                stream.Seek(existingTagLength, SeekOrigin.Begin);

                // Copy the MP3 portion to memory
                stream.CopyTo(tempStream);

                // Rewrite the stream with the new tag in front
                stream.Position = 0;
                stream.SetLength(tagModel.Header.TagSizeWithHeaderFooter + tagModel.Header.PaddingSize + tempStream.Length);
                TagManager.Serialize(tagModel, stream);
                tempStream.Position = 0;
                tempStream.CopyTo(stream);
            }
        }
    }
}
