using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct VorbisBlock
    {
        readonly IntPtr Pcm;

        readonly OggPackBuffer OggPackBuffer;

#if WINDOWS
        readonly int Lw;

        readonly int W;

        readonly int Nw;
#else
        readonly long Lw;

        readonly long W;

        readonly long Nw;
#endif

        readonly int PcmEnd;

        readonly int Mode;

        readonly int EoffLag;

        readonly long GranulePosition;

        readonly long Sequence;

        readonly IntPtr DspState;

        readonly IntPtr LocalStore;

#if WINDOWS
        readonly int LocalTop;

        readonly int LocalAlloc;

        readonly int TotalUse;
#else
        readonly long LocalTop;

        readonly long LocalAlloc;

        readonly long TotalUse;
#endif

        readonly IntPtr Reap;

#if WINDOWS
        readonly int GlueBits;

        readonly int TimeBits;

        readonly int FloorBits;

        readonly int ResBits;
#else
        readonly long GlueBits;

        readonly long TimeBits;

        readonly long FloorBits;

        readonly long ResBits;
#endif

        readonly IntPtr Internal;
    }
}