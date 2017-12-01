using System;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class AlacAtom
    {
        internal byte BitsPerSample { get; }

        internal byte Channels { get; }

        internal uint SampleRate { get; }

        internal AlacAtom([NotNull] byte[] data)
        {
            BitsPerSample = data[53];
            Channels = data[57];
            Array.Reverse(data, 68, 4);
            SampleRate = BitConverter.ToUInt32(data, 68);
        }
    }
}