using System;

#pragma warning disable 169, 649

namespace AudioWorks.Extensions.Vorbis
{
    struct VorbisInfo
    {
        internal int Version;

        internal int Channels;

        internal int Rate;

        internal int BitrateUpper;

        internal int BitrateNominal;

        internal int BitrateLower;

        internal int BitrateWindow;

        internal IntPtr CodecSetup;
    }
}

#pragma warning restore 169, 649