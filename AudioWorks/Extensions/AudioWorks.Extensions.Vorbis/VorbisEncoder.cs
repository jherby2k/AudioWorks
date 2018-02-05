using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class VorbisEncoder : IDisposable
    {
        readonly IntPtr _info;
        readonly IntPtr _block;

        internal IntPtr DspState { get; }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native methods are always expected to return 0")]
        internal VorbisEncoder(int channels, int sampleRate, float baseQuality)
        {
            _info = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(VorbisInfo)));
            SafeNativeMethods.VorbisInfoInit(_info);

            SafeNativeMethods.VorbisEncodeInitVbr(_info, channels, sampleRate, baseQuality);

            DspState = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(VorbisDspState)));
            SafeNativeMethods.VorbisAnalysisInit(DspState, _info);

            _block = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(VorbisBlock)));
            SafeNativeMethods.VorbisBlockInit(DspState, _block);
        }

        internal IntPtr GetBuffer(int samples)
        {
            return SafeNativeMethods.VorbisAnalysisBuffer(DspState, samples);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void Wrote(int samples)
        {
            SafeNativeMethods.VorbisAnalysisWrote(DspState, samples);
        }

        internal bool BlockOut()
        {
            return SafeNativeMethods.VorbisAnalysisBlockOut(DspState, _block) == 1;
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void Analysis(IntPtr packet)
        {
            SafeNativeMethods.VorbisAnalysis(_block, packet);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void AddBlock()
        {
            SafeNativeMethods.VorbisBitrateAddBlock(_block);
        }

        internal bool FlushPacket(out OggPacket packet)
        {
            return SafeNativeMethods.VorbisBitrateFlushPacket(DspState, out packet) == 1;
        }

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void FreeUnmanaged()
        {
            SafeNativeMethods.VorbisBlockClear(_block);
            Marshal.FreeHGlobal(_block);
            SafeNativeMethods.VorbisDspClear(DspState);
            Marshal.FreeHGlobal(DspState);
            SafeNativeMethods.VorbisInfoClear(_info);
            Marshal.FreeHGlobal(_info);
        }

        ~VorbisEncoder()
        {
            FreeUnmanaged();
        }
    }
}