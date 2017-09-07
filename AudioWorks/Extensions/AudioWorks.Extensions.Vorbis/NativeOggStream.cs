using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class NativeOggStream : IDisposable
    {
        readonly IntPtr _state;

        internal NativeOggStream(int serialNumber)
        {
            _state = Marshal.AllocHGlobal(CalculateStateSize());
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

        ~NativeOggStream()
        {
            FreeUnmanaged();
        }

        void FreeUnmanaged()
        {
            SafeNativeMethods.OggStreamClear(_state);
            Marshal.FreeHGlobal(_state);
        }

        [Pure]
        static int CalculateStateSize()
        {
            // Size of ogg_stream_state
            return Marshal.SizeOf(typeof(int)) * 12 +
                   Marshal.SizeOf(typeof(long)) * 2 +
                   Marshal.SizeOf(typeof(IntPtr)) * 3 +
                   Marshal.SizeOf(typeof(char)) * 282;
        }
    }
}