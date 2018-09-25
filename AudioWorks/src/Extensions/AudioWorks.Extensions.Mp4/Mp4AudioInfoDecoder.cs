/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.IO;
using System.Linq;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Mp4
{
    [AudioInfoDecoderExport(".m4a")]
    public sealed class Mp4AudioInfoDecoder : IAudioInfoDecoder
    {
        public AudioInfo ReadAudioInfo(FileStream stream)
        {
            try
            {
                var mp4 = new Mp4Model(stream);

                mp4.DescendToAtom("moov", "trak", "mdia", "minf", "stbl", "stts");
                var stts = new SttsAtom(mp4.ReadAtom(mp4.CurrentAtom));

                var sampleCount = stts.PacketCount * stts.PacketSize;

                mp4.DescendToAtom("moov", "trak", "mdia", "minf", "stbl", "stsd", "mp4a", "esds");
                var esds = new EsdsAtom(mp4.ReadAtom(mp4.CurrentAtom));
                if (esds.IsAac)
                {
                    mp4.Reset();
                    return AudioInfo.CreateForLossy("AAC", esds.Channels, (int) esds.SampleRate, sampleCount,
                        CalculateBitRate(mp4.GetChildAtomInfo().Single(atom =>
                                atom.FourCc.Equals("mdat", StringComparison.Ordinal)).Size,
                            sampleCount,
                            esds.SampleRate));
                }

                // Apple Lossless files have their own atom for storing audio info
                if (!mp4.DescendToAtom("moov", "trak", "mdia", "minf", "stbl", "stsd", "alac"))
                    throw new AudioUnsupportedException("Only AAC and ALAC files are supported.", stream.Name);

                var alac = new AlacAtom(mp4.ReadAtom(mp4.CurrentAtom));
                return AudioInfo.CreateForLossless(
                    "ALAC",
                    alac.Channels,
                    alac.BitsPerSample,
                    (int) alac.SampleRate,
                    sampleCount);
            }
            catch (EndOfStreamException e)
            {
                throw new AudioInvalidException(e.Message, stream.Name);
            }
        }

        static int CalculateBitRate(uint byteCount, uint sampleCount, uint sampleRate)
        {
            return (int) Math.Round(byteCount * 8 / (sampleCount / (double) sampleRate));
        }
    }
}
