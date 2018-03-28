using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class VorbisEncoder : IDisposable
    {
        readonly IntPtr _info = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(VorbisInfo)));
        readonly IntPtr _block = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(VorbisBlock)));

        internal IntPtr DspState { get; } = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(VorbisDspState)));

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native methods are always expected to return 0")]
        internal VorbisEncoder(int channels, int sampleRate, float baseQuality)
        {
            SafeNativeMethods.VorbisInfoInit(_info);
            SafeNativeMethods.VorbisEncodeInitVbr(_info, channels, sampleRate, baseQuality);
            SafeNativeMethods.VorbisAnalysisInit(DspState, _info);
            SafeNativeMethods.VorbisBlockInit(DspState, _block);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native methods are always expected to return 0")]
        internal VorbisEncoder(int channels, int sampleRate, int nominalBitRate, bool managed)
        {
            SafeNativeMethods.VorbisInfoInit(_info);

            // Use 3-step setup so bitrate management can be disabled
            SafeNativeMethods.VorbisEncodeSetupManaged(_info, channels, sampleRate,
                -1, nominalBitRate, -1);
            if (!managed)
                SafeNativeMethods.VorbisEncodeCtl(_info, 0x15, IntPtr.Zero);
            SafeNativeMethods.VorbisEncodeSetupInit(_info);

            SafeNativeMethods.VorbisAnalysisInit(DspState, _info);
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