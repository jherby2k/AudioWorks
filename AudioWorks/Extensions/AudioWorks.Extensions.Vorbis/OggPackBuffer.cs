using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct OggPackBuffer
    {
        internal int EndByte;

        internal int EndBit;

        internal IntPtr Buffer;

        internal IntPtr Ptr;

        internal int Storage;
    }
}