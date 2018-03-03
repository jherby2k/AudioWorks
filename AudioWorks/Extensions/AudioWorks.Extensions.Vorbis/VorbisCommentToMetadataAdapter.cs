using AudioWorks.Common;
using System;
using System.Globalization;
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
                var comment = Encoding.UTF8
                    .GetString(new Span<byte>(commentPtrs[i].ToPointer(), commentLengths[i]).ToArray())
                    .Split(new[] { '=' }, 2);

                try
                {
                    switch (comment[0].ToUpperInvariant())
                    {
                        case "TITLE":
                            Title = comment[1];
                            break;

                        case "ARTIST":
                            Artist = comment[1];
                            break;

                        case "ALBUM":
                            Album = comment[1];
                            break;

                        case "ALBUMARTIST":
                            AlbumArtist = comment[1];
                            break;

                        case "COMPOSER":
                            Composer = comment[1];
                            break;

                        case "GENRE":
                            Genre = comment[1];
                            break;

                        case "DESCRIPTION":
                        case "COMMENT":
                            Comment = comment[1];
                            break;

                        case "DATE":
                        case "YEAR":
                            // Descriptions are allowed after whitespace
                            comment[1] = comment[1].Split(' ')[0];
                            // The DATE comment may contain a full date, or only the year
                            if (DateTime.TryParse(comment[1], CultureInfo.CurrentCulture,
                                DateTimeStyles.NoCurrentDateDefault, out var result))
                            {
                                Day = result.Day.ToString(CultureInfo.InvariantCulture);
                                Month = result.Month.ToString(CultureInfo.InvariantCulture);
                                Year = result.Year.ToString(CultureInfo.InvariantCulture);
                            }
                            else
                                Year = comment[1];
                            break;

                        case "TRACKNUMBER":
                            // The track number and count may be packed into the same comment
                            var segments = comment[1].Split('/');
                            TrackNumber = segments[0];
                            if (segments.Length > 1)
                                TrackCount = segments[1];
                            break;

                        case "TRACKCOUNT":
                        case "TRACKTOTAL":
                        case "TOTALTRACKS":
                            TrackCount = comment[1];
                            break;

                        case "REPLAYGAIN_TRACK_PEAK":
                            TrackPeak = comment[1];
                            break;

                        case "REPLAYGAIN_ALBUM_PEAK":
                            AlbumPeak = comment[1];
                            break;

                        case "REPLAYGAIN_TRACK_GAIN":
                            TrackGain = comment[1].Replace(" dB", string.Empty);
                            break;

                        case "REPLAYGAIN_ALBUM_GAIN":
                            AlbumGain = comment[1].Replace(" dB", string.Empty);
                            break;

                        case "METADATA_BLOCK_PICTURE":
                            CoverArt = CoverArtAdapter.FromComment(comment[1]);
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