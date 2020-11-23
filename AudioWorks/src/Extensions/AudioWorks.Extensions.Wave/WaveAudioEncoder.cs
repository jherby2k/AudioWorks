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
#if NETSTANDARD2_0
using System.Buffers;
#endif
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Wave
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioEncoderExport("Wave", "Waveform Audio File Format")]
    sealed class WaveAudioEncoder : IAudioEncoder, IDisposable
    {
        int _bitsPerSample;
        int _bytesPerSample;
        RiffWriter? _writer;

        public SettingInfoDictionary SettingInfo { get; } = new();

        public string FileExtension { get; } = ".wav";

        public void Initialize(Stream stream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            _bitsPerSample = info.BitsPerSample;
            _bytesPerSample = (int) Math.Ceiling(info.BitsPerSample / 8.0);
            _writer = new RiffWriter(stream);

            // Pre-allocate the entire stream to avoid fragmentation
            var estimatedSize = 44 + info.FrameCount * info.Channels * _bytesPerSample;
            estimatedSize += estimatedSize % 2;
            stream.SetLength(estimatedSize);

            _writer.Initialize("WAVE");
            WriteFmtChunk(info);
            _writer.BeginChunk("data");
        }

        public void Submit(SampleBuffer samples)
        {
            if (samples.Frames == 0) return;

#if NETSTANDARD2_0
            var dataSize = samples.Channels * samples.Frames * _bytesPerSample;

            var buffer = ArrayPool<byte>.Shared.Rent(dataSize);
            try
            {
                samples.CopyToInterleaved(buffer, _bitsPerSample);
                _writer!.Write(buffer, 0, dataSize);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
#else
            Span<byte> buffer = stackalloc byte[samples.Channels * samples.Frames * _bytesPerSample];
            samples.CopyToInterleaved(buffer, _bitsPerSample);
            _writer!.Write(buffer);
#endif
        }

        public void Finish()
        {
            // Finish both data and RIFF chunks
            _writer!.FinishChunk();
            _writer.FinishChunk();

            // The pre-allocation may have been based on an estimated frame count
            _writer.BaseStream.SetLength(_writer.BaseStream.Position);
        }

        public void Dispose() => _writer?.Dispose();

        void WriteFmtChunk(AudioInfo audioInfo)
        {
            _writer!.BeginChunk("fmt ", 16);
            _writer.Write((ushort) 1);
            _writer.Write((ushort) audioInfo.Channels);
            _writer.Write((uint) audioInfo.SampleRate);
            _writer.Write((uint) (_bytesPerSample * audioInfo.Channels * audioInfo.SampleRate));
            _writer.Write((ushort) (_bytesPerSample * audioInfo.Channels));
            _writer.Write((ushort) audioInfo.BitsPerSample);
            _writer.FinishChunk();
        }
    }
}
