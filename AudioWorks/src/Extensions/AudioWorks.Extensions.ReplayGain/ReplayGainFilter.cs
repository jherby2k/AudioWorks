using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.ReplayGain
{
    [AudioFilterExport("ReplayGain")]
    public sealed class ReplayGainFilter : IAudioFilter
    {
        float _scale = 1;

        public SettingInfoDictionary SettingInfo { get; } = new SettingInfoDictionary
        {
            ["ApplyGain"] = new StringSettingInfo("Track", "Album")
        };

        public void Initialize(AudioInfo info, AudioMetadata metadata, SettingDictionary settings)
        {
            if (settings.TryGetValue("ApplyGain", out var applyGainValue))
                _scale = applyGainValue.Equals("Track")
                    ? CalculateScale(metadata.TrackGain, metadata.TrackPeak)
                    : CalculateScale(metadata.AlbumGain, metadata.AlbumPeak);

            // Adjust the metadata so that it remains valid
            metadata.TrackPeak = CalculatePeak(metadata.TrackPeak, _scale);
            metadata.AlbumPeak = CalculatePeak(metadata.AlbumPeak, _scale);
            metadata.TrackGain = CalculateGain(metadata.TrackGain, _scale);
            metadata.AlbumGain = CalculateGain(metadata.AlbumGain, _scale);
        }

        public SampleBuffer Process(SampleBuffer samples)
        {
            if (Math.Abs(_scale - 1) < 0.001)
                return samples;

            Span<float> buffer = stackalloc float[samples.Frames * samples.Channels];
            samples.CopyToInterleaved(buffer);
            samples.Dispose();

            // Optimization - Vectorized implementation is significantly faster with AVX2 (256-bit SIMD)
            if (Vector.IsHardwareAccelerated)
            {
                var sampleVectors = MemoryMarshal.Cast<float, Vector<float>>(buffer);
                for (var vectorIndex = 0; vectorIndex < sampleVectors.Length; vectorIndex++)
                    sampleVectors[vectorIndex] *= _scale;

                for (var sampleIndex = sampleVectors.Length * Vector<float>.Count;
                    sampleIndex < buffer.Length;
                    sampleIndex++)
                    buffer[sampleIndex] *= _scale;
            }
            else
                for (var sampleIndex = 0; sampleIndex < buffer.Length; sampleIndex++)
                    buffer[sampleIndex] *= _scale;

            return new SampleBuffer(buffer, samples.Channels);
        }

        [Pure]
        static float CalculateScale([CanBeNull] string gain, [CanBeNull] string peak)
        {
            return string.IsNullOrEmpty(gain) || string.IsNullOrEmpty(peak)
                ? 1
                : Math.Min(
                    (float) Math.Pow(10, float.Parse(gain, CultureInfo.InvariantCulture) / 20),
                    1 / float.Parse(peak, CultureInfo.InvariantCulture));
        }

        [Pure, ContractAnnotation("peak:null => null; peak:notnull => notnull")]
        static string CalculatePeak([CanBeNull] string peak, float scale)
        {
            return string.IsNullOrEmpty(peak)
                ? string.Empty
                : string.Format(CultureInfo.InvariantCulture, "{0:0.000000}",
                    float.Parse(peak, CultureInfo.InvariantCulture) * scale);
        }

        [Pure, ContractAnnotation("gain:null => null; gain:notnull => notnull")]
        static string CalculateGain([CanBeNull] string gain, float scale)
        {
            return string.IsNullOrEmpty(gain)
                ? string.Empty
                : string.Format(CultureInfo.InvariantCulture, "{0:0.00}",
                    float.Parse(gain, CultureInfo.InvariantCulture) - Math.Log10(scale) * 20);
        }
    }
}
