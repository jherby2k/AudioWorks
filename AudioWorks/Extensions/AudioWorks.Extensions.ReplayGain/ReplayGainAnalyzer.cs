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

        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary
        {
            ["PeakAnalysis"] = new StringSettingInfo("Simple", "Interpolated")
        };

        public void Initialize(AudioInfo audioInfo, SettingDictionary settings)
        {
            _analyzer = new R128Analyzer((uint) audioInfo.Channels, (uint) audioInfo.SampleRate,
                settings.TryGetValue("PeakAnalysis", out var peakAnalysis) &&
                string.CompareOrdinal("Interpolated", (string) peakAnalysis) == 0);
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
        public AudioMetadata GetResult(GroupToken groupToken)
        {
            var groupState = (GroupState) groupToken.GetOrSetGroupState(new GroupState());
            groupState.Handles.Add(_analyzer.Handle);

            var peak = _analyzer.GetPeak();
            groupState.AddPeak(peak);

            var result = new AudioMetadata
            {
                TrackPeak = peak.ToString(CultureInfo.InvariantCulture),
                TrackGain = (_referenceLevel - _analyzer.GetLoudness())
                    .ToString(CultureInfo.InvariantCulture)
            };

            groupToken.CompleteMember();
            groupToken.WaitForMembers();

            result.AlbumPeak = groupState.GroupPeak.ToString(CultureInfo.InvariantCulture);
            result.AlbumGain = (_referenceLevel - _analyzer.GetLoudnessMultiple(groupState.Handles.ToArray()))
                .ToString(CultureInfo.InvariantCulture);

            return result;
        }
    }
}
