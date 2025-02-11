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

using System.Collections.Generic;
using Xunit;

namespace AudioWorks.TestUtilities.DataSources
{
    public static class InvalidFileDataSource
    {
        public static IEnumerable<TheoryDataRow<string>> Data { get; } =
        [
            "Not RIFF Format.wav",
            "Unexpectedly Truncated.wav",
            "Not Wave Format.wav",
            "Missing 'fmt' Chunk.wav",
            "Not MPEG Audio.mp3",
            "Not Audio Layer III.mp3",
            "Not MPEG Audio.m4a",
            "Not Ogg Format.ogg",
            "Not FLAC Format.flac",
            "Not Opus Format.opus"
        ];
    }
}