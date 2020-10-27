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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

        internal static string ReadText(Span<byte> frame, ref int index, TextType textType) =>
            textType switch
            {
                TextType.Ascii => ReadAscii(frame, ref index),
                TextType.Utf16 => ReadUtf16(frame, ref index),
                TextType.Utf16BigEndian => ReadUtf16BigEndian(frame, ref index),
                TextType.Utf8 => ReadUtf8(frame, ref index),
                _ => throw new InvalidFrameException("Invalid text type.")
            };

        internal static string ReadTextEnd(Span<byte> frame, TextType textType) =>
            textType switch
            {
                TextType.Ascii => ReadAsciiEnd(frame),
                TextType.Utf16 => ReadUtf16End(frame),
                TextType.Utf16BigEndian => ReadUtf16BigEndianEnd(frame),
                TextType.Utf8 => ReadUtf8End(frame),
                _ => throw new InvalidFrameException("Invalid text type.")
            };

        internal static unsafe string ReadAscii(Span<byte> frame, ref int index)
        {
            var text = string.Empty;
            var count = frame.Slice(index).IndexOf((byte) 0);
            if (count == -1)
                throw new InvalidFrameException("Invalid ASCII string size");

            if (count > 0)
            {
#if NETSTANDARD2_0
                text = CodePagesEncodingProvider.Instance.GetEncoding(1252).GetString(
                    (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(frame.Slice(index))), count);
#else
                text = CodePagesEncodingProvider.Instance.GetEncoding(1252).GetString(frame.Slice(index, count));
#endif
                index += count; // add the read bytes
            }

            index++; // jump an end of line byte
            return text;
        }

        static string ReadUtf16(Span<byte> frame, ref int index)
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

        static unsafe string ReadUtf16BigEndian(Span<byte> frame, ref int index)
        {
            
            var count = MemoryMarshal.Cast<byte, short>(frame.Slice(index)).IndexOf((short) 0);
            if (count == -1)
                throw new InvalidFrameException("Invalid UTF16BE string size");

            // we can safely let count==0 fall through
#if NETSTANDARD2_0
            var text = new UnicodeEncoding(true, false).GetString(
                (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(frame.Slice(index))), count);
#else
            var text = new UnicodeEncoding(true, false).GetString(frame.Slice(index, count));
#endif
            index += count; // add the bytes read
            index += 2; // skip the EOL
            return text;
        }

        static unsafe string ReadUtf16LittleEndian(Span<byte> frame, ref int index)
        {
            var count = MemoryMarshal.Cast<byte, short>(frame.Slice(index)).IndexOf((short) 0);
            if (count == -1)
                throw new InvalidFrameException("Invalid UTF16LE string size");

            // we can safely let count==0 fall through
#if NETSTANDARD2_0
            var text = new UnicodeEncoding(true, false).GetString(
                (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(frame.Slice(index))), count);
#else
            var text = new UnicodeEncoding(false, false).GetString(frame.Slice(index, count));
#endif
            index += count; // add the bytes read
            index += 2; // skip the EOL
            return text;
        }

        static unsafe string ReadUtf8(Span<byte> frame, ref int index)
        {
            string text = string.Empty;
            var count = frame.Slice(index).IndexOf((byte) 0);
            if (count == -1)
                throw new InvalidFrameException("Invalid UTF8 string size");
            if (count > 0)
            {
#if NETSTANDARD2_0
                text = Encoding.UTF8.GetString(
                    (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(frame.Slice(index))), count);
#else
                text = Encoding.UTF8.GetString(frame.Slice(index, count));
#endif
                index += count; // add the read bytes
            }

            index++; // jump an end of line byte
            return text;
        }

        static unsafe string ReadAsciiEnd(Span<byte> frame)
        {
#if NETSTANDARD2_0
            return CodePagesEncodingProvider.Instance.GetEncoding(1252).GetString(
                (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(frame)), frame.Length).TrimEnd('\0');
#else
            return CodePagesEncodingProvider.Instance.GetEncoding(1252).GetString(frame).TrimEnd('\0');
#endif
        }

        static string ReadUtf16End(Span<byte> frame)
        {
            if (frame[0] == 0xFE && frame[1] == 0xFF)
                return ReadUtf16BigEndianEnd(frame.Slice(2));

            if (frame[0] == 0xFF && frame[1] == 0xFE)
                return ReadUtf16LittleEndianEnd(frame.Slice(2));

            throw new InvalidFrameException("Invalid UTF16 string.");
        }

        static unsafe string ReadUtf16BigEndianEnd(Span<byte> frame)
        {
#if NETSTANDARD2_0
            return new UnicodeEncoding(true, false).GetString(
                (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(frame)), frame.Length).TrimEnd('\0');
#else
            return new UnicodeEncoding(true, false).GetString(frame).TrimEnd('\0');
#endif
        }

        static unsafe string ReadUtf16LittleEndianEnd(Span<byte> frame)
        {
#if NETSTANDARD2_0
            return new UnicodeEncoding(false, false).GetString(
                (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(frame)), frame.Length).TrimEnd('\0');
#else
            return new UnicodeEncoding(false, false).GetString(frame).TrimEnd('\0');
#endif
        }

        static unsafe string ReadUtf8End(Span<byte> frame)
        {
#if NETSTANDARD2_0
            return Encoding.UTF8.GetString(
                (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(frame)), frame.Length).TrimEnd('\0');
#else
            return Encoding.UTF8.GetString(frame).TrimEnd('\0');
#endif
        }

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