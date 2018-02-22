using System.Runtime.ConstrainedExecution;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace AudioWorks.Extensions.Apple
{
    [UsedImplicitly]
    sealed class ExtendedAudioFileHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal ExtendedAudioFileHandle()
            : base(true)
        {
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        protected override bool ReleaseHandle()
        {
            SafeNativeMethods.ExtAudioFileDispose(handle);
            return true;
        }
    }
}