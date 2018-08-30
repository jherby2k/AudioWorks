using System;
#if NETCOREAPP2_1
using System.Buffers.Binary;
#endif
using System.IO;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    static class ExtensionMethods
    {
        public static void WriteBigEndian([NotNull] this BinaryWriter writer, uint value)
        {
#if NETCOREAPP2_1
            Span<byte> buffer = stackalloc byte[4];
            BinaryPrimitives.WriteUInt32BigEndian(buffer, value);
            writer.Write(buffer);
#else
            var buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            writer.Write(buffer);
#endif
        }
    }
}
