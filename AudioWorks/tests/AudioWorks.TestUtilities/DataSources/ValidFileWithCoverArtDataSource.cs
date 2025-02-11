﻿/* Copyright © 2018 Jeremy Herbison

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
using System.Linq;
using Xunit;

namespace AudioWorks.TestUtilities.DataSources
{
    public static class ValidFileWithCoverArtDataSource
    {
        static readonly IEnumerable<TheoryDataRow<string, int, int, int, bool, string, string>> _data =
        [
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            ),
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            ),
            new(
                "ALAC 16-bit 44100Hz Stereo (Covr atom - PNG).m4a",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            ),
            new(
                "ALAC 16-bit 44100Hz Stereo (Covr atom - JPEG).m4a",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            ),
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            ),
            new(
                "Lame CBR 128 44100Hz Stereo (APIC frame - PNG).mp3",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            ),
            new(
                "Lame CBR 128 44100Hz Stereo (APIC frame - JPEG).mp3",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            ),
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (PICTURE comment - PNG).ogg",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo (PICTURE comment - JPEG).ogg",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            ),
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            ),
            new(
                "Opus VBR 44100Hz Stereo (PICTURE comment - PNG).opus",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            ),
            new(
                "Opus VBR 44100Hz Stereo (PICTURE comment - JPEG).opus",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            ),
            new(
                "Opus VBR 44100Hz Stereo.opus",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            )
        ];

        public static IEnumerable<TheoryDataRow<string, int>> FileNamesAndWidth =>
            _data.Select(item => new TheoryDataRow<string, int>(item.Data.Item1, item.Data.Item2)
                { Skip = item.Skip });

        public static IEnumerable<TheoryDataRow<string, int>> FileNamesAndHeight =>
            _data.Select(item => new TheoryDataRow<string, int>(item.Data.Item1, item.Data.Item3)
                { Skip = item.Skip });

        public static IEnumerable<TheoryDataRow<string, int>> FileNamesAndColorDepth =>
            _data.Select(item => new TheoryDataRow<string, int>(item.Data.Item1, item.Data.Item4)
                { Skip = item.Skip });

        public static IEnumerable<TheoryDataRow<string, bool>> FileNamesAndLossless =>
            _data.Select(item => new TheoryDataRow<string, bool>(item.Data.Item1, item.Data.Item5)
                { Skip = item.Skip });

        public static TheoryData<string, string> FileNamesAndMimeType =>
            new(_data.Select(item => (item.Data.Item1, item.Data.Item6)));

        public static TheoryData<string, string> FileNamesAndDataHash =>
            new(_data.Select(item => (item.Data.Item1, item.Data.Item7)));

        public static TheoryData<int, string, string> IndexedFileNamesAndDataHash =>
            new(_data.Select((item, index) => (index, item.Data.Item1, item.Data.Item7)));
    }
}
