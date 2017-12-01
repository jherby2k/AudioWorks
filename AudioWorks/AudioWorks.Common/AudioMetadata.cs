using System;
using System.Globalization;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Contains mutable metadata about the audio file.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class AudioMetadata
    {
        [NotNull] string _title = string.Empty;
        [NotNull] string _artist = string.Empty;
        [NotNull] string _album = string.Empty;
        [NotNull] string _albumArtist = string.Empty;
        [NotNull] string _composer = string.Empty;
        [NotNull] string _genre = string.Empty;
        [NotNull] string _comment = string.Empty;
        [NotNull] string _day = string.Empty;
        [NotNull] string _month = string.Empty;
        [NotNull] string _year = string.Empty;
        [NotNull] string _trackNumber = string.Empty;
        [NotNull] string _trackCount = string.Empty;
        [NotNull] string _trackPeak = string.Empty;
        [NotNull] string _albumPeak = string.Empty;
        [NotNull] string _trackGain = string.Empty;
        [NotNull] string _albumGain = string.Empty;

        /// <summary>
        /// Gets or sets the title. To clear the title, set an empty <paramref name="value"/>.
        /// </summary>
        /// <value>The title.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        [NotNull]
        public string Title
        {
            get => _title;
            set => _title = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the artist. To clear the artist, set an empty <paramref name="value"/>.
        /// </summary>
        /// <value>The artist.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        [NotNull]
        public string Artist
        {
            get => _artist;
            set => _artist = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the album. To clear the album, set an empty <paramref name="value"/>.
        /// </summary>
        /// <value>The album.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        [NotNull]
        public string Album
        {
            get => _album;
            set => _album = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the album artist. To clear the album artist, set an empty <paramref name="value"/>.
        /// </summary>
        /// <value>The album artist.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        [NotNull]
        public string AlbumArtist
        {
            get => _albumArtist;
            set => _albumArtist = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the composer. To clear the composer, set an empty <paramref name="value"/>.
        /// </summary>
        /// <value>The composer.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        [NotNull]
        public string Composer
        {
            get => _composer;
            set => _composer = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the genre. To clear the genre, set an empty <paramref name="value"/>.
        /// </summary>
        /// <value>The genre.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        [NotNull]
        public string Genre
        {
            get => _genre;
            set => _genre = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the comment. To clear the comment, set an empty <paramref name="value"/>.
        /// </summary>
        /// <value>The comment.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        [NotNull]
        public string Comment
        {
            get => _comment;
            set => _comment = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the day of the month as a number from 1 to 31. To clear the day, set an empty
        /// <paramref name="value"/>.
        /// </summary>
        /// <value>The day of the month.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        /// <exception cref="AudioMetadataInvalidException">Thrown if <paramref name="value"/> is not valid.
        /// </exception>
        [NotNull]
        public string Day
        {
            get => _day;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (string.IsNullOrEmpty(value))
                    _day = string.Empty;
                else
                {
                    if (!int.TryParse(value, out var intValue) || intValue < 1 || intValue > 31)
                        throw new AudioMetadataInvalidException("Month must be between 1 and 31.");
                    _day = intValue.ToString("00", CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// Gets or sets the month as a number from 1 to 12. To clear the month, set an empty <paramref name="value"/>.
        /// </summary>
        /// <value>The month.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        /// <exception cref="AudioMetadataInvalidException">Thrown if <paramref name="value"/> is not valid.
        /// </exception>
        [NotNull]
        public string Month
        {
            get => _month;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (string.IsNullOrEmpty(value))
                    _month = string.Empty;
                else
                {
                    if (!int.TryParse(value, out var intValue) || intValue < 1 || intValue > 12)
                        throw new AudioMetadataInvalidException("Month must be between 1 and 12.");
                    _month = intValue.ToString("00", CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// Gets or sets the year. To clear the year, set an empty <paramref name="value"/>.
        /// </summary>
        /// <value>The year.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        /// <exception cref="AudioMetadataInvalidException">Thrown if <paramref name="value"/> is not valid.
        /// </exception>
        [NotNull]
        public string Year
        {
            get => _year;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (string.IsNullOrEmpty(value))
                    _year = string.Empty;
                else
                {
                    if (!Regex.IsMatch(value, "^[1-9][0-9]{3}$"))
                        throw new AudioMetadataInvalidException("Year must be between 1000 and 9999.");
                    _year = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the track number. To clear the track number, set an empty <paramref name="value"/>.
        /// </summary>
        /// <value>The track number.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        /// <exception cref="AudioMetadataInvalidException">Thrown if <paramref name="value"/> is not valid.
        /// </exception>
        [NotNull]
        public string TrackNumber
        {
            get => _trackNumber;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (string.IsNullOrEmpty(value))
                    _trackNumber = string.Empty;
                else
                {
                    if (!int.TryParse(value, out var intValue) || intValue < 1 || intValue > 99)
                        throw new AudioMetadataInvalidException("TrackNumber must be between 1 and 99.");
                    _trackNumber = intValue.ToString("00", CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// Gets or sets the track count. To clear the track count, set an empty <paramref name="value"/>.
        /// </summary>
        /// <value>The track count.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        /// <exception cref="AudioMetadataInvalidException">Thrown if <paramref name="value"/> is not valid.
        /// </exception>
        [NotNull]
        public string TrackCount
        {
            get => _trackCount;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (string.IsNullOrEmpty(value))
                    _trackCount = string.Empty;
                else
                {
                    if (!int.TryParse(value, out var intValue) || intValue < 1 || intValue > 99)
                        throw new AudioMetadataInvalidException("TrackCount must be between 1 and 99.");
                    _trackCount = intValue.ToString("00", CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// Gets or sets the track's peak amplitude. To clear the track peak, set an empty <paramref name="value"/>.
        /// </summary>
        /// <value>The track peak.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        /// <exception cref="AudioMetadataInvalidException">Thrown if <paramref name="value"/> is not valid.
        /// </exception>
        [NotNull]
        public string TrackPeak
        {
            get => _trackPeak;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (string.IsNullOrEmpty(value))
                    _trackPeak = string.Empty;
                else
                {
                    if (!double.TryParse(value, out var floatValue) || floatValue < 0)
                        throw new AudioMetadataInvalidException("TrackPeak cannot be negative.");
                    _trackPeak = floatValue.ToString("F6", CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// Gets or sets the album's peak amplitude. To clear the album peak, set an empty <paramref name="value"/>.
        /// </summary>
        /// <value>The album peak.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        /// <exception cref="AudioMetadataInvalidException">Thrown if <paramref name="value"/> is not valid.
        /// </exception>
        [NotNull]
        public string AlbumPeak
        {
            get => _albumPeak;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (string.IsNullOrEmpty(value))
                    _albumPeak = string.Empty;
                else
                {
                    if (!double.TryParse(value, out var floatValue) || floatValue < 0)
                        throw new AudioMetadataInvalidException("AlbumPeak cannot be negative.");
                    _albumPeak = floatValue.ToString("F6", CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// Gets or sets the track's desired gain adjustment, in dB. To clear the track gain, set an empty
        /// <paramref name="value"/>.
        /// </summary>
        /// <value>The track gain.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        /// <exception cref="AudioMetadataInvalidException">Thrown if <paramref name="value"/> is not valid.
        /// </exception>
        [NotNull]
        public string TrackGain
        {
            get => _trackGain;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (string.IsNullOrEmpty(value))
                    _trackGain = string.Empty;
                else
                {
                    if (!double.TryParse(value, out var floatValue))
                        throw new AudioMetadataInvalidException("TrackGain must be numeric.");
                    _trackGain = floatValue.ToString("F2", CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// Gets or sets the album's desired gain adjustment, in dB. To clear the album gain, set an empty
        /// <paramref name="value"/>.
        /// </summary>
        /// <value>The album gain.</value>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        /// <exception cref="AudioMetadataInvalidException">Thrown if <paramref name="value"/> is not valid.
        /// </exception>
        [NotNull]
        public string AlbumGain
        {
            get => _albumGain;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (string.IsNullOrEmpty(value))
                    _albumGain = string.Empty;
                else
                {
                    if (!double.TryParse(value, out var floatValue))
                        throw new AudioMetadataInvalidException("AlbumGain must be numeric.");
                    _albumGain = floatValue.ToString("F2", CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// Clears all metadata properties by setting each to an empty string.
        /// </summary>
        public void Clear()
        {
            foreach (var property in GetType().GetProperties())
                property.SetValue(this, string.Empty);
        }
    }
}
