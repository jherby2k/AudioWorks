using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct OggPackBuffer
    {
#if WINDOWS
        readonly int EndByte;
#else
        readonly long EndByte;
#endif

        readonly int EndBit;

        readonly IntPtr Buffer;

        readonly IntPtr Ptr;

#if WINDOWS
        readonly int Storage;
#else
        readonly long Storage;
#endif
    }
}