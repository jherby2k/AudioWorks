/* Copyright © 2019 Jeremy Herbison

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
using System.Globalization;
#if !NETCOREAPP2_1
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#endif
using System.Text;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Opus
{
    sealed class CommentHeaderToMetadataAdapter : AudioMetadata
    {
        internal unsafe CommentHeaderToMetadataAdapter(in OggPacket packet)
        {
            if (packet.Bytes < 16)
                throw new AudioMetadataInvalidException("Invalid Opus comment header.");

#if WINDOWS
            var headerBytes = new Span<byte>(packet.Packet.ToPointer(), packet.Bytes);
#else
            var headerBytes = new Span<byte>(packet.Packet.ToPointer(), (int) packet.Bytes);
#endif

#if NETCOREAPP2_1
            if (!Encoding.ASCII.GetString(headerBytes.Slice(0, 8))
#else
            if (!Encoding.ASCII.GetString((byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(headerBytes)), 8)
#endif
                .Equals("OpusTags", StringComparison.Ordinal))
                throw new AudioMetadataInvalidException("Invalid Opus comment header.");
            var headerPosition = 8;

            var vendorLength = (int) BinaryPrimitives.ReadUInt32LittleEndian(headerBytes.Slice(8));
            headerPosition += 4 + vendorLength;

            var commentCount = BinaryPrimitives.ReadUInt32LittleEndian(headerBytes.Slice(headerPosition));
            headerPosition += 4;

            while (commentCount > 0)
            {
                var length = (int) BinaryPrimitives.ReadUInt32LittleEndian(headerBytes.Slice(headerPosition));
                headerPosition += 4;

                var commentBytes = headerBytes.Slice(headerPosition, length);
                headerPosition += length;

                var delimiter = commentBytes.IndexOf((byte) 0x3D); // '='
#if NETCOREAPP2_1
                var key = Encoding.ASCII.GetString(commentBytes.Slice(0, delimiter));
#else
                var keyBytes = commentBytes.Slice(0, delimiter);
                var key = Encoding.ASCII.GetString(
                    (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(keyBytes)),
                    keyBytes.Length);
#endif

                if (key.Equals("METADATA_BLOCK_PICTURE", StringComparison.OrdinalIgnoreCase))
                    CoverArt = CoverArtAdapter.FromBase64(commentBytes.Slice(delimiter + 1));
                else
                {
#if NETCOREAPP2_1
                    var value = Encoding.UTF8.GetString(commentBytes.Slice(delimiter + 1));
#else
                    var valueBytes = commentBytes.Slice(delimiter + 1);
                    var value = Encoding.UTF8.GetString(
                        (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(valueBytes)),
                        valueBytes.Length);
#endif

                    try
                    {
                        // ReSharper disable once SwitchStatementMissingSomeCases
                        switch (key.ToUpperInvariant())
                        {
                            case "TITLE":
                                Title = value;
                                break;

                            case "ARTIST":
                                Artist = value;
                                break;

                            case "ALBUM":
                                Album = value;
                                break;

                            case "ALBUMARTIST":
                                AlbumArtist = value;
                                break;

                            case "COMPOSER":
                                Composer = value;
                                break;

                            case "GENRE":
                                Genre = value;
                                break;

                            case "DESCRIPTION":
                            case "COMMENT":
                                Comment = value;
                                break;

                            case "DATE":
                            case "YEAR":
                                // Descriptions are allowed after whitespace
                                value = value.Split(' ')[0];
                                // The DATE comment may contain a full date, or only the year
                                if (DateTime.TryParse(value, CultureInfo.CurrentCulture,
                                    DateTimeStyles.NoCurrentDateDefault, out var result))
                                {
                                    Day = result.Day.ToString(CultureInfo.InvariantCulture);
                                    Month = result.Month.ToString(CultureInfo.InvariantCulture);
                                    Year = result.Year.ToString(CultureInfo.InvariantCulture);
                                }
                                else
                                    Year = value;

                                break;

                            case "TRACKNUMBER":
                                // The track number and count may be packed into the same comment
                                var segments = value.Split('/');
                                TrackNumber = segments[0];
                                if (segments.Length > 1)
                                    TrackCount = segments[1];
                                break;

                            case "TRACKCOUNT":
                            case "TRACKTOTAL":
                            case "TOTALTRACKS":
                                TrackCount = value;
                                break;
                        }
                    }
                    catch (AudioMetadataInvalidException)
                    {
                        // If a field is invalid, just leave it blank
                    }
                }

                commentCount--;
            }
        }
    }
}
