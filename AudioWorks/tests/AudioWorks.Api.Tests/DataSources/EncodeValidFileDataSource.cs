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
                "3983A342A074A7E8871FEF4FBE0AC73F",
                "3983A342A074A7E8871FEF4FBE0AC73F"
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
                "A6B18F2B4DDC51DC37154410E701251F",
                "A6B18F2B4DDC51DC37154410E701251F"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "FLAC",
                null,
                "90DE035E77A93A3DA90AAA129928259B",
                "90DE035E77A93A3DA90AAA129928259B"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "FLAC",
                null,
                "8C9EFD0837D816A6360BB8CF70A0D392",
                "8C9EFD0837D816A6360BB8CF70A0D392"
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
                "3983A342A074A7E8871FEF4FBE0AC73F",
                "3983A342A074A7E8871FEF4FBE0AC73F"
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
                "C73F21F10850A4542EEA2435226F1DEB",
                "C73F21F10850A4542EEA2435226F1DEB"
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
                "3983A342A074A7E8871FEF4FBE0AC73F",
                "3983A342A074A7E8871FEF4FBE0AC73F"
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
                "7DBB3E3E8079E60932AA5F8B4D9CD57C",
                "7DBB3E3E8079E60932AA5F8B4D9CD57C"
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
                "3983A342A074A7E8871FEF4FBE0AC73F",
                "3983A342A074A7E8871FEF4FBE0AC73F"
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
                "3983A342A074A7E8871FEF4FBE0AC73F",
                "3983A342A074A7E8871FEF4FBE0AC73F"
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
                "FFB7D9F0F4CDF37EDBA799FE371424A7",
                "FFB7D9F0F4CDF37EDBA799FE371424A7"
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
                "F03F417B853C560705CD424AD329EFBC",
                "F03F417B853C560705CD424AD329EFBC"
            },

            #endregion

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
                    // 2048 bytes padding (default)
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
                    ["Padding"] = int.MaxValue,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                "not03305CCE91A686386908415EF35BDE0D",
                "not03305CCE91A686386908415EF35BDE0D"
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

            #region Lame MP3 Encoding

            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "LameMP3",
                null,
                "7CB68FB7ACC70E8CD928E7DB437B16FE",
                "7CB68FB7ACC70E8CD928E7DB437B16FE"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "LameMP3",
                null,
                "C02CA44F3E1CCA3D8BA0DE922C49946E",
                "C02CA44F3E1CCA3D8BA0DE922C49946E"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                null,
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
            },

            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "LameMP3",
                null,
                "A333E74AFF4107E6C6C987AB27DF4B36",
                "A333E74AFF4107E6C6C987AB27DF4B36"
            },

            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "LameMP3",
                null,
                "C0204097396B92D06E2B1BEBA90D0BD9",
                "C0204097396B92D06E2B1BEBA90D0BD9"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                null,
                "55F290095FDCE602C43380CC4F5D1101",
                "55F290095FDCE602C43380CC4F5D1101"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "LameMP3",
                null,
                "3DF865506B4791EB5C872864FA68FEAD",
                "3DF865506B4791EB5C872864FA68FEAD"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "LameMP3",
                null,
                "3DF865506B4791EB5C872864FA68FEAD",
                "3DF865506B4791EB5C872864FA68FEAD"
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
                "55F290095FDCE602C43380CC4F5D1101",
                "55F290095FDCE602C43380CC4F5D1101"
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
                "7B26B3378995DB4716016DF78074B37A",
                "7B26B3378995DB4716016DF78074B37A"
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
                "55F290095FDCE602C43380CC4F5D1101",
                "55F290095FDCE602C43380CC4F5D1101"
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
                "AB114692E780A51DBBE029446A29F4AF",
                "AB114692E780A51DBBE029446A29F4AF"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Tag padding disabled (default)
                    ["TagPadding"] = 0
                },
                "55F290095FDCE602C43380CC4F5D1101",
                "55F290095FDCE602C43380CC4F5D1101"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Maximum tag padding
                    ["TagPadding"] = 268_435_456
                },
                "655CE292707399097673F7EFFA439784",
                "655CE292707399097673F7EFFA439784"
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
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
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
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
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
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
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
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
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
                "BB8B33BD589DA49D751C883B8A0FF653",
                "BB8B33BD589DA49D751C883B8A0FF653"
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
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
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
                "34C345AB6BDA4A4C172D74046EC683D7",
                "34C345AB6BDA4A4C172D74046EC683D7"
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
                "D297129E81AAB0E1078766300A4E295D",
                "D297129E81AAB0E1078766300A4E295D"
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
                "9EB11045A3333DA3CAA399E61619C521",
                "9EB11045A3333DA3CAA399E61619C521"
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
                "0B5272E75B75C90ED3711BB704A69085",
                "0B5272E75B75C90ED3711BB704A69085"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                "7EFC100445D91271A30106D377E78625",
                "7EFC100445D91271A30106D377E78625"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                "45666989AC5E44A62F01CC2BDCB91744",
                "45666989AC5E44A62F01CC2BDCB91744"
            },

            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                "101752F38289276639BEE8F8B6A4DE77",
                "101752F38289276639BEE8F8B6A4DE77"
            },

            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                "31DB825BFE49FC6C88BD42528C56D8E9",
                "31DB825BFE49FC6C88BD42528C56D8E9"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                "197910C7058AEB741296309AF7174BFD",
                "197910C7058AEB741296309AF7174BFD"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                "8ECF9A3242E6D8B195A941AF0024ED13",
                "8ECF9A3242E6D8B195A941AF0024ED13"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                "8ECF9A3242E6D8B195A941AF0024ED13",
                "8ECF9A3242E6D8B195A941AF0024ED13"
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
                "1839CB684855C73D273C3CE3F516E0BC",
                "1839CB684855C73D273C3CE3F516E0BC"
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
                "7D72014E4FA665CE618768EFD9413142",
                "7D72014E4FA665CE618768EFD9413142"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Default quality
                    ["Quality"] = 5,
                    ["SerialNumber"] = 1
                },
                "45666989AC5E44A62F01CC2BDCB91744",
                "45666989AC5E44A62F01CC2BDCB91744"
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
                "F236873FDC61D3143260868E0889D9CF",
                "F236873FDC61D3143260868E0889D9CF"
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
                "42ADAC7AE2CFA4C9EA418193C3E18B46",
                "42ADAC7AE2CFA4C9EA418193C3E18B46"
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
                "11B26EED6F26094F87DD47A04B039E26",
                "11B26EED6F26094F87DD47A04B039E26"
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
                "C87733A5F35D8015E0DC873596975DCB",
                "C87733A5F35D8015E0DC873596975DCB"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Managed bit rate disabled (default)
                    ["BitRate"] = 128,
                    ["SerialNumber"] = 1
                },
                "C3C78067859D261A13ADB86AE699E0B8",
                "C3C78067859D261A13ADB86AE699E0B8"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Managed bit rate explicitly disabled
                    ["BitRate"] = 128,
                    ["Managed"] = false,
                    ["SerialNumber"] = 1
                },
                "C3C78067859D261A13ADB86AE699E0B8",
                "C3C78067859D261A13ADB86AE699E0B8"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Managed bit rate enabled
                    ["BitRate"] = 128,
                    ["Managed"] = true,
                    ["SerialNumber"] = 1
                },
                "4E482E37F344F485CA594BDD82C3F314",
                "4E482E37F344F485CA594BDD82C3F314"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Managed bit rate ignored without bit rate
                    ["Managed"] = true,
                    ["SerialNumber"] = 1
                },
                "45666989AC5E44A62F01CC2BDCB91744",
                "45666989AC5E44A62F01CC2BDCB91744"
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
                "C3C78067859D261A13ADB86AE699E0B8",
                "C3C78067859D261A13ADB86AE699E0B8"
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
                "45666989AC5E44A62F01CC2BDCB91744",
                "45666989AC5E44A62F01CC2BDCB91744"
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
                "44C4E291AB3DF95E545995F9D3EAA32A",
                "44C4E291AB3DF95E545995F9D3EAA32A"
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
                "ECF9270A3FFB3CE2486D118D21C85C23",
                "ECF9270A3FFB3CE2486D118D21C85C23"
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