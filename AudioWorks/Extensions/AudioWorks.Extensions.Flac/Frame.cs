using System;

namespace AudioWorks.Extensions.Flac
{
    struct Frame
    {
        internal FrameHeader Header;

        internal IntPtr SubFrames;

        internal FrameFooter Footer;
    }
}