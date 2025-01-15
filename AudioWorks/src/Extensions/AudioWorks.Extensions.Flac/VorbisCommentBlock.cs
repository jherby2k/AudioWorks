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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace AudioWorks.Extensions.Flac
{
    abstract class VorbisCommentBlock : MetadataBlock
    {
        internal VorbisCommentBlock()
            : base(MetadataType.VorbisComment)
        {
        }

        internal unsafe void Append(string key, string value)
        {
            Span<byte> keyBytes = stackalloc byte[Encoding.ASCII.GetMaxByteCount(key.Length) + 1];
            Span<byte> valueBytes = stackalloc byte[Encoding.UTF8.GetMaxByteCount(value.Length) + 1];

            var keyLength = Encoding.ASCII.GetBytes(key, keyBytes);
            var valueLength = Encoding.UTF8.GetBytes(value, valueBytes);

            // Since SkipLocalsInit is set, make sure the strings are null-terminated
            keyBytes[keyLength] = 0;
            valueBytes[valueLength] = 0;

            SafeNativeMethods.MetadataObjectVorbisCommentEntryFromNameValuePair(
                out var entry,
                new(Unsafe.AsPointer(ref MemoryMarshal.GetReference(keyBytes[..(keyLength + 1)]))),
                new(Unsafe.AsPointer(ref MemoryMarshal.GetReference(valueBytes[..(valueLength + 1)]))));

            // The comment takes ownership of the new entry if 'copy' is false
            SafeNativeMethods.MetadataObjectVorbisCommentAppendComment(Handle, entry, false);
        }
    }
}