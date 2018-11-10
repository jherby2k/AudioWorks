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
#if LINUX
                "4707C81E467497975458C152234AD13F",
                "79528DB2721970437C6D8877F655E273"
#else
                "949F0B102A34CE82A14611F2966331E9"
#endif
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
#if LINUX
                "8233F3397D5097D50A061504D10AA644",
                "B903A8E9B17014CDDB563A3CC73AB7F5"
#else
                "39C3FA3E1632321B5B9E1073C3916193"
#endif
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
#if LINUX
                "8233F3397D5097D50A061504D10AA644",
                "B903A8E9B17014CDDB563A3CC73AB7F5"
#else
                "39C3FA3E1632321B5B9E1073C3916193"
#endif
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
#if LINUX
                "5449B08E4A9CEB9D86B3BEA443F02662",
                "C830D5913F20AEF93853E1A01462708D"
#else
                "C31BC4B2225C20A40A07077367578065"
#endif
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
#if LINUX
                "34B8ED1508B895CE54E48035C2F8F459",
                "27C19A7309E49A5B07C0AD932A7B2875"
#else
                "F08A1DF76544A92E10DE9C1CBCC9346B"
#endif
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
#if LINUX
                "4707C81E467497975458C152234AD13F",
                "79528DB2721970437C6D8877F655E273"
#else
                "949F0B102A34CE82A14611F2966331E9"
#endif
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
#if LINUX
                "446F286CFEC2285F3C5F8996A7489617",
                "2F893F4E2A6FCF6507763801A1CD908C"
#else
                "4EE86066868F97A45F5901CAC7205D0A"
#endif
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
#if LINUX
                "C8D99C071580D2C34E42B815B680A1CA",
                "D10242261CDBC3FF2D9BEB2C232DC1F7"
#else
                "1E4BD1432902997151CDE0EDB2CB1A76"
#endif
            },

            // Existing tag removal
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                new TestAudioMetadata(),
                null,
                null,
#if LINUX
                "D858D62481CDF540B881F2151C0ABB80",
                "3983A342A074A7E8871FEF4FBE0AC73F"
#else
                "4C8DC1C6AFBF380117E3E82B1E815AD6"
#endif
            },

            // Nothing to do
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata(),
                null,
                null,
#if LINUX
                "D858D62481CDF540B881F2151C0ABB80",
                "3983A342A074A7E8871FEF4FBE0AC73F"
#else
                "4C8DC1C6AFBF380117E3E82B1E815AD6"
#endif
            },

            // PNG CoverArt
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                null,
#if LINUX
                "86C7A296259858EC6063A3B0740D18CB",
                "8E3D1A13C4F9BE314C0AD61892472AC6"
#else
                "0D76165A690F3A42E06972C1FC5573FE"
#endif
            },

            // JPEG CoverArt
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                null,
#if LINUX
                "CB279829CACD8B102E3288CA3360BC52",
                "F489D849B9ACD653B8986D45C487742E"
#else
                "E9712B1E4BFB2931BA62ACC8A60D7289"
#endif
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
#if LINUX
                "7F7316891B26F9FC3DE7D2D1304F9CFE",
                "7F7316891B26F9FC3DE7D2D1304F9CFE"
#else
                "7F7316891B26F9FC3DE7D2D1304F9CFE"
#endif
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
#if LINUX
                "9957A905E53C3D2411A3A4BF3DA4DBA3",
                "9957A905E53C3D2411A3A4BF3DA4DBA3"
#else
                "9957A905E53C3D2411A3A4BF3DA4DBA3"
#endif
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
#if LINUX
                "9957A905E53C3D2411A3A4BF3DA4DBA3",
                "9957A905E53C3D2411A3A4BF3DA4DBA3"
#else
                "9957A905E53C3D2411A3A4BF3DA4DBA3"
#endif
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
#if LINUX
                "847E8C85293966F62132E8F851FA7BAE",
                "847E8C85293966F62132E8F851FA7BAE"
#else
                "847E8C85293966F62132E8F851FA7BAE"
#endif
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
#if LINUX
                "BD8C422B4F08A50532168922680B0C2E",
                "BD8C422B4F08A50532168922680B0C2E"
#else
                "BD8C422B4F08A50532168922680B0C2E"
#endif
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
#if LINUX
                "7F7316891B26F9FC3DE7D2D1304F9CFE",
                "7F7316891B26F9FC3DE7D2D1304F9CFE"
#else
                "7F7316891B26F9FC3DE7D2D1304F9CFE"
#endif
            },

            // Existing tag removal
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
                new TestAudioMetadata(),
                null,
                null,
#if LINUX
                "090FD975097BAFC4164370A3DEA9E696",
                "090FD975097BAFC4164370A3DEA9E696"
#else
                "090FD975097BAFC4164370A3DEA9E696"
#endif
            },

            // Nothing to do
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                null,
                null,
#if LINUX
                "090FD975097BAFC4164370A3DEA9E696",
                "090FD975097BAFC4164370A3DEA9E696"
#else
                "090FD975097BAFC4164370A3DEA9E696"
#endif
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
#if LINUX
                "090FD975097BAFC4164370A3DEA9E696",
                "090FD975097BAFC4164370A3DEA9E696"
#else
                "090FD975097BAFC4164370A3DEA9E696"
#endif
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
#if LINUX
                "2A66A8458C32EC663AE48C6294E829AB",
                "2A66A8458C32EC663AE48C6294E829AB"
#else
                "2A66A8458C32EC663AE48C6294E829AB"
#endif
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
#if LINUX
                "D4615919A461B54512B3863ADD487D4B",
                "D4615919A461B54512B3863ADD487D4B"
#else
                "D4615919A461B54512B3863ADD487D4B"
#endif
            },

            // PNG CoverArt (ALAC)
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                null,
#if LINUX
                "767F47AEEA8A8F85DA214D51E0751CD5",
                "767F47AEEA8A8F85DA214D51E0751CD5"
#else
                "767F47AEEA8A8F85DA214D51E0751CD5"
#endif
            },

            // JPEG CoverArt (ALAC)
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                null,
#if LINUX
                "D419908A6F39E2D402BFDE1CB4DA8821",
                "D419908A6F39E2D402BFDE1CB4DA8821"
#else
                "D419908A6F39E2D402BFDE1CB4DA8821"
#endif
            },

            // PNG CoverArt (AAC, converted)
            new object[]
            {
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                null,
#if LINUX
                "679251C0E61FC8EB10286525FE64F60F",
                "679251C0E61FC8EB10286525FE64F60F"
#else
                "679251C0E61FC8EB10286525FE64F60F"
#endif
            },

            // JPEG CoverArt (AAC)
            new object[]
            {
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                null,
#if LINUX
                "679251C0E61FC8EB10286525FE64F60F",
                "679251C0E61FC8EB10286525FE64F60F"
#else
                "679251C0E61FC8EB10286525FE64F60F"
#endif
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
#if LINUX
                "D44FACB05A7F0CB3CB5F5A31B9B52022",
                "D44FACB05A7F0CB3CB5F5A31B9B52022"
#else
                "D44FACB05A7F0CB3CB5F5A31B9B52022"
#endif
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
#if LINUX
                "C64330448267EA7595D19378766B38C5",
                "C64330448267EA7595D19378766B38C5"
#else
                "C64330448267EA7595D19378766B38C5"
#endif
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
#if LINUX
                "C64330448267EA7595D19378766B38C5",
                "C64330448267EA7595D19378766B38C5"
#else
                "C64330448267EA7595D19378766B38C5"
#endif
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
#if LINUX
                "8E71F084665F7763560EAEF79292B1ED",
                "8E71F084665F7763560EAEF79292B1ED"
#else
                "8E71F084665F7763560EAEF79292B1ED"
#endif
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
#if LINUX
                "B4BAD75711B480844A735B8EF169F82A",
                "B4BAD75711B480844A735B8EF169F82A"
#else
                "B4BAD75711B480844A735B8EF169F82A"
#endif
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
#if LINUX
                "076B838A43E883DCC9F0D0ABE8A263D6",
                "076B838A43E883DCC9F0D0ABE8A263D6"
#else
                "076B838A43E883DCC9F0D0ABE8A263D6"
#endif
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
#if LINUX
                "E00628B6DA2A93F2832EAC420B2D8DF0",
                "E00628B6DA2A93F2832EAC420B2D8DF0"
#else
                "E00628B6DA2A93F2832EAC420B2D8DF0"
#endif
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
#if LINUX
                "476D2A59E830366CBFF9F0AE0305B8E2",
                "476D2A59E830366CBFF9F0AE0305B8E2"
#else
                "476D2A59E830366CBFF9F0AE0305B8E2"
#endif
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
#if LINUX
                "D44FACB05A7F0CB3CB5F5A31B9B52022",
                "D44FACB05A7F0CB3CB5F5A31B9B52022"
#else
                "D44FACB05A7F0CB3CB5F5A31B9B52022"
#endif
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
#if LINUX
                "076B838A43E883DCC9F0D0ABE8A263D6",
                "076B838A43E883DCC9F0D0ABE8A263D6"
#else
                "076B838A43E883DCC9F0D0ABE8A263D6"
#endif
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
#if LINUX
                "189A55453749DA1FFAFFEAC6A06DF99B",
                "189A55453749DA1FFAFFEAC6A06DF99B"
#else
                "189A55453749DA1FFAFFEAC6A06DF99B"
#endif
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
#if LINUX
                "D44FACB05A7F0CB3CB5F5A31B9B52022",
                "D44FACB05A7F0CB3CB5F5A31B9B52022"
#else
                "D44FACB05A7F0CB3CB5F5A31B9B52022"
#endif
            },

            // Existing tag removal
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
                new TestAudioMetadata(),
                null,
                null,
#if LINUX
                "963D578D818C25DE5FEE6625BE7BFA98",
                "963D578D818C25DE5FEE6625BE7BFA98"
#else
                "963D578D818C25DE5FEE6625BE7BFA98"
#endif
            },

            // Nothing to do
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata(),
                null,
                null,
#if LINUX
                "963D578D818C25DE5FEE6625BE7BFA98",
                "963D578D818C25DE5FEE6625BE7BFA98"
#else
                "963D578D818C25DE5FEE6625BE7BFA98"
#endif
            },

            // PNG CoverArt (converted)
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                null,
#if LINUX
                "1BF5C0A314A84C08A71E620ECFAC27FF",
                "1BF5C0A314A84C08A71E620ECFAC27FF"
#else
                "1BF5C0A314A84C08A71E620ECFAC27FF"
#endif
            },

            // JPEG CoverArt
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                null,
#if LINUX
                "1BF5C0A314A84C08A71E620ECFAC27FF",
                "1BF5C0A314A84C08A71E620ECFAC27FF"
#else
                "1BF5C0A314A84C08A71E620ECFAC27FF"
#endif
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
#if LINUX
                "920ABDF9377A176F1E3BC23A3854B436",
                "920ABDF9377A176F1E3BC23A3854B436"
#elif OSX
                "11F166F272E635021ABD6BAF37A3BFA5"
#else
                "69A75975D90E906C158C194236FC7125"
#endif
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
#if LINUX
                "AE84B97E3D1DC72C8F037F1FB391F176",
                "AE84B97E3D1DC72C8F037F1FB391F176"
#elif OSX
                "63BB3732D651F6D3A8496091FCF14725"
#else
                "719EDC12E5D730C0C8EB37C90F63563D"
#endif
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
#if LINUX
                "AE84B97E3D1DC72C8F037F1FB391F176",
                "AE84B97E3D1DC72C8F037F1FB391F176"
#elif OSX
                "63BB3732D651F6D3A8496091FCF14725"
#else
                "719EDC12E5D730C0C8EB37C90F63563D"
#endif
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
#if LINUX
                "65D70AA25438FADE7F5BD8F3EA50AF14",
                "65D70AA25438FADE7F5BD8F3EA50AF14"
#elif OSX
                "ED7210753CB1A870F74DB01E959D2FFE"
#else
                "B49EE9F48912935E98EB5B703A0CFEAE"
#endif
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
#if LINUX
                "E27562A26A75428980854641331A0EDA",
                "E27562A26A75428980854641331A0EDA"
#elif OSX
                "17F5C68F72027655E21870F0E7F06CB8"
#else
                "CE24BB00216950315A1E5964FDDC62DC"
#endif
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
#if LINUX
                "920ABDF9377A176F1E3BC23A3854B436",
                "920ABDF9377A176F1E3BC23A3854B436"
#elif OSX
                "11F166F272E635021ABD6BAF37A3BFA5"
#else
                "69A75975D90E906C158C194236FC7125"
#endif
            },

            // Existing tag removal
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using defaults).ogg",
                new TestAudioMetadata(),
                null,
                null,
#if LINUX
                "BF300A616B52AB534976E2578ACF1C56",
                "BF300A616B52AB534976E2578ACF1C56"
#elif OSX
                "873E47897BB3645B63B5B8D8B932198E"
#else
                "C4744A0D9349D8423FF188BF79823868"
#endif
            },

            // Nothing to do
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata(),
                null,
                null,
#if LINUX
                "BF300A616B52AB534976E2578ACF1C56",
                "BF300A616B52AB534976E2578ACF1C56"
#elif OSX
                "873E47897BB3645B63B5B8D8B932198E"
#else
                "C4744A0D9349D8423FF188BF79823868"
#endif
            },

            // PNG CoverArt (Converted)
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                null,
#if LINUX
                "5C771ACA31F957E028BE07E3489DCBC3",
                "5C771ACA31F957E028BE07E3489DCBC3"
#elif OSX
                "2682E9CD4EF8B4FBA041B907FD86614D"
#else
                "8C9453958E15AF2EFBE2054C8F07EFAF"
#endif
            },

            // JPEG CoverArt
            new object[]
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                null,
#if LINUX
                "5C771ACA31F957E028BE07E3489DCBC3",
                "5C771ACA31F957E028BE07E3489DCBC3"
#elif OSX
                "2682E9CD4EF8B4FBA041B907FD86614D"
#else
                "8C9453958E15AF2EFBE2054C8F07EFAF"
#endif
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
