using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    [AudioEncoderExport("FLAC")]
    public sealed class FlacAudioEncoder : IAudioEncoder, IDisposable
    {
        [CanBeNull] StreamEncoder _encoder;
        float _multiplier;
        [CanBeNull] int[] _buffer;

        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary();

        public string FileExtension { get; } = ".flac";

        public void Initialize(FileStream fileStream, AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            _encoder = new StreamEncoder(fileStream);
            _encoder.SetChannels((uint) info.Channels);
            _encoder.SetBitsPerSample((uint) info.BitsPerSample);
            _encoder.SetSampleRate((uint) info.SampleRate);
            _encoder.SetTotalSamplesEstimate((ulong) info.SampleCount);
            _encoder.Initialize();

            _multiplier = (float) Math.Pow(2, info.BitsPerSample - 1);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Submit(SampleCollection samples)
        {
            if (samples.Frames == 0)
                return;

            if (_buffer == null)
                _buffer = new int[samples.Channels * samples.Frames];

            // Interlace the samples in integer format, and store them in the input buffer:
            var index = 0;
            for (var frameIndex = 0; frameIndex < samples.Frames; frameIndex++)
            for (var channelIndex = 0; channelIndex < samples.Channels; channelIndex++)
                _buffer[index++] = (int) Math.Round(samples[channelIndex][frameIndex] * _multiplier);

            _encoder.ProcessInterleaved(_buffer, (uint) samples.Frames);
            //TODO check the result and throw an exception if necessary.
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Finish()
        {
            _encoder.Finish();
            //TODO check the result and throw an exception if necessary.
        }

        public void Dispose()
        {
            _encoder?.Dispose();
        }
    }
}
