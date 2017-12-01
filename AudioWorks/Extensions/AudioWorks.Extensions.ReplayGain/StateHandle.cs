using System.Runtime.ConstrainedExecution;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace AudioWorks.Extensions.ReplayGain
{
    [UsedImplicitly]
    sealed class StateHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal StateHandle()
            : base(true)
        {
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        protected override bool ReleaseHandle()
        {
            SafeNativeMethods.Destroy(ref handle);
            return true;
        }
    }
}