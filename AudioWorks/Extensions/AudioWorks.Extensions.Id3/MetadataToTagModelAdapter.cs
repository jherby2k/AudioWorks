using AudioWorks.Common;
using Id3Lib;
using Id3Lib.Frames;
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace AudioWorks.Extensions.Id3
{
    sealed class MetadataToTagModelAdapter : TagModel
    {
        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "TagHandler creation modifies the underlying TagModel")]
        internal MetadataToTagModelAdapter([NotNull] AudioMetadata metadata)
        {
            // TagHandler is a helper class for setting the most common frames
            new TagHandler(this)
            {
                Title = metadata.Title,
                Artist = metadata.Artist,
                Album = metadata.Album,
                Composer = metadata.Composer,
                Genre = metadata.Genre,
                Comment = metadata.Comment,
                Year = metadata.Year,
                Track = GetTrackText(metadata),
            };

            if (!string.IsNullOrEmpty(metadata.AlbumArtist))
                Add(new FrameText("TPE2") { Text = metadata.AlbumArtist });

            var tdatFrame = new TdatFrame(metadata);
            if (!string.IsNullOrEmpty(tdatFrame.Text))
                Add(new TdatFrame(metadata));

            if (!string.IsNullOrEmpty(metadata.TrackPeak))
                Add(new FrameTextUserDef("TXXX")
                {
                    Description = "REPLAYGAIN_TRACK_PEAK",
                    Text = metadata.TrackPeak,
                    FileAlter = true
                });

            if (!string.IsNullOrEmpty(metadata.AlbumPeak))
                Add(new FrameTextUserDef("TXXX")
                {
                    Description = "REPLAYGAIN_ALBUM_PEAK",
                    Text = metadata.AlbumPeak,
                    FileAlter = true
                });

            if (!string.IsNullOrEmpty(metadata.TrackGain))
                Add(new FrameTextUserDef("TXXX")
                {
                    Description = "REPLAYGAIN_TRACK_GAIN",
                    Text = $"{metadata.TrackGain} dB",
                    FileAlter = true
                });

            if (!string.IsNullOrEmpty(metadata.AlbumGain))
                Add(new FrameTextUserDef("TXXX")
                {
                    Description = "REPLAYGAIN_ALBUM_GAIN",
                    Text = $"{metadata.AlbumGain} dB",
                    FileAlter = true
                });
        }

        [Pure, NotNull]
        static string GetTrackText([NotNull] AudioMetadata metadata)
        {
            if (string.IsNullOrEmpty(metadata.TrackNumber))
                return string.Empty;
            return string.IsNullOrEmpty(metadata.TrackCount)
                ? metadata.TrackNumber
                : $"{metadata.TrackNumber}/{metadata.TrackCount}";
        }
    }
}