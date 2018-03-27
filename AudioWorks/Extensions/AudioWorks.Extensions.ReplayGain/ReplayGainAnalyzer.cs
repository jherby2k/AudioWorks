using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.ReplayGain
{
    [AudioAnalyzerExport("ReplayGain", "ReplayGain 2.0")]
    public sealed class ReplayGainAnalyzer : IAudioAnalyzer
    {
        const int _referenceLevel = -18;

        [CanBeNull] R128Analyzer _analyzer;
        [CanBeNull] GroupState _groupState;

        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary
        {
            ["PeakAnalysis"] = new StringSettingInfo("Simple", "Interpolated")
        };

        public void Initialize(AudioInfo info, SettingDictionary settings, GroupToken groupToken)
        {
            _analyzer = new R128Analyzer((uint) info.Channels, (uint) info.SampleRate,
                settings.TryGetValue("PeakAnalysis", out var peakAnalysis) &&
                string.Equals("Interpolated", (string) peakAnalysis, StringComparison.Ordinal));

            _groupState = (GroupState) groupToken.GetOrSetGroupState(new GroupState());
            // ReSharper disable once PossibleNullReferenceException
            _groupState.Handles.Enqueue(_analyzer.Handle);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public unsafe void Submit(SampleBuffer samples)
        {
            if (samples.Frames == 0) return;

            Span<float> buffer = stackalloc float[samples.Frames * samples.Channels];
            samples.CopyToInterleaved(buffer);

            _analyzer.AddFrames(
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(buffer))),
                (uint) samples.Frames);
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
