using System.Runtime.ConstrainedExecution;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace AudioWorks.Extensions.Lame
{
    [UsedImplicitly]
    sealed class EncoderHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal EncoderHandle()
            : base(true)
        {
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        protected override bool ReleaseHandle()
        {
            SafeNativeMethods.Close(handle);
            return true;
        }
    }
}