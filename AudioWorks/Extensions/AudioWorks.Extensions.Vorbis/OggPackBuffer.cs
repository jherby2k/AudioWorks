using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct OggPackBuffer
    {
        readonly int EndByte;

        readonly int EndBit;

        readonly IntPtr Buffer;

        readonly IntPtr Ptr;

        readonly int Storage;
    }
}