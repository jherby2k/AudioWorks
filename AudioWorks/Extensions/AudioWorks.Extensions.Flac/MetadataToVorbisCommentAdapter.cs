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
            if (!string.IsNullOrEmpty(metadata.AlbumArtist))
                Append("ALBUMARTIST", metadata.AlbumArtist);
            if (!string.IsNullOrEmpty(metadata.Composer))
                Append("COMPOSER", metadata.Composer);
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

            if (!string.IsNullOrEmpty(metadata.TrackPeak))
                Append("REPLAYGAIN_TRACK_PEAK", metadata.TrackPeak);
            if (!string.IsNullOrEmpty(metadata.AlbumPeak))
                Append("REPLAYGAIN_ALBUM_PEAK", metadata.AlbumPeak);
            if (!string.IsNullOrEmpty(metadata.TrackGain))
                Append("REPLAYGAIN_TRACK_GAIN", $"{metadata.TrackGain} dB");
            if (!string.IsNullOrEmpty(metadata.AlbumGain))
                Append("REPLAYGAIN_ALBUM_GAIN", $"{metadata.AlbumGain} dB");
        }
    }
}