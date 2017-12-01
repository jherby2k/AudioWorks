using System.Text;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    abstract class VorbisCommentBlock : MetadataBlock
    {
        internal VorbisCommentBlock()
            : base(MetadataType.VorbisComment)
        {
        }

        internal void Append([NotNull] string key, [NotNull] string value)
        {
            SafeNativeMethods.MetadataObjectVorbisCommentEntryFromNameValuePair(
                out var comment,
                Encoding.ASCII.GetBytes(key),
                Encoding.UTF8.GetBytes(value));

            // The comment takes ownership of the new entry if 'copy' is false
            SafeNativeMethods.MetadataObjectVorbisCommentAppendComment(Handle, comment, false);
        }
    }
}