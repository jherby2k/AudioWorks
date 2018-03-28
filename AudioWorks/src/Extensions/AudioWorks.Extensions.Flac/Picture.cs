using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    struct Picture
    {
        internal readonly PictureType Type;

        readonly IntPtr MimeType;

        readonly IntPtr Description;

        readonly uint Width;

        readonly uint Height;

        readonly uint ColorDepth;

        readonly uint Colors;

        internal readonly uint DataLength;

        internal IntPtr Data;
    }
}