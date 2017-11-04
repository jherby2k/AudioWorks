using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Apple
{
    [StructLayout(LayoutKind.Sequential)]
    struct AudioBuffer
    {
        internal uint NumberChannels;

        internal uint DataByteSize;

        internal IntPtr Data;
    }
}