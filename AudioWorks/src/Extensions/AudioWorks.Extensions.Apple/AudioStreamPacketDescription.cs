using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Apple
{
    [StructLayout(LayoutKind.Sequential)]
    struct AudioStreamPacketDescription
    {
        readonly long StartOffset;

        readonly uint VariableFramesInPacket;

        readonly uint DataByteSize;
    }
}