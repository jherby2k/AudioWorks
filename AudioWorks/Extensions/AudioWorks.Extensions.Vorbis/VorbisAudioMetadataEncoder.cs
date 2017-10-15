using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Composition;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace AudioWorks.Extensions.Vorbis
{
    [Shared]
    [AudioMetadataEncoderExport(".ogg")]
    public sealed class VorbisAudioMetadataEncoder : IAudioMetadataEncoder
    {
        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary();

        public void WriteMetadata(FileStream stream, AudioMetadata metadata, SettingDictionary settings)
        {
            // This buffer is used for both reading and writing:
            var buffer = new byte[4096];

            using (var tempStream = new MemoryStream())
            {
                NativeOggStream inputOggStream = null;
                NativeOggStream outputOggStream = null;

                try
                {
                    using (var sync = new NativeOggSync())
                    {
                        OggPage inPage;

                        do
                        {
                            // Read from the buffer into a page
                            while (!sync.PageOut(out inPage))
                            {
                                var bytesRead = stream.Read(buffer, 0, buffer.Length);
                                var nativeBuffer = sync.Buffer(bytesRead);
                                Marshal.Copy(buffer, 0, nativeBuffer, bytesRead);
                                sync.Wrote(bytesRead);
                            }

                            if (inputOggStream == null)
                                inputOggStream = new NativeOggStream(SafeNativeMethods.OggPageGetSerialNumber(ref inPage));
                            if (outputOggStream == null)
                                outputOggStream = new NativeOggStream(inputOggStream.SerialNumber);

                            inputOggStream.PageIn(ref inPage);

                            while (inputOggStream.PacketOut(out var packet))
                            {
                                // Substitute the new comment packet
                                if (packet.PacketNumber == 1)
                                {
                                    var commentPacket = GetCommentPacket(metadata);
                                    outputOggStream.PacketIn(ref commentPacket);
                                }
                                else
                                    outputOggStream.PacketIn(ref packet);

                                // Page out each packet, flushing at the end of the header
                                if (packet.PacketNumber == 2)
                                    while (outputOggStream.Flush(out var outPage))
                                        WritePage(outPage, tempStream, buffer);
                                else
                                    while (outputOggStream.PageOut(out var outPage))
                                        WritePage(outPage, tempStream, buffer);
                            }
                        } while (!SafeNativeMethods.OggPageEndOfStream(ref inPage));

                        // Once the end of the input is reached, overwrite the original file and return
                        Overwrite(stream, tempStream);
                    }
                }
                finally
                {
                    inputOggStream?.Dispose();
                    outputOggStream?.Dispose();
                }
            }
        }

        static OggPacket GetCommentPacket([NotNull] AudioMetadata metadata)
        {
            var comment = new VorbisComment();
            try
            {
                SafeNativeMethods.VorbisCommentInitialize(out comment);

                if (!string.IsNullOrEmpty("Title"))
                    SafeNativeMethods.VorbisCommentAddTag(ref comment, Encode("TITLE"), Encode(metadata.Title));
                if (!string.IsNullOrEmpty("Artist"))
                    SafeNativeMethods.VorbisCommentAddTag(ref comment, Encode("ARTIST"), Encode(metadata.Artist));
                if (!string.IsNullOrEmpty("Album"))
                    SafeNativeMethods.VorbisCommentAddTag(ref comment, Encode("ALBUM"), Encode(metadata.Album));
                if (!string.IsNullOrEmpty("Genre"))
                    SafeNativeMethods.VorbisCommentAddTag(ref comment, Encode("GENRE"), Encode(metadata.Album));
                if (!string.IsNullOrEmpty("Comment"))
                    SafeNativeMethods.VorbisCommentAddTag(ref comment, Encode("DESCRIPTION"), Encode(metadata.Album));

                if (!string.IsNullOrEmpty(metadata.Day) &&
                    !string.IsNullOrEmpty(metadata.Month) &&
                    !string.IsNullOrEmpty(metadata.Year))
                    SafeNativeMethods.VorbisCommentAddTag(ref comment, Encode("DATE"),
                        Encode($"{metadata.Year}-{metadata.Month}-{metadata.Day}"));
                else if (!string.IsNullOrEmpty(metadata.Year))
                    SafeNativeMethods.VorbisCommentAddTag(ref comment, Encode("YEAR"), Encode(metadata.Year));

                if (!string.IsNullOrEmpty(metadata.TrackNumber))
                    SafeNativeMethods.VorbisCommentAddTag(ref comment, Encode("TRACKNUMBER"),
                        Encode(!string.IsNullOrEmpty(metadata.TrackCount)
                            ? $"{metadata.TrackNumber}/{metadata.TrackCount}"
                            : metadata.TrackNumber));

                SafeNativeMethods.VorbisCommentHeaderOut(ref comment, out var result);

                return result;
            }
            finally
            {
                SafeNativeMethods.VorbisCommentClear(ref comment);
            }
        }

        [Pure, NotNull]
        static byte[] Encode([NotNull] string text)
        {
            // Convert to null-terminated UTF-8 strings
            var keyBytes = new byte[Encoding.UTF8.GetByteCount(text) + 1];
            Encoding.UTF8.GetBytes(text, 0, text.Length, keyBytes, 0);
            return keyBytes;
        }

        static void WritePage(OggPage page, [NotNull] Stream stream, [NotNull] byte[] buffer)
        {
            WritePointer(page.Header, page.HeaderLength, stream, buffer);
            WritePointer(page.Body, page.BodyLength, stream, buffer);
        }

        static void WritePointer(IntPtr location, int length, [NotNull] Stream stream, [NotNull] byte[] buffer)
        {
            var offset = 0;
            while (offset < length)
            {
                var bytesCopied = Math.Min(length - offset, buffer.Length);
                Marshal.Copy(IntPtr.Add(location, offset), buffer, 0, bytesCopied);
                stream.Write(buffer, 0, bytesCopied);
                offset += bytesCopied;
            }
        }

        static void Overwrite([NotNull] Stream originalStream, [NotNull] Stream newStream)
        {
            originalStream.SetLength(newStream.Length);
            originalStream.Position = 0;
            newStream.Position = 0;
            newStream.CopyTo(originalStream);
        }
    }
}
