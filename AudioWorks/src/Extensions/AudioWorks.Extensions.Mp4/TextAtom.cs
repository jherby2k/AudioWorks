using System;
using System.Buffers.Binary;
using System.Text;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Mp4
{
    sealed class TextAtom : WritableAtom
    {
        [NotNull] readonly string _fourCc;

        [NotNull]
        internal string Value { get; }

        internal TextAtom(ReadOnlySpan<byte> data)
        {
            _fourCc = new string(CodePagesEncodingProvider.Instance.GetEncoding(1252)
                .GetChars(data.Slice(0, 4).ToArray()));
            Value = new string(Encoding.UTF8.GetChars(data.Slice(24).ToArray()));
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
            BinaryPrimitives.WriteUInt32BigEndian(result, (uint) result.Length);
            CodePagesEncodingProvider.Instance.GetEncoding(1252).GetBytes(_fourCc).CopyTo(result.AsSpan().Slice(4));

            // Write the data atom header
            BinaryPrimitives.WriteUInt32BigEndian(result.AsSpan().Slice(8), (uint) result.Length - 8);
            BinaryPrimitives.WriteInt32BigEndian(result.AsSpan().Slice(12), 0x64617461);  // 'data'

            // Set the type flag
            result[19] = 1;

            // Set the atom contents
            contents.CopyTo(result.AsSpan().Slice(24));

            return result;
        }
    }
}