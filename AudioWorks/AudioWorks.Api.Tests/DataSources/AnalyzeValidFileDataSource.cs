using AudioWorks.Api.Tests.DataTypes;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class AnalyzeValidFileDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "ReplayGain",
                null,
                new TestAudioMetadata
                {
                    TrackPeak = "0.976563",
                    AlbumPeak = "0.976563",
                    TrackGain = "-8.84",
                    AlbumGain = "-8.84"
                }
            },

            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "0.987757",
                    AlbumPeak = "0.987757",
                    TrackGain = "-8.84",
                    AlbumGain = "-8.84"
                }
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "ReplayGain",
                null,
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                }
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.342166",
                    AlbumPeak = "1.342166",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                }
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ReplayGain",
                null,
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.012000",
                    AlbumPeak = "1.012000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
            },

            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "ReplayGain",
                null,
                new TestAudioMetadata
                {
                    TrackPeak = "0.999969",
                    AlbumPeak = "0.999969",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                }
            },

            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.014152",
                    AlbumPeak = "1.014152",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                }
            },

            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "ReplayGain",
                null,
                new TestAudioMetadata
                {
                    TrackPeak = "0.988553",
                    AlbumPeak = "0.988553",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                }
            },

            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "0.992940",
                    AlbumPeak = "0.992940",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                }
            },

            new object[]
            {
                "FLAC Level 5 8-bit 8000Hz Stereo.flac",
                "ReplayGain",
                null,
                new TestAudioMetadata
                {
                    TrackPeak = "0.976563",
                    AlbumPeak = "0.976563",
                    TrackGain = "-8.84",
                    AlbumGain = "-8.84"
                }
            },

            new object[]
            {
                "FLAC Level 5 8-bit 8000Hz Stereo.flac",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "0.987757",
                    AlbumPeak = "0.987757",
                    TrackGain = "-8.84",
                    AlbumGain = "-8.84"
                }
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                "ReplayGain",
                null,
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                }
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.342166",
                    AlbumPeak = "1.342166",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                }
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                "ReplayGain",
                null,
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.012000",
                    AlbumPeak = "1.012000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
            },

            new object[]
            {
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                "ReplayGain",
                null,
                new TestAudioMetadata
                {
                    TrackPeak = "0.999969",
                    AlbumPeak = "0.999969",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                }
            },

            new object[]
            {
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.014152",
                    AlbumPeak = "1.014152",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                }
            },

            new object[]
            {
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                "ReplayGain",
                null,
                new TestAudioMetadata
                {
                    TrackPeak = "0.988553",
                    AlbumPeak = "0.988553",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                }
            },

            new object[]
            {
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "0.992940",
                    AlbumPeak = "0.992940",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                }
            },

            new object[]
            {
                "ALAC 16-bit 44100Hz Mono.m4a",
                "ReplayGain",
                null,
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                }
            },

            new object[]
            {
                "ALAC 16-bit 44100Hz Mono.m4a",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.342166",
                    AlbumPeak = "1.342166",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                }
            },

            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.012000",
                    AlbumPeak = "1.012000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
            },

            new object[]
            {
                "ALAC 16-bit 48000Hz Stereo.m4a",
                "ReplayGain",
                null,
                new TestAudioMetadata
                {
                    TrackPeak = "0.999969",
                    AlbumPeak = "0.999969",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                }
            },

            new object[]
            {
                "ALAC 16-bit 48000Hz Stereo.m4a",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "1.014152",
                    AlbumPeak = "1.014152",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                }
            },

            new object[]
            {
                "ALAC 24-bit 96000Hz Stereo.m4a",
                "ReplayGain",
                null,
                new TestAudioMetadata
                {
                    TrackPeak = "0.988553",
                    AlbumPeak = "0.988553",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                }
            },

            new object[]
            {
                "ALAC 24-bit 96000Hz Stereo.m4a",
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new TestAudioMetadata
                {
                    TrackPeak = "0.992940",
                    AlbumPeak = "0.992940",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                }
            }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Data
        {
            // Prepend an index to each row
            [UsedImplicitly] get => _data;
        }
    }
}
