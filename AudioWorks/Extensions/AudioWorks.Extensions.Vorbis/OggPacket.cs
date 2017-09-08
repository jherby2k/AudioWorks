using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct OggPacket
    {
        readonly IntPtr Packet;

        readonly int Bytes;

        readonly int BeginningOfStream;

        readonly int EndOfStream;

        readonly long GranulePosition;

        readonly long PacketNumber;
    }
}