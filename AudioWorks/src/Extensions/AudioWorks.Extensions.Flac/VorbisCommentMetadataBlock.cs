using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Explicit)]
    struct VorbisCommentMetadataBlock
    {
        [FieldOffset(0)]
        readonly MetadataType Type;

        [FieldOffset(4)]
        readonly bool IsLast;

        [FieldOffset(8)]
        readonly uint Length;

        [FieldOffset(16)]
        internal VorbisComment VorbisComment;
    }
}