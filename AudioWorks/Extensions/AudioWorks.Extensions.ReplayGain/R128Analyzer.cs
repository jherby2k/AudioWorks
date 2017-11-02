using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Collections.Concurrent;

namespace AudioWorks.Extensions.ReplayGain
{
    sealed class R128Analyzer : IDisposable
    {
        [NotNull] static readonly ConcurrentDictionary<GroupToken, ConcurrentBag<StateHandle>> _globalHandles =
            new ConcurrentDictionary<GroupToken, ConcurrentBag<StateHandle>>();

        readonly uint _channels;
        [NotNull] readonly StateHandle _handle;
        [NotNull] readonly ConcurrentBag<StateHandle> _groupHandles;

        internal R128Analyzer(uint channels, uint sampleRate, [NotNull] GroupToken groupToken)
        {
            _channels = channels;
            _handle = SafeNativeMethods.Initialize(channels, sampleRate, Modes.Global | Modes.SamplePeak);
            _groupHandles = _globalHandles.GetOrAdd(groupToken, new ConcurrentBag<StateHandle>());
            _groupHandles.Add(_handle);
        }

        internal void AddFrames([NotNull] float[] frames)
        {
            SafeNativeMethods.AddFramesFloat(_handle, frames, (uint) frames.LongLength / _channels);
        }

        internal double GetSamplePeak()
        {
            var absolutePeak = 0.0;

            for (uint channel = 0; channel < _channels; channel++)
            {
                SafeNativeMethods.SamplePeak(_handle, channel, out var channelPeak);
                absolutePeak = Math.Max(channelPeak, absolutePeak);
            }

            return absolutePeak;
        }

        internal double GetSamplePeakMultiple()
        {
            var absolutePeak = 0.0;

            foreach (var trackHandle in _groupHandles)
                for (uint channel = 0; channel < _channels; channel++)
                {
                    SafeNativeMethods.SamplePeak(trackHandle, channel, out var channelPeak);
                    absolutePeak = Math.Max(channelPeak, absolutePeak);
                }

            return absolutePeak;
        }

        internal double GetLoudness()
        {
            SafeNativeMethods.LoudnessGlobal(_handle, out var loudness);
            return loudness;
        }

        internal double GetLoudnessMultiple()
        {
            SafeNativeMethods.LoudnessGlobalMultiple(_groupHandles.ToArray(), out var loudness);
            return loudness;
        }

        public void Dispose()
        {
            _handle.Dispose();
        }
    }
}