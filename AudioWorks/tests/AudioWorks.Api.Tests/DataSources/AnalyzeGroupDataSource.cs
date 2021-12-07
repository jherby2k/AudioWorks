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
using AudioWorks.Api.Tests.DataTypes;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class AnalyzeGroupDataSource
    {
        static readonly List<object[]> _data = new()
        {
            // 8000Hz Stereo, default (simple) peaks
            new object[]
            {
                new[]
                {
                    "Track 1 LPCM 8-bit 8000Hz Stereo.wav",
                    "Track 2 LPCM 8-bit 8000Hz Stereo.wav",
                    "Track 3 LPCM 8-bit 8000Hz Stereo.wav"
                },
                "ReplayGain",
                new TestSettingDictionary(),
                new[]
                {
                    new[]
                    {
                        new TestAudioMetadata
                        {
#if NET462
                            TrackPeak = "0.820313",
#else
                            TrackPeak = "0.820312",
#endif
                            AlbumPeak = "1.000000",
                            TrackGain = "-1.36",
                            AlbumGain = "-5.90"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "1.000000",
                            AlbumPeak = "1.000000",
                            TrackGain = "-6.49",
                            AlbumGain = "-5.90"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "1.000000",
                            AlbumPeak = "1.000000",
                            TrackGain = "-7.22",
                            AlbumGain = "-5.90"
                        }
                    }
                }
            },

            // 8000Hz Stereo, interpolated peaks
            new object[]
            {
                new[]
                {
                    "Track 1 LPCM 8-bit 8000Hz Stereo.wav",
                    "Track 2 LPCM 8-bit 8000Hz Stereo.wav",
                    "Track 3 LPCM 8-bit 8000Hz Stereo.wav"
                },
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new[]
                {
                    new[]
                    {
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.820409",
                            AlbumPeak = "1.001346",
                            TrackGain = "-1.36",
                            AlbumGain = "-5.90"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "1.001346",
                            AlbumPeak = "1.001346",
                            TrackGain = "-6.49",
                            AlbumGain = "-5.90"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "1.000070",
                            AlbumPeak = "1.001346",
                            TrackGain = "-7.22",
                            AlbumGain = "-5.90"
                        }
                    }
                }
            },

            // 44100Hz Mono, default (simple) peaks
            new object[]
            {
                new[]
                {
                    "Track 1 LPCM 16-bit 44100Hz Mono.wav",
                    "Track 2 LPCM 16-bit 44100Hz Mono.wav",
                    "Track 3 LPCM 16-bit 44100Hz Mono.wav"
                },
                "ReplayGain",
                new TestSettingDictionary(),
                new[]
                {
                    new[]
                    {
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.590515",
                            AlbumPeak = "0.965790",
                            TrackGain = "4.24",
                            AlbumGain = "-1.83"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.914001",
                            AlbumPeak = "0.965790",
                            TrackGain = "-2.03",
                            AlbumGain = "-1.83"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.965790",
                            AlbumPeak = "0.965790",
                            TrackGain = "-3.84",
                            AlbumGain = "-1.83"
                        }
                    }
                }
            },

            // 44100Hz Mono, interpolated peaks
            new object[]
            {
                new[]
                {
                    "Track 1 LPCM 16-bit 44100Hz Mono.wav",
                    "Track 2 LPCM 16-bit 44100Hz Mono.wav",
                    "Track 3 LPCM 16-bit 44100Hz Mono.wav"
                },
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new[]
                {
                    new[]
                    {
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.591049",
                            AlbumPeak = "0.966563",
                            TrackGain = "4.24",
                            AlbumGain = "-1.83"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.914502",
                            AlbumPeak = "0.966563",
                            TrackGain = "-2.03",
                            AlbumGain = "-1.83"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.966563",
                            AlbumPeak = "0.966563",
                            TrackGain = "-3.84",
                            AlbumGain = "-1.83"
                        }
                    }
                }
            },

            // 44100Hz Stereo, default (simple) peaks
            new object[]
            {
                new[]
                {
                    "Track 1 LPCM 16-bit 44100Hz Stereo.wav",
                    "Track 2 LPCM 16-bit 44100Hz Stereo.wav",
                    "Track 3 LPCM 16-bit 44100Hz Stereo.wav"
                },
                "ReplayGain",
                new TestSettingDictionary(),
                new[]
                {
                    new[]
                    {
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.813904",
                            AlbumPeak = "0.999664",
                            TrackGain = "-1.15",
                            AlbumGain = "-5.70"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.997620",
                            AlbumPeak = "0.999664",
                            TrackGain = "-6.28",
                            AlbumGain = "-5.70"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.999664",
                            AlbumPeak = "0.999664",
                            TrackGain = "-7.03",
                            AlbumGain = "-5.70"
                        }
                    }
                }
            },

            // 44100Hz Stereo, interpolated peaks
            new object[]
            {
                new[]
                {
                    "Track 1 LPCM 16-bit 44100Hz Stereo.wav",
                    "Track 2 LPCM 16-bit 44100Hz Stereo.wav",
                    "Track 3 LPCM 16-bit 44100Hz Stereo.wav"
                },
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new[]
                {
                    new[]
                    {
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.814763",
                            AlbumPeak = "1.000577",
                            TrackGain = "-1.15",
                            AlbumGain = "-5.70"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.998429",
                            AlbumPeak = "1.000577",
                            TrackGain = "-6.28",
                            AlbumGain = "-5.70"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "1.000577",
                            AlbumPeak = "1.000577",
                            TrackGain = "-7.03",
                            AlbumGain = "-5.70"
                        }
                    }
                }
            },

            // 48000Hz Stereo, default (simple) peaks
            new object[]
            {
                new[]
                {
                    "Track 1 LPCM 16-bit 48000Hz Stereo.wav",
                    "Track 2 LPCM 16-bit 48000Hz Stereo.wav",
                    "Track 3 LPCM 16-bit 48000Hz Stereo.wav"
                },
                "ReplayGain",
                new TestSettingDictionary(),
                new[]
                {
                    new[]
                    {
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.814056",
                            AlbumPeak = "0.999634",
                            TrackGain = "-1.15",
                            AlbumGain = "-5.69"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.997467",
                            AlbumPeak = "0.999634",
                            TrackGain = "-6.28",
                            AlbumGain = "-5.69"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.999634",
                            AlbumPeak = "0.999634",
                            TrackGain = "-7.02",
                            AlbumGain = "-5.69"
                        }
                    }
                }
            },

            // 48000Hz Stereo, interpolated peaks
            new object[]
            {
                new[]
                {
                    "Track 1 LPCM 16-bit 48000Hz Stereo.wav",
                    "Track 2 LPCM 16-bit 48000Hz Stereo.wav",
                    "Track 3 LPCM 16-bit 48000Hz Stereo.wav"
                },
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new[]
                {
                    new[]
                    {
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.814716",
                            AlbumPeak = "1.000568",
                            TrackGain = "-1.15",
                            AlbumGain = "-5.69"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.998379",
                            AlbumPeak = "1.000568",
                            TrackGain = "-6.28",
                            AlbumGain = "-5.69"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "1.000568",
                            AlbumPeak = "1.000568",
                            TrackGain = "-7.02",
                            AlbumGain = "-5.69"
                        }
                    }
                }
            },

            // 96000Hz Stereo, default (simple) peaks
            new object[]
            {
                new[]
                {
                    "Track 1 LPCM 24-bit 96000Hz Stereo.wav",
                    "Track 2 LPCM 24-bit 96000Hz Stereo.wav",
                    "Track 3 LPCM 24-bit 96000Hz Stereo.wav"
                },
                "ReplayGain",
                new TestSettingDictionary(),
                new[]
                {
                    new[]
                    {
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.814049",
                            AlbumPeak = "0.999651",
                            TrackGain = "-1.13",
                            AlbumGain = "-5.67"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.997602",
                            AlbumPeak = "0.999651",
                            TrackGain = "-6.26",
                            AlbumGain = "-5.67"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.999651",
                            AlbumPeak = "0.999651",
                            TrackGain = "-7.00",
                            AlbumGain = "-5.67"
                        }
                    }
                }
            },

            // 96000Hz Stereo, interpolated peaks
            new object[]
            {
                new[]
                {
                    "Track 1 LPCM 24-bit 96000Hz Stereo.wav",
                    "Track 2 LPCM 24-bit 96000Hz Stereo.wav",
                    "Track 3 LPCM 24-bit 96000Hz Stereo.wav"
                },
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new[]
                {
                    new[]
                    {
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.814189",
                            AlbumPeak = "0.999758",
                            TrackGain = "-1.13",
                            AlbumGain = "-5.67"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.997720",
                            AlbumPeak = "0.999758",
                            TrackGain = "-6.26",
                            AlbumGain = "-5.67"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.999758",
                            AlbumPeak = "0.999758",
                            TrackGain = "-7.00",
                            AlbumGain = "-5.67"
                        }
                    }
                }
            },

            // 44100Hz Stereo, simple peaks (explicit)
            new object[]
            {
                new[]
                {
                    "Track 1 LPCM 16-bit 44100Hz Stereo.wav",
                    "Track 2 LPCM 16-bit 44100Hz Stereo.wav",
                    "Track 3 LPCM 16-bit 44100Hz Stereo.wav"
                },
                "ReplayGain",
                new TestSettingDictionary
                {
                    ["PeakAnalysis"] = "Simple"
                },
                new[]
                {
                    new[]
                    {
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.813904",
                            AlbumPeak = "0.999664",
                            TrackGain = "-1.15",
                            AlbumGain = "-5.70"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.997620",
                            AlbumPeak = "0.999664",
                            TrackGain = "-6.28",
                            AlbumGain = "-5.70"
                        },
                        new TestAudioMetadata
                        {
                            TrackPeak = "0.999664",
                            AlbumPeak = "0.999664",
                            TrackGain = "-7.03",
                            AlbumGain = "-5.70"
                        }
                    }
                }
            }
        };

        public static IEnumerable<object[]> Data => _data;
    }
}
