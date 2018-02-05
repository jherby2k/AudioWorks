using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct VorbisDspState
    {
        internal int AnalysisP;

        internal IntPtr VorbisInfo;

        internal IntPtr Pcm;

        internal IntPtr PcmRet;

        internal int PcmStorage;

        internal int PcmCurrent;

        internal int PcmReturned;

        internal int PreExtrapolate;

        internal int EofFlag;

        internal int Lw;

        internal int W;

        internal int Nw;

        internal int CenterW;

        internal long GranulePosition;

        internal long Sequence;

        internal long GlueBits;

        internal long TimeBits;

        internal long FloorBits;

        internal long ResBits;

        internal IntPtr BackendState;
    }
}