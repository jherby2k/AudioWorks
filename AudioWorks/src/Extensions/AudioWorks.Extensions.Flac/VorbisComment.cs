using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct VorbisComment
    {
        readonly VorbisCommentEntry Vendor;

        internal readonly uint Count;

        internal readonly IntPtr Comments;
    }
}