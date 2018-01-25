using System.Runtime.ConstrainedExecution;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace AudioWorks.Extensions.Flac
{
    [UsedImplicitly]
    sealed class StreamEncoderHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal StreamEncoderHandle()
            : base(true)
        {
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        protected override bool ReleaseHandle()
        {
            SafeNativeMethods.StreamEncoderDelete(handle);
            return true;
        }
    }
}