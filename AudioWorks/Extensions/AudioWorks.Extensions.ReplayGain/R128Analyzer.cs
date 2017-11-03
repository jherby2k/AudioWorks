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
        [NotNull] readonly GroupToken _groupToken;
        [NotNull] readonly StateHandle _handle;
        [NotNull] readonly ConcurrentBag<StateHandle> _groupHandles;

        internal R128Analyzer(uint channels, uint sampleRate, [NotNull] GroupToken groupToken)
        {
            _channels = channels;
            _groupToken = groupToken;
            _handle = SafeNativeMethods.Initialize(channels, sampleRate, Modes.Global | Modes.TruePeak);
            _groupHandles = _globalHandles.GetOrAdd(groupToken, new ConcurrentBag<StateHandle>());
            _groupHandles.Add(_handle);
        }

        internal void AddFrames([NotNull] float[] frames, uint count)
        {
            SafeNativeMethods.AddFramesFloat(_handle, frames, count);
        }

        internal double GetTruePeak()
        {
            var absolutePeak = 0.0;

            for (uint channel = 0; channel < _channels; channel++)
            {
                SafeNativeMethods.TruePeak(_handle, channel, out var channelPeak);
                absolutePeak = Math.Max(channelPeak, absolutePeak);
            }

            return Math.Min(absolutePeak, 1.0);
        }

        internal double GetTruePeakMultiple()
        {
            var absolutePeak = 0.0;

            foreach (var trackHandle in _groupHandles)
                for (uint channel = 0; channel < _channels; channel++)
                {
                    SafeNativeMethods.TruePeak(trackHandle, channel, out var channelPeak);
                    absolutePeak = Math.Max(channelPeak, absolutePeak);
                }

            return Math.Min(absolutePeak, 1.0);
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
            // The first group member to dispose will take care of all the handles
            if (!_globalHandles.TryRemove(_groupToken, out _))
                return;

            foreach (var handle in _groupHandles)
                handle.Dispose();
        }
    }
}