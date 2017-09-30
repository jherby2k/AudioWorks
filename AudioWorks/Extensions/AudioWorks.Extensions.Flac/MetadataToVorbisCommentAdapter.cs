using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    sealed class MetadataToVorbisCommentAdapter : NativeVorbisCommentBlock
    {
        internal MetadataToVorbisCommentAdapter([NotNull] AudioMetadata metadata)
        {
            if (!string.IsNullOrEmpty(metadata.Title))
                Append("TITLE", metadata.Title);
            if (!string.IsNullOrEmpty(metadata.Artist))
                Append("ARTIST", metadata.Artist);
            if (!string.IsNullOrEmpty(metadata.Album))
                Append("ALBUM", metadata.Album);
            if (!string.IsNullOrEmpty(metadata.Genre))
                Append("GENRE", metadata.Genre);
            if (!string.IsNullOrEmpty(metadata.Comment))
                Append("DESCRIPTION", metadata.Comment);

            if (!string.IsNullOrEmpty(metadata.Day) &&
                !string.IsNullOrEmpty(metadata.Month) &&
                !string.IsNullOrEmpty(metadata.Year))
                Append("DATE", $"{metadata.Year}-{metadata.Month}-{metadata.Day}");
            else if (!string.IsNullOrEmpty(metadata.Year))
                Append("YEAR", metadata.Year);

            if (!string.IsNullOrEmpty(metadata.TrackNumber))
                Append("TRACKNUMBER", !string.IsNullOrEmpty(metadata.TrackCount)
                    ? $"{metadata.TrackNumber}/{metadata.TrackCount}"
                    : metadata.TrackNumber);
        }
    }
}