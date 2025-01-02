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
using AudioWorks.Api.Tests.DataTypes;
#if NET462
using AudioWorks.TestUtilities;
#endif

namespace AudioWorks.Api.Tests.DataSources
{
    public static class SaveMetadataValidFileSource
    {
        static readonly List<object[]> _data = new()
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "79528DB2721970437C6D8877F655E273", // FLAC 1.3.2 (Ubuntu 18.04)
                    "BAE0A61EB5C4912A4793C8B259ED5633", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "122483B2BFAA1A0D62C1F69D54581DF4" // FLAC 1.4.1
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "B903A8E9B17014CDDB563A3CC73AB7F5", // FLAC 1.3.2 (Ubuntu 18.04)
                    "FDB702CF53676594BDD9FBEC77F154A3", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "AACF554CE43DE26E5FCFFA13B1A3AE7D" // FLAC 1.4.1
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "B903A8E9B17014CDDB563A3CC73AB7F5", // FLAC 1.3.2 (Ubuntu 18.04)
                    "FDB702CF53676594BDD9FBEC77F154A3", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "AACF554CE43DE26E5FCFFA13B1A3AE7D" // FLAC 1.4.1
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "C830D5913F20AEF93853E1A01462708D", // FLAC 1.3.2 (Ubuntu 18.04)
                    "A05D916A5C03E9599CBC9A9F6989FDB0", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "A33B5FB0DB8E396163EBDE04161227ED" // FLAC 1.4.1
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "27C19A7309E49A5B07C0AD932A7B2875", // FLAC 1.3.2 (Ubuntu 18.04)
                    "0917BADDD895CCFD370BB222AF216E7B", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "EB6E50DCC56A1B9D2ACD9AE87BAB7445" // FLAC 1.4.1
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "79528DB2721970437C6D8877F655E273", // FLAC 1.3.2 (Ubuntu 18.04)
                    "BAE0A61EB5C4912A4793C8B259ED5633", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "122483B2BFAA1A0D62C1F69D54581DF4" // FLAC 1.4.1
                }
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
                string.Empty,
                new TestSettingDictionary
                {
                    ["Padding"] = 0
                },
                new[]
                {
                    "4D1C0B9CD6909E81C2D5BD38A6F7CF4B", // FLAC 1.3.2 (Ubuntu 18.04)
                    "41092DD891ABD71749834EAF571D30D5", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "B901A689E9D708052FC10AC54FFF1C6E" // FLAC 1.4.1
                }
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
                string.Empty,
                new TestSettingDictionary
                {
                    ["Padding"] = 100
                },
                new[]
                {
                    "D10242261CDBC3FF2D9BEB2C232DC1F7", // FLAC 1.3.2 (Ubuntu 18.04)
                    "D6EF71E33501C350CD5378D55BC25D8C", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "DD85FC38D0FDB520789D253ADAE14AEF" // FLAC 1.4.1
                }
            },

            // Existing tag removal
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "3983A342A074A7E8871FEF4FBE0AC73F", // FLAC 1.3.2 (Ubuntu 18.04)
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "BF84B62C46A60D7AA3CD421004901CFC" // FLAC 1.4.1
                }
            },

            // Nothing to do
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "3983A342A074A7E8871FEF4FBE0AC73F", // FLAC 1.3.2 (Ubuntu 18.04)
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "BF84B62C46A60D7AA3CD421004901CFC" // FLAC 1.4.1
                }
            },

            // PNG CoverArt
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                new TestSettingDictionary(),
                new[]
                {
                    "8E3D1A13C4F9BE314C0AD61892472AC6", // FLAC 1.3.2 (Ubuntu 18.04)
                    "1399992DC07E38EBCD3F5DB1AF48B8C3", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "3529BC5BD517F93010838A57303467E0" // FLAC 1.4.1
                }
            },

            // JPEG CoverArt
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                new TestSettingDictionary(),
                new[]
                {
                    "F489D849B9ACD653B8986D45C487742E", // FLAC 1.3.2 (Ubuntu 18.04)
                    "BB1A252DAA1100EA9D0B4658DE863A86", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "2DCF696EF28B13BDDF622A4C22533A40" // FLAC 1.4.1
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "7F7316891B26F9FC3DE7D2D1304F9CFE"
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "9957A905E53C3D2411A3A4BF3DA4DBA3"
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "9957A905E53C3D2411A3A4BF3DA4DBA3"
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "847E8C85293966F62132E8F851FA7BAE"
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "BD8C422B4F08A50532168922680B0C2E"
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "7F7316891B26F9FC3DE7D2D1304F9CFE"
                }
            },

            // Updated creation time
            new object[]
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
            new object[]
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
            new object[]
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
            new object[]
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
            new object[]
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
            new object[]
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
            new object[]
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
            new object[]
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
            new object[]
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
            new object[]
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
            new object[]
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "23C89D97086DD52D0B12AF0D215CB204"
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "63C6147CC1AEDC8C79CEA3A64A4D1D70"
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "63C6147CC1AEDC8C79CEA3A64A4D1D70"
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "436A20F9A295581E7ADA1124C4DDC457"
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "A6AA8BE5A043577E1DEA4607343E4625"
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "FF55C184441BFF19D872D0B7767AC981"
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "23C89D97086DD52D0B12AF0D215CB204"
                }
            },

            // Existing tag removal
            new object[]
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
            new object[]
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
            new object[]
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
            new object[]
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "BD9FBB0769E89FC46E37BB48A7F8A62F", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "4AA6D9C63E0CFD18F450A99819E28811" // Vorbis 1.3.7 (MacOS)
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "4BCB964FA118AE894302C5D9318EBB3E", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "2CFA7B065217D20087B138681D587899" // Vorbis 1.3.7 (MacOS)
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "4BCB964FA118AE894302C5D9318EBB3E", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "2CFA7B065217D20087B138681D587899" // Vorbis 1.3.7 (MacOS)
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "E84E26FA84319808E2DD078BDE0561CB", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "D87431EDDF5977239830E5BC9BCF042A" // Vorbis 1.3.7 (MacOS)
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "A44D6CC373B6F34D65EADBB32C94C566", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "C9C6B75D075FA0B9B398FFCB760A002D" // Vorbis 1.3.7 (MacOS)
                }
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
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "BD9FBB0769E89FC46E37BB48A7F8A62F", // Vorbis 1.3.7 AoTuV + Lancer (Windows and Ubuntu)
                    "4AA6D9C63E0CFD18F450A99819E28811" // Vorbis 1.3.7 (MacOS)
                }
            },

            // Existing tag removal
            new object[]
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
            new object[]
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
            new object[]
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
                    "36C4695F1C9FBD9E018E98FB4A132D83" // Vorbis 1.3.7 (MacOS)
                }
            },

            // JPEG CoverArt
            new object[]
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
            new object[]
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
                    "9A53EDE65A60F0C1684D16744E33DCFC", // Opus 1.1.2 (Ubuntu 18.04)
                    "D22BBFDDCB298A427057253C4E833057" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // Day unset
            new object[]
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
                    "4A397361C11EC1FE03A43E176E673856", // Opus 1.1.2 (Ubuntu 18.04)
                    "B181BB7AF0A3E37C3A262B942FD197FC" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // Month unset
            new object[]
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
                    "4A397361C11EC1FE03A43E176E673856", // Opus 1.1.2 (Ubuntu 18.04)
                    "B181BB7AF0A3E37C3A262B942FD197FC" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // TrackNumber unset
            new object[]
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
                    "666B7D0315E667A3D6A943294A32BE1F", // Opus 1.1.2 (Ubuntu 18.04)
                    "166F1DD0C71E2CCCF226157A34791E1B" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // TrackCount unset
            new object[]
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
                    "910BF18671BC79D50FB8B9EAE8E4F0DA", // Opus 1.1.2 (Ubuntu 18.04)
                    "8EA7FC13A2F214CADF3A1D447EF5CB65" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // Existing tag
            new object[]
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
                    "86AD4DC307C9523B2C1213FE2359FDEA", // Opus 1.1.2 (Ubuntu 18.04)
                    "BB50765E7DB79BDEADFC81E771101FD7" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // Existing tag removal
            new object[]
            {
                "Opus VBR 44100Hz Stereo (Tagged using defaults).opus",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "C2351EF6BFA1D50983183ADC6136FB9C", // Opus 1.1.2 (Ubuntu 18.04)
                    "367DB09F8408583579F918061FCB6322" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // Nothing to do
            new object[]
            {
                "Opus VBR 44100Hz Stereo.opus",
                new TestAudioMetadata(),
                string.Empty,
                new TestSettingDictionary(),
                new[]
                {
                    "CEF9DD8611EBFEF035761B450D816E95", // Opus 1.1.2 (Ubuntu 18.04)
                    "22D33DFB735C5B7805AB26C7A04CDA86" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            },

            // PNG CoverArt (Converted)
            new object[]
            {
                "Opus VBR 44100Hz Stereo.opus",
                new TestAudioMetadata(),
                "PNG 24-bit 1280 x 935.png",
                new TestSettingDictionary(),
                new[]
                {
                    "1636B4EF09C4EF2B16135E9C2670473B", // Opus 1.1.2 (Legacy .NET on Ubuntu 18.04)
                    "F4AFFDC5F5050965D5C62BB2C7F3D346", // Opus 1.1.2 (.NET Core 3.0+ on Ubuntu 18.04)
                    "C80AD6949880EA8AE45603CDAD7E0D29", // Opus 1.3.1 (Legacy .NET on 32-bit Windows)
                    "6D91ACB2687A9FADC202A4B7C3920593", // Opus 1.3.1 (Legacy .NET on 64-bit Windows)
                    "316157B1807D94E3E111802327A26787" // Opus 1.3.1 (MacOS, Ubuntu and .NET Core 3.0+ on Windows)
                }
            },

            // JPEG CoverArt
            new object[]
            {
                "Opus VBR 44100Hz Stereo.opus",
                new TestAudioMetadata(),
                "JPEG 24-bit 1280 x 935.jpg",
                new TestSettingDictionary(),
                new[]
                {
                    "1636B4EF09C4EF2B16135E9C2670473B", // Opus 1.1.2 (Ubuntu 18.04)
                    "CABF32D411EA5FA9458A79B6DC2C2D69" // Opus 1.3.1 (Ubuntu 20.04, MacOS and Windows)
                }
            }

            #endregion
        };

        public static IEnumerable<object[]> Data => _data.Select((item, index) => item.Prepend(index).ToArray());
    }
}
