﻿/* Copyright © 2018 Jeremy Herbison

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

namespace AudioWorks.Extensions.Vorbis
{
    sealed class MetadataToVorbisCommentAdapter : IDisposable
    {
        VorbisComment _comment;
        bool _unmanagedMemoryAllocated;

        internal MetadataToVorbisCommentAdapter(AudioMetadata metadata)
        {
            LibVorbis.CommentInit(out _comment);

            if (!string.IsNullOrEmpty(metadata.Title))
                AddTag("TITLE", metadata.Title);
            if (!string.IsNullOrEmpty(metadata.Artist))
                AddTag("ARTIST", metadata.Artist);
            if (!string.IsNullOrEmpty(metadata.Album))
                AddTag("ALBUM", metadata.Album);
            if (!string.IsNullOrEmpty(metadata.AlbumArtist))
                AddTag("ALBUMARTIST", metadata.AlbumArtist);
            if (!string.IsNullOrEmpty(metadata.Composer))
                AddTag("COMPOSER", metadata.Composer);
            if (!string.IsNullOrEmpty(metadata.Genre))
                AddTag("GENRE", metadata.Genre);
            if (!string.IsNullOrEmpty(metadata.Comment))
                AddTag("DESCRIPTION", metadata.Comment);

            if (!string.IsNullOrEmpty(metadata.Day) &&
                !string.IsNullOrEmpty(metadata.Month) &&
                !string.IsNullOrEmpty(metadata.Year))
                AddTag("DATE", $"{metadata.Year}-{metadata.Month}-{metadata.Day}");
            else if (!string.IsNullOrEmpty(metadata.Year))
                AddTag("YEAR", metadata.Year);

            if (!string.IsNullOrEmpty(metadata.TrackNumber))
                AddTag("TRACKNUMBER", !string.IsNullOrEmpty(metadata.TrackCount)
                    ? $"{metadata.TrackNumber}/{metadata.TrackCount}"
                    : metadata.TrackNumber);

            if (!string.IsNullOrEmpty(metadata.TrackPeak))
                AddTag("REPLAYGAIN_TRACK_PEAK", metadata.TrackPeak);
            if (!string.IsNullOrEmpty(metadata.AlbumPeak))
                AddTag("REPLAYGAIN_ALBUM_PEAK", metadata.AlbumPeak);
            if (!string.IsNullOrEmpty(metadata.TrackGain))
                AddTag("REPLAYGAIN_TRACK_GAIN", $"{metadata.TrackGain} dB");
            if (!string.IsNullOrEmpty(metadata.AlbumGain))
                AddTag("REPLAYGAIN_ALBUM_GAIN", $"{metadata.AlbumGain} dB");

            // Always store images in JPEG format since Vorbis is also lossy
            if (metadata.CoverArt != null)
                AddTag("METADATA_BLOCK_PICTURE", CoverArtAdapter.ToBase64(
                    CoverArtFactory.ConvertToLossy(metadata.CoverArt)));
        }

        internal void HeaderOut(out OggPacket packet) => _ = LibVorbis.CommentHeaderOut(_comment, out packet);

        internal void HeaderOut(
            nint dspState,
            out OggPacket first,
            out OggPacket second,
            out OggPacket third) =>
            _ = LibVorbis.AnalysisHeaderOut(dspState, _comment, out first, out second, out third);

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void AddTag(string key, string value)
        {
            LibVorbis.CommentAddTag(_comment, key, value);
            _unmanagedMemoryAllocated = true;
        }


        unsafe void AddTag(string key, ReadOnlySpan<byte> value)
        {
            fixed (byte* valueAddress = value)
                LibVorbis.CommentAddTag(_comment, key, valueAddress);
            _unmanagedMemoryAllocated = true;
        }

        void FreeUnmanaged()
        {
            if (_unmanagedMemoryAllocated)
                LibVorbis.CommentClear(ref _comment);
        }

        ~MetadataToVorbisCommentAdapter() => FreeUnmanaged();
    }
}
