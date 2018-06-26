using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct FrameFooter
    {
        readonly ushort Crc;
    }
}