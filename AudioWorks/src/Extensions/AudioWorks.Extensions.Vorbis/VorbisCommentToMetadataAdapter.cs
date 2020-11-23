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
using System.Globalization;
#if NETSTANDARD2_0
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#endif
using System.Text;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class VorbisCommentToMetadataAdapter : AudioMetadata
    {
        internal unsafe VorbisCommentToMetadataAdapter(VorbisComment vorbisComment)
        {
            var commentPtrs = new Span<IntPtr>(vorbisComment.UserComments.ToPointer(), vorbisComment.Comments);
            var commentLengths = new Span<int>(vorbisComment.CommentLengths.ToPointer(), vorbisComment.Comments);

            for (var i = 0; i < vorbisComment.Comments; i++)
            {
                var commentBytes = new Span<byte>(commentPtrs[i].ToPointer(), commentLengths[i]);
                var delimiter = commentBytes.IndexOf((byte) 0x3D); // '='

#if NETSTANDARD2_0
                var keyBytes = commentBytes.Slice(0, delimiter);
                var key = Encoding.ASCII.GetString(
                    (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(keyBytes)),
                    keyBytes.Length);

                if (key.Equals("METADATA_BLOCK_PICTURE", StringComparison.OrdinalIgnoreCase))
                    CoverArt = CoverArtAdapter.FromBase64(commentBytes.Slice(delimiter + 1));
                else
                {
                    var valueBytes = commentBytes.Slice(delimiter + 1);
                    SetText(key, Encoding.UTF8.GetString(
                        (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(valueBytes)),
                        valueBytes.Length));
                }
#else
                var key = Encoding.ASCII.GetString(commentBytes.Slice(0, delimiter));

                if (key.Equals("METADATA_BLOCK_PICTURE", StringComparison.OrdinalIgnoreCase))
                    CoverArt = CoverArtAdapter.FromBase64(commentBytes[(delimiter + 1)..]);
                else
                    SetText(key, Encoding.UTF8.GetString(commentBytes[(delimiter + 1)..]));
#endif
            }
        }

        void SetText(string key, string value)
        {
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

                    case "REPLAYGAIN_TRACK_PEAK":
                        TrackPeak = value;
                        break;

                    case "REPLAYGAIN_ALBUM_PEAK":
                        AlbumPeak = value;
                        break;

                    case "REPLAYGAIN_TRACK_GAIN":
#if NETSTANDARD2_0
                        TrackGain = value.Replace(" dB", string.Empty);
#else
                        TrackGain = value.Replace(" dB", string.Empty, StringComparison.OrdinalIgnoreCase);
#endif
                        break;

                    case "REPLAYGAIN_ALBUM_GAIN":
#if NETSTANDARD2_0
                        AlbumGain = value.Replace(" dB", string.Empty);
#else
                        AlbumGain = value.Replace(" dB", string.Empty, StringComparison.OrdinalIgnoreCase);
#endif
                        break;
                }
            }
            catch (AudioMetadataInvalidException)
            {
                // If a field is invalid, just leave it blank
            }
        }
    }
}