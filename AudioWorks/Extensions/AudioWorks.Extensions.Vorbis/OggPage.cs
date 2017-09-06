using System;

#pragma warning disable 169, 649

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

#pragma warning restore 169, 649