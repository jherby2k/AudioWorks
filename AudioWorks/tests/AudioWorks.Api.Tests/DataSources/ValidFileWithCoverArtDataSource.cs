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
using System.Linq;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class ValidFileWithCoverArtDataSource
    {
        static readonly List<object[]> _data = new()
        {
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            },

            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo (Covr atom - PNG).m4a",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            },

            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo (Covr atom - JPEG).m4a",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            },

            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            },

            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (APIC frame - PNG).mp3",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            },

            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (APIC frame - JPEG).mp3",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            },

            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            },

            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo (PICTURE comment - PNG).ogg",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            },

            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo (PICTURE comment - JPEG).ogg",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            },

            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            },

            new object[]
            {
                "Opus VBR 44100Hz Stereo (PICTURE comment - PNG).opus",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            },

            new object[]
            {
                "Opus VBR 44100Hz Stereo (PICTURE comment - JPEG).opus",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            },

            new object[]
            {
                "Opus VBR 44100Hz Stereo.opus",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            }
        };

        public static IEnumerable<object[]> FileNamesAndWidth => _data.Select(item => new[] { item[0], item[1] });

        public static IEnumerable<object[]> FileNamesAndHeight => _data.Select(item => new[] { item[0], item[2] });

        public static IEnumerable<object[]> FileNamesAndColorDepth => _data.Select(item => new[] { item[0], item[3] });

        public static IEnumerable<object[]> FileNamesAndLossless => _data.Select(item => new[] { item[0], item[4] });

        public static IEnumerable<object[]> FileNamesAndMimeType => _data.Select(item => new[] { item[0], item[5] });

        public static IEnumerable<object[]> FileNamesAndDataHash => _data.Select(item => new[] { item[0], item[6] });

        public static IEnumerable<object[]> IndexedFileNamesAndDataHash =>
            _data.Select((item, index) => new[] { index, item[0], item[6] });
    }
}
