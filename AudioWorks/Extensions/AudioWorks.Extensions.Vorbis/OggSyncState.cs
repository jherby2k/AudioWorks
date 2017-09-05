using System;

namespace AudioWorks.Extensions.Vorbis
{
    struct OggSyncState
    {
        internal IntPtr Data;

        internal int Storage;

        internal int Fill;

        internal int Returned;

        internal int Unsynced;

        internal int HeaderBytes;

        internal int BodyBytes;
    }
}