using AudioWorks.Common;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace AudioWorks.Api.Tests
{
    public static class TestFilesValidSaveMetadataDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new AudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                },
                "31D7FA433DE238316E866C2F01438774"
            }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Data
        {
            [UsedImplicitly] get => _data;
        }
    }
}
