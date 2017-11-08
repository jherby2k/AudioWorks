using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Collections.Concurrent;

namespace AudioWorks.Extensions.ReplayGain
{
    sealed class R128Analyzer : IDisposable
    {
        [NotNull] static readonly ConcurrentDictionary<GroupToken, GroupState> _groupStates =
            new ConcurrentDictionary<GroupToken, GroupState>();

        readonly uint _channels;
        [NotNull] readonly GroupToken _groupToken;
        [NotNull] readonly StateHandle _handle;
        [NotNull] readonly GroupState _groupState;

        internal R128Analyzer(uint channels, uint sampleRate, [NotNull] GroupToken groupToken)
        {
            _channels = channels;
            _groupToken = groupToken;
            _handle = SafeNativeMethods.Initialize(channels, sampleRate, Modes.Global | Modes.TruePeak);
            _groupState = _groupStates.GetOrAdd(groupToken, new GroupState());
            _groupState.Handles.Add(_handle);
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

            foreach (var trackHandle in _groupState.Handles)
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
            SafeNativeMethods.LoudnessGlobalMultiple(_groupState.Handles.ToArray(), out var loudness);
            return loudness;
        }

        public void Dispose()
        {
            if (_groupState.SignalHandleDisposing() != _groupToken.Count)
                return;

            // Dispose all the handles at once
            while (_groupState.Handles.TryTake(out var handle))
                handle.Dispose();

            _groupStates.TryRemove(_groupToken, out _);
        }
    }
}