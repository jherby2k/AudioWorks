using System.Runtime.ConstrainedExecution;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace AudioWorks.Extensions.Flac
{
    [UsedImplicitly]
    sealed class MetadataChainHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal MetadataChainHandle()
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