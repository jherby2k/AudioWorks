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

using Xunit;

namespace AudioWorks.TestUtilities.DataSources
{
    public static class AudioEncoderInfoDataSource
    {
        public static TheoryData<string, string> Data { get; } = new()
        {
            { "Wave", "Waveform Audio File Format" },
            { "FLAC", "Free Lossless Audio Codec" },
#if !LINUX
            { "ALAC", "Apple Lossless Audio Codec" },
            { "AppleAAC", "Apple MPEG-4 Advanced Audio Codec" },
#endif
            { "LameMP3", "Lame MPEG Audio Layer 3" },
            { "Vorbis", "Ogg Vorbis" },
            { "Opus", "Opus" }
        };
    }
}
