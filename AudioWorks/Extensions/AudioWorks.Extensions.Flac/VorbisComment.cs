using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    struct VorbisComment
    {
        readonly VorbisCommentEntry Vendor;

        internal readonly uint Count;

        internal IntPtr Comments;
    }
}