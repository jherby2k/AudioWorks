using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct OggPage
    {
        internal IntPtr Header;

#if (WINDOWS)
        internal readonly int HeaderLength;
#else
        internal readonly long HeaderLength;
#endif

        internal IntPtr Body;

#if (WINDOWS)
        internal readonly int BodyLength;
#else
        internal readonly long BodyLength;
#endif
    }
}