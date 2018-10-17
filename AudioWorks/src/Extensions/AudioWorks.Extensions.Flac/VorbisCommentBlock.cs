/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    abstract class VorbisCommentBlock : MetadataBlock
    {
        internal VorbisCommentBlock()
            : base(MetadataType.VorbisComment)
        {
        }

        internal unsafe void Append([NotNull] string key, [NotNull] string value)
        {
            Span<byte> keyBytes = stackalloc byte[Encoding.ASCII.GetMaxByteCount(key.Length) + 1];
            Span<byte> valueBytes = stackalloc byte[Encoding.UTF8.GetMaxByteCount(value.Length) + 1];

#if NETCOREAPP2_1
            var keyByteCount = Encoding.ASCII.GetBytes(key, keyBytes) + 1;
            var valueByteCount = Encoding.UTF8.GetBytes(value, valueBytes) + 1;
#else
            var keyByteCount = 1;
            fixed (char* keyAddress = key)
                keyByteCount += Encoding.ASCII.GetBytes(
                    keyAddress, key.Length,
                    (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(keyBytes)), keyBytes.Length);

            var valueByteCount = 1;
            fixed (char* valueAddress = value)
                valueByteCount += Encoding.UTF8.GetBytes(
                    valueAddress, value.Length,
                    (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(valueBytes)), valueBytes.Length);
#endif

            SafeNativeMethods.MetadataObjectVorbisCommentEntryFromNameValuePair(
                out var entry,
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(keyBytes.Slice(0, keyByteCount)))),
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(valueBytes.Slice(0, valueByteCount)))));

            // The comment takes ownership of the new entry if 'copy' is false
            SafeNativeMethods.MetadataObjectVorbisCommentAppendComment(Handle, entry, false);
        }
    }
}