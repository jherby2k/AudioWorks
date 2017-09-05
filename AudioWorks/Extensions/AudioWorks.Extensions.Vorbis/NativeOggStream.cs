using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    class NativeOggStream : IDisposable
    {
        readonly IntPtr _state;

        internal NativeOggStream(int serialNumber)
        {
            _state = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(OggStreamState)));
            SafeNativeMethods.OggStreamInitialize(_state, serialNumber);
        }

        internal void PageIn(OggPage page)
        {
            SafeNativeMethods.OggStreamPageIn(_state, ref page);
        }

        internal bool PacketOut(out OggPacket packet)
        {
            return SafeNativeMethods.OggStreamPacketOut(_state, out packet);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            SafeNativeMethods.OggStreamClear(_state);
            Marshal.FreeHGlobal(_state);
        }

        ~NativeOggStream()
        {
            Dispose(false);
        }
    }
}