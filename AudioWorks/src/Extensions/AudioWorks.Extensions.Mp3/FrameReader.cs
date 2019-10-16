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

#if !NETSTANDARD2_0
using System;
#endif
using System.Buffers.Binary;
using System.IO;
using System.Text;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Mp3
{
    sealed class FrameReader : BinaryReader
    {
#if NETSTANDARD2_0
        readonly byte[] _buffer = new byte[4];

#endif
        internal long FrameStart { get; private set; }

        internal FrameReader(Stream input)
            : base(input, Encoding.ASCII, true)
        {
        }

        internal void SeekToNextFrame()
        {
            try
            {
                // A frame begins with the first 11 bits set
                while (true)
                {
                    if (ReadByte() != 0xFF || ReadByte() < 0xE0) continue;
                    FrameStart = BaseStream.Seek(-2, SeekOrigin.Current);
                    return;
                }
            }
            catch (EndOfStreamException)
            {
                throw new AudioInvalidException("Stream is unexpectedly truncated.");
            }
        }

        internal bool VerifyFrameSync(FrameHeader header)
        {
            var frameLength = header.SamplesPerFrame / 8 * header.BitRate * 1000 /
                                header.SampleRate + header.Padding;

            // Seek to where the next frame should start
            var initialPosition = BaseStream.Position;
            BaseStream.Seek(frameLength - 4, SeekOrigin.Current);
            var firstByte = ReadByte();
            var secondByte = ReadByte();
            BaseStream.Position = initialPosition;

            // If another sync is detected, return success
            return firstByte == 0xFF && secondByte >= 0xE0;
        }

        internal uint ReadUInt32BigEndian()
        {
#if NETSTANDARD2_0
            if (Read(_buffer, 0, 4) < 4)
                throw new AudioInvalidException("File is unexpectedly truncated.");

            return BinaryPrimitives.ReadUInt32BigEndian(_buffer);
#else
            Span<byte> buffer = stackalloc byte[4];

            if (Read(buffer) < 4)
                throw new AudioInvalidException("Stream is unexpectedly truncated.");

            return BinaryPrimitives.ReadUInt32BigEndian(buffer);
#endif
        }

        internal string ReadHeaderId()
        {
#if NETSTANDARD2_0
            var buffer = ReadChars(4);
            if (buffer.Length < 4)
#else
            Span<char> buffer = stackalloc char[4];
            if (Read(buffer) < 4)
#endif
                throw new AudioInvalidException("Stream is unexpectedly truncated.");

            return new string(buffer);
        }
    }
}