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

        readonly int BitRateUpper;

        internal readonly int BitRateNominal;

        readonly int BitRateLower;

        readonly int BitRateWindow;

        readonly IntPtr CodecSetup;
    }
}