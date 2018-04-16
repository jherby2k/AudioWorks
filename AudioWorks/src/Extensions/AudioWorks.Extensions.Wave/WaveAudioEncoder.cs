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
        [CanBeNull] byte[] _buffer;

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

            var dataSize = samples.Channels * samples.Frames * _bytesPerSample;

            if (_buffer == null)
                _buffer = ArrayPool<byte>.Shared.Rent(dataSize);

            samples.CopyToInterleaved(_buffer, _bitsPerSample);
            // ReSharper disable once AssignNullToNotNullAttribute
            _writer.Write(_buffer, 0, dataSize);
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
            if (_buffer != null)
                ArrayPool<byte>.Shared.Return(_buffer);
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

        static void ConvertInt32ToBytes(int value, [NotNull] byte[] buffer)
        {
            buffer[0] = (byte) value;
            buffer[1] = (byte) (value >> 8);
            buffer[2] = (byte) (value >> 16);
            buffer[3] = (byte) (value >> 24);
        }
    }
}
