/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.IO;
using System.Text;

namespace AudioWorks.Extensions.Mp4
{
    sealed class TextAtom : WritableAtom
    {
        readonly string _fourCc;

        internal string Value { get; }

        internal TextAtom(ReadOnlySpan<byte> data)
        {
#if NETSTANDARD2_0
            _fourCc = new string(CodePagesEncodingProvider.Instance.GetEncoding(1252)
                .GetChars(data.Slice(0, 4).ToArray()));
            Value = new string(Encoding.UTF8.GetChars(data.Slice(24).ToArray()));
#else
            Span<char> fourCcBuffer = stackalloc char[4];
            (CodePagesEncodingProvider.Instance.GetEncoding(1252) ?? Encoding.ASCII)
                .GetChars(data.Slice(0, 4), fourCcBuffer);
            _fourCc = new string(fourCcBuffer);

            Span<char> charBuffer = stackalloc char[Encoding.UTF8.GetMaxCharCount(data.Length - 24)];
            var charCount = Encoding.UTF8.GetChars(data[24..], charBuffer);
            Value = new string(charBuffer.Slice(0, charCount));
#endif
        }

        internal TextAtom(string fourCc, string value)
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
#if NETSTANDARD2_0
                writer.Write(CodePagesEncodingProvider.Instance.GetEncoding(1252).GetBytes(_fourCc));
#else
                Span<byte> fourCcBuffer = stackalloc byte[4];
                (CodePagesEncodingProvider.Instance.GetEncoding(1252) ?? Encoding.ASCII).GetBytes(_fourCc, fourCcBuffer);
                writer.Write(fourCcBuffer);
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