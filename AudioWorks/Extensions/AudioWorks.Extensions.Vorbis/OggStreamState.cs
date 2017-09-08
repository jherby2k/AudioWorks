using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct OggStreamState
    {
        readonly IntPtr BodyData;

        readonly int BodyStorage;

        readonly int BodyFill;

        readonly int BodyReturned;

        readonly IntPtr LacingValues;

        readonly IntPtr GranuleValues;

        readonly int LacingStorage;

        readonly int LacingFill;

        readonly int LacingPacket;

        readonly int LacingReturned;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 282)]
        readonly char[] Header;

        readonly int HeaderFill;

        readonly int EndOfStream;

        readonly int BeginningOfStream;

        readonly int SerialNumber;

        readonly int PageNumber;

        readonly long PacketNumber;

        readonly long GranulePosition;
    }
}