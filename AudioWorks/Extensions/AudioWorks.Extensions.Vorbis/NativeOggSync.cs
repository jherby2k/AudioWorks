using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class NativeOggSync : IDisposable
    {
        readonly IntPtr _state;

        public NativeOggSync()
        {
            _state = Marshal.AllocHGlobal(Marshal.SizeOf<OggSyncState>());
            SafeNativeMethods.OggSyncInitialize(_state);
        }

        internal bool PageOut(out OggPage page)
        {
            return SafeNativeMethods.OggSyncPageOut(_state, out page);
        }

        internal IntPtr Buffer(int size)
        {
            return SafeNativeMethods.OggSyncBuffer(_state, size);
        }

        internal void Wrote(int bytes)
        {
            SafeNativeMethods.OggSyncWrote(_state, bytes);
        }

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void FreeUnmanaged()
        {
            SafeNativeMethods.OggSyncClear(_state);
            Marshal.FreeHGlobal(_state);
        }

        ~NativeOggSync()
        {
            FreeUnmanaged();
        }
    }
}