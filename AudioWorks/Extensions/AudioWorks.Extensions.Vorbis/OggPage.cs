using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct OggPage
    {
        internal readonly IntPtr Header;

        internal readonly int HeaderLength;

        internal readonly IntPtr Body;

        internal readonly int BodyLength;
    }
}