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

using AudioWorks.Api.Tests.DataTypes;
using System;
using System.Linq;
using Xunit;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class SaveMetadataValidFileSource
    {
        static readonly TheoryData<string, TestAudioMetadata, string, TestSettingDictionary, string[]> _data = new()
        {
            #region FLAC

            // All fields
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "BAE0A61EB5C4912A4793C8B259ED5633", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "1F5FEC2305DE58CE9BA73AFC9E5017AA" // FLAC 1.4.3
                }
            },

            // Day unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "FDB702CF53676594BDD9FBEC77F154A3", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "CB5A4B3F94A3F5A2B152B909A6EC2D8C" // FLAC 1.4.3
                }
            },

            // Month unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "FDB702CF53676594BDD9FBEC77F154A3", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "CB5A4B3F94A3F5A2B152B909A6EC2D8C" // FLAC 1.4.3
                }
            },

            // TrackNumber unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "A05D916A5C03E9599CBC9A9F6989FDB0", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "C238388B2E5ED6E8026FBFE7A60B6AC0" // FLAC 1.4.3
                }
            },

            // TrackCount unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "0917BADDD895CCFD370BB222AF216E7B", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "B4969C097EBD1D6F1CE704C652FC7A0A" // FLAC 1.4.3
                }
            },

            // Existing tag
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "BAE0A61EB5C4912A4793C8B259ED5633", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "1F5FEC2305DE58CE9BA73AFC9E5017AA" // FLAC 1.4.3
                }
            },

            // No padding
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
                string.Empty,
                new TestSettingDictionary
                {
                    ["Padding"] = 0
                },
                new[]
                {
                    "41092DD891ABD71749834EAF571D30D5", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "3E07F2D9194BB9BCE6C7EA4434AB9F78" // FLAC 1.4.3
                }
            },

            // 100 bytes of padding
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
                string.Empty,
                new TestSettingDictionary
                {
                    ["Padding"] = 100
                },
                new[]
                {
                    "D6EF71E33501C350CD5378D55BC25D8C", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "39928199B6A8F6B399C9C09E103E812C" // FLAC 1.4.3
                }
            },

            // Existing tag removal
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "A5B12EAFE3014399E04E427CA756234B" // FLAC 1.4.3
                }
            },

            // Nothing to do
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "A5B12EAFE3014399E04E427CA756234B" // FLAC 1.4.3
                }
            },

            // PNG CoverArt
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                new TestSettingDictionary(),
                new[]
                {
                    "1399992DC07E38EBCD3F5DB1AF48B8C3", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "1F6BDDA2A2585E5E84B4A9F5CFB344C0" // FLAC 1.4.3
                }
            },

            // JPEG CoverArt
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                new TestSettingDictionary(),
                new[]
                {
                    "BB1A252DAA1100EA9D0B4658DE863A86", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "5CADC0776BE4E84F683DB333057E54E1" // FLAC 1.4.3
                }
            },

            #endregion

            #region MP4

            // All fields
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "7F7316891B26F9FC3DE7D2D1304F9CFE"
                }
            },

            // Day unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "9957A905E53C3D2411A3A4BF3DA4DBA3"
                }
            },

            // Month unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "9957A905E53C3D2411A3A4BF3DA4DBA3"
                }
            },

            // TrackNumber unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "847E8C85293966F62132E8F851FA7BAE"
                }
            },

            // TrackCount unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "BD8C422B4F08A50532168922680B0C2E"
                }
            },

            // Existing tag
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "7F7316891B26F9FC3DE7D2D1304F9CFE"
                }
            },

            // Updated creation time
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2018, 9, 1)
                },
                new[]
                {
                    "4AF6E13DD50245A06F6FEA52F2325C47"
                }
            },

            // Updated modification time
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary
                {
                    ["ModificationTime"] = new DateTime(2018, 9, 1)
                },
                new[]
                {
                    "D196EAFE7E8617F867136C526718CEF2"
                }
            },

            // Existing tag removal
            {
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "090FD975097BAFC4164370A3DEA9E696"
                }
            },

            // Nothing to do
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "090FD975097BAFC4164370A3DEA9E696"
                }
            },

            // Default padding (explicit)
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary
                {
                    ["Padding"] = 2048
                },
                new[]
                {
                    "090FD975097BAFC4164370A3DEA9E696"
                }
            },

            // Disabled padding
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary
                {
                    ["Padding"] = 0
                },
                new[]
                {
                    "2A66A8458C32EC663AE48C6294E829AB"
                }
            },

            // Maximum padding
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary
                {
                    ["Padding"] = 16_777_216
                },
                new[]
                {
                    "D4615919A461B54512B3863ADD487D4B"
                }
            },

            // PNG CoverArt (ALAC)
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                new TestSettingDictionary(),
                new[]
                {
                    "767F47AEEA8A8F85DA214D51E0751CD5"
                }
            },

            // JPEG CoverArt (ALAC)
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                new TestSettingDictionary(),
                new[]
                {
                    "D419908A6F39E2D402BFDE1CB4DA8821"
                }
            },

            // PNG CoverArt (AAC, converted)
            {
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                new TestSettingDictionary(),
                new[]
                {
                    "862B09B8240906315D91AB953AA174BF", // 32-bit Legacy .NET
                    "1FC596A38C7F84B7DDBE356215BE169D", // 64-bit Legacy .NET
                    "7D35C341D95B0C3BD3663758116FD17E" // .NET Core 3.0+
                }
            },

            // JPEG CoverArt (AAC)
            {
                "QAAC TVBR 91 44100Hz Stereo.m4a",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                new TestSettingDictionary(),
                new[]
                {
                    "679251C0E61FC8EB10286525FE64F60F"
                }
            },

            #endregion

            #region ID3

            // All fields
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "23C89D97086DD52D0B12AF0D215CB204"
                }
            },

            // Day unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "63C6147CC1AEDC8C79CEA3A64A4D1D70"
                }
            },

            // Month unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "63C6147CC1AEDC8C79CEA3A64A4D1D70"
                }
            },

            // TrackNumber unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "436A20F9A295581E7ADA1124C4DDC457"
                }
            },

            // TrackCount unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "A6AA8BE5A043577E1DEA4607343E4625"
                }
            },

            // Existing tag
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "FF55C184441BFF19D872D0B7767AC981"
                }
            },

            // Tag version 2.4
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
                string.Empty,
                new TestSettingDictionary
                {
                    ["TagVersion"] = "2.4"
                },
                new[]
                {
                    "4E76656E1CF33B1D92C6C52637531B87"
                }
            },

            // UTF-16 encoding
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
                string.Empty,
                new TestSettingDictionary
                {
                    ["TagEncoding"] = "UTF16"
                },
                new[]
                {
                    "59304C396353DF87B20EAFBBA3E4E7E4"
                }
            },

            // UTF-8 encoding
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
                string.Empty,
                new TestSettingDictionary
                {
                    ["TagEncoding"] = "UTF8"
                },
                new[]
                {
                    "1431406FBB1BB1E8442304A8D5C00B72"
                }
            },

            // Default padding (explicit)
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
                string.Empty,
                new TestSettingDictionary
                {
                    ["TagPadding"] = 2048
                },
                new[]
                {
                    "23C89D97086DD52D0B12AF0D215CB204"
                }
            },

            // No padding
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
                string.Empty,
                new TestSettingDictionary
                {
                    ["TagPadding"] = 0
                },
                new[]
                {
                    "A477DAEDA5B65A77443433E50525AC5C"
                }
            },

            // Maximum padding
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
                string.Empty,
                new TestSettingDictionary
                {
                    ["TagPadding"] = 16_777_216
                },
                new[]
                {
                    "5B6CB7B9912D212ACD37BEE11BA34AC5"
                }
            },

            // Existing v1 tag
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "23C89D97086DD52D0B12AF0D215CB204"
                }
            },

            // Existing tag removal
            {
                "Lame CBR 128 44100Hz Stereo (ID3v2.3 Latin1).mp3",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "3EA0A7381A4AD97A317205F1026AC938"
                }
            },

            // Nothing to do
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "963D578D818C25DE5FEE6625BE7BFA98"
                }
            },

            // PNG CoverArt (converted)
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                new TestSettingDictionary(),
                new[]
                {
                    "D0F19C93BFFFDE8A9D649276B5DF5DAE", // 32-bit Legacy .NET
                    "09E9E1004B5279A8513B97D4FD34FEFB", // 64-bit Legacy .NET
                    "8C495F4498B5923C8A4E43E8EE2D5654" // .NET Core 3.0+
                }
            },

            // JPEG CoverArt
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                new TestSettingDictionary(),
                new[]
                {
                    "1BF5C0A314A84C08A71E620ECFAC27FF"
                }
            },

            #endregion

            #region Ogg Vorbis

            // All fields
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "BD9FBB0769E89FC46E37BB48A7F8A62F", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "4AA6D9C63E0CFD18F450A99819E28811" // Vorbis 1.3.7 (MacOS)
                }
            },

            // Day unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "4BCB964FA118AE894302C5D9318EBB3E", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "2CFA7B065217D20087B138681D587899" // Vorbis 1.3.7 (MacOS)
                }
            },

            // Month unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "4BCB964FA118AE894302C5D9318EBB3E", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "2CFA7B065217D20087B138681D587899" // Vorbis 1.3.7 (MacOS)
                }
            },

            // TrackNumber unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "E84E26FA84319808E2DD078BDE0561CB", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "D87431EDDF5977239830E5BC9BCF042A" // Vorbis 1.3.7 (MacOS)
                }
            },

            // TrackCount unset
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "A44D6CC373B6F34D65EADBB32C94C566", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "C9C6B75D075FA0B9B398FFCB760A002D" // Vorbis 1.3.7 (MacOS)
                }
            },

            // Existing tag
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "BD9FBB0769E89FC46E37BB48A7F8A62F", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "4AA6D9C63E0CFD18F450A99819E28811" // Vorbis 1.3.7 (MacOS)
                }
            },

            // Existing tag removal
            {
                "Vorbis Quality 3 44100Hz Stereo (Tagged using defaults).ogg",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "D225108B66480DC5F4368F6CD605AD7C", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "3FDD942360BA5074F9CF7303E80AA4D0" // Vorbis 1.3.7 (MacOS)
                }
            },

            // Nothing to do
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "D225108B66480DC5F4368F6CD605AD7C", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "3FDD942360BA5074F9CF7303E80AA4D0" // Vorbis 1.3.7 (MacOS)
                }
            },

            // PNG CoverArt (Converted)
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                new TestSettingDictionary(),
                new[]
                {
                    "4A74639B61D636F02AE9003D1F8B728A", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on 32-bit Windows)
                    "5202B8FFA7A76EA6AD53FEFFE9F206FD", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on 64-bit Windows)
                    "BFF6FD917D8DC3C22D33FFA780358EDC", // Vorbis 1.3.7 AoTuV + Lancer (.NET Core 3.0+ on Windows and Ubuntu)
                    "620158BEB9F80C0EB6A2982B0AA384BC" // Vorbis 1.3.7 (MacOS)
                }
            },

            // JPEG CoverArt
            {
                "Vorbis Quality 3 44100Hz Stereo.ogg",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                new TestSettingDictionary(),
                new[]
                {
                    "CA3A8F152FFC10FE460E6DBD2DB2F090", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "36C4695F1C9FBD9E018E98FB4A132D83" // Vorbis 1.3.7 (MacOS)
                }
            },

            #endregion

            #region Opus

            // All fields
            {
                "Opus VBR 44100Hz Stereo.opus",
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
                    TrackCount = "12"
                },
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "D22BBFDDCB298A427057253C4E833057" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // Day unset
            {
                "Opus VBR 44100Hz Stereo.opus",
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
                    TrackCount = "12"
                },
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "B181BB7AF0A3E37C3A262B942FD197FC" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // Month unset
            {
                "Opus VBR 44100Hz Stereo.opus",
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
                    TrackCount = "12"
                },
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "B181BB7AF0A3E37C3A262B942FD197FC" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // TrackNumber unset
            {
                "Opus VBR 44100Hz Stereo.opus",
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
                    TrackCount = "12"
                },
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "166F1DD0C71E2CCCF226157A34791E1B" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // TrackCount unset
            {
                "Opus VBR 44100Hz Stereo.opus",
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
                    TrackNumber = "01"
                },
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "8EA7FC13A2F214CADF3A1D447EF5CB65" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // Existing tag
            {
                "Opus VBR 44100Hz Stereo (Tagged using defaults).opus",
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
                    TrackCount = "12"
                },
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "BB50765E7DB79BDEADFC81E771101FD7" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // Existing tag removal
            {
                "Opus VBR 44100Hz Stereo (Tagged using defaults).opus",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "367DB09F8408583579F918061FCB6322" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // Nothing to do
            {
                "Opus VBR 44100Hz Stereo.opus",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "22D33DFB735C5B7805AB26C7A04CDA86" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // PNG CoverArt (Converted)
            {
                "Opus VBR 44100Hz Stereo.opus",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                new TestSettingDictionary(),
                new[]
                {
                    "C80AD6949880EA8AE45603CDAD7E0D29", // Opus 1.3.1 (Legacy .NET on 32-bit Windows)
                    "6D91ACB2687A9FADC202A4B7C3920593", // Opus 1.3.1 (Legacy .NET on 64-bit Windows)
                    "316157B1807D94E3E111802327A26787" // Opus 1.3.1 (MacOS, Ubuntu and .NET Core 3.0+ on Windows)
                }
            },

            // JPEG CoverArt
            {
                "Opus VBR 44100Hz Stereo.opus",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                new TestSettingDictionary(),
                new[]
                {
                    "CABF32D411EA5FA9458A79B6DC2C2D69" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            }

            #endregion
        };

        public static TheoryData<int, string, TestAudioMetadata, string, TestSettingDictionary, string[]> Data
        {
            get
            {
                var results = new TheoryData<int, string, TestAudioMetadata, string, TestSettingDictionary, string[]>();
                foreach (var result in _data.Select((item, index) => (index,
                             (string) item[0],
                             (TestAudioMetadata) item[1],
                             (string) item[2],
                             (TestSettingDictionary) item[3],
                             (string[]) item[4])))
                    results.Add(result.index, result.Item2, result.Item3, result.Item4, result.Item5, result.Item6);
                return results;
            }
        }
    }
}
