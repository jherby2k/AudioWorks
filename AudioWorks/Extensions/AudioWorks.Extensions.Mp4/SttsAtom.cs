using System;
using System.Buffers.Binary;

namespace AudioWorks.Extensions.Mp4
{
    sealed class SttsAtom
    {
        internal uint PacketCount { get; }

        internal uint PacketSize { get; }

        internal SttsAtom(ReadOnlySpan<byte> data)
        {
            PacketCount = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(16));
            PacketSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(20));
        }
    }
}