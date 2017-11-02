using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace AudioWorks.Extensions.ReplayGain
{
    [Shared]
    [AudioAnalyzerExport("ReplayGain")]
    public sealed class ReplayGainAnalyzer : IAudioAnalyzer, IDisposable
    {
        const int _referenceLevel = -18;

        [CanBeNull] float[] _buffer;
        [CanBeNull] R128Analyzer _analyzer;
        [CanBeNull] GroupToken _groupToken;

        public void Initialize(AudioInfo audioInfo, GroupToken groupToken)
        {
            _buffer = new float[audioInfo.Channels * SampleCollection.MaxFrames];
            _analyzer = new R128Analyzer((uint) audioInfo.Channels, (uint) audioInfo.SampleRate, groupToken);
            _groupToken = groupToken;
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void Submit(SampleCollection samples)
        {
            // Interlace the samples, and store them in the buffer:
            var index = 0;
            for (var frame = 0; frame < samples.Frames; frame++)
            for (var channel = 0; channel < samples.Channels; channel++)
                _buffer[index++] = samples[channel][frame];

            _analyzer.AddFrames(_buffer);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public AudioMetadata GetResult()
        {
            var result = new AudioMetadata
            {
                TrackPeak = _analyzer.GetSamplePeak()
                    .ToString(CultureInfo.InvariantCulture),
                TrackGain = (_referenceLevel - _analyzer.GetLoudness())
                    .ToString(CultureInfo.InvariantCulture)
            };

            _groupToken.CompleteMember();
            _groupToken.WaitForMembers();

            result.AlbumPeak = _analyzer.GetSamplePeakMultiple()
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
