using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct VorbisBlock
    {
        readonly IntPtr Pcm;

        readonly OggPackBuffer OggPackBuffer;

        readonly int Lw;

        readonly int W;

        readonly int Nw;

        readonly int PcmEnd;

        readonly int Mode;

        readonly int EoffLag;

        readonly long GranulePosition;

        readonly long Sequence;

        readonly IntPtr DspState;

        readonly IntPtr LocalStore;

        readonly int LocalTop;

        readonly int LocalAlloc;

        readonly int TotalUse;

        readonly IntPtr Reap;

        readonly int GlueBits;

        readonly int TimeBits;

        readonly int FloorBits;

        readonly int ResBits;

        readonly IntPtr Internal;
    }
}