using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    struct Frame
    {
        internal FrameHeader Header;

        readonly IntPtr SubFrames;

        readonly FrameFooter Footer;
    }
}