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
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Vorbis
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioMetadataEncoderExport(".ogg", "Vorbis", "Vorbis Comments")]
    sealed class VorbisAudioMetadataEncoder : IAudioMetadataEncoder
    {
        public SettingInfoDictionary SettingInfo { get; } = new();

        public unsafe void WriteMetadata(Stream stream, AudioMetadata metadata, SettingDictionary settings)
        {
            using (var tempStream = new TempFileStream())
            {
                OggStream? inputOggStream = null;
                OggStream? outputOggStream = null;
                Span<byte> buffer = stackalloc byte[4096];

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
                                var bytesRead = stream.Read(buffer);
                                if (bytesRead == 0)
                                    throw new AudioInvalidException("No Ogg stream was found.");

                                var nativeBuffer = new Span<byte>(sync.Buffer(bytesRead).ToPointer(), bytesRead);
                                buffer[..bytesRead].CopyTo(nativeBuffer);
                                sync.Wrote(bytesRead);
                            }

                            inputOggStream ??= new(LibOgg.OggPageSerialNo(page));
                            outputOggStream ??= new(inputOggStream.SerialNumber);

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

                        } while (!LibOgg.OggPageEos(page));

                        // Once the end of the input is reached, overwrite the original file and return
                        stream.Position = 0;
                        stream.SetLength(tempStream.Length);
                        tempStream.Position = 0;
                        tempStream.CopyTo(stream);
                    }
                }
                finally
                {
                    inputOggStream?.Dispose();
                    outputOggStream?.Dispose();
                }
            }
        }

        static void WritePage(in OggPage page, Stream stream)
        {
            WriteFromUnmanaged(page.Header, page.HeaderLength.Value.ToInt32(), stream);
            WriteFromUnmanaged(page.Body, page.BodyLength.Value.ToInt32(), stream);
        }

        static unsafe void WriteFromUnmanaged(IntPtr location, int length, Stream stream) =>
            stream.Write(new Span<byte>(location.ToPointer(), length));

            static unsafe void UpdateSequenceNumber(ref OggPage page, uint sequenceNumber)
        {
            var sequenceNumberSpan = new Span<byte>(page.Header.ToPointer(), page.HeaderLength.Value.ToInt32())
                .Slice(18, 4);

            // Only do the update if the sequence number has changed
            if (BinaryPrimitives.ReadUInt32LittleEndian(sequenceNumberSpan) == sequenceNumber) return;

            // Update the sequence number
            BinaryPrimitives.WriteUInt32LittleEndian(sequenceNumberSpan, sequenceNumber);

            // Recalculate the CRC
            LibOgg.OggPageChecksumSet(ref page);
        }
    }
}
