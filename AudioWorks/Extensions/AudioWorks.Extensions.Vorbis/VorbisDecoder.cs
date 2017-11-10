using JetBrains.Annotations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class VorbisDecoder : IDisposable
    {
        readonly IntPtr _info;

        internal VorbisDecoder()
        {
            _info = Marshal.AllocHGlobal(Marshal.SizeOf<VorbisInfo>());
            SafeNativeMethods.VorbisInfoInit(_info);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
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