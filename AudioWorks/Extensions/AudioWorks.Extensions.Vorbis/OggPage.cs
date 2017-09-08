using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct OggPage
    {
        readonly IntPtr Header;

        readonly int HeaderLength;

        readonly IntPtr Body;

        readonly int BodyLength;
    }
}