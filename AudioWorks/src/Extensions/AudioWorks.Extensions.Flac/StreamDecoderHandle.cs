using System.Runtime.ConstrainedExecution;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace AudioWorks.Extensions.Flac
{
    [UsedImplicitly]
    sealed class StreamDecoderHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal StreamDecoderHandle()
            : base(true)
        {
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        protected override bool ReleaseHandle()
        {
            SafeNativeMethods.StreamDecoderDelete(handle);
            return true;
        }
    }
}