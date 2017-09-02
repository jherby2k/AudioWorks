using JetBrains.Annotations;
using System.Collections.Generic;

namespace AudioWorks.Extensions.Mp4
{
    class EsdsAtom
    {
        [NotNull] static readonly uint[] _sampleRates =
        {
            96000,
            88200,
            64000,
            48000,
            44100,
            32000,
            24000,
            22050,
            16000,
            12000,
            11025,
            8000,
            7350,
            0
        };

        internal uint SampleRate { get; }

        internal ushort Channels { get; }

        internal EsdsAtom([NotNull] IReadOnlyList<byte> data)
        {
            // This appears to be 0 for Apple Lossless files: 
            if (data[12] == 0) return;

            SampleRate = _sampleRates[(data[43] << 1) & 0b00001110 | (data[44] >> 7) & 0b00000001];
            Channels = (ushort) ((data[44] >> 3) & 0b00001111);
        }
    }
}