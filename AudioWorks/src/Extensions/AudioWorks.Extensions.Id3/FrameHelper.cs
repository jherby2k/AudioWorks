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
using AudioWorks.Common;

namespace AudioWorks.Extensions.Id3
{
    sealed class FrameHelper
    {
        readonly byte _version;

        internal FrameHelper(TagHeader header) => _version = header.Version;

        internal unsafe FrameBase Build(string frameId, ushort flags, ReadOnlySpan<byte> buffer)
        {
            // Build a frame
            var frame = FrameFactory.Build(frameId);

            var extendedHeaderBytes = 0;
            var frameSize = (uint) buffer.Length;

            Stream stream = new UnmanagedMemoryStream((byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(buffer)), buffer.Length);
            var streamsToClose = new List<Stream>(2) { stream };
            try
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    if (GetCompression(flags))
                        throw new AudioUnsupportedException("Compressed ID3v2 frames are not supported.");

                    if (GetEncryption(flags))
                        throw new AudioUnsupportedException("Encrypted ID3v2 frames are not supported.");

                    if (GetGrouping(flags))
                    {
                        // Skip the group byte
                        stream.Seek(1, SeekOrigin.Current);
                        extendedHeaderBytes++;
                    }

                    if (GetDataLengthIndicator(flags))
                    {
                        // Skip the data length
                        stream.Seek(4, SeekOrigin.Current);
                        extendedHeaderBytes += 4;
                    }

                    if (GetUnsynchronisation(flags))
                    {
                        var memoryStream = new MemoryStream();
                        streamsToClose.Add(memoryStream);
                        frameSize -= Sync.Unsafe(stream, memoryStream, frameSize);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        stream = memoryStream;
                    }

                    var frameBuffer = new byte[frameSize - extendedHeaderBytes];
                    stream.Read(frameBuffer, 0, (int) (frameSize - extendedHeaderBytes));
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

        bool GetCompression(ushort flags) => _version == 4 ? (flags & 0b0000_1000) > 0 : (flags & 0b1000_0000) > 0;

        bool GetEncryption(ushort flags) => _version == 4 ? (flags & 0b0000_0100) > 0 : (flags & 0b0100_0000) > 0;

        bool GetGrouping(ushort flags) => _version == 4 ? (flags & 0b0100_0000) > 0 : (flags & 0b0010_0000) > 0;

        bool GetUnsynchronisation(ushort flags) => _version == 4 && (flags & 0b0000_0010) > 0;

        bool GetDataLengthIndicator(ushort flags) => _version == 4 && (flags & 0b0000_0001) > 0;
    }
}