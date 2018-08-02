using System;
#if !NETCOREAPP2_1
using System.Buffers;
#endif
using System.Buffers.Binary;
using System.IO;
using System.Runtime.CompilerServices;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    [AudioMetadataEncoderExport(".ogg")]
    public sealed class VorbisAudioMetadataEncoder : IAudioMetadataEncoder
    {
        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary();

        public unsafe void WriteMetadata(FileStream stream, AudioMetadata metadata, SettingDictionary settings)
        {
            using (var tempStream = new TempFileStream())
            {
                OggStream inputOggStream = null;
                OggStream outputOggStream = null;
#if NETCOREAPP2_1
                Span<byte> buffer = stackalloc byte[4096];
#else
                var buffer = ArrayPool<byte>.Shared.Rent(4096);
#endif

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
#if NETCOREAPP2_1
                                var bytesRead = stream.Read(buffer);
#else
                                var bytesRead = stream.Read(buffer, 0, buffer.Length);
#endif
                                if (bytesRead == 0)
                                    throw new AudioInvalidException("No Ogg stream was found.", stream.Name);

                                var nativeBuffer = new Span<byte>(sync.Buffer(bytesRead).ToPointer(), bytesRead);
#if NETCOREAPP2_1
                                buffer.Slice(0, bytesRead).CopyTo(nativeBuffer);
#else
                                buffer.AsSpan().Slice(0, bytesRead).CopyTo(nativeBuffer);
#endif
                                sync.Wrote(bytesRead);
                            }

                            if (inputOggStream == null)
                                inputOggStream = new OggStream(SafeNativeMethods.OggPageSerialNo(page));
                            if (outputOggStream == null)
                                outputOggStream = new OggStream(inputOggStream.SerialNumber);

                            // Write new header page(s) using a modified comment packet
                            if (!headerWritten)
                            {
                                inputOggStream.PageIn(page);

                                while (inputOggStream.PacketOut(out var packet))
                                    switch (packet.PacketNumber)
                                    {
                                        case 0:
                                            outputOggStream.PacketIn(packet);
                                            break;

                                        // Substitute the new comment packet
                                        case 1:
                                            using (var adapter = new MetadataToVorbisCommentAdapter(metadata))
                                            {
                                                adapter.HeaderOut(out var commentPacket);
                                                outputOggStream.PacketIn(commentPacket);
                                            }

                                            break;

                                        // Flush at the end of the header
                                        case 2:
                                            outputOggStream.PacketIn(packet);
                                            while (outputOggStream.Flush(out var outPage))
                                            {
                                                WritePage(outPage, tempStream);
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
                                WritePage(page, tempStream);
                                pagesWritten++;
                            }

                        } while (!SafeNativeMethods.OggPageEos(page));

                        // Once the end of the input is reached, overwrite the original file and return
                        stream.Position = 0;
                        stream.SetLength(tempStream.Length);
                        tempStream.Position = 0;
                        tempStream.CopyTo(stream);
                    }
                }
                finally
                {
#if !NETCOREAPP2_1
                    ArrayPool<byte>.Shared.Return(buffer);
#endif
                    inputOggStream?.Dispose();
                    outputOggStream?.Dispose();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void WritePage(in OggPage page, [NotNull] Stream stream)
        {
#if WINDOWS
            WriteFromUnmanaged(page.Header, page.HeaderLength, stream);
            WriteFromUnmanaged(page.Body, page.BodyLength, stream);
#else
            WriteFromUnmanaged(page.Header, (int) page.HeaderLength, stream);
            WriteFromUnmanaged(page.Body, (int) page.BodyLength, stream);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static unsafe void WriteFromUnmanaged(IntPtr location, int length, [NotNull] Stream stream)
        {
#if NETCOREAPP2_1
            stream.Write(new Span<byte>(location.ToPointer(), length));
#else
            var buffer = ArrayPool<byte>.Shared.Rent(4096);
            try
            {
                Span<byte> data = new Span<byte>(location.ToPointer(), length);
                var offset = 0;

                while (offset < length)
                {
                    var bytesCopied = Math.Min(length - offset, buffer.Length);
                    data.Slice(offset, bytesCopied).CopyTo(buffer);
                    stream.Write(buffer, 0, bytesCopied);
                    offset += bytesCopied;
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
#endif
        }

        static unsafe void UpdateSequenceNumber(ref OggPage page, uint sequenceNumber)
        {
#if WINDOWS
            var headerSpan = new Span<byte>(page.Header.ToPointer(), page.HeaderLength);
#else
            var headerSpan = new Span<byte>(page.Header.ToPointer(), (int) page.HeaderLength);
#endif
            var sequenceNumberSpan = headerSpan.Slice(18, 4);

            // Only do the update if the sequence number has changed
            if (BinaryPrimitives.ReadUInt32LittleEndian(sequenceNumberSpan) == sequenceNumber) return;

            // Update the sequence number
            BinaryPrimitives.WriteUInt32LittleEndian(sequenceNumberSpan, sequenceNumber);

            // Recalculate the CRC
            var crcSpan = headerSpan.Slice(22, 4);
            crcSpan.Clear();
            BinaryPrimitives.WriteUInt32LittleEndian(crcSpan,
#if WINDOWS
                Crc32.GetChecksum(new Span<byte>(page.Body.ToPointer(), page.BodyLength),
#else
                Crc32.GetChecksum(new Span<byte>(page.Body.ToPointer(), (int) page.BodyLength),
#endif
                    Crc32.GetChecksum(headerSpan)));
        }
    }
}
