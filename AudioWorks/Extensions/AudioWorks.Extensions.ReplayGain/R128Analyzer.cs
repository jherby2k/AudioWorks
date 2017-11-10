using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace AudioWorks.Extensions.ReplayGain
{
    sealed class R128Analyzer : IDisposable
    {
        [NotNull] static readonly ConcurrentDictionary<GroupToken, GroupState> _groupStates =
            new ConcurrentDictionary<GroupToken, GroupState>();

        readonly uint _channels;
        [NotNull] readonly GroupToken _groupToken;
        readonly bool _calculateTruePeaks;
        [NotNull] readonly StateHandle _handle;
        [NotNull] readonly GroupState _groupState;

        internal R128Analyzer(uint channels, uint sampleRate, [NotNull] GroupToken groupToken, bool calculateTruePeaks)
        {
            _channels = channels;
            _groupToken = groupToken;
            _calculateTruePeaks = calculateTruePeaks;
            _handle = SafeNativeMethods.Init(channels, sampleRate,
                calculateTruePeaks ? Modes.Global | Modes.TruePeak : Modes.Global | Modes.SamplePeak);
            _groupState = _groupStates.GetOrAdd(groupToken, new GroupState());
            _groupState.Handles.Add(_handle);
        }

        internal void AddFrames([NotNull] float[] frames, uint count)
        {
            SafeNativeMethods.AddFramesFloat(_handle, frames, new UIntPtr(count));
        }

        internal double GetPeak()
        {
            var absolutePeak = 0.0;

            for (uint channel = 0; channel < _channels; channel++)
            {
                double channelPeak;
                if (_calculateTruePeaks)
                    SafeNativeMethods.TruePeak(_handle, channel, out channelPeak);
                else
                    SafeNativeMethods.SamplePeak(_handle, channel, out channelPeak);
                absolutePeak = Math.Max(channelPeak, absolutePeak);
            }

            _groupState.AddPeak(absolutePeak);
            return absolutePeak;
        }

        internal double GetPeakMultiple()
        {
            return _groupState.GroupPeak;
        }

        internal double GetLoudness()
        {
            SafeNativeMethods.LoudnessGlobal(_handle, out var loudness);
            return loudness;
        }

        internal double GetLoudnessMultiple()
        {
            SafeNativeMethods.LoudnessGlobalMultiple(
                _groupState.Handles.Select(handle => handle.DangerousGetHandle()).ToArray(),
                new UIntPtr((uint) _groupState.Handles.Count),
                out var loudness);
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