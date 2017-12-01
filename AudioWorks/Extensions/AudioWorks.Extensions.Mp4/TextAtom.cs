using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class TextAtom : WritableAtom
    {
        [NotNull] readonly string _fourCc;

        [NotNull]
        public string Value { get; }

        internal TextAtom([NotNull] IReadOnlyCollection<byte> data)
        {
            _fourCc = new string(CodePagesEncodingProvider.Instance.GetEncoding(1252).GetChars(data.Take(4).ToArray()));
            Value = new string(Encoding.UTF8.GetChars(data.Skip(24).Take(data.Count - 24).ToArray()));
        }

        internal TextAtom([NotNull] string fourCc, [NotNull] string value)
        {
            _fourCc = fourCc;
            Value = value;
        }

        internal override byte[] GetBytes()
        {
            var contents = Encoding.UTF8.GetBytes(Value);

            var result = new byte[contents.Length + 24];

            // Write the atom header
            ConvertToBigEndianBytes((uint)result.Length).CopyTo(result, 0);
            CodePagesEncodingProvider.Instance.GetEncoding(1252).GetBytes(_fourCc).CopyTo(result, 4);

            // Write the data atom header
            ConvertToBigEndianBytes((uint)result.Length - 8).CopyTo(result, 8);
            BitConverter.GetBytes(0x61746164).CopyTo(result, 12); // 'atad'

            // Set the type flag
            result[19] = 1;

            // Set the atom contents
            contents.CopyTo(result, 24);

            return result;
        }

        [Pure, NotNull]
        static byte[] ConvertToBigEndianBytes(uint value)
        {
            var result = BitConverter.GetBytes(value);
            Array.Reverse(result);
            return result;
        }
    }
}