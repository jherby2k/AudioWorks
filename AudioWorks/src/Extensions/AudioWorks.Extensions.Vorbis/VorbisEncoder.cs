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
    sealed unsafe class VorbisEncoder : IDisposable
    {
        readonly nint _info = Marshal.AllocHGlobal(sizeof(VorbisInfo));
        readonly nint _block = Marshal.AllocHGlobal(sizeof(VorbisBlock));

        internal nint DspState { get; } = Marshal.AllocHGlobal(sizeof(VorbisDspState));

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native methods are always expected to return 0")]
        internal VorbisEncoder(int channels, int sampleRate, float baseQuality)
        {
            LibVorbis.InfoInit(_info);
            LibVorbisEnc.EncodeInitVbr(_info, new(channels), new(sampleRate), baseQuality);
            LibVorbis.AnalysisInit(DspState, _info);
            LibVorbis.BlockInit(DspState, _block);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native methods are always expected to return 0")]
        internal VorbisEncoder(int channels, int sampleRate, int maxBitRate, int nominalBitRate, int minBitRate)
        {
            LibVorbis.InfoInit(_info);
            LibVorbisEnc.EncodeInit(_info,
                new(channels), new(sampleRate), new(maxBitRate), new(nominalBitRate), new(minBitRate));
            LibVorbis.AnalysisInit(DspState, _info);
            LibVorbis.BlockInit(DspState, _block);
        }

        internal nint GetBuffer(int samples) => LibVorbis.AnalysisBuffer(DspState, samples);

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void Wrote(int samples) => LibVorbis.AnalysisWrote(DspState, samples);

        internal bool BlockOut() => LibVorbis.AnalysisBlockOut(DspState, _block) == 1;

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void Analysis(nint packet) => LibVorbis.Analysis(_block, packet);

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void AddBlock() => LibVorbis.BitrateAddBlock(_block);

        internal bool FlushPacket(out OggPacket packet) =>
            LibVorbis.BitrateFlushPacket(DspState, out packet) == 1;

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void FreeUnmanaged()
        {
            LibVorbis.BlockClear(_block);
            Marshal.FreeHGlobal(_block);
            LibVorbis.DspClear(DspState);
            Marshal.FreeHGlobal(DspState);
            LibVorbis.InfoClear(_info);
            Marshal.FreeHGlobal(_info);
        }

        ~VorbisEncoder() => FreeUnmanaged();
    }
}