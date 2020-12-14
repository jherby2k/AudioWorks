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
using AudioWorks.Api.Tests.DataTypes;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class AnalyzeValidFileDataSource
    {
        static readonly List<object[]> _data = new()
        {
            // 8000Hz Stereo, default (simple) peaks
            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "ReplayGain",
                new TestSettingDictionary(),
                new[]
                {
                    new TestAudioMetadata // .NET Core 3.0+
                    {
                        TrackPeak = "0.976562",
                        AlbumPeak = "0.976562",
                        TrackGain = "-8.84",
                        AlbumGain = "-8.84"
                    },
                    new TestAudioMetadata // Legacy .NET
                    {
                        TrackPeak = "0.976563",
                        AlbumPeak = "0.976563",
                        TrackGain = "-8.84",
                        AlbumGain = "-8.84"
                    }
                }
            },

            // 8000Hz Stereo, interpolated peaks
            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new[]
                {
                    new TestAudioMetadata // libebur128 1.1.0 (Ubuntu 16.04)
                    {
                        TrackPeak = "0.988234",
                        AlbumPeak = "0.988234",
                        TrackGain = "-8.84",
                        AlbumGain = "-8.84"
                    },
                    new TestAudioMetadata // libebur128 1.2.4
                    {
                        TrackPeak = "0.987757",
                        AlbumPeak = "0.987757",
                        TrackGain = "-8.84",
                        AlbumGain = "-8.84"
                    }
                }
            },

            // 44100Hz Mono, default (simple) peaks
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "ReplayGain",
                new TestSettingDictionary(),
                new[]
                {
                    new TestAudioMetadata
                    {
                        TrackPeak = "1.000000",
                        AlbumPeak = "1.000000",
                        TrackGain = "-9.75",
                        AlbumGain = "-9.75"
                    }
                }
            },

            // 44100Hz Mono, interpolated peaks
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new[]
                {
                    new TestAudioMetadata // libebur128 1.1.0 (Ubuntu 16.04)
                    {
                        TrackPeak = "1.326816",
                        AlbumPeak = "1.326816",
                        TrackGain = "-9.75",
                        AlbumGain = "-9.75"
                    },
                    new TestAudioMetadata // libebur128 1.2.4
                    {
                        TrackPeak = "1.342166",
                        AlbumPeak = "1.342166",
                        TrackGain = "-9.75",
                        AlbumGain = "-9.75"
                    }
                }
            },

            // 44100Hz Stereo, default (simple) peaks
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ReplayGain",
                new TestSettingDictionary(),
                new[]
                {
                    new TestAudioMetadata
                    {
                        TrackPeak = "1.000000",
                        AlbumPeak = "1.000000",
                        TrackGain = "-8.67",
                        AlbumGain = "-8.67"
                    }
                }
            },

            // 44100Hz Stereo, interpolated peaks
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new[]
                {
                    new TestAudioMetadata // libebur128 1.1.0 (Ubuntu 16.04)
                    {
                        TrackPeak = "1.013034",
                        AlbumPeak = "1.013034",
                        TrackGain = "-8.67",
                        AlbumGain = "-8.67"
                    },
                    new TestAudioMetadata // libebur128 1.2.4
                    {
                        TrackPeak = "1.012000",
                        AlbumPeak = "1.012000",
                        TrackGain = "-8.67",
                        AlbumGain = "-8.67"
                    }
                }
            },

            // 48000Hz Stereo, default (simple) peaks
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "ReplayGain",
                new TestSettingDictionary(),
                new[]
                {
                    new TestAudioMetadata
                    {
                        TrackPeak = "0.999969",
                        AlbumPeak = "0.999969",
                        TrackGain = "-8.66",
                        AlbumGain = "-8.66"
                    }
                }
            },

            // 48000Hz Stereo, interpolated peaks
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new[]
                {
                    new TestAudioMetadata // libebur128 1.1.0 (Ubuntu 16.04)
                    {
                        TrackPeak = "1.011720",
                        AlbumPeak = "1.011720",
                        TrackGain = "-8.66",
                        AlbumGain = "-8.66"
                    },
                    new TestAudioMetadata // libebur128 1.2.4
                    {
                        TrackPeak = "1.014152",
                        AlbumPeak = "1.014152",
                        TrackGain = "-8.66",
                        AlbumGain = "-8.66"
                    }
                }
            },

            // 96000Hz Stereo, default (simple) peaks
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "ReplayGain",
                new TestSettingDictionary(),
                new[]
                {
                    new TestAudioMetadata
                    {
                        TrackPeak = "0.988553",
                        AlbumPeak = "0.988553",
                        TrackGain = "-8.64",
                        AlbumGain = "-8.64"
                    }
                }
            },

            // 96000Hz Stereo, interpolated peaks
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new[]
                {
                    new TestAudioMetadata // libebur128 1.1.0 (Ubuntu 16.04)
                    {
                        TrackPeak = "0.993001",
                        AlbumPeak = "0.993001",
                        TrackGain = "-8.64",
                        AlbumGain = "-8.64"
                    },
                    new TestAudioMetadata // libebur128 1.2.4
                    {
                        TrackPeak = "0.992940",
                        AlbumPeak = "0.992940",
                        TrackGain = "-8.64",
                        AlbumGain = "-8.64"
                    }
                }
            },

            // 44100Hz Stereo, simple peaks (explicit)
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Simple"
                },
                new[]
                {
                    new TestAudioMetadata
                    {
                        TrackPeak = "1.000000",
                        AlbumPeak = "1.000000",
                        TrackGain = "-8.67",
                        AlbumGain = "-8.67"
                    }
                }
            }
        };

        public static IEnumerable<object[]> Data => _data;

        public static IEnumerable<object[]> Analyzers =>
            _data.Select(item => new[] { item[1] }).Distinct(new ArrayComparer());

        public static IEnumerable<object[]> FileNamesAndAnalyzers =>
            _data.Select(item => new[] { item[0], item[1] }).Distinct(new ArrayComparer());
    }
}
