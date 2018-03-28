using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct OggPacket
    {
        readonly IntPtr Packet;

#if (WINDOWS)
        readonly int Bytes;

        readonly int BeginningOfStream;

        readonly int EndOfStream;
#else
        readonly long Bytes;

        readonly long BeginningOfStream;

        readonly long EndOfStream;
#endif

        readonly long GranulePosition;

        internal readonly long PacketNumber;
    }
}