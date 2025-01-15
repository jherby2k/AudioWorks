/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.ReplayGain
{
    sealed class R128Analyzer
    {
        readonly uint _channels;
        readonly bool _calculateTruePeaks;

        internal StateHandle Handle { get; }

        internal R128Analyzer(uint channels, uint sampleRate, bool calculateTruePeaks)
        {
            _channels = channels;
            _calculateTruePeaks = calculateTruePeaks;
            Handle = SafeNativeMethods.Init(channels, new(sampleRate),
                calculateTruePeaks ? Modes.Global | Modes.TruePeak : Modes.Global | Modes.SamplePeak);
        }

        internal void AddFrames(Span<float> samples, uint frames) =>
            SafeNativeMethods.AddFramesFloat(Handle, MemoryMarshal.GetReference(samples), new(frames));

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

        internal static double GetLoudnessMultiple(StateHandle[] handles)
        {
            SafeNativeMethods.LoudnessGlobalMultiple(
                handles.Select(handle => handle.DangerousGetHandle()).ToArray(),
                new((uint) handles.Length),
                out var loudness);
            return loudness;
        }
    }
}