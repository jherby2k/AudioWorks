using JetBrains.Annotations;
using System;

namespace AudioWorks.Common
{
    [PublicAPI]
    public sealed class AudioMetadata
    {
        [NotNull] string _title = string.Empty;
        [NotNull] string _artist = string.Empty;
        [NotNull] string _album = string.Empty;
        [NotNull] string _comment = string.Empty;

        [NotNull]
        public string Title
        {
            get => _title;
            set => _title = value ?? throw new ArgumentNullException();
        }

        [NotNull]
        public string Artist
        {
            get => _artist;
            set => _artist = value ?? throw new ArgumentNullException();
        }

        [NotNull]
        public string Album
        {
            get => _album;
            set => _album = value ?? throw new ArgumentNullException();
        }

        [NotNull]
        public string Comment
        {
            get => _comment;
            set => _comment = value ?? throw new ArgumentNullException();
        }
    }
}
