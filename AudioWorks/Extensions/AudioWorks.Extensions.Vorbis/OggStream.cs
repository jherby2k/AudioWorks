using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class OggStream : IDisposable
    {
        readonly IntPtr _state;

        internal int SerialNumber => Marshal.PtrToStructure<OggStreamState>(_state).SerialNumber;

        internal OggStream(int serialNumber)
        {
            _state = Marshal.AllocHGlobal(Marshal.SizeOf<OggStreamState>());
            SafeNativeMethods.OggStreamInitialize(_state, serialNumber);
        }

        internal void PageIn(ref OggPage page)
        {
            SafeNativeMethods.OggStreamPageIn(_state, ref page);
        }

        internal bool PageOut(out OggPage page)
        {
            return SafeNativeMethods.OggStreamPageOut(_state, out page);
        }

        internal void PacketIn(ref OggPacket packet)
        {
            SafeNativeMethods.OggStreamPacketIn(_state, ref packet);
        }

        internal bool PacketOut(out OggPacket packet)
        {
            return SafeNativeMethods.OggStreamPacketOut(_state, out packet);
        }

        internal bool Flush(out OggPage page)
        {
            return SafeNativeMethods.OggStreamFlush(_state, out page);
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

        ~OggStream()
        {
            FreeUnmanaged();
        }
    }
}