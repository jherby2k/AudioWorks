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
using System.Text;

namespace AudioWorks.Extensions.Id3
{
    static class TextBuilder
    {
        static Dictionary<TextType, Encoding> _encodings = new Dictionary<TextType, Encoding>
        {
            [TextType.Ascii] = CodePagesEncodingProvider.Instance.GetEncoding(1252),
            [TextType.Utf16] = Encoding.Unicode,
            [TextType.Utf8] = new UTF8Encoding(false)
        };

        internal static string ReadText(byte[] frame, ref int index, TextType textType) =>
            textType switch
            {
                TextType.Ascii => ReadAscii(frame, ref index),
                TextType.Utf16 => ReadUtf16(frame, ref index),
                TextType.Utf16BigEndian => ReadUtf16BigEndian(frame, ref index),
                TextType.Utf8 => ReadUtf8(frame, ref index),
                _ => throw new InvalidFrameException("Invalid text type.")
            };

        internal static string ReadTextEnd(byte[] frame, int index, TextType textType) =>
            textType switch
            {
                TextType.Ascii => ReadAsciiEnd(frame, index),
                TextType.Utf16 => ReadUtf16End(frame, index),
                TextType.Utf16BigEndian => ReadUtf16BigEndianEnd(frame, index),
                TextType.Utf8 => ReadUtf8End(frame, index),
                _ => throw new InvalidFrameException("Invalid text type.")
            };

        internal static string ReadAscii(byte[] frame, ref int index)
        {
            var text = string.Empty;
            var count = Memory.FindByte(frame, 0, index);
            if (count == -1)
                throw new InvalidFrameException("Invalid ASCII string size");

            if (count > 0)
            {
                text = CodePagesEncodingProvider.Instance.GetEncoding(1252).GetString(frame, index, count);
                index += count; // add the read bytes
            }

            index++; // jump an end of line byte
            return text;
        }

        static string ReadUtf16(byte[] frame, ref int index)
        {
            // check for empty string first, and throw a useful exception
            // otherwise we'll get an out-of-range exception when we look for the BOM
            if (index >= frame.Length - 2)
                throw new InvalidFrameException("ReadUTF16: string must be terminated");

            if (frame[index] == 0xfe && frame[index + 1] == 0xff) // Big Endian
            {
                index += 2;
                return ReadUtf16BigEndian(frame, ref index);
            }

            if (frame[index] == 0xff && frame[index + 1] == 0xfe) // Little Endian
            {
                index += 2;
                return ReadUtf16LittleEndian(frame, ref index);
            }

            if (frame[index] == 0x00 && frame[index + 1] == 0x00) // empty string
            {
                index += 2;
                return string.Empty;
            }

            throw new InvalidFrameException("Invalid UTF16 string.");
        }

        static string ReadUtf16BigEndian(byte[] frame, ref int index)
        {
            var count = Memory.FindShort(frame, 0, index);
            if (count == -1)
                throw new InvalidFrameException("Invalid UTF16BE string size");

            // we can safely let count==0 fall through
            var text = new UnicodeEncoding(true, false).GetString(frame, index, count);
            index += count; // add the bytes read
            index += 2; // skip the EOL
            return text;
        }

        static string ReadUtf16LittleEndian(byte[] frame, ref int index)
        {
            var count = Memory.FindShort(frame, 0, index);
            if (count == -1)
                throw new InvalidFrameException("Invalid UTF16LE string size");

            // we can safely let count==0 fall through
            var text = new UnicodeEncoding(false, false).GetString(frame, index, count);
            index += count; // add the bytes read
            index += 2; // skip the EOL
            return text;
        }

        static string ReadUtf8(byte[] frame, ref int index)
        {
            string text = string.Empty;
            var count = Memory.FindByte(frame, 0, index);
            if (count == -1)
                throw new InvalidFrameException("Invalid UTF8 string size");
            if (count > 0)
            {
                text = Encoding.UTF8.GetString(frame, index, count);
                index += count; // add the read bytes
            }

            index++; // jump an end of line byte
            return text;
        }

        static string ReadAsciiEnd(byte[] frame, int index) => CodePagesEncodingProvider.Instance.GetEncoding(1252)
            .GetString(frame, index, frame.Length - index).TrimEnd('\0');

        static string ReadUtf16End(byte[] frame, int index)
        {
            // check for empty string first
            // otherwise we'll get an exception when we look for the BOM
            // SourceForge bug ID: 2686976
            if (index >= frame.Length - 2)
                return string.Empty;

            if (frame[index] == 0xfe && frame[index + 1] == 0xff) // Big Endian
                return ReadUtf16BigEndianEnd(frame, index + 2);

            if (frame[index] == 0xff && frame[index + 1] == 0xfe) // Little Endian
                return ReadUtf16LittleEndianEnd(frame, index + 2);

            throw new InvalidFrameException("Invalid UTF16 string.");
        }

        static string ReadUtf16BigEndianEnd(byte[] frame, int index) => new UnicodeEncoding(true, false)
            .GetString(frame, index, frame.Length - index).TrimEnd('\0');

        static string ReadUtf16LittleEndianEnd(byte[] frame, int index) => new UnicodeEncoding(false, false)
            .GetString(frame, index, frame.Length - index).TrimEnd('\0');

        static string ReadUtf8End(byte[] frame, int index) => Encoding.UTF8
                .GetString(frame, index, frame.Length - index).TrimEnd('\0');

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
    }
}