using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace AudioWorks.Extensions.ReplayGain
{
    [AudioAnalyzerExport("ReplayGain")]
    public sealed class ReplayGainAnalyzer : IAudioAnalyzer, IDisposable
    {
        const int _referenceLevel = -18;

        [CanBeNull] float[] _buffer;
        [CanBeNull] R128Analyzer _analyzer;
        [CanBeNull] GroupToken _groupToken;

        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary
        {
            ["PeakAnalysis"] = new StringSettingInfo("Simple", "Interpolated")
        };

        public void Initialize(AudioInfo audioInfo, SettingDictionary settings, GroupToken groupToken)
        {
            _analyzer = new R128Analyzer((uint) audioInfo.Channels, (uint) audioInfo.SampleRate, groupToken,
                settings.TryGetValue("PeakAnalysis", out var peakAnalysis) &&
                string.CompareOrdinal("Interpolated", (string) peakAnalysis) == 0);
            _groupToken = groupToken;
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Submit(SampleCollection samples)
        {
            if (samples.Frames == 0)
                return;

            if (_buffer == null)
                _buffer = new float[samples.Channels * samples.Frames];

            // Interlace the samples, and store them in the buffer:
            var index = 0;
            for (var frameIndex = 0; frameIndex < samples.Frames; frameIndex++)
            for (var channelIndex = 0; channelIndex < samples.Channels; channelIndex++)
                _buffer[index++] = samples[channelIndex][frameIndex];

            _analyzer.AddFrames(_buffer, (uint) samples.Frames);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public AudioMetadata GetResult()
        {
            var result = new AudioMetadata
            {
                TrackPeak = _analyzer.GetPeak()
                    .ToString(CultureInfo.InvariantCulture),
                TrackGain = (_referenceLevel - _analyzer.GetLoudness())
                    .ToString(CultureInfo.InvariantCulture)
            };

            _groupToken.CompleteMember();
            _groupToken.WaitForMembers();

            result.AlbumPeak = _analyzer.GetPeakMultiple()
                .ToString(CultureInfo.InvariantCulture);
            result.AlbumGain = (_referenceLevel - _analyzer.GetLoudnessMultiple())
                .ToString(CultureInfo.InvariantCulture);

            return result;
        }

        public void Dispose()
        {
            _analyzer?.Dispose();
        }
    }
}
