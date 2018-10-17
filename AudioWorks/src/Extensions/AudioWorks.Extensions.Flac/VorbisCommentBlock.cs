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
            Span<byte> keySpan = stackalloc byte[Encoding.ASCII.GetMaxByteCount(key.Length) + 1];
            Span<byte> valueSpan = stackalloc byte[Encoding.UTF8.GetMaxByteCount(value.Length) + 1];

#if NETCOREAPP2_1
            var keyLength = Encoding.ASCII.GetBytes(key, keySpan) + 1;
            var valueLength = Encoding.UTF8.GetBytes(value, valueSpan) + 1;
#else
            var keyLength = Encoding.ASCII.GetBytes(
                                (char*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(key.AsSpan())),
                                key.Length,
                                (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(keySpan)),
                                keySpan.Length)
                            + 1;

            var valueLength = Encoding.UTF8.GetBytes(
                                  (char*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(value.AsSpan())),
                                  value.Length,
                                  (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(valueSpan)),
                                  valueSpan.Length)
                              + 1;
#endif

            SafeNativeMethods.MetadataObjectVorbisCommentEntryFromNameValuePair(
                out var entry,
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(keySpan.Slice(0, keyLength)))),
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(valueSpan.Slice(0, valueLength)))));

            // The comment takes ownership of the new entry if 'copy' is false
            SafeNativeMethods.MetadataObjectVorbisCommentAppendComment(Handle, entry, false);
        }
    }
}