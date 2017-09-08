using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    struct Frame
    {
        readonly FrameHeader Header;

        readonly IntPtr SubFrames;

        readonly FrameFooter Footer;
    }
}