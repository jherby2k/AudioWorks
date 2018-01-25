using System.Collections.Generic;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Wave
{
    sealed class RiffWriter : BinaryWriter
    {
        [NotNull] readonly Stack<(bool sizeUpdated, uint position)> _chunkSizePositions =
            new Stack<(bool sizeUpdated, uint position)>();

        internal RiffWriter([NotNull] Stream output)
            : base(output, Encoding.ASCII, true)
        {
        }

        internal void Initialize([NotNull] string fourCc)
        {
            BeginChunk("RIFF");
            Write(fourCc.ToCharArray());
        }

        internal void BeginChunk([NotNull] string chunkId)
        {
            Write(chunkId.ToCharArray());
            _chunkSizePositions.Push((false, (uint) BaseStream.Position));
            Write((uint) 0);
        }

        internal void BeginChunk([NotNull] string chunkId, uint chunkSize)
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