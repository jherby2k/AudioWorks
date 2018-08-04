using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct VorbisDspState
    {
        readonly int AnalysisP;

        readonly IntPtr VorbisInfo;

        readonly IntPtr Pcm;

        readonly IntPtr PcmRet;

        readonly int PcmStorage;

        readonly int PcmCurrent;

        readonly int PcmReturned;

        readonly int PreExtrapolate;

        readonly int EofFlag;

#if WINDOWS
        readonly int Lw;

        readonly int W;

        readonly int Nw;

        readonly int CenterW;
#else
        readonly long Lw;

        readonly long W;

        readonly long Nw;

        readonly long CenterW;
#endif

        readonly long GranulePosition;

        readonly long Sequence;

        readonly long GlueBits;

        readonly long TimeBits;

        readonly long FloorBits;

        readonly long ResBits;

        readonly IntPtr BackendState;
    }
}