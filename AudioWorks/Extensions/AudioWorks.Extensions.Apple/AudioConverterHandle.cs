using System.Runtime.ConstrainedExecution;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace AudioWorks.Extensions.Apple
{
    [UsedImplicitly]
    sealed class AudioConverterHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal AudioConverterHandle()
            : base(true)
        {
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        protected override bool ReleaseHandle()
        {
            SafeNativeMethods.AudioConverterDispose(handle);
            return true;
        }
    }
}