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

using System.Linq;
using Xunit;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class ValidFileWithCoverArtDataSource
    {
        static readonly TheoryData<string, int, int, int, bool, string, string> _data = new()
        {
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            },
            {
                "ALAC 16-bit 44100Hz Stereo (Covr atom - PNG).m4a",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            },
            {
                "ALAC 16-bit 44100Hz Stereo (Covr atom - JPEG).m4a",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            },
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            },
            {
                "Lame CBR 128 44100Hz Stereo (APIC frame - PNG).mp3",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            },
            {
                "Lame CBR 128 44100Hz Stereo (APIC frame - JPEG).mp3",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            },
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (PICTURE comment - PNG).ogg",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            },
            {
                "Vorbis Quality 3 44100Hz Stereo (PICTURE comment - JPEG).ogg",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            },
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                0,
                0,
                0,
                false,
                string.Empty,
                string.Empty
            },
            {
                "Opus VBR 44100Hz Stereo (PICTURE comment - PNG).opus",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            },
            {
                "Opus VBR 44100Hz Stereo (PICTURE comment - JPEG).opus",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            },
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

        public static TheoryData<string, int> FileNamesAndWidth
        {
            get
            {
                var results = new TheoryData<string, int>();
                foreach (var result in _data.Select(item => ((string) item[0], (int) item[1])))
                    results.Add(result.Item1, result.Item2);
                return results;
            }
        }

        public static TheoryData<string, int> FileNamesAndHeight
        {
            get
            {
                var results = new TheoryData<string, int>();
                foreach (var result in _data.Select(item => ((string) item[0], (int) item[2])))
                    results.Add(result.Item1, result.Item2);
                return results;
            }
        }

        public static TheoryData<string, int> FileNamesAndColorDepth
        {
            get
            {
                var results = new TheoryData<string, int>();
                foreach (var result in _data.Select(item => ((string) item[0], (int) item[3])))
                    results.Add(result.Item1, result.Item2);
                return results;
            }
        }

        public static TheoryData<string, bool> FileNamesAndLossless
        {
            get
            {
                var results = new TheoryData<string, bool>();
                foreach (var result in _data.Select(item => ((string) item[0], (bool) item[4])))
                    results.Add(result.Item1, result.Item2);
                return results;
            }
        }

        public static TheoryData<string, string> FileNamesAndMimeType
        {
            get
            {
                var results = new TheoryData<string, string>();
                foreach (var result in _data.Select(item => ((string) item[0], (string) item[5])))
                    results.Add(result.Item1, result.Item2);
                return results;
            }
        }

        public static TheoryData<string, string> FileNamesAndDataHash
        {
            get
            {
                var results = new TheoryData<string, string>();
                foreach (var result in _data.Select(item => ((string) item[0], (string) item[6])))
                    results.Add(result.Item1, result.Item2);
                return results;
            }
        }

        public static TheoryData<int, string, string> IndexedFileNamesAndDataHash
        {
            get
            {
                var results = new TheoryData<int, string, string>();
                foreach (var result in _data.Select((item, index) => (index, (string) item[0], (string) item[6])))
                    results.Add(result.Item1, result.Item2, result.Item3);
                return results;
            }
        }
    }
}
