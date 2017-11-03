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
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ReplayGain",
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
                new TestAudioMetadata
                {
                    TrackPeak = "1.000000",
                    AlbumPeak = "1.000000",
                    TrackGain = "-8.67",
                    AlbumGain = "-8.67"
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
