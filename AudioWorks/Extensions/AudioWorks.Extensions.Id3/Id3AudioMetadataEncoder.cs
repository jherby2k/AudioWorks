using AudioWorks.Common;
using Id3Lib;
using System.IO;

namespace AudioWorks.Extensions.Id3
{
    [AudioMetadataEncoderExport(".mp3")]
    sealed class Id3AudioMetadataEncoder : IAudioMetadataEncoder
    {
        public void WriteMetadata(FileStream stream, AudioMetadata metadata)
        {
            var tagModel = new MetadataToTagModelAdapter(metadata);
            tagModel.Header.Version = 3;
            tagModel.UpdateSize();

            // Copy the audio to memory, then rewrite the whole stream:
            using (var tempStream = new MemoryStream())
            {
                // Copy the MP3 to memory
                stream.CopyTo(tempStream);

                // Rewrite the stream with tag first
                stream.Position = 0;
                stream.SetLength(tagModel.Header.TagSizeWithHeaderFooter + tagModel.Header.PaddingSize + tempStream.Length);
                TagManager.Serialize(tagModel, stream);
                tempStream.Position = 0;
                tempStream.CopyTo(stream);
            }
        }
    }
}
