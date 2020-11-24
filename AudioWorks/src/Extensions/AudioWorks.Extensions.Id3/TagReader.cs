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
using System.Buffers.Binary;
using System.IO;
using System.Text;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Id3
{
    sealed class TagReader : BinaryReader
    {
#if NETSTANDARD2_0
        readonly byte[] _buffer = new byte[4];

#endif
        internal TagReader(Stream input)
            : base(input, Encoding.ASCII, true)
        {
        }

        internal uint ReadUInt32SyncSafe()
        {
#if NETSTANDARD2_0
            if (Read(_buffer, 0, 4) < 4)
                throw new AudioInvalidException("Stream is unexpectedly truncated.");

            if (_buffer[0] > 0x7F || _buffer[1] > 0x7F || _buffer[2] > 0x7F || _buffer[3] > 0x7F)
                throw new AudioInvalidException("Invalid sync-safe integer.");

            _buffer[3] = (byte) (((_buffer[3] >> 0) & 0b0111_1111) | ((_buffer[2] & 0b0000_0001) << 7));
            _buffer[2] = (byte) (((_buffer[2] >> 1) & 0b0011_1111) | ((_buffer[1] & 0b0000_0011) << 6));
            _buffer[1] = (byte) (((_buffer[1] >> 2) & 0b0001_1111) | ((_buffer[0] & 0b0000_0111) << 5));
            _buffer[0] = (byte) ((_buffer[0] >> 3) & 0b0000_1111);

            return BinaryPrimitives.ReadUInt32BigEndian(_buffer);
#else
            Span<byte> buffer = stackalloc byte[4];
            if (Read(buffer) < 4)
                throw new AudioInvalidException("Stream is unexpectedly truncated.");

            if (buffer[0] > 0x7F || buffer[1] > 0x7F || buffer[2] > 0x7F || buffer[3] > 0x7F)
                throw new AudioInvalidException("Invalid sync-safe integer.");

            buffer[3] = (byte) (((buffer[3] >> 0) & 0b0111_1111) | ((buffer[2] & 0b0000_0001) << 7));
            buffer[2] = (byte) (((buffer[2] >> 1) & 0b0011_1111) | ((buffer[1] & 0b0000_0011) << 6));
            buffer[1] = (byte) (((buffer[1] >> 2) & 0b0001_1111) | ((buffer[0] & 0b0000_0111) << 5));
            buffer[0] = (byte) ((buffer[0] >> 3) & 0b0000_1111);

            return BinaryPrimitives.ReadUInt32BigEndian(buffer);
#endif
        }

        internal uint ReadUInt32BigEndian()
        {
#if NETSTANDARD2_0
            if (Read(_buffer, 0, 4) < 4)
                throw new AudioInvalidException("Stream is unexpectedly truncated.");

            return BinaryPrimitives.ReadUInt32BigEndian(_buffer);
#else
            Span<byte> buffer = stackalloc byte[4];
            if (Read(buffer) < 4)
                throw new AudioInvalidException("Stream is unexpectedly truncated.");

            return BinaryPrimitives.ReadUInt32BigEndian(buffer);
#endif
        }

        internal ushort ReadUInt16BigEndian()
        {
#if NETSTANDARD2_0
            if (Read(_buffer, 0, 2) < 2)
                throw new AudioInvalidException("Stream is unexpectedly truncated.");

            return BinaryPrimitives.ReadUInt16BigEndian(_buffer);
#else
            Span<byte> buffer = stackalloc byte[2];
            if (Read(buffer) < 2)
                throw new AudioInvalidException("Stream is unexpectedly truncated.");

            return BinaryPrimitives.ReadUInt16BigEndian(buffer);
#endif
        }
    }
}