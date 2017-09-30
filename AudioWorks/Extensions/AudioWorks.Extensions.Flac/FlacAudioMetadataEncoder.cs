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
            return new SettingInfoDictionary
            {
                ["Padding"] = new IntSettingInfo(0, 16_777_216)
            };
        }

        public void WriteMetadata(FileStream stream, AudioMetadata metadata, SettingDictionary settings)
        {
            var padding = GetPadding(settings);
            NativeVorbisCommentBlock vorbisCommentBlock = null;

            try
            {
                using (var chain = new NativeMetadataChain(stream))
                {
                    chain.Read();

                    vorbisCommentBlock = new MetadataToVorbisCommentAdapter(metadata);

                    // Iterate over the existing blocks, replacing and deleting as needed:
                    using (var iterator = chain.GetIterator())
                        UpdateChain(iterator, vorbisCommentBlock, padding);

                    // If FLAC requests a temporary file, use a MemoryStream instead. Then overwrite the original:
                    if (chain.CheckIfTempFileNeeded(!padding.HasValue))
                        using (var tempStream = new MemoryStream())
                        {
                            chain.WriteWithTempFile(!padding.HasValue, tempStream);

                            // Clear the original stream, and copy the temporary one over it:
                            stream.SetLength(tempStream.Length);
                            stream.Position = 0;
                            tempStream.WriteTo(stream);
                        }
                    else
                        chain.Write(!padding.HasValue);
                }
            }
            finally
            {
                vorbisCommentBlock?.Dispose();
            }

        }

        [Pure]
        int? GetPadding([NotNull] SettingDictionary settings)
        {
            if (!settings.TryGetValue("Padding", out var stringValue))
                return null;
            return (int) stringValue;
        }

        static void UpdateChain(
            [NotNull] NativeMetadataIterator iterator,
            [NotNull] NativeVorbisCommentBlock newComments,
            int? padding)
        {
            var metadataInserted = false;

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

                    // Delete any padding
                    case MetadataType.Padding:
                        iterator.DeleteBlock(false);
                        break;
                }
            } while (iterator.Next());

            // If there was no existing metadata block to replace, insert it now
            if (!metadataInserted)
                iterator.InsertBlockAfter(newComments);

            // If padding was explicitly requested, add it
            if (padding.HasValue)
                iterator.InsertBlockAfter(new NativePaddingBlock(padding.Value));
        }
    }
}
