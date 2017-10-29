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
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
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
