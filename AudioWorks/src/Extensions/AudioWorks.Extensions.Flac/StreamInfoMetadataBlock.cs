using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct StreamInfoMetadataBlock
    {
        readonly MetadataType Type;

        readonly bool IsLast;

        readonly uint Length;

        internal readonly StreamInfo StreamInfo;
    }
}