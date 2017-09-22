using AudioWorks.Common;
using Id3Lib;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Id3
{
    sealed class MetadataToTagModelAdapter : TagModel
    {
        internal MetadataToTagModelAdapter([NotNull] AudioMetadata metadata)
        {
            // TagHandler is a helper class for setting the most common frames
            // ReSharper disable once ObjectCreationAsStatement
            new TagHandler(this)
            {
                Title = metadata.Title,
                Artist = metadata.Artist,
                Album = metadata.Album,
                Genre = metadata.Genre,
                Comment = metadata.Comment,
                Year = metadata.Year,
                Track = GetTrackText(metadata)
            };

            // TDAT isn't supported by TagHandler
            var tdatFrame = new TdatFrame(metadata);
            if (!string.IsNullOrEmpty(tdatFrame.Text))
                Add(new TdatFrame(metadata));
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