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

using AudioWorks.Common;

namespace AudioWorks.Extensions.Id3
{
    sealed class MetadataToTagModelAdapter : TagModel
    {
        internal MetadataToTagModelAdapter(AudioMetadata metadata, int version, string encoding)
        {
            Header.Version = (byte) version;

            var textType = encoding switch
            {
                "UTF16" => TextType.Utf16,
                "UTF8" => TextType.Utf8,
                _ => TextType.Ascii
            };

            AddTextFrame("TIT2", metadata.Title, textType);
            AddTextFrame("TPE1", metadata.Artist, textType);
            AddTextFrame("TALB", metadata.Album, textType);
            AddTextFrame("TPE2", metadata.AlbumArtist, textType);
            AddTextFrame("TCOM", metadata.Composer, textType);
            AddTextFrame("TCON", metadata.Genre, textType);
            AddTextFrame("COMM", metadata.Comment, textType);

            if (version == 3)
            {
                AddTextFrame("TDAT", GetDateText(metadata), textType);
                AddTextFrame("TYER", metadata.Year, textType);
            }
            else
                AddTextFrame("TDRC", GetTimeStamp(metadata), textType);

            AddTextFrame("TRCK", GetTrackText(metadata), textType);

            AddReplayGainFrame(metadata.TrackPeak, "REPLAYGAIN_TRACK_PEAK");
            AddReplayGainFrame(metadata.AlbumPeak, "REPLAYGAIN_ALBUM_PEAK");
            AddReplayGainFormattedFrame(metadata.TrackGain, "REPLAYGAIN_TRACK_GAIN");
            AddReplayGainFormattedFrame(metadata.AlbumGain, "REPLAYGAIN_ALBUM_GAIN");

            if (metadata.CoverArt == null) return;

            // Always store images in JPEG format, since MP3 is also lossy
            var lossyCoverArt = CoverArtFactory.ConvertToLossy(metadata.CoverArt);
            Frames.Add(new FramePicture
            {
                PictureType = PictureType.CoverFront,
                Mime = lossyCoverArt.MimeType,
                PictureData = lossyCoverArt.Data.ToArray()
            });
        }

        void AddReplayGainFormattedFrame(string value, string description)
        {
            if (!string.IsNullOrEmpty(value))
                AddReplayGainFrame($"{value} dB", description);
        }

        void AddReplayGainFrame(string value, string description) => AddTextFrame("TXXX", value, TextType.Ascii, description, true);

        void AddTextFrame(string frameId, string value, TextType textType = TextType.Ascii, string? description = null, bool fileAlter = false)
        {
            if (string.IsNullOrEmpty(value)) return;

            var frame = (FrameText) FrameFactory.Build(frameId);
            frame.Text = value;
            frame.TextType = textType;
            frame.FileAlter = fileAlter;

            if (description != null)
                if (frame is IFrameDescription frameDescription)
                    frameDescription.Description = description;

            Frames.Add(frame);
        }

        static string GetDateText(AudioMetadata metadata)
        {
            if (string.IsNullOrEmpty(metadata.Day) || string.IsNullOrEmpty(metadata.Month))
                return string.Empty;
            return metadata.Day + metadata.Month;
        }

        static string GetTimeStamp(AudioMetadata metadata)
        {
            if (string.IsNullOrEmpty(metadata.Year)) return string.Empty;

            if (string.IsNullOrEmpty(metadata.Month)) return metadata.Year;

            return string.IsNullOrEmpty(metadata.Day)
                ? $"{metadata.Year}-{metadata.Month}"
                : $"{metadata.Year}-{metadata.Month}-{metadata.Day}";
        }

        static string GetTrackText(AudioMetadata metadata)
        {
            if (string.IsNullOrEmpty(metadata.TrackNumber))
                return string.Empty;
            return string.IsNullOrEmpty(metadata.TrackCount)
                ? metadata.TrackNumber
                : $"{metadata.TrackNumber}/{metadata.TrackCount}";
        }
    }
}