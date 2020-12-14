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

namespace AudioWorks.Api.Tests.DataSources
{
    public static class AudioMetadataEncoderInfoDataSource
    {
        static readonly List<object[]> _data = new()
        {
            new object[]
            {
                ".flac",
                "FLAC",
                "FLAC"
            },
            new object[]
            {
                ".m4a",
                "iTunes",
                "iTunes-compatible MPEG-4"
            },
            new object[]
            {
                ".mp3",
                "ID3",
                "ID3 version 2.x"
            },
            new object[]
            {
                ".ogg",
                "Vorbis",
                "Vorbis Comments"
            },
            new object[]
            {
                ".opus",
                "Opus",
                "Opus Comments"
            }
        };

        public static IEnumerable<object[]> Data => _data;
    }
}
