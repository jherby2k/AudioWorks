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
        internal static uint ReadUInt32BigEndian([NotNull] this BinaryReader reader)
        {
            // TODO throw on read < 4 bytes?
#if NETCOREAPP2_1
            Span<byte> buffer = stackalloc byte[4];
            reader.Read(buffer);
            return BinaryPrimitives.ReadUInt32BigEndian(buffer);
#else
            return ((uint) reader.ReadByte() << 24) +
                   ((uint) reader.ReadByte() << 16) +
                   ((uint) reader.ReadByte() << 8) +
                   reader.ReadByte();
#endif
        }

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
