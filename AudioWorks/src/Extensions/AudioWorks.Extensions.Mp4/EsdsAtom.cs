/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;

namespace AudioWorks.Extensions.Mp4
{
    sealed class EsdsAtom
    {
        static readonly uint[] _sampleRates =
        [
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
        ];

        internal bool IsAac { get; }

        internal uint SampleRate { get; }

        internal ushort Channels { get; }

        internal EsdsAtom(ReadOnlySpan<byte> data)
        {
            // Confirm this is an AAC descriptor
            if (data[12] != 0x03 || data[25] != 0x40)
                return;

            IsAac = true;
            SampleRate = _sampleRates[(data[43] << 1) & 0b00001110 | (data[44] >> 7) & 0b00000001];
            Channels = (ushort) ((data[44] >> 3) & 0b00001111);
        }
    }
}