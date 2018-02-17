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

        internal void SetData([NotNull] byte[] data)
        {
            SafeNativeMethods.MetadataObjectPictureSetData(Handle, data, (uint) data.Length, true);
        }
    }
}