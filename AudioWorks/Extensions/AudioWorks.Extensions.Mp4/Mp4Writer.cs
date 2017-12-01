using System;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class Mp4Writer : BinaryWriter
    {
        internal Mp4Writer([NotNull] Stream output)
            : base(output, Encoding.UTF8, true)
        {
        }

        internal void WriteBigEndian(uint value)
        {
            var buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            Write(buffer);
        }
    }
}
