using JetBrains.Annotations;
using System;

namespace AudioWorks.Extensions.Mp4
{
    class SttsAtom
    {
        internal uint PacketCount { get; }

        internal uint PacketSize { get; }

        internal SttsAtom([NotNull] byte[] data)
        {
            Array.Reverse(data, 16, 4);
            PacketCount = BitConverter.ToUInt32(data, 16);

            Array.Reverse(data, 20, 4);
            PacketSize = BitConverter.ToUInt32(data, 20);
        }
    }
}