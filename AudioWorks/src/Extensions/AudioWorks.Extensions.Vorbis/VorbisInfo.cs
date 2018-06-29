using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct VorbisInfo
    {
        readonly int Version;

        internal readonly int Channels;

        internal readonly int Rate;

        readonly int BitrateUpper;

        internal readonly int BitrateNominal;

        readonly int BitrateLower;

        readonly int BitrateWindow;

        readonly IntPtr CodecSetup;
    }
}