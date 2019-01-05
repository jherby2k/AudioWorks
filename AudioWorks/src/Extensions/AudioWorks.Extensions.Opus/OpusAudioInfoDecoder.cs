/* Copyright © 2019 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Buffers.Binary;
#if !NETCOREAPP2_1
using System.Buffers;
#endif
using System.IO;
#if !NETCOREAPP2_1
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#endif
using System.Text;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Opus
{
    [AudioInfoDecoderExport(".opus")]
    public sealed class OpusAudioInfoDecoder : IAudioInfoDecoder
    {
        const string _format = "Opus";

        public string Format => _format;

        public unsafe AudioInfo ReadAudioInfo(Stream stream)
        {
            OggStream oggStream = null;
#if NETCOREAPP2_1
            Span<byte> buffer = stackalloc byte[4096];
#else
            var buffer = ArrayPool<byte>.Shared.Rent(4096);
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
#if NETCOREAPP2_1
                            var bytesRead = stream.Read(buffer);
#else
                            var bytesRead = stream.Read(buffer, 0, buffer.Length);
#endif
                            if (bytesRead == 0)
                                throw new AudioInvalidException("No Ogg stream was found.");

                            var nativeBuffer = new Span<byte>(sync.Buffer(bytesRead).ToPointer(), bytesRead);
#if NETCOREAPP2_1
                            buffer.Slice(0, bytesRead).CopyTo(nativeBuffer);
#else
                            buffer.AsSpan().Slice(0, bytesRead).CopyTo(nativeBuffer);
#endif
                            sync.Wrote(bytesRead);
                        }

                        if (oggStream == null)
                            oggStream = new OggStream(SafeNativeMethods.OggPageSerialNo(page));

                        oggStream.PageIn(page);

                        while (oggStream.PacketOut(out var packet))
                            return ParseHeader(packet);
                    } while (!SafeNativeMethods.OggPageEos(page));

                    throw new AudioInvalidException("The end of the Ogg stream was reached without finding a header.");
                }
            }
            finally
            {
#if !NETCOREAPP2_1
                ArrayPool<byte>.Shared.Return(buffer);
#endif
                oggStream?.Dispose();
            }
        }

        [NotNull]
        static unsafe AudioInfo ParseHeader(in OggPacket packet)
        {
            if (packet.Bytes < 19)
                throw new AudioUnsupportedException("Not an Opus stream.");

#if WINDOWS
            var headerBytes = new Span<byte>(packet.Packet.ToPointer(), packet.Bytes);
#else
            var headerBytes = new Span<byte>(packet.Packet.ToPointer(), (int) packet.Bytes);
#endif

#if NETCOREAPP2_1
            if (!Encoding.ASCII.GetString(headerBytes.Slice(0, 8))
#else
            if (!Encoding.ASCII.GetString((byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(headerBytes)), 8)
#endif
                .Equals("OpusHead", StringComparison.Ordinal))
                throw new AudioUnsupportedException("Not an Opus stream.");

            return AudioInfo.CreateForLossy(
                "Opus",
                headerBytes[9],
                (int) BinaryPrimitives.ReadUInt32LittleEndian(headerBytes.Slice(12)));
        }
    }
}
