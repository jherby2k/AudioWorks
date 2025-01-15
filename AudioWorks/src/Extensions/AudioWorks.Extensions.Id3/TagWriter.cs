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

namespace AudioWorks.Extensions.Id3
{
    sealed class TagWriter : BinaryWriter
    {
        internal TagWriter(Stream output)
            : base(output, Encoding.ASCII, true)
        {
        }

        internal void WriteSyncSafe(uint value)
        {
            Span<byte> buffer = stackalloc byte[4];
            BinaryPrimitives.WriteUInt32BigEndian(buffer, value);

            buffer[0] = (byte) (((buffer[1] >> 5) & 0x0000_0100) | (buffer[0] << 3) & 0b0111_1111);
            buffer[1] = (byte) (((buffer[2] >> 6) & 0x0000_0010) | (buffer[1] << 2) & 0b0111_1111);
            buffer[2] = (byte) (((buffer[3] >> 7) & 0b0000_0001) | (buffer[2] << 1) & 0b0111_1111);
            buffer[3] = (byte) (buffer[3] & 0b0111_1111);

            Write(buffer);
        }

        internal void WriteBigEndian(uint value)
        {
            Span<byte> buffer = stackalloc byte[4];
            BinaryPrimitives.WriteUInt32BigEndian(buffer, value);

            Write(buffer);
        }
    }
}