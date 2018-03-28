using System;
using System.Buffers;
using System.Buffers.Binary;
using System.IO;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    [AudioMetadataEncoderExport(".ogg")]
    public sealed class VorbisAudioMetadataEncoder : IAudioMetadataEncoder
    {
        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary();

        public void WriteMetadata(FileStream stream, AudioMetadata metadata, SettingDictionary settings)
        {
            // This buffer is used for both reading and writing:
            var buffer = ArrayPool<byte>.Shared.Rent(4096);

            using (var tempStream = new MemoryStream())
            {
                OggStream inputOggStream = null;
                OggStream outputOggStream = null;

                try
                {
                    using (var sync = new OggSync())
                    {
                        var headerWritten = false;
                        OggPage page;
                        var pagesWritten = 0u;

                        do
                        {
                            // Read from the buffer into a page
                            while (!sync.PageOut(out page))
                            {
                                var nativeBuffer = sync.Buffer(buffer.Length);
                                var bytesRead = stream.Read(buffer, 0, buffer.Length);
                                Marshal.Copy(buffer, 0, nativeBuffer, bytesRead);
                                sync.Wrote(bytesRead);
                            }

                            if (inputOggStream == null)
                                inputOggStream = new OggStream(SafeNativeMethods.OggPageSerialNo(ref page));
                            if (outputOggStream == null)
                                outputOggStream = new OggStream(inputOggStream.SerialNumber);

                            // Write new header page(s) using a modified comment packet
                            if (!headerWritten)
                            {
                                inputOggStream.PageIn(ref page);

                                while (inputOggStream.PacketOut(out var packet))
                                    switch (packet.PacketNumber)
                                    {
                                        case 0:
                                            outputOggStream.PacketIn(ref packet);
                                            break;

                                        // Substitute the new comment packet
                                        case 1:
                                            using (var adapter = new MetadataToVorbisCommentAdapter(metadata))
                                            {
                                                adapter.HeaderOut(out var commentPacket);
                                                outputOggStream.PacketIn(ref commentPacket);
                                            }

                                            break;

                                        // Flush at the end of the header
                                        case 2:
                                            outputOggStream.PacketIn(ref packet);
                                            while (outputOggStream.Flush(out var outPage))
                                            {
                                                WritePage(ref outPage, tempStream, buffer);
                                                pagesWritten++;
                                            }

                                            headerWritten = true;
                                            break;

                                        default:
                                            throw new AudioInvalidException("Missing header packet.");
                                    }
                            }
                            else
                            {
                                // Copy the existing data pages verbatim, with updated sequence numbers
                                UpdateSequenceNumber(ref page, pagesWritten);
                                WritePage(ref page, tempStream, buffer);
                                pagesWritten++;
                            }

                        } while (!SafeNativeMethods.OggPageEos(ref page));

                        // Once the end of the input is reached, overwrite the original file and return
                        stream.Position = 0;
                        stream.SetLength(tempStream.Length);
                        tempStream.WriteTo(stream);
                    }
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(buffer);
                    inputOggStream?.Dispose();
                    outputOggStream?.Dispose();
                }
            }
        }

        static void WritePage(ref OggPage page, [NotNull] Stream stream, [NotNull] byte[] buffer)
        {
#if (WINDOWS)
            WriteFromUnmanaged(page.Header, page.HeaderLength, stream, buffer);
            WriteFromUnmanaged(page.Body, page.BodyLength, stream, buffer);
#else
            WriteFromUnmanaged(page.Header, (int) page.HeaderLength, stream, buffer);
            WriteFromUnmanaged(page.Body, (int) page.BodyLength, stream, buffer);
#endif
        }

        static void WriteFromUnmanaged(IntPtr location, int length, [NotNull] Stream stream, [NotNull] byte[] buffer)
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

        static unsafe void UpdateSequenceNumber(ref OggPage page, uint sequenceNumber)
        {
            var headerSpan = new Span<byte>(page.Header.ToPointer(), page.HeaderLength);
            var sequenceNumberSpan = headerSpan.Slice(18, 4);

            // Only do the update if the sequence number has changed
            if (BinaryPrimitives.ReadUInt32LittleEndian(sequenceNumberSpan) == sequenceNumber) return;

            // Update the sequence number
            BinaryPrimitives.WriteUInt32LittleEndian(sequenceNumberSpan, sequenceNumber);

            // Recalculate the CRC
            var crcSpan = headerSpan.Slice(22, 4);
            crcSpan.Clear();
            BinaryPrimitives.WriteUInt32LittleEndian(crcSpan,
                Crc32.GetChecksum(new Span<byte>(page.Body.ToPointer(), page.BodyLength),
                    Crc32.GetChecksum(headerSpan)));
        }
    }
}
