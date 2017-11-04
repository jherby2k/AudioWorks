using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;

namespace AudioWorks.Extensions.Apple
{
    [UsedImplicitly]
    sealed class AudioConverterHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public AudioConverterHandle()
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