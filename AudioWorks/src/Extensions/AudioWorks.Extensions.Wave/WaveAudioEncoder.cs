using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Wave
{
    [AudioEncoderExport("Wave", "Linear PCM Wave")]
    public sealed class WaveAudioEncoder : IAudioEncoder, IDisposable
    {
        int _bitsPerSample;
        int _bytesPerSample;
        [CanBeNull] RiffWriter _writer;
#if !NETCOREAPP2_1
        [CanBeNull] byte[] _buffer;
#endif

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

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Submit(SampleBuffer samples)
        {
            if (samples.Frames == 0) return;

#if NETCOREAPP2_1
            Span<byte> buffer = stackalloc byte[samples.Channels * samples.Frames * _bytesPerSample];
            samples.CopyToInterleaved(buffer, _bitsPerSample);
            _writer.Write(buffer);
#else
            var dataSize = samples.Channels * samples.Frames * _bytesPerSample;

            if (_buffer == null)
                _buffer = ArrayPool<byte>.Shared.Rent(dataSize);

            samples.CopyToInterleaved(_buffer, _bitsPerSample);
            // ReSharper disable once AssignNullToNotNullAttribute
            _writer.Write(_buffer, 0, dataSize);
#endif
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Finish()
        {
            // Finish the data and RIFF chunks
            _writer.FinishChunk();
            _writer.FinishChunk();
        }

        public void Dispose()
        {
            _writer?.Dispose();
#if !NETCOREAPP2_1
            if (_buffer != null)
                ArrayPool<byte>.Shared.Return(_buffer);
#endif
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        void WriteFmtChunk([NotNull] AudioInfo audioInfo)
        {
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
