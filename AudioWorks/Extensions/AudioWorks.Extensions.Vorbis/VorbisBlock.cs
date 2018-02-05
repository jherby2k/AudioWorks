using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct VorbisBlock
    {
        internal IntPtr Pcm;

        internal OggPackBuffer OggPackBuffer;

        internal int Lw;

        internal int W;

        internal int Nw;

        internal int PcmEnd;

        internal int Mode;

        internal int EoffLag;

        internal long GranulePosition;

        internal long Sequence;

        internal IntPtr DspState;

        internal IntPtr LocalStore;

        internal int LocalTop;

        internal int LocalAlloc;

        internal int TotalUse;

        internal IntPtr Reap;

        internal int GlueBits;

        internal int TimeBits;

        internal int FloorBits;

        internal int ResBits;

        internal IntPtr Internal;
    }
}