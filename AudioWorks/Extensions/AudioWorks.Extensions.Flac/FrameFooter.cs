using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    struct FrameFooter
    {
        readonly ushort Crc;
    }
}