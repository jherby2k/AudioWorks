/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Collections.Generic;
using System.Linq;
using AudioWorks.Api.Tests.DataTypes;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class AnalyzeValidFileDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            // 8000Hz Stereo, default (simple) peaks
            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "ReplayGain",
                null,
#if LINUX
                new TestAudioMetadata
                {
                    TrackPeak = "0.976563",
                    AlbumPeak = "0.976563",
                    TrackGain = "-8.84",
                    AlbumGain = "-8.84"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "0.976563",
                    AlbumPeak = "0.976563",
                    TrackGain = "-8.84",
                    AlbumGain = "-8.84"
                }
#else
                new TestAudioMetadata
                {
                    TrackPeak = "0.976563",
                    AlbumPeak = "0.976563",
                    TrackGain = "-8.84",
                    AlbumGain = "-8.84"
                }
#endif
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
#if LINUX
                new TestAudioMetadata
                {
                    TrackPeak = "0.988234",
                    AlbumPeak = "0.988234",
                    TrackGain = "-8.84",
                    AlbumGain = "-8.84"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "0.987757",
                    AlbumPeak = "0.987757",
                    TrackGain = "-8.84",
                    AlbumGain = "-8.84"
                }
#else
                new TestAudioMetadata
                {
                    TrackPeak = "0.987757",
                    AlbumPeak = "0.987757",
                    TrackGain = "-8.84",
                    AlbumGain = "-8.84"
                }
#endif
            },

            // 44100Hz Mono, default (simple) peaks
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "ReplayGain",
                null,
#if LINUX
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                }
#else
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                }
#endif
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
#if LINUX
                new TestAudioMetadata
                {
                    TrackPeak = "1.326816",
                    AlbumPeak = "1.326816",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.342166",
                    AlbumPeak = "1.342166",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                }
#else
                new TestAudioMetadata
                {
                    TrackPeak = "1.342166",
                    AlbumPeak = "1.342166",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                }
#endif
            },

            // 44100Hz Stereo, default (simple) peaks
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ReplayGain",
                null,
#if LINUX
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
#else
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
#endif
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
#if LINUX
                new TestAudioMetadata
                {
                    TrackPeak = "1.013034",
                    AlbumPeak = "1.013034",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.012000",
                    AlbumPeak = "1.012000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
#else
                new TestAudioMetadata
                {
                    TrackPeak = "1.012000",
                    AlbumPeak = "1.012000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
#endif
            },

            // 48000Hz Stereo, default (simple) peaks
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "ReplayGain",
                null,
#if LINUX
                new TestAudioMetadata
                {
                    TrackPeak = "0.999969",
                    AlbumPeak = "0.999969",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "0.999969",
                    AlbumPeak = "0.999969",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                }
#else
                new TestAudioMetadata
                {
                    TrackPeak = "0.999969",
                    AlbumPeak = "0.999969",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                }
#endif
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
#if LINUX
                new TestAudioMetadata
                {
                    TrackPeak = "1.011720",
                    AlbumPeak = "1.011720",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.014152",
                    AlbumPeak = "1.014152",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                }
#else
                new TestAudioMetadata
                {
                    TrackPeak = "1.014152",
                    AlbumPeak = "1.014152",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                }
#endif
            },

            // 96000Hz Stereo, default (simple) peaks
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "ReplayGain",
                null,
#if LINUX
                new TestAudioMetadata
                {
                    TrackPeak = "0.988553",
                    AlbumPeak = "0.988553",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "0.988553",
                    AlbumPeak = "0.988553",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                }
#else
                new TestAudioMetadata
                {
                    TrackPeak = "0.988553",
                    AlbumPeak = "0.988553",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                }
#endif
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
#if LINUX
                new TestAudioMetadata
                {
                    TrackPeak = "0.993001",
                    AlbumPeak = "0.993001",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "0.992940",
                    AlbumPeak = "0.992940",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                }
#else
                new TestAudioMetadata
                {
                    TrackPeak = "0.992940",
                    AlbumPeak = "0.992940",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                }
#endif
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
#if LINUX
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
#else
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
#endif
            }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Data
        {
            [UsedImplicitly] get => _data;
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Analyzers
        {
            [UsedImplicitly]
            get => _data.Select(item => new[] { item[1] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndAnalyzers
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[1] });
        }
    }
}
