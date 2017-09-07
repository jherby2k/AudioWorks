using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    struct StreamInfo
    {
        internal uint MinBlockSize;

        internal uint MaxBlockSize;

        internal uint MinFrameSize;

        internal uint MaxFrameSize;

        internal uint SampleRate;

        internal uint Channels;

        internal uint BitsPerSample;

        internal ulong TotalSamples;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        internal byte[] Md5Sum;
    }
}