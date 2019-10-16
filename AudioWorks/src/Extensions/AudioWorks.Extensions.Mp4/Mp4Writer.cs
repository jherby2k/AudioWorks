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
using System.IO;
using System.Text;

namespace AudioWorks.Extensions.Mp4
{
    sealed class Mp4Writer : BinaryWriter
    {
        internal Mp4Writer(Stream output)
            : base(output, Encoding.UTF8, true)
        {
        }

        internal void WriteBigEndian(uint value)
        {
            Span<byte> buffer = stackalloc byte[4];
            BinaryPrimitives.WriteUInt32BigEndian(buffer, value);
#if NETSTANDARD2_0
            Write(buffer.ToArray());
#else
            Write(buffer);
#endif
        }

        internal void WriteBigEndian(ulong value)
        {
            Span<byte> buffer = stackalloc byte[8];
            BinaryPrimitives.WriteUInt64BigEndian(buffer, value);
#if NETSTANDARD2_0
            Write(buffer.ToArray());
#else
            Write(buffer);
#endif
        }

        internal void WriteZeros(int count)
        {
            for (var i = 0; i < count; i++)
                Write((byte) 0);
        }
    }
}
