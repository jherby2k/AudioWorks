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
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    abstract class PictureBlock : MetadataBlock
    {
        internal PictureBlock()
            : base(MetadataType.Picture)
        {
        }

        internal void SetType(PictureType type)
        {
            Marshal.WriteInt32(IntPtr.Add(Handle.DangerousGetHandle(),
                Marshal.OffsetOf<PictureMetadataBlock>("Picture").ToInt32() +
                Marshal.OffsetOf<Picture>("Type").ToInt32()), (int) type);
        }

        internal void SetMimeType([NotNull] string mimeType)
        {
            SafeNativeMethods.MetadataObjectPictureSetMimeType(Handle, mimeType, true);
        }

        internal void SetWidth(int width)
        {
            Marshal.WriteInt32(IntPtr.Add(Handle.DangerousGetHandle(),
                Marshal.OffsetOf<PictureMetadataBlock>("Picture").ToInt32() +
                Marshal.OffsetOf<Picture>("Width").ToInt32()), width);
        }

        internal void SetHeight(int height)
        {
            Marshal.WriteInt32(IntPtr.Add(Handle.DangerousGetHandle(),
                Marshal.OffsetOf<PictureMetadataBlock>("Picture").ToInt32() +
                Marshal.OffsetOf<Picture>("Height").ToInt32()), height);
        }

        internal void SetColorDepth(int depth)
        {
            Marshal.WriteInt32(IntPtr.Add(Handle.DangerousGetHandle(),
                Marshal.OffsetOf<PictureMetadataBlock>("Picture").ToInt32() +
                Marshal.OffsetOf<Picture>("ColorDepth").ToInt32()), depth);
        }

        internal unsafe void SetData(ReadOnlySpan<byte> data)
        {
            fixed (byte* dataAddress = data)
                SafeNativeMethods.MetadataObjectPictureSetData(
                    Handle,
                    dataAddress,
                    (uint) data.Length,
                    true);
        }
    }
}