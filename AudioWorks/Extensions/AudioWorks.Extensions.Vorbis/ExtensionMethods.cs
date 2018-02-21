using System;
using System.IO;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    static class ExtensionMethods
    {
        public static void WriteBigEndian([NotNull] this BinaryWriter writer, uint value)
        {
            var buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            writer.Write(buffer);
        }
    }
}
