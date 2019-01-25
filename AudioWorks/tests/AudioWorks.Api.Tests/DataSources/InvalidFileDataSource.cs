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
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class InvalidFileDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[] { "Not RIFF Format.wav" },
            new object[] { "Unexpectedly Truncated.wav" },
            new object[] { "Not Wave Format.wav" },
            new object[] { "Missing 'fmt' Chunk.wav" },
            new object[] { "Not MPEG Audio.mp3"},
            new object[] { "Not Audio Layer III.mp3"},
            new object[] { "Not MPEG Audio.m4a" },
            new object[] { "Not Ogg Format.ogg"},
            new object[] { "Not FLAC Format.flac"},
            new object[] { "Not Opus Format.opus"}
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Data
        {
            [UsedImplicitly] get => _data;
        }
    }
}