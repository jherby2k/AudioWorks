using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    [AudioMetadataEncoderExport(".flac")]
    public sealed class FlacAudioMetadataEncoder : IAudioMetadataEncoder
    {
        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary
        {
            ["Padding"] = new IntSettingInfo(0, 16_777_216)
        };

        public void WriteMetadata(FileStream stream, AudioMetadata metadata, SettingDictionary settings)
        {
            var padding = GetPadding(settings);

            using (var chain = new MetadataChain(stream))
            using (var comments = new MetadataToVorbisCommentAdapter(metadata))
            {
                chain.Read();

                PictureBlock pictureBlock = null;
                try
                {
                    if (metadata.CoverArt != null)
                        pictureBlock = new CoverArtToPictureBlockAdapter(metadata.CoverArt);

                    // Iterate over the existing blocks, replacing and deleting as needed:
                    using (var iterator = chain.GetIterator())
                        UpdateChain(iterator, comments, pictureBlock, padding);

                    // If FLAC requests a temporary file, use a MemoryStream instead. Then overwrite the original:
                    if (chain.CheckIfTempFileNeeded(!padding.HasValue))
                        using (var tempStream = new MemoryStream())
                        {
                            chain.WriteWithTempFile(!padding.HasValue, tempStream);

                            // Clear the original stream, and copy the temporary one over it:
                            stream.Position = 0;
                            stream.SetLength(tempStream.Length);
                            tempStream.WriteTo(stream);
                        }
                    else
                        chain.Write(!padding.HasValue);
                }
                finally
                {
                    pictureBlock?.Dispose();
                }
            }
        }

        [Pure]
        static int? GetPadding([NotNull] SettingDictionary settings)
        {
            if (settings.TryGetValue<int>("Padding", out var result))
                return result;
            return null;
        }

        static void UpdateChain(
            [NotNull] MetadataIterator iterator,
            [NotNull] VorbisCommentBlock newComments,
            [CanBeNull] PictureBlock pictureBlock,
            int? padding)
        {
            var metadataInserted = false;
            var pictureInserted = false;

            do
            {
                switch ((MetadataType) Marshal.ReadInt32(iterator.GetBlock()))
                {
                    // Replace the existing Vorbis comment
                    case MetadataType.VorbisComment:
                        iterator.DeleteBlock(false);
                        iterator.InsertBlockAfter(newComments);
                        metadataInserted = true;
                        break;

                    // Replace the existing Picture block:
                    case MetadataType.Picture:
                        iterator.DeleteBlock(false);
                        if (pictureBlock != null)
                            iterator.InsertBlockAfter(pictureBlock);
                        pictureInserted = true;
                        break;

                    // Delete any padding
                    case MetadataType.Padding:
                        iterator.DeleteBlock(false);
                        break;
                }
            } while (iterator.Next());

            // If there was no existing metadata block to replace, insert it now
            if (!metadataInserted)
                iterator.InsertBlockAfter(newComments);

            // If there was no existing picture block to replace, and a new one is available, insert it now
            if (pictureBlock != null && !pictureInserted)
                iterator.InsertBlockAfter(pictureBlock);

            // If padding was explicitly requested, add it
            if (padding.HasValue)
                iterator.InsertBlockAfter(new PaddingBlock(padding.Value));
        }
    }
}
