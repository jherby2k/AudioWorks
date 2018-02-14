using System;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class TrackNumberAtom : WritableAtom
    {
        internal byte TrackNumber { get; }

        internal byte TrackCount { get; }

        internal TrackNumberAtom([NotNull] IReadOnlyList<byte> data)
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

        internal override byte[] GetBytes()
        {
            var result = new byte[32];

            // Write the atom header:
            ConvertToBigEndianBytes(32).CopyTo(result, 0);
            BitConverter.GetBytes(0x6e6b7274).CopyTo(result, 4); // 'nkrt'

            // Write the data atom header:
            ConvertToBigEndianBytes(24).CopyTo(result, 8);
            BitConverter.GetBytes(0x61746164).CopyTo(result, 12); // 'atad'

            // Set the track number (the rest of the bytes are set to 0):
            result[27] = TrackNumber;
            result[29] = TrackCount;

            return result;
        }

        [Pure, NotNull]
        static byte[] ConvertToBigEndianBytes(uint value)
        {
            var result = BitConverter.GetBytes(value);
            Array.Reverse(result);
            return result;
        }
    }
}