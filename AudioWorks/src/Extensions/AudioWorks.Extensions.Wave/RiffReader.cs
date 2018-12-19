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

using System;
using System.IO;
using System.Text;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Wave
{
    sealed class RiffReader : BinaryReader
    {
        internal uint RiffChunkSize { get; private set; }

        internal RiffReader([NotNull] Stream input)
            : base(input, Encoding.ASCII, true)
        {
        }

        internal void Initialize()
        {
            BaseStream.Position = 0;
            if (!ReadFourCc().Equals("RIFF", StringComparison.OrdinalIgnoreCase))
                throw new AudioInvalidException("Not a valid RIFF stream.");

            RiffChunkSize = ReadUInt32();
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
                throw new AudioInvalidException("Stream is unexpectedly truncated.");

            return new string(buffer);
        }

        internal uint SeekToChunk([NotNull] string chunkId)
        {
            BaseStream.Position = 12;

            var currentChunkId = ReadFourCc();
            var currentChunkLength = ReadUInt32();

            while (!currentChunkId.Equals(chunkId, StringComparison.Ordinal))
            {
                // Chunks are word-aligned:
                BaseStream.Seek(currentChunkLength + currentChunkLength % 2, SeekOrigin.Current);

                if (BaseStream.Position >= RiffChunkSize + 8)
                    return 0;

                currentChunkId = ReadFourCc();
                currentChunkLength = ReadUInt32();
            }

            return currentChunkLength;
        }
    }
}
