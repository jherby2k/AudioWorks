using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class MetadataToVorbisCommentAdapter : IDisposable
    {
        VorbisComment _comment;
        bool _unmanagedMemoryAllocated;

        internal MetadataToVorbisCommentAdapter([NotNull] AudioMetadata metadata)
        {
            SafeNativeMethods.VorbisCommentInit(out _comment);

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
                AddTag("METADATA_BLOCK_PICTURE", Encode(CoverArtFactory.ConvertToLossy(metadata.CoverArt)));
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void HeaderOut(out OggPacket packet)
        {
            SafeNativeMethods.VorbisCommentHeaderOut(ref _comment, out packet);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void HeaderOut(
            IntPtr dspState,
            out OggPacket first,
            out OggPacket second,
            out OggPacket third)
        {
            SafeNativeMethods.VorbisAnalysisHeaderOut(dspState, ref _comment, out first, out second, out third);
        }

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void AddTag([NotNull] string tag, [NotNull] string contents)
        {
            SafeNativeMethods.VorbisCommentAddTag(ref _comment, Encode(tag), Encode(contents));
            _unmanagedMemoryAllocated = true;
        }

        [Pure, NotNull]
        static byte[] Encode([NotNull] string text)
        {
            // Convert to null-terminated UTF-8 strings
            var keyBytes = new byte[Encoding.UTF8.GetByteCount(text) + 1];
            Encoding.UTF8.GetBytes(text, 0, text.Length, keyBytes, 0);
            return keyBytes;
        }

        [Pure, NotNull]
        static string Encode([NotNull] ICoverArt coverArt)
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                // Set the picture type as "Front Cover"
                writer.WriteBigEndian(3);

                var mimeBytes = Encoding.ASCII.GetBytes(coverArt.MimeType);
                writer.WriteBigEndian((uint)mimeBytes.Length);
                writer.Write(mimeBytes);

                var descriptionBytes = Encoding.UTF8.GetBytes(string.Empty);
                writer.WriteBigEndian((uint)descriptionBytes.Length);
                writer.Write(descriptionBytes);

                writer.WriteBigEndian((uint)coverArt.Width);
                writer.WriteBigEndian((uint)coverArt.Height);
                writer.WriteBigEndian((uint)coverArt.ColorDepth);
                writer.WriteBigEndian(0); // Always 0 for PNG and JPEG

                var data = coverArt.GetData();
                writer.WriteBigEndian((uint)data.Length);
                writer.Write(data);

                return Convert.ToBase64String(stream.ToArray());
            }
        }

        void FreeUnmanaged()
        {
            if (_unmanagedMemoryAllocated)
                SafeNativeMethods.VorbisCommentClear(ref _comment);
        }

        ~MetadataToVorbisCommentAdapter()
        {
            FreeUnmanaged();
        }
    }
}
