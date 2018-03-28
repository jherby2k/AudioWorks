using System;
using System.IO;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    static class ExtensionMethods
    {
        internal static uint ReadUInt32BigEndian([NotNull] this BinaryReader reader)
        {
            return ((uint) reader.ReadByte() << 24) +
                   ((uint) reader.ReadByte() << 16) +
                   ((uint) reader.ReadByte() << 8) +
                   reader.ReadByte();
        }

        public static void WriteBigEndian([NotNull] this BinaryWriter writer, uint value)
        {
            var buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            writer.Write(buffer);
        }
    }
}
