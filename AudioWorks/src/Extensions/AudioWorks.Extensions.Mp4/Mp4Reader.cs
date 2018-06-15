using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;
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
            //TODO throw if read length is < 4
            return new string(ReadChars(4));
        }

        internal uint ReadUInt32BigEndian()
        {
            //TODO throw if read length is < 4
#if NETCOREAPP2_1
            Span<byte> buffer = stackalloc byte[4];
            Read(buffer);
            return BinaryPrimitives.ReadUInt32BigEndian(buffer);
#else
            Read(_buffer, 0, 4);
            return BinaryPrimitives.ReadUInt32BigEndian(_buffer);
#endif
        }
    }
}
