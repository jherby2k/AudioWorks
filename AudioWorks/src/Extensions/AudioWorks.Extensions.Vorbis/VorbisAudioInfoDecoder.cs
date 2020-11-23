﻿/* Copyright © 2018 Jeremy Herbison

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
#if NETSTANDARD2_0
using System.Buffers;
#endif
using System.IO;
using System.Text;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Vorbis
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioInfoDecoderExport(".ogg", "Ogg Vorbis")]
    sealed class VorbisAudioInfoDecoder : IAudioInfoDecoder
    {
        const string _format = "Ogg Vorbis";

        public string Format => _format;

        public unsafe AudioInfo ReadAudioInfo(Stream stream)
        {
            OggStream? oggStream = null;
            SafeNativeMethods.VorbisCommentInit(out var vorbisComment);
#if NETSTANDARD2_0
            var buffer = ArrayPool<byte>.Shared.Rent(4096);
#else
            Span<byte> buffer = stackalloc byte[4096];
#endif

            try
            {
                using (var sync = new OggSync())
                using (var decoder = new VorbisDecoder())
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
                            buffer.AsSpan().Slice(0, bytesRead).CopyTo(nativeBuffer);
#else
                            buffer.Slice(0, bytesRead).CopyTo(nativeBuffer);
#endif
                            sync.Wrote(bytesRead);
                        }

                        oggStream ??= new OggStream(SafeNativeMethods.OggPageSerialNo(page));
                        oggStream.PageIn(page);

                        while (oggStream.PacketOut(out var packet))
                        {
                            if (!SafeNativeMethods.VorbisSynthesisIdHeader(packet))
                                throw new AudioUnsupportedException("Not a Vorbis stream.");

                            decoder.HeaderIn(vorbisComment, packet);

                            var info = decoder.GetInfo();
                            return AudioInfo.CreateForLossy(
                                "Vorbis",
                                info.Channels,
#if WINDOWS
                                info.Rate,
                                GetFinalGranulePosition(oggStream.SerialNumber, stream),
                                Math.Max(info.BitRateNominal, 0));
#else
                                (int) info.Rate,
                                GetFinalGranulePosition((int) oggStream.SerialNumber, stream),
                                Math.Max((int) info.BitRateNominal, 0));
#endif
                        }
                    } while (!SafeNativeMethods.OggPageEos(page));

                    throw new AudioInvalidException("The end of the Ogg stream was reached without finding a header.");
                }
            }
            finally
            {
#if NETSTANDARD2_0
                ArrayPool<byte>.Shared.Return(buffer);
#endif
                SafeNativeMethods.VorbisCommentClear(ref vorbisComment);
                oggStream?.Dispose();
            }
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
