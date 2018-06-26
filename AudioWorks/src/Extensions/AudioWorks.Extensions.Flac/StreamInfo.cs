using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct StreamInfo
    {
        readonly uint MinBlockSize;

        readonly uint MaxBlockSize;

        readonly uint MinFrameSize;

        readonly uint MaxFrameSize;

        internal readonly uint SampleRate;

        internal readonly uint Channels;

        internal readonly uint BitsPerSample;

        internal readonly ulong TotalSamples;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] readonly byte[] Md5Sum;
    }
}