using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Apple
{
    [StructLayout(LayoutKind.Sequential)]
    struct AudioBufferList
    {
        internal uint NumberBuffers;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        internal AudioBuffer[] Buffers;
    }
}