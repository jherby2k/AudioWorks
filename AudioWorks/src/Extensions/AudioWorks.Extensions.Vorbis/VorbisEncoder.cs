/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

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
        internal VorbisEncoder(int channels, int sampleRate, int maxBitRate, int nominalBitRate, int minBitRate)
        {
            SafeNativeMethods.VorbisInfoInit(_info);
            SafeNativeMethods.VorbisEncodeInit(_info, channels, sampleRate, maxBitRate, nominalBitRate, minBitRate);
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