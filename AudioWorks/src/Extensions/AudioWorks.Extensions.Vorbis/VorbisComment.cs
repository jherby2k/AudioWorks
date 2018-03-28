using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct VorbisComment
    {
        internal IntPtr UserComments;

        internal IntPtr CommentLengths;

        internal readonly int Comments;

        readonly IntPtr Vendor;
    }
}