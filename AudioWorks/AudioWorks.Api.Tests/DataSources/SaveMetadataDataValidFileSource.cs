using AudioWorks.Common;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class SaveMetadataDataValidFileSource
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
                null,
                "31D7FA433DE238316E866C2F01438774"
            },
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
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                },
                null,
                "5A82FBCEC6956464CD6FF47CA01E1722"
            },
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
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                },
                null,
                "5A82FBCEC6956464CD6FF47CA01E1722"
            },
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
                    TrackNumber = "01"
                },
                null,
                "E2261D4647D4460A66A4D04504E09785"
            },
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
                    TrackCount = "12"
                },
                null,
                "3BA138E7E248A13ECEDEDCC9BAA5F00E"
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
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
                null,
                "C3B980749573D1EC7F0BCDCCCCA18CAD"
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
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
                new SettingDictionary
                {
                    ["Version"] = "2.4"
                },
                "B4E482019F195AD273CD490A6A877AA3"
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
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
                new SettingDictionary
                {
                    ["Padding"] = 0
                },
                "31D7FA433DE238316E866C2F01438774"
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (ID3v1).mp3",
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
                null,
                "31D7FA433DE238316E866C2F01438774"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
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
                null,
                "98350CA7615C73841CAF148CDB7DE496"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new AudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                },
                null,
                "48AF7FF63B3013026C5F52B0583E2A47"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new AudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12"
                },
                null,
                "48AF7FF63B3013026C5F52B0583E2A47"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
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
                },
                null,
                "C71F8BFCD9CFC110D8917EC65FCC1591"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
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
                    TrackCount = "12"
                },
                null,
                "5970D8A15E2A1F1DE56502167BF31130"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
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
                null,
                "98350CA7615C73841CAF148CDB7DE496"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
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
                new SettingDictionary
                {
                    ["Padding"] = 0
                },
                "125A73BB863C358B6A1CCF0C89C13C63"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
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
                new SettingDictionary
                {
                    ["Padding"] = 100
                },
                "0E0E483A3B65B264F6E5FA8EC27112E5"
            }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Data
        {
            // Prepend an index to each row
            [UsedImplicitly] get => _data.Select((item, index) => item.Prepend(index).ToArray());
        }
    }
}
