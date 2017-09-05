using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class NativeVorbisDecoder : IDisposable
    {
        readonly IntPtr _info;

        internal NativeVorbisDecoder()
        {
            _info = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(VorbisInfo)));
            SafeNativeMethods.VorbisInfoInitialize(_info);
        }

        internal void HeaderIn(ref VorbisComment comment, ref OggPacket packet)
        {
            SafeNativeMethods.VorbisSynthesisHeaderIn(_info, ref comment, ref packet);
        }

        internal VorbisInfo GetInfo()
        {
            return Marshal.PtrToStructure<VorbisInfo>(_info);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            SafeNativeMethods.VorbisInfoClear(_info);
            Marshal.FreeHGlobal(_info);
        }

        ~NativeVorbisDecoder()
        {
            Dispose(false);
        }
    }
}