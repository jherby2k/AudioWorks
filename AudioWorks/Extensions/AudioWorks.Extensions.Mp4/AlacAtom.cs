using System;
using System.Buffers.Binary;

namespace AudioWorks.Extensions.Mp4
{
    sealed class AlacAtom
    {
        internal byte BitsPerSample { get; }

        internal byte Channels { get; }

        internal uint SampleRate { get; }

        internal AlacAtom(ReadOnlySpan<byte> data)
        {
            BitsPerSample = data[53];
            Channels = data[57];
            SampleRate = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(68));
        }
    }
}