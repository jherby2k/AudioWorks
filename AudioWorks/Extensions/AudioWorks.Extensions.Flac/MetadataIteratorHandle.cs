using System.Runtime.ConstrainedExecution;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace AudioWorks.Extensions.Flac
{
    [UsedImplicitly]
    sealed class MetadataIteratorHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal MetadataIteratorHandle()
            : base(true)
        {
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        protected override bool ReleaseHandle()
        {
            SafeNativeMethods.MetadataIteratorDelete(handle);
            return true;
        }
    }
}