using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct OggStreamState
    {
        readonly IntPtr BodyData;

#if (WINDOWS)
        readonly int BodyStorage;

        readonly int BodyFill;

        readonly int BodyReturned;
#else
        readonly long BodyStorage;

        readonly long BodyFill;

        readonly long BodyReturned;
#endif

        readonly IntPtr LacingValues;

        readonly IntPtr GranuleValues;

#if (WINDOWS)
        readonly int LacingStorage;

        readonly int LacingFill;

        readonly int LacingPacket;

        readonly int LacingReturned;
#else
        readonly long LacingStorage;

        readonly long LacingFill;

        readonly long LacingPacket;

        readonly long LacingReturned;
#endif

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 282)]
        readonly byte[] Header;

        readonly int HeaderFill;

        readonly int EndOfStream;

        readonly int BeginningOfStream;

#if (WINDOWS)
        internal readonly int SerialNumber;
#else
        internal readonly long SerialNumber;
#endif

        readonly int PageNumber;

        readonly long PacketNumber;

        readonly long GranulePosition;
    }
}