/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Globalization;
using System.IO;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class TrackNumberAtom : WritableAtom
    {
        internal byte TrackNumber { get; }

        internal byte TrackCount { get; }

        internal TrackNumberAtom(ReadOnlySpan<byte> data)
        {
            TrackNumber = data[27];
            TrackCount = data[29];
        }

        internal TrackNumberAtom([NotNull] string trackNumber, [NotNull] string trackCount)
        {
            TrackNumber =
                !string.IsNullOrEmpty(trackNumber)
                    ? byte.Parse(trackNumber, CultureInfo.InvariantCulture)
                    : (byte) 0;

            TrackCount =
                !string.IsNullOrEmpty(trackCount)
                    ? byte.Parse(trackCount, CultureInfo.InvariantCulture)
                    : (byte) 0;
        }

        internal override void Write(Stream output)
        {
            using (var writer = new Mp4Writer(output))
            {
                // Write the atom header
                writer.WriteBigEndian(32);
                writer.WriteBigEndian(0x74726B6E); // 'trkn'

                // Write the data atom header
                writer.WriteBigEndian(24);
                writer.WriteBigEndian(0x64617461); // 'data'

                writer.WriteZeros(11);
                writer.Write(TrackNumber);
                writer.WriteZeros(1);
                writer.Write(TrackCount);
                writer.WriteZeros(2);
            }
        }
    }
}