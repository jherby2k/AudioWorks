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
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    sealed class MetadataToVorbisCommentAdapter : VorbisCommentBlock
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