﻿/* Copyright © 2018 Jeremy Herbison

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
using System.Buffers.Text;
using System.Text;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Vorbis
{
    static class CoverArtAdapter
    {
        internal static ICoverArt? FromBase64(ReadOnlySpan<byte> value)
        {
            // Use heap allocations for cover art > 256kB
            var byteCount = Base64.GetMaxDecodedFromUtf8Length(value.Length);
            var decodedValue = byteCount < 0x40000 ? stackalloc byte[byteCount] : new byte[byteCount];
            Base64.DecodeFromUtf8(value, decodedValue, out _, out _);

            // If the image isn't a "Front Cover" or "Other", return null
            var imageType = BinaryPrimitives.ReadUInt32BigEndian(decodedValue);
            if (imageType != 3 && imageType != 0) return null;

            var offset = 4;

            // Seek past the mime type and description
            offset += (int) BinaryPrimitives.ReadUInt32BigEndian(decodedValue[offset..]) + 4;
            offset += (int) BinaryPrimitives.ReadUInt32BigEndian(decodedValue[offset..]) + 4;

            // Seek past the width, height, color depth and type
            offset += 16;

            return CoverArtFactory.GetOrCreate(
                decodedValue.Slice(offset + 4, (int) BinaryPrimitives.ReadUInt32BigEndian(decodedValue[offset..])));
        }

        internal static ReadOnlySpan<byte> ToBase64(ICoverArt coverArt)
        {
            var dataLength = 32 + coverArt.MimeType.Length + coverArt.Data.Length;
            Span<byte> buffer = new byte[Base64.GetMaxEncodedToUtf8Length(dataLength) + 1];

            // Set the picture type as "Front Cover"
            BinaryPrimitives.WriteUInt32BigEndian(buffer, 3);

            BinaryPrimitives.WriteUInt32BigEndian(buffer[4..], (uint) coverArt.MimeType.Length);
            Encoding.ASCII.GetBytes(coverArt.MimeType, buffer[8..]);

            BinaryPrimitives.WriteUInt32BigEndian(buffer[(12 + coverArt.MimeType.Length)..], (uint) coverArt.Width);
            BinaryPrimitives.WriteUInt32BigEndian(buffer[(16 + coverArt.MimeType.Length)..], (uint) coverArt.Height);
            BinaryPrimitives.WriteUInt32BigEndian(buffer[(20 + coverArt.MimeType.Length)..], (uint) coverArt.ColorDepth);

            BinaryPrimitives.WriteUInt32BigEndian(buffer[(28 + coverArt.MimeType.Length)..], (uint) coverArt.Data.Length);
            coverArt.Data.CopyTo(buffer[(32 + coverArt.MimeType.Length)..]);

            Base64.EncodeToUtf8InPlace(buffer, dataLength, out var bytesWritten);

            // Since SkipLocalsInit is set, make sure the buffer is null-terminated
            buffer[bytesWritten++] = 0;

            return buffer[..bytesWritten];
        }
    }
}