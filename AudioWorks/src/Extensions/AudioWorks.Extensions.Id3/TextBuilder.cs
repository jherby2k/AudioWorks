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

using System.IO;
using System.Text;

namespace AudioWorks.Extensions.Id3
{
    static class TextBuilder
    {
        internal static string ReadText(byte[] frame, ref int index, TextType textType)
        {
            switch (textType)
            {
                case TextType.Ascii:
                    return ReadAscii(frame, ref index);
                case TextType.Utf16:
                    return ReadUtf16(frame, ref index);
                case TextType.Utf16BigEndian:
                    return ReadUtf16BigEndian(frame, ref index);
                case TextType.Utf8:
                    return ReadUtf8(frame, ref index);
                default:
                    throw new InvalidFrameException("Invalid text type.");
            }
        }

        internal static string ReadTextEnd(byte[] frame, int index, TextType textType)
        {
            switch (textType)
            {
                case TextType.Ascii:
                        return ReadAsciiEnd(frame, index);
                case TextType.Utf16:
                        return ReadUtf16End(frame, index);
                case TextType.Utf16BigEndian:
                        return ReadUtf16BigEndianEnd(frame, index);
                case TextType.Utf8:
                        return ReadUtf8End(frame, index);
                default:
                        throw new InvalidFrameException("Invalid text type.");
            }
        }

        internal static string ReadAscii(byte[] frame, ref int index)
        {
            var text = string.Empty;
            var count = Memory.FindByte(frame, 0, index);
            if (count == -1)
                throw new InvalidFrameException("Invalid ASCII string size");

            if (count > 0)
            {
                var encoding = CodePagesEncodingProvider.Instance.GetEncoding(1252); // Should be ASCII
                text = encoding.GetString(frame, index, count);
                index += count; // add the read bytes
            }

            index++; // jump an end of line byte
            return text;
        }

        internal static byte[] WriteText(string text, TextType textType)
        {
            switch (textType)
            {
                case TextType.Ascii:
                    return WriteAscii(text);
                case TextType.Utf16:
                    return WriteUtf16(text);
                case TextType.Utf16BigEndian:
                    return WriteUtf16BigEndian(text);
                case TextType.Utf8:
                    return WriteUtf8(text);
                default:
                    throw new InvalidFrameException("Invalid text type.");
            }
        }

        internal static byte[] WriteTextEnd(string text, TextType textType)
        {
            switch (textType)
            {
                case TextType.Ascii:
                    return WriteAsciiEnd(text);
                case TextType.Utf16:
                    return WriteUtf16End(text);
                case TextType.Utf16BigEndian:
                    return WriteUtf16BigEndianEnd(text);
                case TextType.Utf8:
                    return WriteUtf8End(text);
                default:
                    throw new InvalidFrameException("Invalid text type.");
            }
        }

        internal static byte[] WriteAscii(string text)
        {
            using (var buffer = new MemoryStream())
            using (var writer = new BinaryWriter(buffer, Encoding.UTF8, true))
            {
                if (string.IsNullOrEmpty(text)) //Write a null string
                {
                    writer.Write((byte) 0);
                    return buffer.ToArray();
                }

                writer.Write(CodePagesEncodingProvider.Instance.GetEncoding(1252).GetBytes(text));
                writer.Write((byte) 0); //EOL
                return buffer.ToArray();
            }
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
            var encoding = new UnicodeEncoding(true, false);
            var count = Memory.FindShort(frame, 0, index);
            if (count == -1)
                throw new InvalidFrameException("Invalid UTF16BE string size");

            // we can safely let count==0 fall through
            var text = encoding.GetString(frame, index, count);
            index += count; // add the bytes read
            index += 2; // skip the EOL
            return text;
        }

        static string ReadUtf16LittleEndian(byte[] frame, ref int index)
        {
            var encoding = new UnicodeEncoding(false, false);
            var count = Memory.FindShort(frame, 0, index);
            if (count == -1)
                throw new InvalidFrameException("Invalid UTF16LE string size");

            // we can safely let count==0 fall through
            var text = encoding.GetString(frame, index, count);
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

        static string ReadAsciiEnd(byte[] frame, int index)
        {
            return CodePagesEncodingProvider.Instance.GetEncoding(1252).GetString(frame, index, frame.Length - index);
        }

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

        static string ReadUtf16BigEndianEnd(byte[] frame, int index)
        {
            return new UnicodeEncoding(true, false).GetString(frame, index, frame.Length - index);
        }

        static string ReadUtf16LittleEndianEnd(byte[] frame, int index)
        {
            return new UnicodeEncoding(false, false).GetString(frame, index, frame.Length - index);
        }

        static string ReadUtf8End(byte[] frame, int index)
        {
            return Encoding.UTF8.GetString(frame, index, frame.Length - index);
        }

        static byte[] WriteUtf16(string text)
        {
            using (var buffer = new MemoryStream())
            using (var writer = new BinaryWriter(buffer, Encoding.UTF8, true))
            {
                if (string.IsNullOrEmpty(text)) //Write a null string
                {
                    writer.Write((ushort) 0);
                    return buffer.ToArray();
                }

                writer.Write((byte) 0xff); //Little endian, we have UTF16BE for big endian
                writer.Write((byte) 0xfe);
                writer.Write(new UnicodeEncoding(false, false).GetBytes(text));
                writer.Write((ushort) 0);
                return buffer.ToArray();
            }
        }

        static byte[] WriteUtf16BigEndian(string text)
        {
            using (var buffer = new MemoryStream())
            using (var writer = new BinaryWriter(buffer, Encoding.UTF8, true))
            {
                if (string.IsNullOrEmpty(text)) //Write a null string
                {
                    writer.Write((ushort) 0);
                    return buffer.ToArray();
                }

                writer.Write(new UnicodeEncoding(true, false).GetBytes(text));
                writer.Write((ushort) 0);
                return buffer.ToArray();
            }
        }

        static byte[] WriteUtf8(string text)
        {
            using (var buffer = new MemoryStream())
            using (var writer = new BinaryWriter(buffer, Encoding.UTF8, true))
            {
                if (string.IsNullOrEmpty(text)) //Write a null string
                {
                    writer.Write((byte) 0);
                    return buffer.ToArray();
                }

                writer.Write(Encoding.UTF8.GetBytes(text));
                writer.Write((byte) 0);
                return buffer.ToArray();
            }
        }

        static byte[] WriteAsciiEnd(string text)
        {
            using (var buffer = new MemoryStream())
            using (var writer = new BinaryWriter(buffer, Encoding.UTF8, true))
            {
                if (string.IsNullOrEmpty(text))
                    return buffer.ToArray();

                writer.Write(CodePagesEncodingProvider.Instance.GetEncoding(1252).GetBytes(text));
                return buffer.ToArray();
            }
        }

        static byte[] WriteUtf16End(string text)
        {
            using (var buffer = new MemoryStream(text.Length + 2))
            using (var writer = new BinaryWriter(buffer, Encoding.UTF8, true))
            {
                if (string.IsNullOrEmpty(text)) //Write a null string
                    return buffer.ToArray();
                writer.Write((byte) 0xff); // Little endian
                writer.Write((byte) 0xfe);
                writer.Write(new UnicodeEncoding(false, false).GetBytes(text));
                return buffer.ToArray();
            }
        }

        static byte[] WriteUtf16BigEndianEnd(string text)
        {
            using (var buffer = new MemoryStream())
            using (var writer = new BinaryWriter(buffer, Encoding.UTF8, true))
            {
                if (string.IsNullOrEmpty(text)) //Write a null string
                    return buffer.ToArray();
                writer.Write(new UnicodeEncoding(true, false).GetBytes(text));
                return buffer.ToArray();
            }
        }

        static byte[] WriteUtf8End(string text)
        {
            using (var buffer = new MemoryStream())
            using (var writer = new BinaryWriter(buffer, Encoding.UTF8, true))
            {
                if (string.IsNullOrEmpty(text)) //Write a null string
                    return buffer.ToArray();

                writer.Write(Encoding.UTF8.GetBytes(text));
                return buffer.ToArray();
            }
        }
    }
}