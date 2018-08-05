using System;
using System.Collections.Generic;
using System.Linq;
using AudioWorks.Api.Tests.DataTypes;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class EncodeValidFileDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            #region Wave Encoding

            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "Wave",
                null,
                "818EE6CBF16F76F923D33650E7A52708",
                "818EE6CBF16F76F923D33650E7A52708"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "Wave",
                null,
                "509B83828F13945E4121E4C4897A8649",
                "509B83828F13945E4121E4C4897A8649"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Wave",
                null,
                "5D4B869CD72BE208BC7B47F35E13BE9A",
                "5D4B869CD72BE208BC7B47F35E13BE9A"
            },

            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Wave",
                null,
                "EFBC44B9FA9C04449D67ECD16CB7F3D8",
                "EFBC44B9FA9C04449D67ECD16CB7F3D8"
            },

            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Wave",
                null,
                "D55BD1987676A7D6C2A04BF09C10F64F",
                "D55BD1987676A7D6C2A04BF09C10F64F"
            },

            new object[]
            {
                "FLAC Level 5 8-bit 8000Hz Stereo.flac",
                "Wave",
                null,
                "818EE6CBF16F76F923D33650E7A52708",
                "818EE6CBF16F76F923D33650E7A52708"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                "Wave",
                null,
                "509B83828F13945E4121E4C4897A8649",
                "509B83828F13945E4121E4C4897A8649"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                "Wave",
                null,
                "5D4B869CD72BE208BC7B47F35E13BE9A",
                "5D4B869CD72BE208BC7B47F35E13BE9A"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                "Wave",
                null,
                "EFBC44B9FA9C04449D67ECD16CB7F3D8",
                "EFBC44B9FA9C04449D67ECD16CB7F3D8"
            },

            new object[]
            {
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                "Wave",
                null,
                "D55BD1987676A7D6C2A04BF09C10F64F",
                "D55BD1987676A7D6C2A04BF09C10F64F"
            },

#if !LINUX
            new object[]
            {
                "ALAC 16-bit 44100Hz Mono.m4a",
                "Wave",
                null,
                "509B83828F13945E4121E4C4897A8649",
                "509B83828F13945E4121E4C4897A8649"
            },

            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                "Wave",
                null,
                "5D4B869CD72BE208BC7B47F35E13BE9A",
                "5D4B869CD72BE208BC7B47F35E13BE9A"
            },

            new object[]
            {
                "ALAC 16-bit 48000Hz Stereo.m4a",
                "Wave",
                null,
                "EFBC44B9FA9C04449D67ECD16CB7F3D8",
                "EFBC44B9FA9C04449D67ECD16CB7F3D8"
            },

            new object[]
            {
                "ALAC 24-bit 96000Hz Stereo.m4a",
                "Wave",
                null,
                "D55BD1987676A7D6C2A04BF09C10F64F",
                "D55BD1987676A7D6C2A04BF09C10F64F"
            },
#endif

            #endregion

            #region FLAC Encoding

            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "FLAC",
                null,
                "44AA2E52CED28503D02D51957B19DF74",
                "44AA2E52CED28503D02D51957B19DF74"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "FLAC",
                null,
                "588ACB7827AF0D1A6A18751EEFEA3604",
                "588ACB7827AF0D1A6A18751EEFEA3604"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                null,
#if LINUX
                "3983A342A074A7E8871FEF4FBE0AC73F",
                "3983A342A074A7E8871FEF4FBE0AC73F"
#else
                "0137D7EA15464514D6C8445D4940654A",
                "0137D7EA15464514D6C8445D4940654A"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "FLAC",
                null,
                "8A532C4C9D61AF027BC6F684C59FE9A6",
                "8A532C4C9D61AF027BC6F684C59FE9A6"
            },

            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "FLAC",
                null,
                "4A4DE0494E31D82F446421C876FB10EA",
                "4A4DE0494E31D82F446421C876FB10EA"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "FLAC",
                null,
#if LINUX
                "A6B18F2B4DDC51DC37154410E701251F",
                "A6B18F2B4DDC51DC37154410E701251F"
#else
                "CAA2E90C27940A68EFE083E439527C27",
                "CAA2E90C27940A68EFE083E439527C27"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "FLAC",
                null,
#if LINUX
                "90DE035E77A93A3DA90AAA129928259B",
                "90DE035E77A93A3DA90AAA129928259B"
#else
                "2F737CBE53D42EFCE49B231FE6C9132B",
                "2F737CBE53D42EFCE49B231FE6C9132B"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "FLAC",
                null,
#if LINUX
                "8C9EFD0837D816A6360BB8CF70A0D392",
                "8C9EFD0837D816A6360BB8CF70A0D392"
#else
                "F396A80ABD33E1E30B7C1F5485808F1B",
                "F396A80ABD33E1E30B7C1F5485808F1B"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Default compression
                    ["CompressionLevel"] = 5
                },
#if LINUX
                "3983A342A074A7E8871FEF4FBE0AC73F",
                "3983A342A074A7E8871FEF4FBE0AC73F"
#else
                "0137D7EA15464514D6C8445D4940654A",
                "0137D7EA15464514D6C8445D4940654A"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Minimum compression
                    ["CompressionLevel"] = 0
                },
                "D352B276E4712ABBA3A8F1B9CA8BAB55",
                "D352B276E4712ABBA3A8F1B9CA8BAB55"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Maximum compression
                    ["CompressionLevel"] = 8
                },
#if LINUX
                "C73F21F10850A4542EEA2435226F1DEB",
                "C73F21F10850A4542EEA2435226F1DEB"
#else
                "63CB2495C4A463A41B019F8A8D86AB3F",
                "63CB2495C4A463A41B019F8A8D86AB3F"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Default seek point interval
                    ["SeekPointInterval"] = 10
                },
#if LINUX
                "3983A342A074A7E8871FEF4FBE0AC73F",
                "3983A342A074A7E8871FEF4FBE0AC73F"
#else
                "0137D7EA15464514D6C8445D4940654A",
                "0137D7EA15464514D6C8445D4940654A"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Disabled seek points
                    ["SeekPointInterval"] = 0
                },
#if LINUX
                "7DBB3E3E8079E60932AA5F8B4D9CD57C",
                "7DBB3E3E8079E60932AA5F8B4D9CD57C"
#else
                "260617EF462F485D9470044D8F16D05A",
                "260617EF462F485D9470044D8F16D05A"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Maximum seek point interval
                    ["SeekPointInterval"] = 600
                },
#if LINUX
                "3983A342A074A7E8871FEF4FBE0AC73F",
                "3983A342A074A7E8871FEF4FBE0AC73F"
#else
                "0137D7EA15464514D6C8445D4940654A",
                "0137D7EA15464514D6C8445D4940654A"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Default padding
                    ["Padding"] = 8192
                },
#if LINUX
                "3983A342A074A7E8871FEF4FBE0AC73F",
                "3983A342A074A7E8871FEF4FBE0AC73F"
#else
                "0137D7EA15464514D6C8445D4940654A",
                "0137D7EA15464514D6C8445D4940654A"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Disabled padding
                    ["Padding"] = 0
                },
#if LINUX
                "FFB7D9F0F4CDF37EDBA799FE371424A7",
                "FFB7D9F0F4CDF37EDBA799FE371424A7"
#else
                "D0886BDBA9B11ED9644F52B6832DD066",
                "D0886BDBA9B11ED9644F52B6832DD066"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Maximum padding
                    ["Padding"] = 16_775_369
                },
#if LINUX
                "F03F417B853C560705CD424AD329EFBC",
                "F03F417B853C560705CD424AD329EFBC"
#else
                "D231A96C6D04252FE4A98C9BB9409885",
                "D231A96C6D04252FE4A98C9BB9409885"
#endif
            },

            #endregion

#if !LINUX
            #region ALAC Encoding

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "50F7F27DBCCE5874118C3DE9B0F0306D",
                "50F7F27DBCCE5874118C3DE9B0F0306D"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "4A2E22037B18F3318920EA47BA76825C",
                "4A2E22037B18F3318920EA47BA76825C"
            },

            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "C299C20C8EF4ED5B6B5664E6B81C3244",
                "C299C20C8EF4ED5B6B5664E6B81C3244"
            },

            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "26442948986C55394D8AE960E66101C3",
                "26442948986C55394D8AE960E66101C3"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "C8E2DD6861F837C845A52A4C34523C85",
                "C8E2DD6861F837C845A52A4C34523C85"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "9A2F8D8A4BCEF6064025DC788DF06C55",
                "9A2F8D8A4BCEF6064025DC788DF06C55"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "36A4FA66194B49D1DA1C111E7D444EB3",
                "36A4FA66194B49D1DA1C111E7D444EB3"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    // Different creation time
                    ["CreationTime"] = new DateTime(2016, 12, 1),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "2F72E377036957C669D858AEA26DF62F",
                "2F72E377036957C669D858AEA26DF62F"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    // Different modification time
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2018, 12, 1)
                },
                "F57326FFFD308ED69B83F7F451938D55",
                "F57326FFFD308ED69B83F7F451938D55"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    // Default padding (explicit)
                    ["Padding"] = 2048,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "4A2E22037B18F3318920EA47BA76825C",
                "4A2E22037B18F3318920EA47BA76825C"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    // Disabled padding
                    ["Padding"] = 0,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "03305CCE91A686386908415EF35BDE0D",
                "03305CCE91A686386908415EF35BDE0D"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    // Maximum padding
                    ["Padding"] = 16_777_216,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "815E83D61745D4E117E12D31543C47BF",
                "815E83D61745D4E117E12D31543C47BF"
            },

            #endregion

            #region Apple AAC Encoding

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "09CD8B8C8E9D8BC09121D8C9F871F9B7",
                "D281CFECEEBE5A14D0D3D953D12F71DC"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "9A0F6E1984B428F236E1209C13AED4D1",
                "47189DECF29E68A40F60645F97714BE3"
            },

            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "CB39DFBF414790022574435C2D30297D",
                "589C93B14B5A2C8EF39239949A7729FF"
            },

            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "E0C34EA1479C8979D3AF3A2C98D4E699",
                "BA760ADA182CA749797AC1B978266CB1"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "7BAD797AA7C5F71C7168C24077271029",
                "A3DC2D21D29A05456284B0B8C09E1F94"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "57FCD0244E6B8DB26713BB6A64A94172",
                "B8E8CCF5EFBA855262F0B29C4871A9B5"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "57FCD0244E6B8DB26713BB6A64A94172",
                "B8E8CCF5EFBA855262F0B29C4871A9B5"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // Default VBR quality
                    ["VBRQuality"] = 9,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "9A0F6E1984B428F236E1209C13AED4D1",
                "47189DECF29E68A40F60645F97714BE3"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // Minimum VBR quality
                    ["VBRQuality"] = 0,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "78299761793D1A6EC79CBB9233156FD8",
                "78299761793D1A6EC79CBB9233156FD8"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // Maximum VBR quality
                    ["VBRQuality"] = 14,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "7EDD94F25082AEEE82B2AA87E795AB6D",
                "560039278EF9183F0FB2C47E5744E475"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // Minimum bit rate (stereo is automatically increased to 64)
                    ["BitRate"] = 32,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "0177BB1DEB19854CA8495C4CBBB25366",
                "DB44ACD7770861D4A3C6D7EE644C5E1C"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // Minimum bit rate (mono)
                    ["BitRate"] = 32,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "9E77C0824474E3600F1A919715609A1B",
                "2321A80FDC5F36A1860523948548F8E5"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // Maximum bit rate (stereo)
                    ["BitRate"] = 320,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "EBD496E30A953A8D0FE11C2609EFABC3",
                "A67A5F8D1A55CD2A29EEFA54E583AEA1"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // Maximum bit rate (mono is automatically reduced to 256)
                    ["BitRate"] = 320,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "DE5F94EC1EACB75A3D049AE9960A7ACB",
                "8F6858F8F86AA821789D926E0B4F63B6"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // Constrained bit rate mode (default)
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "B26C14FD53A4027C26FA3A57CB96AF4C",
                "EEEAF1FB2801EF0FB49B9B87350B5587"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // Constrained bit rate mode (explicit)
                    ["ControlMode"] = "Constrained",
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "B26C14FD53A4027C26FA3A57CB96AF4C",
                "EEEAF1FB2801EF0FB49B9B87350B5587"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // Average bit rate mode
                    ["ControlMode"] = "Average",
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "B65D496ADABF3DBCDB24136A9655C295",
                "6AD4BD76918C74B976FD7774163CD7ED"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // Constant bit rate mode
                    ["ControlMode"] = "Constant",
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "365D7E965534C8690B4694B27D0CF1C9",
                "4E99289AFF43EA387442E30EAFB7305A"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track",
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "9A0F6E1984B428F236E1209C13AED4D1",
                "47189DECF29E68A40F60645F97714BE3"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track",
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "DDA8DBB070EA36F77455A41A2628B6AA",
                "14145EA9D279E2FA457AD85F19DC0896"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "AppleAAC",
                new TestSettingDictionary
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album",
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "5502D724D98AA24FE49FA8AFB0FC63A6",
                "90D1426E435372B957E6558E4DC5D7FD"
            },

            #endregion
#endif

            #region Lame MP3 Encoding

            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "LameMP3",
                null,
#if LINUX
                "F2BD0875E273743A8908F96DCCFDFC44",
                "F2BD0875E273743A8908F96DCCFDFC44"
#else
                "7CB68FB7ACC70E8CD928E7DB437B16FE",
                "7CB68FB7ACC70E8CD928E7DB437B16FE"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "LameMP3",
                null,
#if LINUX
                "1CB5B915B3A72CBE76087E16F96A0A3E",
                "1CB5B915B3A72CBE76087E16F96A0A3E"
#else
                "C02CA44F3E1CCA3D8BA0DE922C49946E",
                "C02CA44F3E1CCA3D8BA0DE922C49946E"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                null,
#if LINUX
                "10E44CEE38E66E9737677BE52E7A286D",
                "10E44CEE38E66E9737677BE52E7A286D"
#else
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "LameMP3",
                null,
#if LINUX
                "1454732B48913F2A3898164BA366DA01",
                "1454732B48913F2A3898164BA366DA01"
#else
                "A333E74AFF4107E6C6C987AB27DF4B36",
                "A333E74AFF4107E6C6C987AB27DF4B36"
#endif
            },

            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "LameMP3",
                null,
#if LINUX
                "AD56C3A1ACD627DBDA4B5A28AFE0355D",
                "AD56C3A1ACD627DBDA4B5A28AFE0355D"
#else
                "C0204097396B92D06E2B1BEBA90D0BD9",
                "C0204097396B92D06E2B1BEBA90D0BD9"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                null,
#if LINUX
                "32EEC2B69A048975FB3BD034E8B392A4",
                "32EEC2B69A048975FB3BD034E8B392A4"
#else
                "0E420A25F4CAE013B6328E5A52E38B3D",
                "0E420A25F4CAE013B6328E5A52E38B3D"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "LameMP3",
                null,
#if LINUX
                "230577EAC431F91BDB7DF5BBAAF655A0",
                "230577EAC431F91BDB7DF5BBAAF655A0"
#else
                "78575206A37994E84C29BE944D7C7A4F",
                "78575206A37994E84C29BE944D7C7A4F"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "LameMP3",
                null,
#if LINUX
                "230577EAC431F91BDB7DF5BBAAF655A0",
                "230577EAC431F91BDB7DF5BBAAF655A0"
#else
                "78575206A37994E84C29BE944D7C7A4F",
                "78575206A37994E84C29BE944D7C7A4F"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Default tag version
                    ["TagVersion"] = "2.3"
                },
#if LINUX
                "32EEC2B69A048975FB3BD034E8B392A4",
                "32EEC2B69A048975FB3BD034E8B392A4"
#else
                "0E420A25F4CAE013B6328E5A52E38B3D",
                "0E420A25F4CAE013B6328E5A52E38B3D"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Tag version 2.4
                    ["TagVersion"] = "2.4"
                },
#if LINUX
                "24F1B744BDA5C6A94B9FE6136DEC4614",
                "24F1B744BDA5C6A94B9FE6136DEC4614"
#else
                "6D7E02945EFBFAB16E41827E4C7F9D7F",
                "6D7E02945EFBFAB16E41827E4C7F9D7F"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Default tag encoding
                    ["TagEncoding"] = "Latin1"
                },
#if LINUX
                "32EEC2B69A048975FB3BD034E8B392A4",
                "32EEC2B69A048975FB3BD034E8B392A4"
#else
                "0E420A25F4CAE013B6328E5A52E38B3D",
                "0E420A25F4CAE013B6328E5A52E38B3D"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // UTF-16 tag encoding
                    ["TagEncoding"] = "UTF16"
                },
#if LINUX
                "1B5C266B6D799765BA1272BE28A7C435",
                "1B5C266B6D799765BA1272BE28A7C435"
#else
                "92ED87DB059CA2A609E8AADF0AF84909",
                "92ED87DB059CA2A609E8AADF0AF84909"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Default tag padding (explicit)
                    ["TagPadding"] = 2048
                },
#if LINUX
                "32EEC2B69A048975FB3BD034E8B392A4",
                "32EEC2B69A048975FB3BD034E8B392A4"
#else
                "0E420A25F4CAE013B6328E5A52E38B3D",
                "0E420A25F4CAE013B6328E5A52E38B3D"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Tag padding disabled
                    ["TagPadding"] = 0
                },
#if LINUX
                "5737ED221E55314FD5B9FA167C1C1651",
                "5737ED221E55314FD5B9FA167C1C1651"
#else
                "55F290095FDCE602C43380CC4F5D1101",
                "55F290095FDCE602C43380CC4F5D1101"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Maximum tag padding
                    ["TagPadding"] = 16_777_216
                },
#if LINUX
                "D270226F14E850B208624CD2AC59512D",
                "D270226F14E850B208624CD2AC59512D"
#else
                "2572B2613BC3B8E63048527A247A7906",
                "2572B2613BC3B8E63048527A247A7906"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Tag version does nothing without metadata
                    ["TagVersion"] = "2.4"
                },
#if LINUX
                "10E44CEE38E66E9737677BE52E7A286D",
                "10E44CEE38E66E9737677BE52E7A286D"
#else
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Tag encoding does nothing without metadata
                    ["TagEncoding"] = "UTF16"
                },
#if LINUX
                "10E44CEE38E66E9737677BE52E7A286D",
                "10E44CEE38E66E9737677BE52E7A286D"
#else
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Tag padding does nothing without metadata
                    ["TagPadding"] = 100
                },
#if LINUX
                "10E44CEE38E66E9737677BE52E7A286D",
                "10E44CEE38E66E9737677BE52E7A286D"
#else
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Default VBR quality
                    ["VBRQuality"] = 3
                },
#if LINUX
                "10E44CEE38E66E9737677BE52E7A286D",
                "10E44CEE38E66E9737677BE52E7A286D"
#else
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Minimum VBR quality
                    ["VBRQuality"] = 9
                },
#if LINUX
                "65D418A236D86A8CE33E07A76C98DF08",
                "65D418A236D86A8CE33E07A76C98DF08"
#else
                "BB8B33BD589DA49D751C883B8A0FF653",
                "BB8B33BD589DA49D751C883B8A0FF653"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Maximum VBR quality
                    ["VBRQuality"] = 0
                },
                "5DE234656056DFDAAD30E4DA9FD26366",
                "5DE234656056DFDAAD30E4DA9FD26366"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Minimum bit rate
                    ["BitRate"] = 8
                },
                "2BBC83E74AB1A4EB150BC6E1EB9920B5",
                "2BBC83E74AB1A4EB150BC6E1EB9920B5"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Maximum bit rate
                    ["BitRate"] = 320
                },
                "BEB5029A08011BCEDFFA99173B763E7F",
                "BEB5029A08011BCEDFFA99173B763E7F"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Forced bit rate disabled (default)
                    ["BitRate"] = 128
                },
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6",
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Forced bit rate explicitly disabled
                    ["BitRate"] = 128,
                    ["ForceCBR"] = false
                },
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6",
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Forced bit rate enabled
                    ["BitRate"] = 128,
                    ["ForceCBR"] = true
                },
                "EACCA2FD6404ACA1AB46027FAE6A667B",
                "EACCA2FD6404ACA1AB46027FAE6A667B"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Forced bit rate ignored without bit rate
                    ["ForceCBR"] = true
                },
#if LINUX
                "10E44CEE38E66E9737677BE52E7A286D",
                "10E44CEE38E66E9737677BE52E7A286D"
#else
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // VBR quality ignored with bit rate
                    ["VBRQuality"] = 3,
                    ["BitRate"] = 128
                },
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6",
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track"
                },
#if LINUX
                "10E44CEE38E66E9737677BE52E7A286D",
                "10E44CEE38E66E9737677BE52E7A286D"
#else
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track"
                },
#if LINUX
                "8EFA63733A5527E08092CAD86E86F76E",
                "8EFA63733A5527E08092CAD86E86F76E"
#else
                "56C04FEBD02CB1127649A21E70B78C0E",
                "56C04FEBD02CB1127649A21E70B78C0E"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album"
                },
#if LINUX
                "FB96F0590FB1A603C847DEF075D8FC5F",
                "FB96F0590FB1A603C847DEF075D8FC5F"
#else
                "F8A627B834782D13EBB9A9B722094F05",
                "F8A627B834782D13EBB9A9B722094F05"
#endif
            },

            #endregion

            #region Ogg Vorbis Encoding

            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "F4351B0F0454B305E353318B410ECBB7",
                "F4351B0F0454B305E353318B410ECBB7"
#else
                "C3379168580C0B08178735BEBF80EA72",
                "C3379168580C0B08178735BEBF80EA72"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "ECC538FF2379AFB7DA3C0A40D2F9B183",
                "ECC538FF2379AFB7DA3C0A40D2F9B183"
#else
                "1F962FD6341344AE7A3D01C2ACDC01F1",
                "1F962FD6341344AE7A3D01C2ACDC01F1"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "4FF4ECBB0DAD17ABBB42A3A098B5B79B",
                "4FF4ECBB0DAD17ABBB42A3A098B5B79B"
#else
                "BAD08F056C5BA8308C7B9D1D2E1C2564",
                "BAD08F056C5BA8308C7B9D1D2E1C2564"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "9602838283C7BA2E83930679CFE937D3",
                "9602838283C7BA2E83930679CFE937D3"
#else
                "2D2C106A4DB35969205DCCDAF5229539",
                "2D2C106A4DB35969205DCCDAF5229539"
#endif
            },

            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "A40F3915C0CC3ED6DA131CE5EDFAD724",
                "A40F3915C0CC3ED6DA131CE5EDFAD724"
#else
                "5F3982A0B34D80B598CBF57265804EA0",
                "5F3982A0B34D80B598CBF57265804EA0"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "D2A49419AB33AA6B299500696FF8E45D",
                "D2A49419AB33AA6B299500696FF8E45D"
#else
                "1ACAE5D89454C496451F897C6BD32FD0",
                "1ACAE5D89454C496451F897C6BD32FD0"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "CDA9A8AF5DFEDE173FA113A0BA8316AF",
                "CDA9A8AF5DFEDE173FA113A0BA8316AF"
#else
                "EC59CDECA88BD881B225A5616E82C42C",
                "EC59CDECA88BD881B225A5616E82C42C"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "CDA9A8AF5DFEDE173FA113A0BA8316AF",
                "CDA9A8AF5DFEDE173FA113A0BA8316AF"
#else
                "EC59CDECA88BD881B225A5616E82C42C",
                "EC59CDECA88BD881B225A5616E82C42C"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Minimum serial #
                    ["SerialNumber"] = int.MinValue
                },
#if LINUX
                "EEE89A23404EF9D5ED0D87DB36886EE5",
                "EEE89A23404EF9D5ED0D87DB36886EE5"
#else
                "AACC33C1A52E37098DBBD948039ACF06",
                "AACC33C1A52E37098DBBD948039ACF06"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Maximum serial #
                    ["SerialNumber"] = int.MaxValue
                },
#if LINUX
                "B0CD6D94A249D8CCCAD8FD1BDCFF0484",
                "B0CD6D94A249D8CCCAD8FD1BDCFF0484"
#else
                "626A3AE3A7452955075D5578A7A60830",
                "626A3AE3A7452955075D5578A7A60830"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Default quality (explicit)
                    ["Quality"] = 5,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "4FF4ECBB0DAD17ABBB42A3A098B5B79B",
                "4FF4ECBB0DAD17ABBB42A3A098B5B79B"
#else
                "BAD08F056C5BA8308C7B9D1D2E1C2564",
                "BAD08F056C5BA8308C7B9D1D2E1C2564"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Minimum quality
                    ["Quality"] = -1,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "5A4810D3420C0D8F5F01366ADA4ADCA5",
                "5A4810D3420C0D8F5F01366ADA4ADCA5"
#else
                "7FC140C80519059284FD728CD4198515",
                "7FC140C80519059284FD728CD4198515"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Maximum quality
                    ["Quality"] = 10,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "B29E49F15FF28688977D1410F3AC549C",
                "B29E49F15FF28688977D1410F3AC549C"
#else
                "0B65C4E0389B976E508AEB677F686170",
                "0B65C4E0389B976E508AEB677F686170"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Minimum bit rate
                    ["BitRate"] = 45,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "CFFA4031CD86194861D2AEB3671C2980",
                "CFFA4031CD86194861D2AEB3671C2980"
#else
                "0F9B9459E5E02BD639E086C50904ACD7",
                "0F9B9459E5E02BD639E086C50904ACD7"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Maximum bit rate
                    ["BitRate"] = 500,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "B7305400434E748A541AA40DFCE008AC",
                "B7305400434E748A541AA40DFCE008AC"
#else
                "A5E58EDDF584651D768E809B45AF0E57",
                "A5E58EDDF584651D768E809B45AF0E57"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Forced bit rate disabled (default)
                    ["BitRate"] = 128,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "A74BCB8E6E196514559814E05FCCC71A",
                "A74BCB8E6E196514559814E05FCCC71A"
#else
                "6419168FF3EF995A41F17FCAF44E9C4A",
                "6419168FF3EF995A41F17FCAF44E9C4A"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Forced bit rate disabled (explicit)
                    ["BitRate"] = 128,
                    ["ForceCBR"] = false,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "A74BCB8E6E196514559814E05FCCC71A",
                "A74BCB8E6E196514559814E05FCCC71A"
#else
                "6419168FF3EF995A41F17FCAF44E9C4A",
                "6419168FF3EF995A41F17FCAF44E9C4A"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Forced bit rate enabled
                    ["BitRate"] = 128,
                    ["ForceCBR"] = true,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "8BB26B2BDDDC220D5B6908479981B8D3",
                "8BB26B2BDDDC220D5B6908479981B8D3"
#else
                "1DE19792FF2C7D898197AE88677BABFD",
                "1DE19792FF2C7D898197AE88677BABFD"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Forced bit rate ignored without bit rate
                    ["ForceCBR"] = true,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "4FF4ECBB0DAD17ABBB42A3A098B5B79B",
                "4FF4ECBB0DAD17ABBB42A3A098B5B79B"
#else
                "BAD08F056C5BA8308C7B9D1D2E1C2564",
                "BAD08F056C5BA8308C7B9D1D2E1C2564"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Quality ignored with bit rate
                    ["Quality"] = 3,
                    ["BitRate"] = 128,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "A74BCB8E6E196514559814E05FCCC71A",
                "A74BCB8E6E196514559814E05FCCC71A"
#else
                "6419168FF3EF995A41F17FCAF44E9C4A",
                "6419168FF3EF995A41F17FCAF44E9C4A"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
#if LINUX
                "4FF4ECBB0DAD17ABBB42A3A098B5B79B",
                "4FF4ECBB0DAD17ABBB42A3A098B5B79B"
#else
                "BAD08F056C5BA8308C7B9D1D2E1C2564",
                "BAD08F056C5BA8308C7B9D1D2E1C2564"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
#if LINUX
                "3BE0C521F649D9BE8E71E44C20D1B0A4",
                "3BE0C521F649D9BE8E71E44C20D1B0A4"
#else
                "CFCCC97C8BE4941921C95F2D501CF66C",
                "CFCCC97C8BE4941921C95F2D501CF66C"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album",
                    ["SerialNumber"] = 1
                },
#if LINUX
                "9376D4E99CDB475D5073D1CC545D8D0B",
                "9376D4E99CDB475D5073D1CC545D8D0B"
#else
                "E89F1CDE37BFB3E81D96B1BD044F8D07",
                "E89F1CDE37BFB3E81D96B1BD044F8D07"
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