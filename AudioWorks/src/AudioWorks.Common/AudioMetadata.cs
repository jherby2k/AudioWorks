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
        [NotNull] string _title;
        [NotNull] string _artist;
        [NotNull] string _album;
        [NotNull] string _albumArtist;
        [NotNull] string _composer;
        [NotNull] string _genre;
        [NotNull] string _comment;
        [NotNull] string _day;
        [NotNull] string _month;
        [NotNull] string _year;
        [NotNull] string _trackNumber;
        [NotNull] string _trackCount;
        [NotNull] string _trackPeak;
        [NotNull] string _albumPeak;
        [NotNull] string _trackGain;
        [NotNull] string _albumGain;

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
        /// Gets or sets the cover art. To clear the cover art, set a null <paramref name="value"/>.
        /// </summary>
        /// <value>The cover art.</value>
        [CanBeNull]
        public ICoverArt CoverArt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMetadata"/> class.
        /// </summary>
        public AudioMetadata()
        {
            _title = string.Empty;
            _artist = string.Empty;
            _album = string.Empty;
            _albumArtist = string.Empty;
            _composer = string.Empty;
            _genre = string.Empty;
            _comment = string.Empty;
            _day = string.Empty;
            _month = string.Empty;
            _year = string.Empty;
            _trackNumber = string.Empty;
            _trackCount = string.Empty;
            _trackPeak = string.Empty;
            _albumPeak = string.Empty;
            _trackGain = string.Empty;
            _albumGain = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMetadata"/> class by copying an existing
        /// <see cref="AudioMetadata"/> object.
        /// </summary>
        /// <param name="metadata">The <see cref="AudioMetadata"/> object to copy.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="metadata"/> is null.</exception>
        public AudioMetadata([NotNull] AudioMetadata metadata)
        {
            if (metadata == null) throw new ArgumentNullException(nameof(metadata));

            _title = metadata.Title;
            _artist = metadata.Artist;
            _album = metadata.Album;
            _albumArtist = metadata.AlbumArtist;
            _composer = metadata.Composer;
            _genre = metadata.Genre;
            _comment = metadata.Comment;
            _day = metadata.Day;
            _month = metadata.Month;
            _year = metadata.Year;
            _trackNumber = metadata.TrackNumber;
            _trackCount = metadata.TrackCount;
            _trackPeak = metadata.TrackPeak;
            _albumPeak = metadata.AlbumPeak;
            _trackGain = metadata.TrackGain;
            _albumGain = metadata.AlbumGain;
            CoverArt = metadata.CoverArt;
        }

        /// <summary>
        /// Clears all properties.
        /// </summary>
        public void Clear()
        {
            foreach (var property in GetType().GetProperties())
                property.SetValue(this, property.PropertyType == typeof(string) ? string.Empty : null);
        }
    }
}
