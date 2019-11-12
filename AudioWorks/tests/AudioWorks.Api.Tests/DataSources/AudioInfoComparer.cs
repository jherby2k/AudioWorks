/* Copyright © 2019 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using AudioWorks.Common;
using System;
using System.Collections.Generic;

namespace AudioWorks.Api.Tests.DataSources
{
    sealed class AudioInfoComparer : IEqualityComparer<AudioInfo>
    {
        public bool Equals(AudioInfo? x, AudioInfo? y)
        {
            if (x == y) return true;
            if (x == null || y == null) return false;

            return x.Format.Equals(y.Format, StringComparison.Ordinal) &&
                   x.Channels == y.Channels &&
                   x.BitsPerSample == y.BitsPerSample &&
                   x.SampleRate == y.SampleRate &&
                   x.BitRate == y.BitRate &&
                   x.FrameCount == y.FrameCount;
        }

        public int GetHashCode(AudioInfo obj) =>
            HashCode.Combine(
                obj.Format,
                obj.Channels,
                obj.BitsPerSample,
                obj.SampleRate,
                obj.BitRate,
                obj.FrameCount);
    }
}