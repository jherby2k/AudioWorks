using System;
using System.Buffers.Binary;
using System.IO;
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
            Value = CoverArtFactory.GetOrCreate(imageData);
        }

        internal override void Write(Stream output)
        {
            using (var writer = new Mp4Writer(output))
            {
                // Write the atom header
                writer.WriteBigEndian((uint) Value.Data.Length + 24);
                writer.WriteBigEndian(0x636F7672); // 'covr'

                // Write the data atom header
                writer.WriteBigEndian((uint) Value.Data.Length + 16);
                writer.WriteBigEndian(0x64617461); // 'data'

                // Set the type flag to PNG or JPEG
                writer.WriteBigEndian(Value.Lossless ? 14u : 13u);
                writer.WriteZeros(4);

                writer.Write(Value.Data.ToArray());
            }
        }
    }
}