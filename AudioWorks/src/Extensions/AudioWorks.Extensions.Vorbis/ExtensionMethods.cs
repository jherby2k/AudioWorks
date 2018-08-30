using System;
#if NETCOREAPP2_1
using System.Buffers.Binary;
#endif
using System.IO;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    static class ExtensionMethods
    {
        internal static uint ReadUInt32BigEndian([NotNull] this BinaryReader reader)
        {
#if NETCOREAPP2_1
            Span<byte> buffer = stackalloc byte[4];
            if (reader.Read(buffer) < 4)
                throw new AudioInvalidException("File is unexpectedly truncated.",
                    ((FileStream) reader.BaseStream).Name);

            return BinaryPrimitives.ReadUInt32BigEndian(buffer);
#else
            try
            {
                return ((uint) reader.ReadByte() << 24) +
                       ((uint) reader.ReadByte() << 16) +
                       ((uint) reader.ReadByte() << 8) +
                       reader.ReadByte();
            }
            catch (EndOfStreamException)
            {
                throw new AudioInvalidException("File is unexpectedly truncated.",
                    ((FileStream) reader.BaseStream).Name);
            }
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
