using System.Runtime.ConstrainedExecution;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace AudioWorks.Extensions.Apple
{
    [UsedImplicitly]
    sealed class AudioFileHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal AudioFileHandle()
            : base(true)
        {
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        protected override bool ReleaseHandle()
        {
            SafeNativeMethods.AudioFileClose(handle);
            return true;
        }
    }
}