using AudioWorks.Common;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

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

#if NETCOREAPP2_1
                var key = Encoding.ASCII.GetString(commentBytes.Slice(0, delimiter));
                var value = Encoding.UTF8.GetString(commentBytes.Slice(delimiter + 1));
#else
                var keyBytes = commentBytes.Slice(0, delimiter);
                var valueBytes = commentBytes.Slice(delimiter + 1);

                var key = Encoding.ASCII.GetString(
                    (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(keyBytes)),
                    keyBytes.Length);
                var value = Encoding.UTF8.GetString(
                    (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(valueBytes)),
                    valueBytes.Length);
#endif

                try
                {
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
#if NETCOREAPP2_1
                            TrackGain = value.Replace(" dB", string.Empty, StringComparison.OrdinalIgnoreCase);
#else
                            TrackGain = value.Replace(" dB", string.Empty);
#endif
                            break;

                        case "REPLAYGAIN_ALBUM_GAIN":
#if NETCOREAPP2_1
                            AlbumGain = value.Replace(" dB", string.Empty, StringComparison.OrdinalIgnoreCase);
#else
                            AlbumGain = value.Replace(" dB", string.Empty);
#endif
                            break;

                        case "METADATA_BLOCK_PICTURE":
                            CoverArt = CoverArtAdapter.FromComment(value);
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
}