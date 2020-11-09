/* Copyright © 2020 Jeremy Herbison

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
using System.Collections.Generic;
using System.IO;
#if NETSTANDARD2_0
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#endif
using System.Text;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Id3
{
    static class TextBuilder
    {
        static readonly Dictionary<TextType, Encoding> _encodings = new Dictionary<TextType, Encoding>
        {
            [TextType.Ascii] = CodePagesEncodingProvider.Instance.GetEncoding(1252),
            [TextType.Utf16] = Encoding.Unicode,
            [TextType.Utf16BigEndian] = Encoding.BigEndianUnicode,
            [TextType.Utf8] = new UTF8Encoding(false)
        };

        static readonly Dictionary<TextType, int> _nullCharacterLengths = new Dictionary<TextType, int>
        {
            [TextType.Ascii] = 1,
            [TextType.Utf16] = 2,
            [TextType.Utf16BigEndian] = 2,
            [TextType.Utf8] = 1
        };

        internal static string ReadText(Span<byte> frame, ref int index, TextType textType) =>
            textType switch
            {
                TextType.Utf16 => ReadUtf16(frame, ref index),
                var t when Enum.IsDefined(typeof(TextType), t) => ReadTextNoPreamble(frame, ref index, textType),
                _ => throw new AudioInvalidException("Invalid text type.")
            };

        internal static string ReadTextEnd(Span<byte> frame, TextType textType) =>
            textType switch
            {
                TextType.Utf16 => ReadUtf16End(frame),
                var t when Enum.IsDefined(typeof(TextType), t) => ReadTextEndNoPreamble(frame, textType),
                _ => throw new AudioInvalidException("Invalid text type.")
            };

        internal static void WriteText(Stream stream, string text, TextType textType, bool nullTerminate = true)
        {
            var encoding = _encodings[textType];
            using (var writer = new BinaryWriter(stream, encoding, true))
            {
#if NETSTANDARD2_0
                writer.Write(encoding.GetPreamble());
                writer.Write(text.ToCharArray());
#else
                writer.Write(encoding.Preamble);
                writer.Write(text.AsSpan());
#endif
                if (nullTerminate)
                    writer.Write('\0');
            }
        }

#if NETSTANDARD2_0
        static unsafe string ReadTextNoPreamble(Span<byte> frame, ref int index, TextType textType)
#else
        static string ReadTextNoPreamble(Span<byte> frame, ref int index, TextType textType)
#endif
        {
            var text = string.Empty;
            var count = frame.Slice(index).IndexOf((byte) 0);
            if (count == -1)
                throw new AudioInvalidException("Invalid ASCII string size.");

            if (count > 0)
            {
#if NETSTANDARD2_0
                text = _encodings[textType].GetString(
                    (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(frame.Slice(index))), count);
#else
                text = _encodings[textType].GetString(frame.Slice(index, count));
#endif
                index += count;
            }

            index += _nullCharacterLengths[textType];
            return text;
        }

        static string ReadUtf16(Span<byte> frame, ref int index)
        {
            if (index >= frame.Length - 2)
                throw new AudioInvalidException("UTF16 string is not terminated.");

            if (frame[index] == 0xFE && frame[index + 1] == 0xFF)
            {
                index += 2;
                return ReadTextNoPreamble(frame, ref index, TextType.Utf16BigEndian);
            }

            if (frame[index] == 0xFF && frame[index + 1] == 0xFE)
            {
                index += 2;
                return ReadTextNoPreamble(frame, ref index, TextType.Utf16);
            }

            throw new AudioInvalidException("Invalid UTF16 string.");
        }

#if NETSTANDARD2_0
        static unsafe string ReadTextEndNoPreamble(Span<byte> frame, TextType textType) => _encodings[textType]
            .GetString((byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(frame)), frame.Length).TrimEnd('\0');
#else
        static string ReadTextEndNoPreamble(Span<byte> frame, TextType textType) =>
            _encodings[textType].GetString(frame).TrimEnd('\0');
#endif

        static string ReadUtf16End(Span<byte> frame)
        {
             if (frame[0] == 0xFE && frame[1] == 0xFF)
                return ReadTextEndNoPreamble(frame.Slice(2), TextType.Utf16BigEndian);

             if (frame[0] == 0xFF && frame[1] == 0xFE)
                return ReadTextEndNoPreamble(frame.Slice(2), TextType.Utf16);

             throw new AudioInvalidException("Invalid UTF16 string.");
        }
    }
}