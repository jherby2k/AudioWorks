using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct VorbisComment
    {
        internal readonly IntPtr UserComments;

        internal readonly IntPtr CommentLengths;

        internal readonly int Comments;

        readonly IntPtr Vendor;
    }
}