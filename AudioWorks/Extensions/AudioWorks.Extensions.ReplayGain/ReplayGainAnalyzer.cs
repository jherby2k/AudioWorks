using AudioWorks.Common;
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace AudioWorks.Extensions.ReplayGain
{
    [AudioAnalyzerExport("ReplayGain")]
    public sealed class ReplayGainAnalyzer : IAudioAnalyzer
    {
        const int _referenceLevel = -18;

        [CanBeNull] float[] _buffer;
        [CanBeNull] R128Analyzer _analyzer;
        [CanBeNull] GroupState _groupState;

        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary
        {
            ["PeakAnalysis"] = new StringSettingInfo("Simple", "Interpolated")
        };

        public void Initialize(AudioInfo audioInfo, SettingDictionary settings, GroupToken groupToken)
        {
            _analyzer = new R128Analyzer((uint) audioInfo.Channels, (uint) audioInfo.SampleRate,
                settings.TryGetValue("PeakAnalysis", out var peakAnalysis) &&
                string.CompareOrdinal("Interpolated", (string) peakAnalysis) == 0);

            _groupState = (GroupState) groupToken.GetOrSetGroupState(new GroupState());
            // ReSharper disable once PossibleNullReferenceException
            _groupState.Handles.Enqueue(_analyzer.Handle);
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
            var peak = _analyzer.GetPeak();
            _groupState.AddPeak(peak);

            return new AudioMetadata
            {
                TrackPeak = peak.ToString(CultureInfo.InvariantCulture),
                TrackGain = (_referenceLevel - _analyzer.GetLoudness())
                    .ToString(CultureInfo.InvariantCulture)
            };
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public AudioMetadata GetGroupResult()
        {
            return new AudioMetadata
            {
                AlbumPeak = _groupState.GroupPeak.ToString(CultureInfo.InvariantCulture),
                AlbumGain = (_referenceLevel - R128Analyzer.GetLoudnessMultiple(_groupState.Handles.ToArray()))
                    .ToString(CultureInfo.InvariantCulture)
            };
        }
    }
}
