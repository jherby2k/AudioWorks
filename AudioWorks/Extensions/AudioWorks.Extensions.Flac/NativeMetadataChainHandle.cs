using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;

namespace AudioWorks.Extensions.Flac
{
    [UsedImplicitly]
    sealed class NativeMetadataChainHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal NativeMetadataChainHandle()
            : base(true)
        {
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        protected override bool ReleaseHandle()
        {
            SafeNativeMethods.MetadataChainDelete(handle);
            return true;
        }
    }
}