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
using AudioWorks.Common;

namespace AudioWorks.Extensions.Id3
{
    sealed class MetadataToTagModelAdapter : TagModel
    {
        internal MetadataToTagModelAdapter(AudioMetadata metadata, string encoding)
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

            if (metadata.CoverArt == null) return;

            // Always store images in JPEG format since MP3 is also lossy
            var lossyCoverArt = CoverArtFactory.ConvertToLossy(metadata.CoverArt);
            Add(new FramePicture("APIC")
            {
                PictureType = PictureType.CoverFront,
                Mime = lossyCoverArt.MimeType,
                PictureData = lossyCoverArt.Data.ToArray()
            });
        }

        void AddTextFrame(string frameId, string value, string encoding)
        {
            if (!string.IsNullOrEmpty(value))
                Add(new FrameText(frameId)
                {
                    Text = value,
                    TextType = GetTextType(encoding)
                });
        }

        void AddFullTextFrame(string frameId, string value, string language, string encoding)
        {
            if (!string.IsNullOrEmpty(value))
                Add(new FrameFullText(frameId)
                {
                    Text = value,
                    Language = language,
                    TextType = GetTextType(encoding)
                });
        }

        void AddUserDefinedFrame(string description, string value, string encoding, bool fileAlter)
        {
            if (!string.IsNullOrEmpty(value))
                Add(new FrameTextUserDef("TXXX")
                {
                    Description = description,
                    Text = value,
                    TextType = GetTextType(encoding),
                    FileAlter = fileAlter
                });
        }

        static string GetDateText(AudioMetadata metadata)
        {
            if (string.IsNullOrEmpty(metadata.Day) || string.IsNullOrEmpty(metadata.Month))
                return string.Empty;
            return metadata.Day + metadata.Month;
        }

        static string GetTrackText(AudioMetadata metadata)
        {
            if (string.IsNullOrEmpty(metadata.TrackNumber))
                return string.Empty;
            return string.IsNullOrEmpty(metadata.TrackCount)
                ? metadata.TrackNumber
                : $"{metadata.TrackNumber}/{metadata.TrackCount}";
        }

        static TextType GetTextType(string encoding) =>
            encoding.Equals("Latin1", StringComparison.Ordinal)
                ? TextType.Ascii
                : TextType.Utf16;
    }
}