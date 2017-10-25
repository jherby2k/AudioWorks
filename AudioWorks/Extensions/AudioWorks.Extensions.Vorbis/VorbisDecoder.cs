using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class VorbisDecoder : IDisposable
    {
        readonly IntPtr _info;

        internal VorbisDecoder()
        {
            _info = Marshal.AllocHGlobal(Marshal.SizeOf<VorbisInfo>());
            SafeNativeMethods.VorbisInfoInitialize(_info);
        }

        internal void HeaderIn(ref VorbisComment comment, ref OggPacket packet)
        {
            SafeNativeMethods.VorbisSynthesisHeaderIn(_info, ref comment, ref packet);
        }

        [Pure]
        internal VorbisInfo GetInfo()
        {
            return Marshal.PtrToStructure<VorbisInfo>(_info);
        }

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void FreeUnmanaged()
        {
            SafeNativeMethods.VorbisInfoClear(_info);
            Marshal.FreeHGlobal(_info);
        }

        ~VorbisDecoder()
        {
            FreeUnmanaged();
        }
    }
}