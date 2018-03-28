using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Apple
{
    [StructLayout(LayoutKind.Sequential)]
    struct AudioStreamBasicDescription
    {
        internal double SampleRate;

        internal AudioFormat AudioFormat;

        internal AudioFormatFlags Flags;

        internal uint BytesPerPacket;

        internal uint FramesPerPacket;

        internal uint BytesPerFrame;

        internal uint ChannelsPerFrame;

        internal uint BitsPerChannel;

        readonly uint Reserved;
    }
}