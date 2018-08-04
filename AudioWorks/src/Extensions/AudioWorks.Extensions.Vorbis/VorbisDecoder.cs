using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class VorbisDecoder : IDisposable
    {
        readonly IntPtr _info = Marshal.AllocHGlobal(Marshal.SizeOf<VorbisInfo>());

        internal VorbisDecoder()
        {
            SafeNativeMethods.VorbisInfoInit(_info);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void HeaderIn(in VorbisComment comment, in OggPacket packet)
        {
            SafeNativeMethods.VorbisSynthesisHeaderIn(_info, comment, packet);
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