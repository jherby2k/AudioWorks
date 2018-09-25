/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

#if NETCOREAPP2_1
using System;
#endif
using System.Buffers.Binary;
using System.IO;
using System.Text;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class Mp4Reader : BinaryReader
    {
#if !NETCOREAPP2_1
        [NotNull] readonly byte[] _buffer = new byte[4];

#endif
        internal Mp4Reader([NotNull] Stream input)
            : base(input, CodePagesEncodingProvider.Instance.GetEncoding(1252), true)
        {
        }

        [NotNull]
        internal string ReadFourCc()
        {
#if NETCOREAPP2_1
            Span<char> buffer = stackalloc char[4];
            if (Read(buffer) < 4)
#else
            var buffer = ReadChars(4);
            if (buffer.Length < 4)
#endif
                throw new AudioInvalidException("File is unexpectedly truncated.", ((FileStream) BaseStream).Name);

            return new string(buffer);
        }

        internal uint ReadUInt32BigEndian()
        {
#if NETCOREAPP2_1
            Span<byte> buffer = stackalloc byte[4];
            if (Read(buffer) < 4)
                throw new AudioInvalidException("File is unexpectedly truncated.", ((FileStream) BaseStream).Name);

            return BinaryPrimitives.ReadUInt32BigEndian(buffer);
#else
            if (Read(_buffer, 0, 4) < 4)
                throw new AudioInvalidException("File is unexpectedly truncated.", ((FileStream) BaseStream).Name);

            return BinaryPrimitives.ReadUInt32BigEndian(_buffer);
#endif
        }
    }
}
