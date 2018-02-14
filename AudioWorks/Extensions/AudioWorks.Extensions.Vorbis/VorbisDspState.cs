using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct VorbisDspState
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

        readonly int Lw;

        readonly int W;

        readonly int Nw;

        readonly int CenterW;

        readonly long GranulePosition;

        readonly long Sequence;

        readonly long GlueBits;

        readonly long TimeBits;

        readonly long FloorBits;

        readonly long ResBits;

        readonly IntPtr BackendState;
    }
}