using JetBrains.Annotations;
using System;
using System.Linq;

namespace AudioWorks.Extensions.ReplayGain
{
    sealed class R128Analyzer
    {
        readonly uint _channels;
        readonly bool _calculateTruePeaks;

        [NotNull]
        internal StateHandle Handle { get; }

        internal R128Analyzer(uint channels, uint sampleRate, bool calculateTruePeaks)
        {
            _channels = channels;
            _calculateTruePeaks = calculateTruePeaks;
            Handle = SafeNativeMethods.Init(channels, sampleRate,
                calculateTruePeaks ? Modes.Global | Modes.TruePeak : Modes.Global | Modes.SamplePeak);
        }

        internal void AddFrames([NotNull] float[] frames, uint count)
        {
            SafeNativeMethods.AddFramesFloat(Handle, frames, new UIntPtr(count));
        }

        internal double GetPeak()
        {
            var absolutePeak = 0.0;

            for (uint channel = 0; channel < _channels; channel++)
            {
                double channelPeak;
                if (_calculateTruePeaks)
                    SafeNativeMethods.TruePeak(Handle, channel, out channelPeak);
                else
                    SafeNativeMethods.SamplePeak(Handle, channel, out channelPeak);
                absolutePeak = Math.Max(channelPeak, absolutePeak);
            }

            return absolutePeak;
        }

        internal double GetLoudness()
        {
            SafeNativeMethods.LoudnessGlobal(Handle, out var loudness);
            return loudness;
        }

        internal double GetLoudnessMultiple([NotNull, ItemNotNull] StateHandle[] handles)
        {
            SafeNativeMethods.LoudnessGlobalMultiple(
                handles.Select(handle => handle.DangerousGetHandle()).ToArray(),
                new UIntPtr((uint) handles.Length),
                out var loudness);
            return loudness;
        }
    }
}