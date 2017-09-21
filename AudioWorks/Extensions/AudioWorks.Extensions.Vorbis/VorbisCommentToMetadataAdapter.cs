using AudioWorks.Common;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class VorbisCommentToMetadataAdapter : AudioMetadata
    {
        internal VorbisCommentToMetadataAdapter(VorbisComment vorbisComment)
        {
            var commentLengths = new int[vorbisComment.Comments];
            Marshal.Copy(vorbisComment.CommentLengths, commentLengths, 0, commentLengths.Length);

            var commentPtrs = new IntPtr[vorbisComment.Comments];
            Marshal.Copy(vorbisComment.UserComments, commentPtrs, 0, commentPtrs.Length);

            for (var i = 0; i < vorbisComment.Comments; i++)
            {
                var commentBytes = new byte[commentLengths[i]];
                Marshal.Copy(commentPtrs[i], commentBytes, 0, commentLengths[i]);

                var comment = Encoding.UTF8.GetString(commentBytes).Split(new[] { '=' }, 2);

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