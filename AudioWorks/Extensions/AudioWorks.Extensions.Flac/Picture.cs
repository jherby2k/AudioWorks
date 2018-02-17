using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    struct Picture
    {
        readonly PictureType Type;

        readonly IntPtr MimeType;

        readonly IntPtr Description;

        readonly uint Width;

        readonly uint Height;

        readonly uint ColorDepth;

        readonly uint Colors;

        readonly uint DataLength;

        readonly IntPtr Data;
    }
}