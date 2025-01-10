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
using AudioWorks.Api.Tests.DataTypes;
using Xunit;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class AnalyzeValidFileDataSource
    {
        public static TheoryData<string, string, TestSettingDictionary, TestAudioMetadata> Data { get; } = new()
        {
            // 8000Hz Stereo, default (simple) peaks
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "ReplayGain",
                new(),
                new()
                {
#if NET472
                    TrackPeak = "0.976563",
                    AlbumPeak = "0.976563",
#else
                    TrackPeak = "0.976562",
                    AlbumPeak = "0.976562",
#endif
                    TrackGain = "-8.84",
                    AlbumGain = "-8.84"
                }
            },

            // 8000Hz Stereo, interpolated peaks
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "ReplayGain",
                new()
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new()
                {
                    TrackPeak = "0.987757",
                    AlbumPeak = "0.987757",
                    TrackGain = "-8.84",
                    AlbumGain = "-8.84"
                }
            },

            // 44100Hz Mono, default (simple) peaks
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "ReplayGain",
                new(),
                new()
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                }
            },

            // 44100Hz Mono, interpolated peaks
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "ReplayGain",
                new()
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new()
                {
                    TrackPeak = "1.342166",
                    AlbumPeak = "1.342166",
                    TrackGain = "-9.75",
                    AlbumGain = "-9.75"
                }
            },

            // 44100Hz Stereo, default (simple) peaks
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ReplayGain",
                new(),
                new()
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
            },

            // 44100Hz Stereo, interpolated peaks
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ReplayGain",
                new()
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new()
                {
                    TrackPeak = "1.012000",
                    AlbumPeak = "1.012000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
            },

            // 48000Hz Stereo, default (simple) peaks
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "ReplayGain",
                new(),
                new()
                {
                    TrackPeak = "0.999969",
                    AlbumPeak = "0.999969",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                }
            },

            // 48000Hz Stereo, interpolated peaks
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "ReplayGain",
                new()
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new()
                {
                    TrackPeak = "1.014152",
                    AlbumPeak = "1.014152",
                    TrackGain = "-8.66",
                    AlbumGain = "-8.66"
                }
            },

            // 96000Hz Stereo, default (simple) peaks
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "ReplayGain",
                new(),
                new()
                {
                    TrackPeak = "0.988553",
                    AlbumPeak = "0.988553",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                }
            },

            // 96000Hz Stereo, interpolated peaks
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "ReplayGain",
                new()
                {
                    ["PeakAnalysis"] = "Interpolated"
                },
                new()
                {
                    TrackPeak = "0.992940",
                    AlbumPeak = "0.992940",
                    TrackGain = "-8.64",
                    AlbumGain = "-8.64"
                }
            },

            // 44100Hz Stereo, simple peaks (explicit)
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ReplayGain",
                new()
                {
                    ["PeakAnalysis"] = "Simple"
                },
                new()
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
                }
            }
        };

        public static TheoryData<string> Analyzers =>
            new(Data.Select(item => item.Data.Item1).Distinct());

        public static TheoryData<string, string> FileNamesAndAnalyzers =>
            new(Data.Select(item => (item.Data.Item1, item.Data.Item2)).Distinct());
    }
}
