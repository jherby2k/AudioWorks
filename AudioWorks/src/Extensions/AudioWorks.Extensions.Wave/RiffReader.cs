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
                throw new AudioInvalidException("File is unexpectedly truncated.", ((FileStream) BaseStream).Name);

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
