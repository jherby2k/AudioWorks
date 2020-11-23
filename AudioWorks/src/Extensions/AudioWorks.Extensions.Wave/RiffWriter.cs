﻿/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AudioWorks.Extensions.Wave
{
    sealed class RiffWriter : BinaryWriter
    {
        readonly Stack<(bool sizeUpdated, uint position)> _chunkSizePositions = new();

        internal RiffWriter(Stream output)
            : base(output, Encoding.ASCII, true)
        {
        }

        internal void Initialize(string fourCc)
        {
            BeginChunk("RIFF");
            Write(fourCc.ToCharArray());
        }

        internal void BeginChunk(string chunkId)
        {
            Write(chunkId.ToCharArray());
            _chunkSizePositions.Push((false, (uint) BaseStream.Position));
            Write((uint) 0);
        }

        internal void BeginChunk(string chunkId, uint chunkSize)
        {
            Write(chunkId.ToCharArray());
            _chunkSizePositions.Push((true, (uint) BaseStream.Position));
            Write(chunkSize);
        }

        internal void FinishChunk()
        {
            var (sizeUpdated, position) = _chunkSizePositions.Pop();

            if (!sizeUpdated)
            {
                var currentPosition = (uint) BaseStream.Position;
                BaseStream.Position = position;
                Write((uint) (currentPosition - BaseStream.Position - 4));
                BaseStream.Position = currentPosition;
            }

            // Chunks should be word-aligned
            if (BaseStream.Position % 2 == 1)
                Write((byte) 0);
        }
    }
}