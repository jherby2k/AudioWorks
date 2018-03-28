using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    sealed class PaddingBlock : MetadataBlock
    {
        internal PaddingBlock(int length)
            : base(MetadataType.Padding)
        {
            Marshal.WriteIntPtr(Handle.DangerousGetHandle(), 8, new IntPtr(length));
        }
    }
}