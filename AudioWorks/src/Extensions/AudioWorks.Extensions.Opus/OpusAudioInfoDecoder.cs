﻿/* Copyright © 2019 Jeremy Herbison

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
#if NETSTANDARD2_0
using System.Buffers;
#endif
using System.IO;
#if NETSTANDARD2_0
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#endif
using System.Text;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Opus
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioInfoDecoderExport(".opus", "Opus")]
    sealed class OpusAudioInfoDecoder : IAudioInfoDecoder
    {
        const string _format = "Opus";

        public string Format => _format;

        public unsafe AudioInfo ReadAudioInfo(Stream stream)
        {
            OggStream? oggStream = null;
#if NETSTANDARD2_0
            var buffer = ArrayPool<byte>.Shared.Rent(4096);
#else
            Span<byte> buffer = stackalloc byte[4096];
#endif

            try
            {
                using (var sync = new OggSync())
                {
                    OggPage page;

                    do
                    {
                        // Read from the buffer into a page
                        while (!sync.PageOut(out page))
                        {
#if NETSTANDARD2_0
                            var bytesRead = stream.Read(buffer, 0, buffer.Length);
#else
                            var bytesRead = stream.Read(buffer);
#endif
                            if (bytesRead == 0)
                                throw new AudioInvalidException("No Ogg stream was found.");

                            var nativeBuffer = new Span<byte>(sync.Buffer(bytesRead).ToPointer(), bytesRead);
#if NETSTANDARD2_0
                            buffer.AsSpan()[..bytesRead].CopyTo(nativeBuffer);
#else
                            buffer[..bytesRead].CopyTo(nativeBuffer);
#endif
                            sync.Wrote(bytesRead);
                        }

                        oggStream ??= new(SafeNativeMethods.OggPageSerialNo(page));
                        oggStream.PageIn(page);

                        while (oggStream.PacketOut(out var packet))
#if WINDOWS
                            return GetAudioInfo(oggStream.SerialNumber, packet, stream);
#else
                            return GetAudioInfo((int) oggStream.SerialNumber, packet, stream);
#endif
                    } while (!SafeNativeMethods.OggPageEos(page));

                    throw new AudioInvalidException("The end of the Ogg stream was reached without finding a header.");
                }
            }
            finally
            {
#if NETSTANDARD2_0
                ArrayPool<byte>.Shared.Return(buffer);
#endif
                oggStream?.Dispose();
            }
        }

        static unsafe AudioInfo GetAudioInfo(int serialNumber, in OggPacket headerPacket, Stream stream)
        {
            if (headerPacket.Bytes < 19)
                throw new AudioUnsupportedException("Not an Opus stream.");

#if WINDOWS
            var headerBytes = new Span<byte>(headerPacket.Packet.ToPointer(), headerPacket.Bytes);
#else
            var headerBytes = new Span<byte>(headerPacket.Packet.ToPointer(), (int) headerPacket.Bytes);
#endif

#if NETSTANDARD2_0
            if (!Encoding.ASCII.GetString((byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(headerBytes)), 8)
#else
            if (!Encoding.ASCII.GetString(headerBytes[..8])
#endif
                .Equals("OpusHead", StringComparison.Ordinal))
                throw new AudioUnsupportedException("Not an Opus stream.");

            var sampleRate = BinaryPrimitives.ReadUInt32LittleEndian(headerBytes[12..]);

            return AudioInfo.CreateForLossy(
                "Opus",
                headerBytes[9],
                (int) sampleRate,
                (long) Math.Max(
                    (GetFinalGranulePosition(serialNumber, stream) -
                     BinaryPrimitives.ReadUInt16LittleEndian(headerBytes[10..])) / (double) 48000 * sampleRate,
                    0.0));
        }

        static long GetFinalGranulePosition(int serialNumber, Stream stream)
        {
            // The largest possible Ogg page is 65,307 bytes long
            stream.Seek(-Math.Min(65307, stream.Length), SeekOrigin.End);
            try
            {
                // Optimization - this is accessed frequently
                var streamLength = stream.Length;

                // Scan to the start of the last Ogg page
                while (stream.Position < streamLength)
                {
                    if (stream.ReadByte() != 0x4F ||
                        stream.ReadByte() != 0x67 ||
                        stream.ReadByte() != 0x67 ||
                        stream.ReadByte() != 0x53 ||
                        stream.ReadByte() != 0 ||
                        stream.ReadByte() >> 2 != 1)
                        continue;

                    using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
                    {
                        var result = reader.ReadInt64();

                        if (reader.ReadUInt32() == serialNumber)
                            return result;

                        // If the serial # was from a different stream, just give up
                        break;
                    }
                }
            }
            catch (EndOfStreamException)
            {
            }

            return 0;
        }
    }
}
