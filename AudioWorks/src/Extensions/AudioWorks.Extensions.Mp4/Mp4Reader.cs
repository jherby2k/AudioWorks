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

namespace AudioWorks.Extensions.Mp4
{
    sealed class Mp4Reader : BinaryReader
    {
#if NETSTANDARD2_0
        readonly byte[] _buffer = new byte[4];

#endif
        internal Mp4Reader(Stream input)
            : base(input, CodePagesEncodingProvider.Instance.GetEncoding(1252), true)
        {
        }

        internal string ReadFourCc()
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
    }
}
