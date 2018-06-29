using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct Frame
    {
        internal readonly FrameHeader Header;

        readonly IntPtr SubFrames;

        readonly FrameFooter Footer;
    }
}