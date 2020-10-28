/* Copyright © 2020 Jeremy Herbison

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
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace AudioWorks.Extensions.Id3
{
    sealed class FrameHelper
    {
        readonly byte _version;

        internal FrameHelper(TagHeader header) => _version = header.Version;

        internal unsafe FrameBase Build(string frameId, ushort flags, Span<byte> buffer)
        {
            // Build a frame
            var frame = FrameFactory.Build(frameId);

            var index = 0;
            var size = (uint) buffer.Length;

            Stream stream = new UnmanagedMemoryStream((byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(buffer)), buffer.Length);
            var streamsToClose = new List<Stream>(3) { stream };
            try
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    if (GetGrouping(flags))
                    {
                        // Skip the group byte
                        reader.ReadByte();
                        index++;
                    }

                    if (GetCompression(flags))
                    {
                        size = Swap.UInt32(_version == 4
                            ? Sync.UnsafeBigEndian(reader.ReadUInt32())
                            : reader.ReadUInt32());

                        index = 0;
                        stream = new InflaterInputStream(stream);
                        streamsToClose.Add(stream);
                    }

                    if (GetUnsynchronisation(flags))
                    {
                        var memoryStream = new MemoryStream();
                        streamsToClose.Add(memoryStream);
                        size = Sync.Unsafe(stream, memoryStream, size);
                        index = 0;
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        stream = memoryStream;
                    }

                    var frameBuffer = new byte[size - index];
                    stream.Read(frameBuffer, 0, (int) (size - index));
                    frame.Parse(frameBuffer);
                    return frame;
                }
            }
            finally
            {
                foreach (var streamToClose in streamsToClose)
                    streamToClose.Close();
            }
        }

        bool GetGrouping(ushort flags) => _version == 4 ? (flags & 0x0040) > 0 : (flags & 0x0020) > 0;

        bool GetCompression(ushort flags) => _version == 4 ? (flags & 0x0008) > 0 : (flags & 0x0080) > 0;

        bool GetUnsynchronisation(ushort flags) => _version == 4 && (flags & 0x0002) > 0;
    }
}