using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    struct OggStreamState
    {
        internal IntPtr BodyData;

        internal int BodyStorage;

        internal int BodyFill;

        internal int BodyReturned;

        internal IntPtr LacingValues;

        internal IntPtr GranuleValues;

        internal int LacingStorage;

        internal int LacingFill;

        internal int LacingPacket;

        internal int LacingReturned;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 282)]
        internal char[] Header;

        internal int HeaderFill;

        internal int EndOfStream;

        internal int BeginningOfStream;

        internal int SerialNumber;

        internal int PageNumber;

        internal long PacketNumber;

        internal long GranulePosition;
    }
}