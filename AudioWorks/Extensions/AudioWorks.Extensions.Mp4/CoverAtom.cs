using System;
using System.Linq;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class CoverAtom : WritableAtom
    {
        [NotNull]
        public ICoverArt Value { get; }

        internal CoverAtom([NotNull] ICoverArt coverArt)
        {
            Value = coverArt;
        }

        public CoverAtom([NotNull] byte[] data)
        {
            // There could be more than one data atom. Ignore all but the first.
            var imageData = new byte[BitConverter.ToUInt32(data.Skip(8).Take(4).Reverse().ToArray(), 0) - 16];
            Array.Copy(data, 24, imageData, 0, imageData.Length);
            Value = CoverArtFactory.Create(imageData);
        }

        internal override byte[] GetBytes()
        {
            var data = Value.GetData();

            var result = new byte[data.Length + 24];

            // Write the atom header
            ConvertToBigEndianBytes((uint)result.Length).CopyTo(result, 0);
            BitConverter.GetBytes(0x72766f63).CopyTo(result, 4); // 'rvoc'

            // Write the data atom header
            ConvertToBigEndianBytes((uint)result.Length - 8).CopyTo(result, 8);
            BitConverter.GetBytes(0x61746164).CopyTo(result, 12); // 'atad'

            // Set the type flag to PNG or JPEG
            result[19] = (byte) (Value.Lossless ? 0xe : 0xd);

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