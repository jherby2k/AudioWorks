using System;

namespace AudioWorks.Extensions.Vorbis
{
    struct OggPage
    {
        internal IntPtr Header;

        internal int HeaderLength;

        internal IntPtr Body;

        internal int BodyLength;
    }
}