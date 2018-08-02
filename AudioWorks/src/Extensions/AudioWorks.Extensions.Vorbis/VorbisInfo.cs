using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct VorbisInfo
    {
        readonly int Version;

        internal readonly int Channels;

#if WINDOWS
        internal readonly int Rate;

        readonly int BitRateUpper;

        internal readonly int BitRateNominal;

        readonly int BitRateLower;

        readonly int BitRateWindow;
#else
        internal readonly long Rate;

        readonly long BitRateUpper;

        internal readonly long BitRateNominal;

        readonly long BitRateLower;

        readonly long BitRateWindow;
#endif
        readonly IntPtr CodecSetup;
    }
}