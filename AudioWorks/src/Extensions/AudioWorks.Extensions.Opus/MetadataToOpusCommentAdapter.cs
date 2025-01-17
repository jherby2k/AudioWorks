/* Copyright © 2019 Jeremy Herbison

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
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Opus
{
    sealed class MetadataToOpusCommentAdapter : IDisposable
    {
        internal OpusCommentsHandle Handle { get; }

        internal MetadataToOpusCommentAdapter(AudioMetadata metadata)
        {
            Handle = LibOpusEnc.CommentsCreate();

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

            if (!string.IsNullOrEmpty(metadata.TrackGain))
                AddTag("R128_TRACK_GAIN", ConvertGain(metadata.TrackGain));
            if (!string.IsNullOrEmpty(metadata.AlbumGain))
                AddTag("R128_ALBUM_GAIN", ConvertGain(metadata.AlbumGain));

            if (metadata.CoverArt == null) return;

            // Always store images in JPEG format since Vorbis is also lossy
            var coverArt = CoverArtFactory.ConvertToLossy(metadata.CoverArt);

            var error = LibOpusEnc.CommentsAddPictureFromMemory(
                Handle,
                coverArt.Data,
                new(coverArt.Data.Length),
                -1,
                IntPtr.Zero);
            if (error != 0)
                throw new AudioEncodingException($"Opus encountered error {error} writing the cover art.");
        }

        internal unsafe void HeaderOut(out OggPacket packet) =>
            packet = new()
            {
                Packet = Marshal.ReadIntPtr(Handle.DangerousGetHandle()),
                Bytes = new(Marshal.ReadInt32(IntPtr.Add(Handle.DangerousGetHandle(), sizeof(IntPtr)))),
                PacketNumber = 1
            };

        public void Dispose() => Handle.Dispose();

        unsafe void AddTag(string key, string value)
        {
            // Optimization - avoid allocating on the heap
            Span<byte> keyBytes = stackalloc byte[Encoding.ASCII.GetMaxByteCount(key.Length) + 1];
            var keyLength = Encoding.ASCII.GetBytes(key, keyBytes);

            // Use heap allocations for comments > 256kB (usually pictures)
            var valueMaxByteCount = Encoding.UTF8.GetMaxByteCount(value.Length) + 1;
            var valueBytes = valueMaxByteCount < 0x40000
                ? stackalloc byte[valueMaxByteCount]
                : new byte[valueMaxByteCount];
            var valueLength = Encoding.UTF8.GetBytes(value, valueBytes);

            // Since SkipLocalsInit is set, make sure the strings are null-terminated
            keyBytes[keyLength] = 0;
            valueBytes[valueLength] = 0;

            fixed (byte* valueBytesAddress = valueBytes)
            {
                var error = LibOpusEnc.CommentsAdd(Handle, ref MemoryMarshal.GetReference(keyBytes),
                    valueBytesAddress);
                if (error != 0)
                    throw new AudioEncodingException($"Opus encountered error {error} writing a comment.");
            }
        }

        static string ConvertGain(string gain) =>
            Math.Round((double.Parse(gain, CultureInfo.InvariantCulture) - 5) * 256)
                .ToString("F0", CultureInfo.InvariantCulture);
    }
}
