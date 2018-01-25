using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Wave
{
    [AudioEncoderExport("Wave")]
    public sealed class WaveAudioEncoder : IAudioEncoder, IDisposable
    {
        int _bytesPerSample;
        float _multiplier;
        [CanBeNull] RiffWriter _writer;
        [NotNull] readonly byte[] _buffer = new byte[4];

        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary();

        public string FileExtension { get; } = ".wav";

        public void Initialize(FileStream fileStream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            _bytesPerSample = (int) Math.Ceiling(info.BitsPerSample / (double) 8);
            _multiplier = (float) Math.Pow(2, info.BitsPerSample - 1);
            _writer = new RiffWriter(fileStream);

            _writer.Initialize("WAVE");
            WriteFmtChunk(info);
            // ReSharper disable once PossibleNullReferenceException
            _writer.BeginChunk("data");
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Submit(SampleCollection samples)
        {
            if (_bytesPerSample == 1)
                for (var frameIndex = 0; frameIndex < samples.Frames; frameIndex++)
                for (var channelIndex = 0; channelIndex < samples.Channels; channelIndex++)
                    _writer.Write((byte) Math.Round(samples[channelIndex][frameIndex] * _multiplier + 128));
            else
                for (var frameIndex = 0; frameIndex < samples.Frames; frameIndex++)
                for (var channelIndex = 0; channelIndex < samples.Channels; channelIndex++)
                {
                    // Optimization - BitConverter generates too much garbage
                    ConvertInt32ToBytes((int) Math.Round(samples[channelIndex][frameIndex] * _multiplier), _buffer);
                    _writer.Write(_buffer, 0, _bytesPerSample);
                }
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
