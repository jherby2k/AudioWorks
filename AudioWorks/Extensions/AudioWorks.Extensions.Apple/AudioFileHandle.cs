using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;

namespace AudioWorks.Extensions.Apple
{
    [UsedImplicitly]
    sealed class AudioFileHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public AudioFileHandle()
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