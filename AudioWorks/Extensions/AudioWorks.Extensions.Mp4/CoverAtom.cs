using System;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class CoverAtom : WritableAtom
    {
        [NotNull] readonly ICoverArt _coverArt;

        internal CoverAtom([NotNull] ICoverArt coverArt)
        {
            _coverArt = coverArt;
        }

        internal override byte[] GetBytes()
        {
            var data = _coverArt.GetData();

            var result = new byte[data.Length + 24];

            // Write the atom header
            ConvertToBigEndianBytes((uint)result.Length).CopyTo(result, 0);
            BitConverter.GetBytes(0x72766f63).CopyTo(result, 4); // 'rvoc'

            // Write the data atom header
            ConvertToBigEndianBytes((uint)result.Length - 8).CopyTo(result, 8);
            BitConverter.GetBytes(0x61746164).CopyTo(result, 12); // 'atad'

            // Set the type flag to PNG or JPEG
            result[19] = (byte) (_coverArt.Lossless ? 0xe : 0xd);

            // Set the atom contents
            data.CopyTo(result, 24);

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