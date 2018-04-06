using System;
using System.Buffers.Binary;
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

        public CoverAtom(ReadOnlySpan<byte> data)
        {
            // There could be more than one data atom. Ignore all but the first.
            var imageData = new byte[BinaryPrimitives.ReadUInt32BigEndian(data.Slice(8, 4)) - 16];
            data.Slice(24, imageData.Length).CopyTo(imageData);
            Value = CoverArtFactory.Create(imageData);
        }

        internal override byte[] GetBytes()
        {
            var result = new byte[Value.Data.Length + 24];

            // Write the atom header
            BinaryPrimitives.WriteUInt32BigEndian(result, (uint) result.Length);
            BinaryPrimitives.WriteInt32BigEndian(result.AsSpan().Slice(4), 0x636F7672); // 'covr'

            // Write the data atom header
            BinaryPrimitives.WriteUInt32BigEndian(result.AsSpan().Slice(8), (uint) result.Length - 8);
            BinaryPrimitives.WriteInt32BigEndian(result.AsSpan().Slice(12), 0x64617461); // 'data'

            // Set the type flag to PNG or JPEG
            result[19] = (byte) (Value.Lossless ? 0x0E : 0x0D);

            // Set the atom contents
            Value.Data.CopyTo(result.AsSpan().Slice(24));

            return result;
        }
    }
}