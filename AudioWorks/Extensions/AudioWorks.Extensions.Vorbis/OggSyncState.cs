using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    struct OggSyncState
    {
        readonly IntPtr Data;

        readonly int Storage;

        readonly int Fill;

        readonly int Returned;

        readonly int Unsynced;

        readonly int HeaderBytes;

        readonly int BodyBytes;
    }
}