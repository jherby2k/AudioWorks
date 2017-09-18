using JetBrains.Annotations;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AudioWorks.Common
{
    [PublicAPI]
    public class AudioMetadata
    {
        [NotNull] string _title = string.Empty;
        [NotNull] string _artist = string.Empty;
        [NotNull] string _album = string.Empty;
        [NotNull] string _genre = string.Empty;
        [NotNull] string _comment = string.Empty;
        [NotNull] string _day = string.Empty;
        [NotNull] string _month = string.Empty;
        [NotNull] string _year = string.Empty;
        [NotNull] string _trackNumber = string.Empty;
        [NotNull] string _trackCount = string.Empty;

        [NotNull]
        public string Title
        {
            get => _title;
            set => _title = value ?? throw new ArgumentNullException(nameof(value));
        }

        [NotNull]
        public string Artist
        {
            get => _artist;
            set => _artist = value ?? throw new ArgumentNullException(nameof(value));
        }

        [NotNull]
        public string Album
        {
            get => _album;
            set => _album = value ?? throw new ArgumentNullException(nameof(value));
        }

        [NotNull]
        public string Genre
        {
            get => _genre;
            set => _genre = value ?? throw new ArgumentNullException(nameof(value));
        }

        [NotNull]
        public string Comment
        {
            get => _comment;
            set => _comment = value ?? throw new ArgumentNullException(nameof(value));
        }

        [NotNull]
        public string Day
        {
            get => _day;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (value == string.Empty)
                    _day = string.Empty;
                else
                {
                    if (!int.TryParse(value, out var intValue) || intValue < 1 || intValue > 31)
                        throw new ArgumentException("Month must be between 1 and 31.", nameof(value));
                    _day = intValue.ToString("00", CultureInfo.InvariantCulture);
                }
            }
        }

        [NotNull]
        public string Month
        {
            get => _month;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (value == string.Empty)
                    _day = string.Empty;
                else
                {
                    if (!int.TryParse(value, out var intValue) || intValue < 1 || intValue > 12)
                        throw new ArgumentException("Month must be between 1 and 12.", nameof(value));
                    _month = intValue.ToString("00", CultureInfo.InvariantCulture);
                }
            }
        }

        [NotNull]
        public string Year
        {
            get => _year;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (value == string.Empty)
                    _day = string.Empty;
                else
                {
                    if (!Regex.IsMatch(value, "^[1-9][0-9]{3}$"))
                        throw new ArgumentException("Year must be between 1000 and 9999.", nameof(value));
                    _year = value;
                }
            }
        }

        [NotNull]
        public string TrackNumber
        {
            get => _trackNumber;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (value == string.Empty)
                    _trackNumber = string.Empty;
                else
                {
                    if (!int.TryParse(value, out var intValue) || intValue < 1 || intValue > 99)
                        throw new ArgumentException("TrackNumber must be between 1 and 99.", nameof(value));
                    _trackNumber = intValue.ToString("00", CultureInfo.InvariantCulture);
                }
            }
        }

        [NotNull]
        public string TrackCount
        {
            get => _trackCount;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (value == string.Empty)
                    _trackCount = string.Empty;
                else
                {
                    if (!int.TryParse(value, out var intValue) || intValue < 1 || intValue > 99)
                        throw new ArgumentException("TrackCount must be between 1 and 99.", nameof(value));
                    _trackCount = intValue.ToString("00", CultureInfo.InvariantCulture);
                }
            }
        }
    }
}
