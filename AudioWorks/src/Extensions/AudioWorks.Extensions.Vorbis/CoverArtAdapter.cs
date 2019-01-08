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
using System.Buffers.Binary;
using System.Buffers.Text;
using System.IO;
using System.Text;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    static class CoverArtAdapter
    {
        [Pure, CanBeNull]
        internal static ICoverArt FromBase64(ReadOnlySpan<byte> value)
        {
            Span<byte> decodedValue = new byte[Base64.GetMaxDecodedFromUtf8Length(value.Length)];
            Base64.DecodeFromUtf8(value, decodedValue, out _, out _);

            // If the image isn't a "Front Cover" or "Other", return null
            var imageType = BinaryPrimitives.ReadUInt32BigEndian(decodedValue);
            if (imageType != 3 && imageType != 0) return null;

            var offset = 4;

            // Seek past the mime type and description
            offset += (int) BinaryPrimitives.ReadUInt32BigEndian(decodedValue.Slice(offset)) + 4;
            offset += (int) BinaryPrimitives.ReadUInt32BigEndian(decodedValue.Slice(offset)) + 4;

            // Seek past the width, height, color depth and type
            offset += 16;

            return CoverArtFactory.GetOrCreate(
                decodedValue.Slice(offset + 4, (int) BinaryPrimitives.ReadUInt32BigEndian(decodedValue.Slice(offset))));
        }

        [Pure, NotNull]
        internal static string ToComment([NotNull] ICoverArt coverArt)
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                // Set the picture type as "Front Cover"
                writer.WriteBigEndian(3);

                var mimeBytes = Encoding.ASCII.GetBytes(coverArt.MimeType);
                writer.WriteBigEndian((uint) mimeBytes.Length);
                writer.Write(mimeBytes);

                var descriptionBytes = Encoding.UTF8.GetBytes(string.Empty);
                writer.WriteBigEndian((uint) descriptionBytes.Length);
                writer.Write(descriptionBytes);

                writer.WriteBigEndian((uint) coverArt.Width);
                writer.WriteBigEndian((uint) coverArt.Height);
                writer.WriteBigEndian((uint) coverArt.ColorDepth);
                writer.WriteBigEndian(0); // Always 0 for PNG and JPEG

                writer.WriteBigEndian((uint) coverArt.Data.Length);
                writer.Write(coverArt.Data.ToArray());

                return Convert.ToBase64String(stream.GetBuffer());
            }
        }
    }
}