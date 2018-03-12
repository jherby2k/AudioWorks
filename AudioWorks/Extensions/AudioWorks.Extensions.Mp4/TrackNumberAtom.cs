using System;
using System.Buffers.Binary;
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
            Span<byte> result = stackalloc byte[32];

            // Write the atom header:
            BinaryPrimitives.WriteUInt32BigEndian(result, 32);
            BinaryPrimitives.WriteInt32BigEndian(result.Slice(4), 0x74726b6e); // 'trkn'

            // Write the data atom header:
            BinaryPrimitives.WriteUInt32BigEndian(result.Slice(8), 24);
            BinaryPrimitives.WriteInt32BigEndian(result.Slice(12), 0x64617461); // 'data'

            // Set the track number (the rest of the bytes are set to 0):
            result[27] = TrackNumber;
            result[29] = TrackCount;

            return result.ToArray();
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