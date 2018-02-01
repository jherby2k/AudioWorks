using System;
using AudioWorks.Common;
using Id3Lib;
using Id3Lib.Frames;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Id3
{
    sealed class MetadataToTagModelAdapter : TagModel
    {
        internal MetadataToTagModelAdapter([NotNull] AudioMetadata metadata, [NotNull] string encoding)
        {
            AddTextFrame("TIT2", metadata.Title, encoding);
            AddTextFrame("TPE1", metadata.Artist, encoding);
            AddTextFrame("TALB", metadata.Album, encoding);
            AddTextFrame("TPE2", metadata.AlbumArtist, encoding);
            AddTextFrame("TCOM", metadata.Composer, encoding);
            AddTextFrame("TCON", metadata.Genre, encoding);
            AddFullTextFrame("COMM", metadata.Comment, "eng", encoding);
            AddTextFrame("TDAT", GetDateText(metadata), encoding);
            AddTextFrame("TYER", metadata.Year, encoding);
            AddTextFrame("TRCK", GetTrackText(metadata), encoding);

            // ReplayGain fields are always in Latin-1, encoding as per specification
            AddUserDefinedFrame("REPLAYGAIN_TRACK_PEAK", metadata.TrackPeak, "Latin1", true);
            AddUserDefinedFrame("REPLAYGAIN_ALBUM_PEAK", metadata.AlbumPeak, "Latin1", true);
            if (!string.IsNullOrEmpty(metadata.TrackGain))
                AddUserDefinedFrame("REPLAYGAIN_TRACK_GAIN", $"{metadata.TrackGain} dB", "Latin1", true);
            if (!string.IsNullOrEmpty(metadata.AlbumGain))
                AddUserDefinedFrame("REPLAYGAIN_ALBUM_GAIN", $"{metadata.AlbumGain} dB", "Latin1", true);
        }

        void AddTextFrame(
            [NotNull] string frameId,
            [NotNull] string value,
            [NotNull] string encoding)
        {
            if (!string.IsNullOrEmpty(value))
                Add(new FrameText(frameId)
                {
                    Text = value,
                    TextCode = string.Equals("Latin1", encoding, StringComparison.Ordinal)
                        ? TextCode.Ascii
                        : TextCode.Utf16
                });
        }

        void AddFullTextFrame(
            [NotNull] string frameId,
            [NotNull] string value,
            [NotNull] string language,
            [NotNull] string encoding)
        {
            if (!string.IsNullOrEmpty(value))
                Add(new FrameFullText(frameId)
                {
                    Text = value,
                    Language = language,
                    TextCode = string.Equals("Latin1", encoding, StringComparison.Ordinal)
                        ? TextCode.Ascii
                        : TextCode.Utf16
                });
        }

        void AddUserDefinedFrame(
            [NotNull] string description,
            [NotNull] string value,
            [NotNull] string encoding,
            bool fileAlter)
        {
            if (!string.IsNullOrEmpty(value))
                Add(new FrameTextUserDef("TXXX")
                {
                    Description = description,
                    Text = value,
                    TextCode = string.Equals("Latin1", encoding, StringComparison.Ordinal)
                        ? TextCode.Ascii
                        : TextCode.Utf16,
                    FileAlter = fileAlter
                });
        }

        [Pure, NotNull]
        static string GetDateText([NotNull] AudioMetadata metadata)
        {
            if (string.IsNullOrEmpty(metadata.Day) || string.IsNullOrEmpty(metadata.Month))
                return string.Empty;
            return metadata.Day + metadata.Month;
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