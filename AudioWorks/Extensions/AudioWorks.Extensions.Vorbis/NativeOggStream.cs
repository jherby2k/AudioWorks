using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class NativeOggStream : IDisposable
    {
        readonly IntPtr _state;

        internal NativeOggStream(int serialNumber)
        {
            _state = Marshal.AllocHGlobal(Marshal.SizeOf<OggStreamState>());
            SafeNativeMethods.OggStreamInitialize(_state, serialNumber);
        }

        internal void PageIn(ref OggPage page)
        {
            SafeNativeMethods.OggStreamPageIn(_state, ref page);
        }

        internal bool PacketOut(out OggPacket packet)
        {
            return SafeNativeMethods.OggStreamPacketOut(_state, out packet);
        }

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void FreeUnmanaged()
        {
            SafeNativeMethods.OggStreamClear(_state);
            Marshal.FreeHGlobal(_state);
        }

        ~NativeOggStream()
        {
            FreeUnmanaged();
        }
    }
}