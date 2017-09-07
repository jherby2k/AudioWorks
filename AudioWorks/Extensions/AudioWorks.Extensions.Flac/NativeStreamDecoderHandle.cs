using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;

namespace AudioWorks.Extensions.Flac
{
    [UsedImplicitly]
    sealed class NativeStreamDecoderHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public NativeStreamDecoderHandle()
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