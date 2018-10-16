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
#if !NETCOREAPP2_1
using System.Buffers;
#endif
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Wave
{
    [AudioEncoderExport("Wave", "Waveform Audio File Format")]
    public sealed class WaveAudioEncoder : IAudioEncoder, IDisposable
    {
        int _bitsPerSample;
        int _bytesPerSample;
        [CanBeNull] RiffWriter _writer;

        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary();

        public string FileExtension { get; } = ".wav";

        public void Initialize(FileStream fileStream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            _bitsPerSample = info.BitsPerSample;
            _bytesPerSample = (int) Math.Ceiling(info.BitsPerSample / 8.0);
            _writer = new RiffWriter(fileStream);

            _writer.Initialize("WAVE");
            WriteFmtChunk(info);
            // ReSharper disable once PossibleNullReferenceException
            _writer.BeginChunk("data");
        }

        public void Submit(SampleBuffer samples)
        {
            if (samples.Frames == 0) return;

#if NETCOREAPP2_1
            Span<byte> buffer = stackalloc byte[samples.Channels * samples.Frames * _bytesPerSample];
            samples.CopyToInterleaved(buffer, _bitsPerSample);
            // ReSharper disable once PossibleNullReferenceException
            _writer.Write(buffer);
#else
            var dataSize = samples.Channels * samples.Frames * _bytesPerSample;

            var buffer = ArrayPool<byte>.Shared.Rent(dataSize);
            try
            {
                samples.CopyToInterleaved(buffer, _bitsPerSample);
                // ReSharper disable once PossibleNullReferenceException
                _writer.Write(buffer, 0, dataSize);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
#endif
        }

        public void Finish()
        {
            // Finish both data and RIFF chunks
            // ReSharper disable once PossibleNullReferenceException
            _writer.FinishChunk();
            _writer.FinishChunk();
        }

        public void Dispose()
        {
            _writer?.Dispose();
        }

        void WriteFmtChunk([NotNull] AudioInfo audioInfo)
        {
            // ReSharper disable once PossibleNullReferenceException
            _writer.BeginChunk("fmt ", 16);
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
