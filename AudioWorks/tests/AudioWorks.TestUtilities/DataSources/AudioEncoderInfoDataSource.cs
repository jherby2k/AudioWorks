/* Copyright © 2020 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xunit;

namespace AudioWorks.TestUtilities.DataSources
{
    public static class AudioEncoderInfoDataSource
    {
        public static IEnumerable<TheoryDataRow<string, string>> Data { get; } =
        [
            new("Wave", "Waveform Audio File Format"),
            new("FLAC", "Free Lossless Audio Codec"),
            new("ALAC", "Apple Lossless Audio Codec")
                { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new("AppleAAC", "Apple MPEG-4 Advanced Audio Codec")
                { Skip = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Not supported on Linux" : null },
            new("LameMP3", "Lame MPEG Audio Layer 3"),
            new("Vorbis", "Ogg Vorbis"),
            new("Opus", "Opus")
        ];
    }
}
