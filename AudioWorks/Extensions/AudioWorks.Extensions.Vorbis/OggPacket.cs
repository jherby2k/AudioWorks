using System;

namespace AudioWorks.Extensions.Vorbis
{
    struct OggPacket
    {
        internal IntPtr Packet;

        internal int Bytes;

        internal int BeginningOfStream;

        internal int EndOfStream;

        internal long GranulePosition;

        internal long PacketNumber;
    }
}