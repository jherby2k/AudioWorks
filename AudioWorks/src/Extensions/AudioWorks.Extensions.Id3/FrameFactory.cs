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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Id3
{
    static class FrameFactory
    {
        static readonly Dictionary<string, Type> _frameTypes = new();

        static FrameFactory()
        {
            // Search the assembly for defined frame types
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            foreach (var frameAttribute in (FrameAttribute[]) type.GetCustomAttributes<FrameAttribute>(false))
                _frameTypes.Add(frameAttribute.FrameId, type);
        }

        internal static FrameBase Build(string frameId)
        {
            //Try to find the most specific frame first
            if (_frameTypes.TryGetValue(frameId, out var type) && Activator.CreateInstance(type) is FrameBase newFrame)
                return newFrame;

            //Get the T*** frame
            if (_frameTypes.TryGetValue(frameId[..1], out type) && Activator.CreateInstance(type, frameId) is FrameBase txxxFrame)
                return txxxFrame;

            throw new ArgumentException($"'{frameId}' is not a supported frame ID.", nameof(frameId));
        }

        internal static unsafe FrameBase Build(string frameId, FrameFlags flags, ReadOnlySpan<byte> buffer)
        {
            var frame = Build(frameId);

            var extendedHeaderBytes = 0;
            var frameSize = (uint) buffer.Length;

            Stream stream = new UnmanagedMemoryStream((byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(buffer)), buffer.Length);
            var streamsToClose = new List<Stream>(2) { stream };
            try
            {
                if (flags.Compression)
                    throw new AudioUnsupportedException("Compressed ID3v2 frames are not supported.");

                if (flags.Encryption)
                    throw new AudioUnsupportedException("Encrypted ID3v2 frames are not supported.");

                if (flags.Grouping)
                {
                    // Skip the group byte
                    stream.Seek(1, SeekOrigin.Current);
                    extendedHeaderBytes++;
                }

                if (flags.DataLengthIndicator)
                {
                    // Skip the data length
                    stream.Seek(4, SeekOrigin.Current);
                    extendedHeaderBytes += 4;
                }

                if (flags.Unsynchronisation)
                {
                    var memoryStream = new MemoryStream(new byte[stream.Length]);
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
            finally
            {
                foreach (var streamToClose in streamsToClose)
                    streamToClose.Close();
            }
        }
    }
}