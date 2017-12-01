using System;
using System.Globalization;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    sealed class VorbisCommentToMetadataAdapter : AudioMetadata
    {
        internal void Set([NotNull] string field, [NotNull] string value)
        {
            try
            {
                switch (field.ToUpperInvariant())
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
                        TrackGain = value.Replace(" dB", string.Empty);
                        break;

                    case "REPLAYGAIN_ALBUM_GAIN":
                        AlbumGain = value.Replace(" dB", string.Empty);
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