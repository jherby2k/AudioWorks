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

        internal CoverAtom([NotNull] ICoverArt coverArt) => Value = coverArt;

        public CoverAtom(ReadOnlySpan<byte> data) =>
            Value = CoverArtFactory.GetOrCreate(data.Slice(24,
                (int) BinaryPrimitives.ReadUInt32BigEndian(data.Slice(8, 4)) - 16));

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

#if NETSTANDARD2_0
                writer.Write(Value.Data.ToArray());
#else
                writer.Write(Value.Data);
#endif
            }
        }
    }
}