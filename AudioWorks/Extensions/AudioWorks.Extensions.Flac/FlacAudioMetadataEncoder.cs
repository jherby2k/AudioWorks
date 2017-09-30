using AudioWorks.Common;
using JetBrains.Annotations;
using System.IO;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [AudioMetadataEncoderExport(".flac")]
    sealed class FlacAudioMetadataEncoder : IAudioMetadataEncoder
    {
        public SettingInfoDictionary GetSettingInfo()
        {
            return new SettingInfoDictionary();
        }

        public void WriteMetadata(FileStream stream, AudioMetadata metadata, SettingDictionary settings)
        {
            NativeVorbisCommentBlock vorbisCommentBlock = null;

            try
            {
                using (var chain = new NativeMetadataChain(stream))
                {
                    chain.Read();

                    vorbisCommentBlock = new MetadataToVorbisCommentAdapter(metadata);

                    // Iterate over the existing blocks, replacing and deleting as needed:
                    using (var iterator = chain.GetIterator())
                        UpdateChain(iterator, vorbisCommentBlock);

                    // If FLAC requests a temporary file, use a MemoryStream instead. Then overwrite the original:
                    if (chain.CheckIfTempFileNeeded(false))
                        using (var tempStream = new MemoryStream())
                        {
                            chain.WriteWithTempFile(false, tempStream);

                            // Clear the original stream, and copy the temporary one over it:
                            stream.SetLength(tempStream.Length);
                            stream.Position = 0;
                            tempStream.WriteTo(stream);
                        }
                    else
                        chain.Write(false);
                }
            }
            finally
            {
                vorbisCommentBlock?.Dispose();
            }

        }

        static void UpdateChain(
            [NotNull] NativeMetadataIterator iterator,
            [NotNull] NativeVorbisCommentBlock newComments)
        {
            var metadataInserted = false;

            do
            {
                switch ((MetadataType) Marshal.ReadInt32(iterator.GetBlock()))
                {
                    // Replace the existing Vorbis comment:
                    case MetadataType.VorbisComment:
                        iterator.DeleteBlock(false);
                        iterator.InsertBlockAfter(newComments);
                        metadataInserted = true;
                        break;

                    // Delete any padding:
                    case MetadataType.Padding:
                        iterator.DeleteBlock(false);
                        break;
                }
            } while (iterator.Next());

            // If there was no existing metadata block to replace, insert it now:
            if (!metadataInserted)
                iterator.InsertBlockAfter(newComments);
        }
    }
}
