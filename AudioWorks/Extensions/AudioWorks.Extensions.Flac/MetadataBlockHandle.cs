using System.Runtime.ConstrainedExecution;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace AudioWorks.Extensions.Flac
{
    [UsedImplicitly]
    sealed class MetadataBlockHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        bool _dropOwnership;

        internal MetadataBlockHandle()
            : base(true)
        {
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        protected override bool ReleaseHandle()
        {
            if (!_dropOwnership)
                SafeNativeMethods.MetadataObjectDelete(handle);
            return true;
        }

        internal void DropOwnership()
        {
            _dropOwnership = true;
        }
    }
}