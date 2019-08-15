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
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Wave
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioInfoDecoderExport(".wav", "Waveform Audio")]
    sealed class WaveAudioInfoDecoder : IAudioInfoDecoder
    {
        const string _format = "Waveform Audio File Format (WAVE)";

        public string Format => _format;

        public AudioInfo ReadAudioInfo(Stream stream)
        {
            using (var reader = new RiffReader(stream))
                try
                {
                    reader.Initialize();

                    if (stream.Length != reader.RiffChunkSize + 8)
                        throw new AudioInvalidException("Stream is unexpectedly truncated.");

                    reader.BaseStream.Position = 8;
                    if (!reader.ReadFourCc().Equals("WAVE", StringComparison.Ordinal))
                        throw new AudioInvalidException("Not a Wave stream.");

                    var fmtChunkSize = reader.SeekToChunk("fmt ");
                    if (fmtChunkSize == 0)
                        throw new AudioInvalidException("Missing 'fmt' chunk.");

                    var isExtensible = false;
                    switch (reader.ReadUInt16())
                    {
                        // WAVE_FORMAT_PCM
                        case 1:
                            break;

                        // WAVE_FORMAT_EXTENSIBLE
                        case 0xFFFE:
                            isExtensible = true;
                            break;

                        default:
                            throw new AudioUnsupportedException("Only PCM wave streams are supported.");
                    }

                    var channels = reader.ReadUInt16();
                    var sampleRate = reader.ReadUInt32();

                    // Ignore nAvgBytesPerSec
                    stream.Seek(4, SeekOrigin.Current);

                    var blockAlign = reader.ReadUInt16();

                    // Use wValidBitsPerSample if this is WAVE_FORMAT_EXTENSIBLE
                    if (isExtensible)
                        stream.Seek(4, SeekOrigin.Current);

                    return AudioInfo.CreateForLossless(
                        "LPCM",
                        channels,
                        reader.ReadUInt16(),
                        (int) sampleRate,
                        reader.SeekToChunk("data") / blockAlign);
                }
                catch (EndOfStreamException e)
                {
                    // The end of the stream was unexpectedly reached
                    throw new AudioInvalidException(e.Message);
                }
        }
    }
}
