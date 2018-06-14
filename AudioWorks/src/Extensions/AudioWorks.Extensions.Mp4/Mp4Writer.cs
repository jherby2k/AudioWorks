using System;
using System.Buffers.Binary;
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
            Span<byte> buffer = stackalloc byte[4];
            BinaryPrimitives.WriteUInt32BigEndian(buffer, value);
#if NETCOREAPP2_1
            Write(buffer);
#else
            Write(buffer.ToArray());
#endif
        }

        internal void WriteBigEndian(ulong value)
        {
            Span<byte> buffer = stackalloc byte[8];
            BinaryPrimitives.WriteUInt64BigEndian(buffer, value);
#if NETCOREAPP2_1
            Write(buffer);
#else
            Write(buffer.ToArray());
#endif
        }

        internal void WriteZeros(int count)
        {
            for (var i = 0; i < count; i++)
                Write((byte) 0);
        }
    }
}
