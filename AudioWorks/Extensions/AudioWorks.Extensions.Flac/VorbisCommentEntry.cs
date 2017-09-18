using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    struct VorbisCommentEntry
    {
        internal readonly uint Length;

        internal readonly IntPtr Entry;
    }
}
