using JetBrains.Annotations;
using System.IO;
using System.Text;

namespace AudioWorks.Extensions.Wave
{
    class RiffReader : BinaryReader
    {
        internal uint RiffChunkSize { get; private set; }

        public RiffReader(Stream input)
            : base(input, Encoding.ASCII, true)
        {
        }

        internal void Initialize()
        {
            BaseStream.Position = 0;
            if (new string(ReadChars(4)) != "RIFF")
                throw new IOException("Not a valid RIFF stream.");

            RiffChunkSize = ReadUInt32();
        }

        [NotNull]
        internal string ReadFourcc()
        {
            BaseStream.Position = 8;
            return new string(ReadChars(4));
        }

        internal uint SeekToChunk(string chunkId)
        {
            BaseStream.Position = 12;

            var currentChunkId = new string(ReadChars(4));
            var currentChunkLength = ReadUInt32();

            while (currentChunkId != chunkId)
            {
                // Chunks are word-aligned:
                BaseStream.Seek(currentChunkLength + currentChunkLength % 2, SeekOrigin.Current);

                if (BaseStream.Position >= RiffChunkSize + 8)
                    return 0;

                currentChunkId = new string(ReadChars(4));
                currentChunkLength = ReadUInt32();
            }

            return currentChunkLength;
        }
    }
}
