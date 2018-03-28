using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    struct StreamInfoMetadataBlock
    {
        readonly MetadataType Type;

        readonly bool IsLast;

        readonly uint Length;

        internal StreamInfo StreamInfo;
    }
}