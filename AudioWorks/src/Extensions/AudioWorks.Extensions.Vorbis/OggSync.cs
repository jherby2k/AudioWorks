using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class OggSync : IDisposable
    {
        readonly IntPtr _state;

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method always returns 0")]
        internal OggSync()
        {
            _state = Marshal.AllocHGlobal(Marshal.SizeOf<OggSyncState>());
            SafeNativeMethods.OggSyncInit(_state);
        }

        internal bool PageOut(out OggPage page)
        {
            return SafeNativeMethods.OggSyncPageOut(_state, out page) == 1;
        }

#if WINDOWS
        internal IntPtr Buffer(int size)
#else
        internal IntPtr Buffer(long size)
#endif
        {
            return SafeNativeMethods.OggSyncBuffer(_state, size);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
#if WINDOWS
        internal void Wrote(int bytes)
#else
        internal void Wrote(long bytes)
#endif
        {
            SafeNativeMethods.OggSyncWrote(_state, bytes);
        }

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method always returns 0")]
        void FreeUnmanaged()
        {
            SafeNativeMethods.OggSyncClear(_state);
            Marshal.FreeHGlobal(_state);
        }

        ~OggSync()
        {
            FreeUnmanaged();
        }
    }
}