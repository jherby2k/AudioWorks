using System;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class EsdsAtom
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

        internal bool IsAac { get; }

        internal uint SampleRate { get; }

        internal ushort Channels { get; }

        internal EsdsAtom(ReadOnlySpan<byte> data)
        {
            // Confirm this is an AAC descriptor
            if (data[12] != 0x3 || data[25] != 0x40)
                return;

            IsAac = true;
            SampleRate = _sampleRates[(data[43] << 1) & 0b00001110 | (data[44] >> 7) & 0b00000001];
            Channels = (ushort) ((data[44] >> 3) & 0b00001111);
        }
    }
}