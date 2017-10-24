using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Text;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class MetadataToVorbisCommentAdapter : IDisposable
    {
        VorbisComment _comment;

        internal MetadataToVorbisCommentAdapter([NotNull] AudioMetadata metadata)
        {
            SafeNativeMethods.VorbisCommentInitialize(out _comment);

            if (!string.IsNullOrEmpty(metadata.Title))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("TITLE"), Encode(metadata.Title));
            if (!string.IsNullOrEmpty(metadata.Artist))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("ARTIST"), Encode(metadata.Artist));
            if (!string.IsNullOrEmpty(metadata.Album))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("ALBUM"), Encode(metadata.Album));
            if (!string.IsNullOrEmpty(metadata.AlbumArtist))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("ALBUMARTIST"), Encode(metadata.AlbumArtist));
            if (!string.IsNullOrEmpty(metadata.Composer))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("COMPOSER"), Encode(metadata.Composer));
            if (!string.IsNullOrEmpty(metadata.Genre))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("GENRE"), Encode(metadata.Genre));
            if (!string.IsNullOrEmpty("Comment"))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("DESCRIPTION"), Encode(metadata.Comment));

            if (!string.IsNullOrEmpty(metadata.Day) &&
                !string.IsNullOrEmpty(metadata.Month) &&
                !string.IsNullOrEmpty(metadata.Year))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("DATE"), Encode($"{metadata.Year}-{metadata.Month}-{metadata.Day}"));
            else if (!string.IsNullOrEmpty(metadata.Year))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("YEAR"), Encode(metadata.Year));

            if (!string.IsNullOrEmpty(metadata.TrackNumber))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("TRACKNUMBER"), Encode(!string.IsNullOrEmpty(metadata.TrackCount)
                        ? $"{metadata.TrackNumber}/{metadata.TrackCount}"
                        : metadata.TrackNumber));

            if (!string.IsNullOrEmpty(metadata.TrackPeak))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("REPLAYGAIN_TRACK_PEAK"), Encode(metadata.TrackPeak));
            if (!string.IsNullOrEmpty(metadata.AlbumPeak))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("REPLAYGAIN_ALBUM_PEAK"), Encode(metadata.AlbumPeak));
            if (!string.IsNullOrEmpty(metadata.TrackGain))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("REPLAYGAIN_TRACK_GAIN"), Encode($"{metadata.TrackGain} dB"));
            if (!string.IsNullOrEmpty(metadata.AlbumGain))
                SafeNativeMethods.VorbisCommentAddTag(ref _comment,
                    Encode("REPLAYGAIN_ALBUM_GAIN"), Encode($"{metadata.AlbumGain} dB"));
        }

        internal void HeaderOut(out OggPacket packet)
        {
            SafeNativeMethods.VorbisCommentHeaderOut(ref _comment, out packet);
        }

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void FreeUnmanaged()
        {
            SafeNativeMethods.VorbisCommentClear(ref _comment);
        }

        [Pure, NotNull]
        static byte[] Encode([NotNull] string text)
        {
            // Convert to null-terminated UTF-8 strings
            var keyBytes = new byte[Encoding.UTF8.GetByteCount(text) + 1];
            Encoding.UTF8.GetBytes(text, 0, text.Length, keyBytes, 0);
            return keyBytes;
        }

        ~MetadataToVorbisCommentAdapter()
        {
            FreeUnmanaged();
        }
    }
}
