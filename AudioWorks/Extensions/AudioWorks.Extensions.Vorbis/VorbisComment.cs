using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct VorbisComment
    {
        readonly IntPtr UserComments;

        readonly IntPtr CommentLengths;

        readonly int Comments;

        readonly IntPtr Vendor;
    }
}