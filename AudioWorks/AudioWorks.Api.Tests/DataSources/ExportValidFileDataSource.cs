using System.Collections.Generic;
using System.Linq;
using AudioWorks.Api.Tests.DataTypes;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class ExportValidFileDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            // Wave encoding
            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "Wave",
                null,
                "818EE6CBF16F76F923D33650E7A52708"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "Wave",
                null,
                "509B83828F13945E4121E4C4897A8649"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Wave",
                null,
                "5D4B869CD72BE208BC7B47F35E13BE9A"
            },
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Wave",
                null,
                "EFBC44B9FA9C04449D67ECD16CB7F3D8"
            },
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Wave",
                null,
                "D55BD1987676A7D6C2A04BF09C10F64F"
            },
            new object[]
            {
                "FLAC Level 5 8-bit 8000Hz Stereo.flac",
                "Wave",
                null,
                "818EE6CBF16F76F923D33650E7A52708"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                "Wave",
                null,
                "509B83828F13945E4121E4C4897A8649"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                "Wave",
                null,
                "5D4B869CD72BE208BC7B47F35E13BE9A"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                "Wave",
                null,
                "EFBC44B9FA9C04449D67ECD16CB7F3D8"
            },
            new object[]
            {
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                "Wave",
                null,
                "D55BD1987676A7D6C2A04BF09C10F64F"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Wave",
                null,
                "5D4B869CD72BE208BC7B47F35E13BE9A"
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Mono.m4a",
                "Wave",
                null,
                "509B83828F13945E4121E4C4897A8649"
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                "Wave",
                null,
                "5D4B869CD72BE208BC7B47F35E13BE9A"
            },
            new object[]
            {
                "ALAC 16-bit 48000Hz Stereo.m4a",
                "Wave",
                null,
                "EFBC44B9FA9C04449D67ECD16CB7F3D8"
            },
            new object[]
            {
                "ALAC 24-bit 96000Hz Stereo.m4a",
                "Wave",
                null,
                "D55BD1987676A7D6C2A04BF09C10F64F"
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
                "Wave",
                null,
                "5D4B869CD72BE208BC7B47F35E13BE9A"
            },

            // FLAC encoding
            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "FLAC",
                null,
                "44AA2E52CED28503D02D51957B19DF74"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "FLAC",
                null,
                "588ACB7827AF0D1A6A18751EEFEA3604"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                null,
                "3983A342A074A7E8871FEF4FBE0AC73F"
            },
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "FLAC",
                null,
                "8A532C4C9D61AF027BC6F684C59FE9A6"
            },
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "FLAC",
                null,
                "4A4DE0494E31D82F446421C876FB10EA"
            },
            new object[]
            {
                "FLAC Level 5 8-bit 8000Hz Stereo.flac",
                "FLAC",
                null,
                "44AA2E52CED28503D02D51957B19DF74"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                "FLAC",
                null,
                "588ACB7827AF0D1A6A18751EEFEA3604"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                "FLAC",
                null,
                "3983A342A074A7E8871FEF4FBE0AC73F"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                "FLAC",
                null,
                "8A532C4C9D61AF027BC6F684C59FE9A6"
            },
            new object[]
            {
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                "FLAC",
                null,
                "4A4DE0494E31D82F446421C876FB10EA"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "FLAC",
                null,
                "A6B18F2B4DDC51DC37154410E701251F"
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Mono.m4a",
                "FLAC",
                null,
                "588ACB7827AF0D1A6A18751EEFEA3604"
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                "FLAC",
                null,
                "3983A342A074A7E8871FEF4FBE0AC73F"
            },
            new object[]
            {
                "ALAC 16-bit 48000Hz Stereo.m4a",
                "FLAC",
                null,
                "8A532C4C9D61AF027BC6F684C59FE9A6"
            },
            new object[]
            {
                "ALAC 24-bit 96000Hz Stereo.m4a",
                "FLAC",
                null,
                "4A4DE0494E31D82F446421C876FB10EA"
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
                "FLAC",
                null,
                "0EE0AF9261C48854D66957F1567256C9"
            },
            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["CompressionLevel"] = "8"
                },
                "055A797607AD0156A1AB5FA3761D6F4A"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["CompressionLevel"] = "8"
                },
                "197F4E4A832513C6D6A4F0B2B0E9160B"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["CompressionLevel"] = "8"
                },
                "C73F21F10850A4542EEA2435226F1DEB"
            },
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["CompressionLevel"] = "8"
                },
                "7315CA9F4837F3C26E73F7897B06942A"
            },
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["CompressionLevel"] = "8"
                },
                "FC18E5E206B847D5121E4EAEF160BB47"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["SeekPointInterval"] = "0"
                },
                "7DBB3E3E8079E60932AA5F8B4D9CD57C"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["SeekPointInterval"] = "1"
                },
                "03C1D2C32C63848D0E5CB287E705FF48"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["Padding"] = "0"
                },
                "FFB7D9F0F4CDF37EDBA799FE371424A7"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["Padding"] = "10240"
                },
                "BC1409EBD6849C10194075C06647EEE2"
            },

            // Lame encoding
            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "LameMP3",
                null,
                "6687273F561AFA080E551094EAA6A7B7"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "LameMP3",
                null,
                "278E145CD9B5D289F7E942F13F21D40A"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                null,
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
            },
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "LameMP3",
                null,
                "B38922C3B31040AC33FB26562E4CDF4D"
            },
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "LameMP3",
                null,
                "3FC4319E0575FD00749855437442F0ED"
            },
            new object[]
            {
                "FLAC Level 5 8-bit 8000Hz Stereo.flac",
                "LameMP3",
                null,
                "6687273F561AFA080E551094EAA6A7B7"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                "LameMP3",
                null,
                "278E145CD9B5D289F7E942F13F21D40A"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                "LameMP3",
                null,
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                "LameMP3",
                null,
                "B38922C3B31040AC33FB26562E4CDF4D"
            },
            new object[]
            {
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                "LameMP3",
                null,
                "3FC4319E0575FD00749855437442F0ED"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                null,
                "31BFAF0AD95A00E47D6D55D3D1C8F3A1"
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Mono.m4a",
                "LameMP3",
                null,
                "278E145CD9B5D289F7E942F13F21D40A"
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                "LameMP3",
                null,
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
            },
            new object[]
            {
                "ALAC 16-bit 48000Hz Stereo.m4a",
                "LameMP3",
                null,
                "B38922C3B31040AC33FB26562E4CDF4D"
            },
            new object[]
            {
                "ALAC 24-bit 96000Hz Stereo.m4a",
                "LameMP3",
                null,
                "3FC4319E0575FD00749855437442F0ED"
            },
            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo (Tagged).m4a",
                "LameMP3",
                null,
                "032B83AD8A54FBAF67ED0C41D4870AB6"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    ["TagVersion"] = "2.4"
                },
                "DE767AE0737E5A64F694F49805C187C7"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    ["TagEncoding"] = "UTF16"
                },
                "6CB3E505F78FD128C54E8E44B33C6593"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    ["TagPadding"] = "100"
                },
                "EF02F7065F0B05D955D2604DCD4D5557"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    ["BitRate"] = "8"
                },
                "2BBC83E74AB1A4EB150BC6E1EB9920B5"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    ["BitRate"] = "320"
                },
                "BEB5029A08011BCEDFFA99173B763E7F"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    ["ForceCBR"] = true
                },
                "EACCA2FD6404ACA1AB46027FAE6A667B"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    ["BitRate"] = "8",
                    ["ForceCBR"] = true
                },
                "E350012375B3222543D4E2757AF4CC88"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    ["BitRate"] = "320",
                    ["ForceCBR"] = true
                },
                "77D0EF309A2EB2D1F62A2A6E787EA8F3"
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