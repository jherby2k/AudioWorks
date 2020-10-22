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
using System.Buffers.Binary;

namespace AudioWorks.Extensions.Id3
{
    static class Memory
    {
        internal static bool Compare(ReadOnlySpan<byte> b1, ReadOnlySpan<byte> b2) => b1.SequenceCompareTo(b2) == 0;

        internal static byte[] Extract(ReadOnlySpan<byte> src, int srcIndex, int count)
        {
            if (src == null || srcIndex < 0 || count < 0)
                throw new InvalidOperationException();

            if (src.Length - srcIndex < count)
                throw new InvalidOperationException();

            return src.Slice(srcIndex, count).ToArray();
        }

        internal static int FindByte(ReadOnlySpan<byte> src, byte val, int index)
        {
            if (index > src.Length)
                throw new InvalidOperationException();

            return src.Slice(index).IndexOf(val);
        }

        internal static int FindShort(ReadOnlySpan<byte> src, short val, int index)
        {
            var size = src.Length;
            if (index > size)
                throw new InvalidOperationException();

            for (var n = index; n < size; n += 2)
                if (BinaryPrimitives.ReadInt16LittleEndian(src.Slice(n)) == val)
                    return n - index;
            return -1;
        }

        internal static void Clear(Span<byte> dst, int begin, int end)
        {
            if (begin > end || begin > dst.Length || end > dst.Length)
                throw new InvalidOperationException();

            dst.Slice(begin, end - begin).Clear();
        }
    }
}