using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;

namespace AudioWorks.Extensions.Flac
{
    [UsedImplicitly]
    sealed class NativeMetadataBlockHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        bool _dropOwnership;

        internal NativeMetadataBlockHandle()
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