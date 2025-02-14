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

using System;
using System.Collections.Generic;
using System.Linq;
using AudioWorks.Common;
using Xunit;

namespace AudioWorks.TestUtilities.DataSources
{
    public static class SaveMetadataValidFileSource
    {
        static readonly IEnumerable<TheoryDataRow<string, AudioMetadata, string, SettingDictionary, string[]>> _data =
        [
            #region FLAC

            // All fields
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new()
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
                string.Empty,
                [],
                [
                    "BAE0A61EB5C4912A4793C8B259ED5633", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "1F5FEC2305DE58CE9BA73AFC9E5017AA" // FLAC 1.4.3
                ]
            ),

            // Day unset
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new()
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
                string.Empty,
                [],
                [
                    "FDB702CF53676594BDD9FBEC77F154A3", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "CB5A4B3F94A3F5A2B152B909A6EC2D8C" // FLAC 1.4.3
                ]
            ),

            // Month unset
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new()
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
                string.Empty,
                [],
                [
                    "FDB702CF53676594BDD9FBEC77F154A3", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "CB5A4B3F94A3F5A2B152B909A6EC2D8C" // FLAC 1.4.3
                ]
            ),

            // TrackNumber unset
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new()
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
                string.Empty,
                [],
                [
                    "A05D916A5C03E9599CBC9A9F6989FDB0", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "C238388B2E5ED6E8026FBFE7A60B6AC0" // FLAC 1.4.3
                ]
            ),

            // TrackCount unset
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new()
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
                string.Empty,
                [],
                [
                    "0917BADDD895CCFD370BB222AF216E7B", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "B4969C097EBD1D6F1CE704C652FC7A0A" // FLAC 1.4.3
                ]
            ),

            // Existing tag
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                new()
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
                string.Empty,
                [],
                [
                    "BAE0A61EB5C4912A4793C8B259ED5633", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "1F5FEC2305DE58CE9BA73AFC9E5017AA" // FLAC 1.4.3
                ]
            ),

            // No padding
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new()
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
                string.Empty,
                new()
                {
                    ["Padding"] = 0
                },
                [
                    "41092DD891ABD71749834EAF571D30D5", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "3E07F2D9194BB9BCE6C7EA4434AB9F78" // FLAC 1.4.3
                ]
            ),

            // 100 bytes of padding
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new()
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
                string.Empty,
                new()
                {
                    ["Padding"] = 100
                },
                [
                    "D6EF71E33501C350CD5378D55BC25D8C", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "39928199B6A8F6B399C9C09E103E812C" // FLAC 1.4.3
                ]
            ),

            // Existing tag removal
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                new(),
                string.Empty,
                [],
                [
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "A5B12EAFE3014399E04E427CA756234B" // FLAC 1.4.3
                ]
            ),

            // Nothing to do
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new(),
                string.Empty,
                [],
                [
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "A5B12EAFE3014399E04E427CA756234B" // FLAC 1.4.3
                ]
            ),

            // PNG CoverArt
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new(),
                "PNG 24-bit 1280 x 935.png",
                [],
                [
                    "1399992DC07E38EBCD3F5DB1AF48B8C3", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "1F6BDDA2A2585E5E84B4A9F5CFB344C0" // FLAC 1.4.3
                ]
            ),

            // JPEG CoverArt
            new(
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new(),
                "JPEG 24-bit 1280 x 935.jpg",
                [],
                [
                    "BB1A252DAA1100EA9D0B4658DE863A86", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "5CADC0776BE4E84F683DB333057E54E1" // FLAC 1.4.3
                ]
            ),

            #endregion

            #region MP4

            // All fields
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new()
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
                string.Empty,
                [],
                [
                    "7F7316891B26F9FC3DE7D2D1304F9CFE"
                ]
            ),

            // Day unset
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new()
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
                string.Empty,
                [],
                [
                    "9957A905E53C3D2411A3A4BF3DA4DBA3"
                ]
            ),

            // Month unset
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new()
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
                string.Empty,
                [],
                [
                    "9957A905E53C3D2411A3A4BF3DA4DBA3"
                ]
            ),

            // TrackNumber unset
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new()
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
                string.Empty,
                [],
                [
                    "847E8C85293966F62132E8F851FA7BAE"
                ]
            ),

            // TrackCount unset
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new()
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
                string.Empty,
                [],
                [
                    "BD8C422B4F08A50532168922680B0C2E"
                ]
            ),

            // Existing tag
            new(
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
                new()
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
                string.Empty,
                [],
                [
                    "7F7316891B26F9FC3DE7D2D1304F9CFE"
                ]
            ),

            // Updated creation time
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                string.Empty,
                new()
                {
                    ["CreationTime"] = new DateTime(2018, 9, 1)
                },
                [
                    "4AF6E13DD50245A06F6FEA52F2325C47"
                ]
            ),

            // Updated modification time
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                string.Empty,
                new()
                {
                    ["ModificationTime"] = new DateTime(2018, 9, 1)
                },
                [
                    "D196EAFE7E8617F867136C526718CEF2"
                ]
            ),

            // Existing tag removal
            new(
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
                new(),
                string.Empty,
                [],
                [
                    "090FD975097BAFC4164370A3DEA9E696"
                ]
            ),

            // Nothing to do
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                string.Empty,
                [],
                [
                    "090FD975097BAFC4164370A3DEA9E696"
                ]
            ),

            // Default padding (explicit)
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                string.Empty,
                new()
                {
                    ["Padding"] = 2048
                },
                [
                    "090FD975097BAFC4164370A3DEA9E696"
                ]
            ),

            // Disabled padding
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                string.Empty,
                new()
                {
                    ["Padding"] = 0
                },
                [
                    "2A66A8458C32EC663AE48C6294E829AB"
                ]
            ),

            // Maximum padding
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                string.Empty,
                new()
                {
                    ["Padding"] = 16_777_216
                },
                [
                    "D4615919A461B54512B3863ADD487D4B"
                ]
            ),

            // PNG CoverArt (ALAC)
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                "PNG 24-bit 1280 x 935.png",
                [],
                [
                    "767F47AEEA8A8F85DA214D51E0751CD5"
                ]
            ),

            // JPEG CoverArt (ALAC)
            new(
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new(),
                "JPEG 24-bit 1280 x 935.jpg",
                [],
                [
                    "D419908A6F39E2D402BFDE1CB4DA8821"
                ]
            ),

            // PNG CoverArt (AAC, converted)
            new(
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                new(),
                "PNG 24-bit 1280 x 935.png",
                [],
                [
                    "1F72E87B05DA969322B0501DF63B6905", // Intel
                    "902BF760BB86246A8EA1890E31E6802E" // ARM
                ]
            ),

            // JPEG CoverArt (AAC)
            new(
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                new(),
                "JPEG 24-bit 1280 x 935.jpg",
                [],
                [
                    "679251C0E61FC8EB10286525FE64F60F"
                ]
            ),

            #endregion

            #region ID3

            // All fields
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
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
                string.Empty,
                [],
                [
                    "23C89D97086DD52D0B12AF0D215CB204"
                ]
            ),

            // Day unset
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
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
                string.Empty,
                [],
                [
                    "63C6147CC1AEDC8C79CEA3A64A4D1D70"
                ]
            ),

            // Month unset
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
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
                string.Empty,
                [],
                [
                    "63C6147CC1AEDC8C79CEA3A64A4D1D70"
                ]
            ),

            // TrackNumber unset
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
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
                string.Empty,
                [],
                [
                    "436A20F9A295581E7ADA1124C4DDC457"
                ]
            ),

            // TrackCount unset
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
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
                string.Empty,
                [],
                [
                    "A6AA8BE5A043577E1DEA4607343E4625"
                ]
            ),

            // Existing tag
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
                new()
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
                string.Empty,
                [],
                [
                    "FF55C184441BFF19D872D0B7767AC981"
                ]
            ),

            // Tag version 2.4
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
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
                string.Empty,
                new()
                {
                    ["TagVersion"] = "2.4"
                },
                [
                    "4E76656E1CF33B1D92C6C52637531B87"
                ]
            ),

            // UTF-16 encoding
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
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
                string.Empty,
                new()
                {
                    ["TagEncoding"] = "UTF16"
                },
                [
                    "59304C396353DF87B20EAFBBA3E4E7E4"
                ]
            ),

            // UTF-8 encoding
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
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
                string.Empty,
                new()
                {
                    ["TagEncoding"] = "UTF8"
                },
                [
                    "1431406FBB1BB1E8442304A8D5C00B72"
                ]
            ),

            // Default padding (explicit)
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
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
                string.Empty,
                new()
                {
                    ["TagPadding"] = 2048
                },
                [
                    "23C89D97086DD52D0B12AF0D215CB204"
                ]
            ),

            // No padding
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
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
                string.Empty,
                new()
                {
                    ["TagPadding"] = 0
                },
                [
                    "A477DAEDA5B65A77443433E50525AC5C"
                ]
            ),

            // Maximum padding
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new()
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
                string.Empty,
                new()
                {
                    ["TagPadding"] = 16_777_216
                },
                [
                    "5B6CB7B9912D212ACD37BEE11BA34AC5"
                ]
            ),

            // Existing v1 tag
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v1).mp3",
                new()
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
                string.Empty,
                [],
                [
                    "23C89D97086DD52D0B12AF0D215CB204"
                ]
            ),

            // Existing tag removal
            new(
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
                new(),
                string.Empty,
                [],
                [
                    "3EA0A7381A4AD97A317205F1026AC938"
                ]
            ),

            // Nothing to do
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new(),
                string.Empty,
                [],
                [
                    "963D578D818C25DE5FEE6625BE7BFA98"
                ]
            ),

            // PNG CoverArt (converted)
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new(),
                "PNG 24-bit 1280 x 935.png",
                [],
                [
                    "DFB1301AEF575A97D738485F9542DC05", // Intel
                    "D1FDCC69B8BF283754790E3B0745482B" // ARM
                ]
            ),

            // JPEG CoverArt
            new(
                "Lame CBR 128 44100Hz Stereo.mp3",
                new(),
                "JPEG 24-bit 1280 x 935.jpg",
                [],
                [
                    "1BF5C0A314A84C08A71E620ECFAC27FF"
                ]
            ),

            #endregion

            #region Ogg Vorbis

            // All fields
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new()
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
                string.Empty,
                [],
                [
                    "BD9FBB0769E89FC46E37BB48A7F8A62F", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "4AA6D9C63E0CFD18F450A99819E28811" // Vorbis 1.3.7 (MacOS)
                ]
            ),

            // Day unset
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new()
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
                string.Empty,
                [],
                [
                    "4BCB964FA118AE894302C5D9318EBB3E", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "2CFA7B065217D20087B138681D587899" // Vorbis 1.3.7 (MacOS)
                ]
            ),

            // Month unset
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new()
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
                string.Empty,
                [],
                [
                    "4BCB964FA118AE894302C5D9318EBB3E", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "2CFA7B065217D20087B138681D587899" // Vorbis 1.3.7 (MacOS)
                ]
            ),

            // TrackNumber unset
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new()
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
                string.Empty,
                [],
                [
                    "E84E26FA84319808E2DD078BDE0561CB", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "D87431EDDF5977239830E5BC9BCF042A" // Vorbis 1.3.7 (MacOS)
                ]
            ),

            // TrackCount unset
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new()
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
                string.Empty,
                [],
                [
                    "A44D6CC373B6F34D65EADBB32C94C566", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "C9C6B75D075FA0B9B398FFCB760A002D" // Vorbis 1.3.7 (MacOS)
                ]
            ),

            // Existing tag
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using defaults).ogg",
                new()
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
                string.Empty,
                [],
                [
                    "BD9FBB0769E89FC46E37BB48A7F8A62F", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "4AA6D9C63E0CFD18F450A99819E28811" // Vorbis 1.3.7 (MacOS)
                ]
            ),

            // Existing tag removal
            new(
                "Vorbis Quality 3 44100Hz Stereo (Tagged using defaults).ogg",
                new(),
                string.Empty,
                [],
                [
                    "D225108B66480DC5F4368F6CD605AD7C", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "3FDD942360BA5074F9CF7303E80AA4D0" // Vorbis 1.3.7 (MacOS)
                ]
            ),

            // Nothing to do
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new(),
                string.Empty,
                [],
                [
                    "D225108B66480DC5F4368F6CD605AD7C", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "3FDD942360BA5074F9CF7303E80AA4D0" // Vorbis 1.3.7 (MacOS)
                ]
            ),

            // PNG CoverArt (Converted)
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new(),
                "PNG 24-bit 1280 x 935.png",
                [],
                [
                    "FC6665047AC5EE74B3A64B08F7C15264", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "8E3BEF265FA8D9C8632A1CB4B5155156", // Vorbis 1.3.7 (MacOS on Intel)
                    "9924B82CBA5C2A07AA4BCA87E51651BD" // Vorbis 1.3.7 (MacOS on ARM)
                ]
            ),

            // JPEG CoverArt
            new(
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new(),
                "JPEG 24-bit 1280 x 935.jpg",
                [],
                [
                    "CA3A8F152FFC10FE460E6DBD2DB2F090", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "36C4695F1C9FBD9E018E98FB4A132D83" // Vorbis 1.3.7 (MacOS)
                ]
            ),

            #endregion

            #region Opus

            // All fields
            new(
                "Opus VBR 44100Hz Stereo.opus",
                new()
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
                    TrackCount = "12"
                },
                string.Empty,
                [],
                [
                    "D22BBFDDCB298A427057253C4E833057", // Opus 1.3.1 (Ubuntu 20.04 and 22.04)
                    "83EAAF39634A97A730D4359E23CEE33F" // Opus 1.5.2 (MacOS and Windows)
                ]
            ),

            // Day unset
            new(
                "Opus VBR 44100Hz Stereo.opus",
                new()
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
                    TrackCount = "12"
                },
                string.Empty,
                [],
                [
                    "B181BB7AF0A3E37C3A262B942FD197FC", // Opus 1.3.1 (Ubuntu 20.04 and 22.04)
                    "A298D22F719E0B950B31310E05BC1B66" // Opus 1.5.2 (MacOS and Windows)
                ]
            ),

            // Month unset
            new(
                "Opus VBR 44100Hz Stereo.opus",
                new()
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
                    TrackCount = "12"
                },
                string.Empty,
                [],
                [
                    "B181BB7AF0A3E37C3A262B942FD197FC", // Opus 1.3.1 (Ubuntu 20.04 and 22.04)
                    "A298D22F719E0B950B31310E05BC1B66" // Opus 1.5.2 (MacOS and Windows)
                ]
            ),

            // TrackNumber unset
            new(
                "Opus VBR 44100Hz Stereo.opus",
                new()
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
                    TrackCount = "12"
                },
                string.Empty,
                [],
                [
                    "166F1DD0C71E2CCCF226157A34791E1B", // Opus 1.3.1 (Ubuntu 20.04 and 22.04)
                    "8F3233D946C369EA87132A3EDB50071E" // Opus 1.5.2 (MacOS and Windows)
                ]
            ),

            // TrackCount unset
            new(
                "Opus VBR 44100Hz Stereo.opus",
                new()
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
                    TrackNumber = "01"
                },
                string.Empty,
                [],
                [
                    "8EA7FC13A2F214CADF3A1D447EF5CB65", // Opus 1.3.1 (Ubuntu 20.04 and 22.04)
                    "41253CA69E1D124E2CAC11B5D945EC67" // Opus 1.5.2 (MacOS and Windows)
                ]
            ),

            // Existing tag
            new(
                "Opus VBR 44100Hz Stereo (Tagged using defaults).opus",
                new()
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
                    TrackCount = "12"
                },
                string.Empty,
                [],
                [
                    "BB50765E7DB79BDEADFC81E771101FD7", // Opus 1.3.1 (Ubuntu 20.04 and 22.04)
                    "56AD6E2C6333F2A033914E9CF6FBA6CB" // Opus 1.5.2 (MacOS and Windows)
                ]
            ),

            // Existing tag removal
            new(
                "Opus VBR 44100Hz Stereo (Tagged using defaults).opus",
                new(),
                string.Empty,
                [],
                [
                    "367DB09F8408583579F918061FCB6322", // Opus 1.3.1 (Ubuntu 20.04 and 22.04)
                    "CF85C82F03F478E096F1248F792D0B1E" // Opus 1.5.2 (MacOS and Windows)
                ]
            ),

            // Nothing to do
            new(
                "Opus VBR 44100Hz Stereo.opus",
                new(),
                string.Empty,
                [],
                [
                    "22D33DFB735C5B7805AB26C7A04CDA86", // Opus 1.3.1 (Ubuntu 20.04 and 22.04)
                    "507E009E734B28534500E143CEA74EFD" // Opus 1.5.2 (MacOS and Windows)
                ]
            ),

            // PNG CoverArt (Converted)
            new(
                "Opus VBR 44100Hz Stereo.opus",
                new(),
                "PNG 24-bit 1280 x 935.png",
                [],
                [
                    "F152E6100DC6002C78DDCC6DF83D8880", // Opus 1.3.1 (Ubuntu 20.04 and 22.04)
                    "E7E9C4686F7B92FC843FEDA3522C961B", // Opus 1.5.2 (MacOS and Windows on Intel)
                    "14460A58ACC0BF9ED0BBC2122B6557D1" // Opus 1.5.2 (MacOS on ARM)
                ]
            ),

            // JPEG CoverArt
            new(
                "Opus VBR 44100Hz Stereo.opus",
                new(),
                "JPEG 24-bit 1280 x 935.jpg",
                [],
                [
                    "CABF32D411EA5FA9458A79B6DC2C2D69", // Opus 1.3.1 (Ubuntu 20.04)
                    "EDBD61E6DED683084D781F9F3C578D25" // Opus 1.5.2 (MacOS and Windows)
                ]
            )

            #endregion
        ];

        public static IEnumerable<TheoryDataRow<int, string, AudioMetadata, string, SettingDictionary, string[]>> Data =>
            _data.Select((item, index) =>
                new TheoryDataRow<int, string, AudioMetadata, string, SettingDictionary, string[]>(
                        index, item.Data.Item1, item.Data.Item2, item.Data.Item3, item.Data.Item4, item.Data.Item5)
                    { Skip = item.Skip });
    }
}
