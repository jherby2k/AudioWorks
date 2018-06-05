using System.Collections.Generic;
using System.Linq;
using AudioWorks.Api.Tests.DataTypes;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class SaveMetadataValidFileSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            #region FLAC

            // All fields
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "79528DB2721970437C6D8877F655E273"
            },

            // Day unset
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "B903A8E9B17014CDDB563A3CC73AB7F5"
            },

            // Month unset
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "B903A8E9B17014CDDB563A3CC73AB7F5"
            },

            // TrackNumber unset
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "C830D5913F20AEF93853E1A01462708D"
            },

            // TrackCount unset
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "27C19A7309E49A5B07C0AD932A7B2875"
            },

            // Existing tag
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "79528DB2721970437C6D8877F655E273"
            },

            // No padding
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                new TestSettingDictionary
                {
                    ["Padding"] = 0
                },
                "2F893F4E2A6FCF6507763801A1CD908C"
            },

            // 100 bytes of padding
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                new TestSettingDictionary
                {
                    ["Padding"] = 100
                },
                "D10242261CDBC3FF2D9BEB2C232DC1F7"
            },

            // Existing tag removal
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                new TestAudioMetadata(),
                null,
                null,
                "3983A342A074A7E8871FEF4FBE0AC73F"
            },

            // Nothing to do
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata(),
                null,
                null,
                "3983A342A074A7E8871FEF4FBE0AC73F"
            },

            // PNG CoverArt
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                null,
                "ABC14285A7AA837A876EA751BEF07678"
            },

            // JPEG CoverArt
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                null,
                "1579D820A2BD52D8A1438306AA291113"
            },

            #endregion

            #region MP4

            // All fields
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "7F7316891B26F9FC3DE7D2D1304F9CFE"
            },

            // Day unset
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "9957A905E53C3D2411A3A4BF3DA4DBA3"
            },

            // Month unset
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "9957A905E53C3D2411A3A4BF3DA4DBA3"
            },

            // TrackNumber unset
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "847E8C85293966F62132E8F851FA7BAE"
            },

            // TrackCount unset
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "BD8C422B4F08A50532168922680B0C2E"
            },

            // Existing tag
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "7F7316891B26F9FC3DE7D2D1304F9CFE"
            },

            // Existing tag removal
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
                new TestAudioMetadata(),
                null,
                null,
                "090FD975097BAFC4164370A3DEA9E696"
            },

            // Nothing to do
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                null,
                null,
                "090FD975097BAFC4164370A3DEA9E696"
            },

            // Default padding (explicit)
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                null,
                new TestSettingDictionary
                {
                    ["Padding"] = 2048
                },
                "090FD975097BAFC4164370A3DEA9E696"
            },

            // Disabled padding
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                null,
                new TestSettingDictionary
                {
                    ["Padding"] = 0
                },
                "2A66A8458C32EC663AE48C6294E829AB"
            },

            // Maximum padding
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                null,
                new TestSettingDictionary
                {
                    ["Padding"] = 16_777_216
                },
                "D4615919A461B54512B3863ADD487D4B"
            },

            // PNG CoverArt (ALAC)
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                null,
                "45960157DDA201516AF0F69AD0CF3411"
            },

            // JPEG CoverArt (ALAC)
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                null,
                "981B4257C1C506872433846D856C7C88"
            },

            // PNG CoverArt (AAC, converted)
            new object[]
            {
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                null,
                "1D9FE9A1BF10D593B1951D5EC3713EB4"
            },

            // JPEG CoverArt (AAC)
            new object[]
            {
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                null,
                "1D9FE9A1BF10D593B1951D5EC3713EB4"
            },

            #endregion

            #region ID3

            // All fields
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "D44FACB05A7F0CB3CB5F5A31B9B52022"
            },

            // Day unset
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "C64330448267EA7595D19378766B38C5"
            },

            // Month unset
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "C64330448267EA7595D19378766B38C5"
            },

            // TrackNumber unset
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "8E71F084665F7763560EAEF79292B1ED"
            },

            // TrackCount unset
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "B4BAD75711B480844A735B8EF169F82A"
            },

            // Existing tag
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "076B838A43E883DCC9F0D0ABE8A263D6"
            },

            // Tag version 2.4
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                new TestSettingDictionary
                {
                    ["TagVersion"] = "2.4"
                },
                "E00628B6DA2A93F2832EAC420B2D8DF0"
            },

            // UTF-16 encoding
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                new TestSettingDictionary
                {
                    ["TagEncoding"] = "UTF16"
                },
                "476D2A59E830366CBFF9F0AE0305B8E2"
            },

            // Default padding (explicit)
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                new TestSettingDictionary
                {
                    ["TagPadding"] = 2048
                },
                "D44FACB05A7F0CB3CB5F5A31B9B52022"
            },

            // No padding
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                new TestSettingDictionary
                {
                    ["TagPadding"] = 0
                },
                "076B838A43E883DCC9F0D0ABE8A263D6"
            },

            // Maximum padding
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                new TestSettingDictionary
                {
                    ["TagPadding"] = 16_777_216
                },
                "189A55453749DA1FFAFFEAC6A06DF99B"
            },

            // Existing v1 tag
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (ID3v1).mp3",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "D44FACB05A7F0CB3CB5F5A31B9B52022"
            },

            // Existing tag removal
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
                new TestAudioMetadata(),
                null,
                null,
                "963D578D818C25DE5FEE6625BE7BFA98"
            },

            // Nothing to do
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata(),
                null,
                null,
                "963D578D818C25DE5FEE6625BE7BFA98"
            },

            // PNG CoverArt (converted)
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                null,
                "0C3A7576B5A8E17BCC2FD93A3F45C635"
            },

            // JPEG CoverArt
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                null,
                "0C3A7576B5A8E17BCC2FD93A3F45C635"
            },

            #endregion

            #region Ogg Vorbis

            // All fields
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "11F166F272E635021ABD6BAF37A3BFA5"
            },

            // Day unset
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "63BB3732D651F6D3A8496091FCF14725"
            },

            // Month unset
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "63BB3732D651F6D3A8496091FCF14725"
            },

            // TrackNumber unset
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "ED7210753CB1A870F74DB01E959D2FFE"
            },

            // TrackCount unset
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "17F5C68F72027655E21870F0E7F06CB8"
            },

            // Existing tag
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using defaults).ogg",
                new TestAudioMetadata
                {
                    Title = "Test Title",
                    Artist = "Test Artist",
                    Album = "Test Album",
                    AlbumArtist = "Test Album Artist",
                    Composer = "Test Composer",
                    Genre = "Test Genre",
                    Comment = "Test Comment",
                    Day = "31",
                    Month = "01",
                    Year = "2017",
                    TrackNumber = "01",
                    TrackCount = "12",
                    TrackPeak = "0.5",
                    AlbumPeak = "0.6",
                    TrackGain = "0.7",
                    AlbumGain = "0.8"
                },
                null,
                null,
                "11F166F272E635021ABD6BAF37A3BFA5"
            },

            // Existing tag removal
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using defaults).ogg",
                new TestAudioMetadata(),
                null,
                null,
                "873E47897BB3645B63B5B8D8B932198E"
            },

            // Nothing to do
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata(),
                null,
                null,
                "873E47897BB3645B63B5B8D8B932198E"
            },

            // PNG CoverArt (Converted)
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                null,
                "8BE9DB1A5CDECC97DF66B2B307E9A723"
            },

            // JPEG CoverArt
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                null,
                "8BE9DB1A5CDECC97DF66B2B307E9A723"
            }

            #endregion
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Data
        {
            // Prepend an index to each row
            [UsedImplicitly] get => _data.Select((item, index) => item.Prepend(index).ToArray());
        }
    }
}
