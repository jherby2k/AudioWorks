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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
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

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void HeaderOut(out OggPacket packet) => LibVorbis.CommentHeaderOut(_comment, out packet);

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void HeaderOut(
            IntPtr dspState,
            out OggPacket first,
            out OggPacket second,
            out OggPacket third) =>
            LibVorbis.AnalysisHeaderOut(dspState, _comment, out first, out second, out third);

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        unsafe void AddTag(string key, string value)
        {
            // Optimization - avoid allocating on the heap
            Span<byte> keyBytes = stackalloc byte[Encoding.ASCII.GetMaxByteCount(key.Length) + 1];
            var keyLength = Encoding.ASCII.GetBytes(key, keyBytes);

            // Use heap allocations for comments > 256kB
            var valueMaxByteCount = Encoding.UTF8.GetMaxByteCount(value.Length) + 1;
            var valueBytes = valueMaxByteCount < 0x40000
                ? stackalloc byte[valueMaxByteCount]
                : new byte[valueMaxByteCount];
            var valueLength = Encoding.UTF8.GetBytes(value, valueBytes);

            // Since SkipLocalsInit is set, make sure the strings are null-terminated
            keyBytes[keyLength] = 0;
            valueBytes[valueLength] = 0;

            fixed (byte* valueBytesAddress = valueBytes)
                LibVorbis.CommentAddTag(_comment, ref MemoryMarshal.GetReference(keyBytes),
                    valueBytesAddress);

            _unmanagedMemoryAllocated = true;
        }

        unsafe void AddTag(string key, ReadOnlySpan<byte> value)
        {
            // Optimization - avoid allocating on the heap
            Span<byte> keyBytes = stackalloc byte[Encoding.ASCII.GetMaxByteCount(key.Length) + 1];
            var keyLength = Encoding.ASCII.GetBytes(key, keyBytes);

            // Since SkipLocalsInit is set, make sure the key is null-terminated
            keyBytes[keyLength] = 0;

            fixed (byte* valueAddress = value)
                LibVorbis.CommentAddTag(_comment, ref MemoryMarshal.GetReference(keyBytes),
                    valueAddress);

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
