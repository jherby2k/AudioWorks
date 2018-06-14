using System;
using System.IO;
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
#if NETCOREAPP2_1
            Span<char> fourCcBuffer = stackalloc char[4];
            CodePagesEncodingProvider.Instance.GetEncoding(1252)
                .GetChars(data.Slice(0, 4), fourCcBuffer);
            _fourCc = new string(fourCcBuffer);

            Span<char> charBuffer = stackalloc char[Encoding.UTF8.GetMaxCharCount(data.Length - 24)];
            var charCount = Encoding.UTF8.GetChars(data.Slice(24), charBuffer);
            Value = new string(charBuffer.Slice(0, charCount));
#else
            _fourCc = new string(CodePagesEncodingProvider.Instance.GetEncoding(1252)
                .GetChars(data.Slice(0, 4).ToArray()));
            Value = new string(Encoding.UTF8.GetChars(data.Slice(24).ToArray()));
#endif
        }

        internal TextAtom([NotNull] string fourCc, [NotNull] string value)
        {
            _fourCc = fourCc;
            Value = value;
        }

        internal override void Write(Stream output)
        {
            var contents = Encoding.UTF8.GetBytes(Value);

            using (var writer = new Mp4Writer(output))
            {
                // Write the atom header
                writer.WriteBigEndian((uint) contents.Length + 24);
#if NETCOREAPP2_1
                Span<byte> fourCcBuffer = stackalloc byte[4];
                CodePagesEncodingProvider.Instance.GetEncoding(1252).GetBytes(_fourCc, fourCcBuffer);
                writer.Write(fourCcBuffer);
#else
                writer.Write(CodePagesEncodingProvider.Instance.GetEncoding(1252).GetBytes(_fourCc));
#endif

                // Write the data atom header
                writer.WriteBigEndian((uint) contents.Length + 16);
                writer.WriteBigEndian(0x64617461); // 'data'

                // Set the type flag
                writer.WriteBigEndian(1);
                writer.WriteZeros(4);

                writer.Write(contents);
            }
        }
    }
}