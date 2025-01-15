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
using AudioWorks.Common;

namespace AudioWorks.Extensions.Mp4
{
    sealed class Mp4Reader : BinaryReader
    {
        internal Mp4Reader(Stream input)
            : base(input, CodePagesEncodingProvider.Instance.GetEncoding(1252) ?? Encoding.ASCII, true)
        {
        }

        internal string ReadFourCc()
        {
            Span<char> buffer = stackalloc char[4];
            if (Read(buffer) < 4)
                throw new AudioInvalidException("Stream is unexpectedly truncated.");

            return new(buffer);
        }

        internal uint ReadUInt32BigEndian()
        {
            Span<byte> buffer = stackalloc byte[4];
            if (Read(buffer) < 4)
                throw new AudioInvalidException("Stream is unexpectedly truncated.");

            return BinaryPrimitives.ReadUInt32BigEndian(buffer);
        }
    }
}
