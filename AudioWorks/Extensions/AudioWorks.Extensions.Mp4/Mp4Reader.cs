using JetBrains.Annotations;
using System;
using System.IO;
using System.Text;

namespace AudioWorks.Extensions.Mp4
{
    sealed class Mp4Reader : BinaryReader
    {
        [NotNull] readonly byte[] _buffer = new byte[4];

        public Mp4Reader([NotNull] Stream input)
            : base(input, Encoding.ASCII, true)
        {
        }

        [NotNull]
        internal string ReadFourCc()
        {
            return new string(ReadChars(4));
        }

        internal uint ReadUInt32BigEndian()
        {
            Read(_buffer, 0, 4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(_buffer);
            return BitConverter.ToUInt32(_buffer, 0);
        }
    }
}
