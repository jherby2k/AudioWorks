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

#if !LINUX
using System;
#endif
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
#if !OSX
                "818EE6CBF16F76F923D33650E7A52708",
#endif
                "818EE6CBF16F76F923D33650E7A52708"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "Wave",
                null,
#if !OSX
                "509B83828F13945E4121E4C4897A8649",
#endif
                "509B83828F13945E4121E4C4897A8649"
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Wave",
                null,
#if !OSX
                "5D4B869CD72BE208BC7B47F35E13BE9A",
#endif
                "5D4B869CD72BE208BC7B47F35E13BE9A"
            },

            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Wave",
                null,
#if !OSX
                "EFBC44B9FA9C04449D67ECD16CB7F3D8",
#endif
                "EFBC44B9FA9C04449D67ECD16CB7F3D8"
            },

            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Wave",
                null,
#if !OSX
                "D55BD1987676A7D6C2A04BF09C10F64F",
#endif
                "D55BD1987676A7D6C2A04BF09C10F64F"
            },

            new object[]
            {
                "FLAC Level 5 8-bit 8000Hz Stereo.flac",
                "Wave",
                null,
#if !OSX
                "818EE6CBF16F76F923D33650E7A52708",
#endif
                "818EE6CBF16F76F923D33650E7A52708"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                "Wave",
                null,
#if !OSX
                "509B83828F13945E4121E4C4897A8649",
#endif
                "509B83828F13945E4121E4C4897A8649"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                "Wave",
                null,
#if !OSX
                "5D4B869CD72BE208BC7B47F35E13BE9A",
#endif
                "5D4B869CD72BE208BC7B47F35E13BE9A"
            },

            new object[]
            {
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                "Wave",
                null,
#if !OSX
                "EFBC44B9FA9C04449D67ECD16CB7F3D8",
#endif
                "EFBC44B9FA9C04449D67ECD16CB7F3D8"
            },

            new object[]
            {
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                "Wave",
                null,
#if !OSX
                "D55BD1987676A7D6C2A04BF09C10F64F",
#endif
                "D55BD1987676A7D6C2A04BF09C10F64F"
            },

#if !LINUX
            new object[]
            {
                "ALAC 16-bit 44100Hz Mono.m4a",
                "Wave",
                null,
#if !OSX
                "509B83828F13945E4121E4C4897A8649",
#endif
                "509B83828F13945E4121E4C4897A8649"
            },

            new object[]
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                "Wave",
                null,
#if !OSX
                "5D4B869CD72BE208BC7B47F35E13BE9A",
#endif
                "5D4B869CD72BE208BC7B47F35E13BE9A"
            },

            new object[]
            {
                "ALAC 16-bit 48000Hz Stereo.m4a",
                "Wave",
                null,
#if !OSX
                "EFBC44B9FA9C04449D67ECD16CB7F3D8",
#endif
                "EFBC44B9FA9C04449D67ECD16CB7F3D8"
            },

            new object[]
            {
                "ALAC 24-bit 96000Hz Stereo.m4a",
                "Wave",
                null,
#if !OSX
                "D55BD1987676A7D6C2A04BF09C10F64F",
#endif
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
#if LINUX
                "ADF68390D58F5312FE3B01D75FE5BE57",
                "44AA2E52CED28503D02D51957B19DF74"
#elif OSX
                "44AA2E52CED28503D02D51957B19DF74"
#else
                "CB1D6A33DA49FC73DF635D5E586B3723",
                "CB1D6A33DA49FC73DF635D5E586B3723"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "FLAC",
                null,
#if LINUX
                "9A4394FF3EA062E68526EFBCC3851FB9",
                "588ACB7827AF0D1A6A18751EEFEA3604"
#elif OSX
                "588ACB7827AF0D1A6A18751EEFEA3604"
#else
                "283E4BC36FF501D0270EC0D65A1B155F",
                "283E4BC36FF501D0270EC0D65A1B155F"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                null,
#if LINUX
                "75A88A48CC2EDE69F79E4D86F3B67B11",
                "3983A342A074A7E8871FEF4FBE0AC73F"
#elif OSX
                "3983A342A074A7E8871FEF4FBE0AC73F"
#else
                "B2B05B6CA5D2637A53EE6B3DA1E2E81E",
                "B2B05B6CA5D2637A53EE6B3DA1E2E81E"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "FLAC",
                null,
#if LINUX
                "EFAA1634FCAC4C0AA6544F085DCAA315",
                "8A532C4C9D61AF027BC6F684C59FE9A6"
#elif OSX
                "8A532C4C9D61AF027BC6F684C59FE9A6"
#else
                "300CAF6DBE50F55C656479C09C80F549",
                "300CAF6DBE50F55C656479C09C80F549"
#endif
            },

            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "FLAC",
                null,
#if LINUX
                "20A6542862C62E47F75FA91CA3863F21",
                "4A4DE0494E31D82F446421C876FB10EA"
#elif OSX
                "4A4DE0494E31D82F446421C876FB10EA"
#else
                "D1D9B1C83E566E5636B4AABAE68A53E8",
                "D1D9B1C83E566E5636B4AABAE68A53E8"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "FLAC",
                null,
#if LINUX
                "AE5FE89E167550019A672C46D81B765E",
                "A6B18F2B4DDC51DC37154410E701251F"
#elif OSX
                "A6B18F2B4DDC51DC37154410E701251F"
#else
                "C542D7414270A8005180955BC067AAD2",
                "C542D7414270A8005180955BC067AAD2"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "FLAC",
                null,
#if LINUX
                "4E48D943C0D10F7B59490398AA68C305",
                "CC3E8D9A5B48AE40CB9D0EDD38D433B5"
#elif OSX
                "CC3E8D9A5B48AE40CB9D0EDD38D433B5"
#else
                "3EF5A3226AAE8B9FB8CF1071FB1F4283",
                "3EF5A3226AAE8B9FB8CF1071FB1F4283"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "FLAC",
                null,
#if LINUX
                "075CCCEB8C9263F21C95CDF5C2B9D691",
                "93016EE621E1F515BDAF0D405917DA25"
#elif OSX
                "93016EE621E1F515BDAF0D405917DA25"
#else
                "17861B26D0B7A7EAAE797A88A3DDAD09",
                "17861B26D0B7A7EAAE797A88A3DDAD09"
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
                "75A88A48CC2EDE69F79E4D86F3B67B11",
                "3983A342A074A7E8871FEF4FBE0AC73F"
#elif OSX
                "3983A342A074A7E8871FEF4FBE0AC73F"
#else
                "B2B05B6CA5D2637A53EE6B3DA1E2E81E",
                "B2B05B6CA5D2637A53EE6B3DA1E2E81E"
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
#if LINUX
                "1F6BE17A7FCDCC0D879F4A067B4CEF8B",
                "D352B276E4712ABBA3A8F1B9CA8BAB55"
#elif OSX
                "D352B276E4712ABBA3A8F1B9CA8BAB55"
#else
                "7A1DB858F2D081888F77E40DB326AC90",
                "7A1DB858F2D081888F77E40DB326AC90"
#endif
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
                "78D765C28F4817CB0189B8A1238F2C27",
                "C73F21F10850A4542EEA2435226F1DEB"
#elif OSX
                "C73F21F10850A4542EEA2435226F1DEB"
#else
                "0E1F80EC1E6DA0B214B4B48B8D8B9A47",
                "0E1F80EC1E6DA0B214B4B48B8D8B9A47"
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
                "75A88A48CC2EDE69F79E4D86F3B67B11",
                "3983A342A074A7E8871FEF4FBE0AC73F"
#elif OSX
                "3983A342A074A7E8871FEF4FBE0AC73F"
#else
                "B2B05B6CA5D2637A53EE6B3DA1E2E81E",
                "B2B05B6CA5D2637A53EE6B3DA1E2E81E"
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
                "2ED9148F4629DDBCCF43BF903E8F237E",
                "7DBB3E3E8079E60932AA5F8B4D9CD57C"
#elif OSX
                "7DBB3E3E8079E60932AA5F8B4D9CD57C"
#else
                "760DD05CEDE71C7F9B69F23010C8BC29",
                "760DD05CEDE71C7F9B69F23010C8BC29"
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
                "75A88A48CC2EDE69F79E4D86F3B67B11",
                "3983A342A074A7E8871FEF4FBE0AC73F"
#elif OSX
                "3983A342A074A7E8871FEF4FBE0AC73F"
#else
                "B2B05B6CA5D2637A53EE6B3DA1E2E81E",
                "B2B05B6CA5D2637A53EE6B3DA1E2E81E"
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
                "75A88A48CC2EDE69F79E4D86F3B67B11",
                "3983A342A074A7E8871FEF4FBE0AC73F"
#elif OSX
                "3983A342A074A7E8871FEF4FBE0AC73F"
#else
                "B2B05B6CA5D2637A53EE6B3DA1E2E81E",
                "B2B05B6CA5D2637A53EE6B3DA1E2E81E"
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
                "F303C709C209A5B6A986D7EBF0CCC07B",
                "FFB7D9F0F4CDF37EDBA799FE371424A7"
#elif OSX
                "FFB7D9F0F4CDF37EDBA799FE371424A7"
#else
                "D19405704036182A720AADD70F1C1BB0",
                "D19405704036182A720AADD70F1C1BB0"
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
                "67E4AB6AD90E9FFE867ED57E6AFCC09C",
                "F03F417B853C560705CD424AD329EFBC"
#elif OSX
                "F03F417B853C560705CD424AD329EFBC"
#else
                "C45FD63C6BAF49A37A9019063B975F69",
                "C45FD63C6BAF49A37A9019063B975F69"
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
#if OSX
                "50F7F27DBCCE5874118C3DE9B0F0306D"
#else
                "50F7F27DBCCE5874118C3DE9B0F0306D",
                "50F7F27DBCCE5874118C3DE9B0F0306D"
#endif
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
#if OSX
                "4A2E22037B18F3318920EA47BA76825C"
#else
                "4A2E22037B18F3318920EA47BA76825C",
                "4A2E22037B18F3318920EA47BA76825C"
#endif
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
#if OSX
                "C299C20C8EF4ED5B6B5664E6B81C3244"
#else
                "C299C20C8EF4ED5B6B5664E6B81C3244",
                "C299C20C8EF4ED5B6B5664E6B81C3244"
#endif
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
#if OSX
                "26442948986C55394D8AE960E66101C3"
#else
                "26442948986C55394D8AE960E66101C3",
                "26442948986C55394D8AE960E66101C3"
#endif
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
#if OSX
                "C8E2DD6861F837C845A52A4C34523C85"
#else
                "C8E2DD6861F837C845A52A4C34523C85",
                "C8E2DD6861F837C845A52A4C34523C85"
#endif
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
#if OSX
                "FAF8B7679D0B2446D83BA248CB491410"
#else
                "FAF8B7679D0B2446D83BA248CB491410",
                "FAF8B7679D0B2446D83BA248CB491410"
#endif
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
#if OSX
                "38406F719F6EF9E5F5D4E7862AA5C351"
#else
                "38406F719F6EF9E5F5D4E7862AA5C351",
                "38406F719F6EF9E5F5D4E7862AA5C351"
#endif
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
#if OSX
                "2F72E377036957C669D858AEA26DF62F"
#else
                "2F72E377036957C669D858AEA26DF62F",
                "2F72E377036957C669D858AEA26DF62F"
#endif
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
#if OSX
                "F57326FFFD308ED69B83F7F451938D55"
#else
                "F57326FFFD308ED69B83F7F451938D55",
                "F57326FFFD308ED69B83F7F451938D55"
#endif
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
#if OSX
                "4A2E22037B18F3318920EA47BA76825C"
#else
                "4A2E22037B18F3318920EA47BA76825C",
                "4A2E22037B18F3318920EA47BA76825C"
#endif
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
#if OSX
                "03305CCE91A686386908415EF35BDE0D"
#else
                "03305CCE91A686386908415EF35BDE0D",
                "03305CCE91A686386908415EF35BDE0D"
#endif
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
#if OSX
                "815E83D61745D4E117E12D31543C47BF"
#else
                "815E83D61745D4E117E12D31543C47BF",
                "815E83D61745D4E117E12D31543C47BF"
#endif
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
#if OSX
                "A4A51635A3E24D6AD0C377C918FBE7B6"
#else
                "09CD8B8C8E9D8BC09121D8C9F871F9B7",
                "D281CFECEEBE5A14D0D3D953D12F71DC"
#endif
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
#if OSX
                "336CE3C01C6F9D430E6FC947B93820F7"
#else
                "9A0F6E1984B428F236E1209C13AED4D1",
                "47189DECF29E68A40F60645F97714BE3"
#endif
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
#if OSX
                "6C9073E678B05536C0B6AE3BF98691DD"
#else
                "CB39DFBF414790022574435C2D30297D",
                "589C93B14B5A2C8EF39239949A7729FF"
#endif
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
#if OSX
                "8CA08D60F2A8FD8E9E966FB86C56246B"
#else
                "E0C34EA1479C8979D3AF3A2C98D4E699",
                "BA760ADA182CA749797AC1B978266CB1"
#endif
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
#if OSX
                "0F0823DFE0AC879E021E881F70AF5775"
#else
                "7BAD797AA7C5F71C7168C24077271029",
                "A3DC2D21D29A05456284B0B8C09E1F94"
#endif
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
#if OSX
                "7F43E990D02B3D55798A69F675362138"
#else
                "9AC3DEF9B464D0E1AB2D4F91C1A08B83",
                "DC77F4678649D575CE3E91DB950CDF55"
#endif
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
#if OSX
                "7F43E990D02B3D55798A69F675362138"
#else
                "9AC3DEF9B464D0E1AB2D4F91C1A08B83",
                "DC77F4678649D575CE3E91DB950CDF55"
#endif
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
#if OSX
                "336CE3C01C6F9D430E6FC947B93820F7"
#else
                "9A0F6E1984B428F236E1209C13AED4D1",
                "47189DECF29E68A40F60645F97714BE3"
#endif
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
#if OSX
                "924AD280AF0C8E068F2CA14211631527"
#else
                "78299761793D1A6EC79CBB9233156FD8",
                "78299761793D1A6EC79CBB9233156FD8"
#endif
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
#if OSX
                "B26CC58D8473661388019F210607C1B7"
#else
                "7EDD94F25082AEEE82B2AA87E795AB6D",
                "560039278EF9183F0FB2C47E5744E475"
#endif
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
#if OSX
                "31E4469B7D826DF20C38EC8EC80C0D16"
#else
                "0177BB1DEB19854CA8495C4CBBB25366",
                "DB44ACD7770861D4A3C6D7EE644C5E1C"
#endif
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
#if OSX
                "2321A80FDC5F36A1860523948548F8E5"
#else
                "9E77C0824474E3600F1A919715609A1B",
                "2321A80FDC5F36A1860523948548F8E5"
#endif
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
#if OSX
                "E36539ED8969070A53809CECCD0B422A"
#else
                "EBD496E30A953A8D0FE11C2609EFABC3",
                "A67A5F8D1A55CD2A29EEFA54E583AEA1"
#endif
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
#if OSX
                "9116562A92B3F1FA983A180972CE670E"
#else
                "DE5F94EC1EACB75A3D049AE9960A7ACB",
                "8F6858F8F86AA821789D926E0B4F63B6"
#endif
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
#if OSX
                "503F9957F1FE77F23189507AFC641297"
#else
                "B26C14FD53A4027C26FA3A57CB96AF4C",
                "EEEAF1FB2801EF0FB49B9B87350B5587"
#endif
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
#if OSX
                "503F9957F1FE77F23189507AFC641297"
#else
                "B26C14FD53A4027C26FA3A57CB96AF4C",
                "EEEAF1FB2801EF0FB49B9B87350B5587"
#endif
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
#if OSX
                "31FCA720DAD2B27CCFD20D52F8E32B2D"
#else
                "B65D496ADABF3DBCDB24136A9655C295",
                "6AD4BD76918C74B976FD7774163CD7ED"
#endif
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
#if OSX
                "1D7F6BDC889271BEA523A07DD29BDD37"
#else
                "365D7E965534C8690B4694B27D0CF1C9",
                "4E99289AFF43EA387442E30EAFB7305A"
#endif
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
#if OSX
                "336CE3C01C6F9D430E6FC947B93820F7"
#else
                "9A0F6E1984B428F236E1209C13AED4D1",
                "47189DECF29E68A40F60645F97714BE3"
#endif
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
#if OSX
                "6F97B672D446B19B9370EE3FF4A5DCB5"
#else
                "DDA8DBB070EA36F77455A41A2628B6AA",
                "14145EA9D279E2FA457AD85F19DC0896"
#endif
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
#if OSX
                "FD0ADBEFE09CE73D7A71768366A029A4"
#else
                "5502D724D98AA24FE49FA8AFB0FC63A6",
                "90D1426E435372B957E6558E4DC5D7FD"
#endif
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
                "362C1DC415B6ED27B3BB0C43EEC7614A",
                "F2BD0875E273743A8908F96DCCFDFC44"
#elif OSX
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
                "3FBE256A1ABC2C530FAAA632929F9AC2",
                "1CB5B915B3A72CBE76087E16F96A0A3E"
#elif OSX
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
                "B46B30D5E331FE64F24C620CFD5C717D",
                "10E44CEE38E66E9737677BE52E7A286D"
#elif OSX
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
                "F2FFFC7C7001A93C0CC19466D51FC597",
                "1454732B48913F2A3898164BA366DA01"
#elif OSX
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
                "BFF278600EB87215603D727BAA7905BD",
                "AD56C3A1ACD627DBDA4B5A28AFE0355D"
#elif OSX
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
                "EED1BD58A65E3CA92EEC176F156DEF24",
                "32EEC2B69A048975FB3BD034E8B392A4"
#elif OSX
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
                "E73399A30F5497FD72E1AEE0A3E76EAF",
                "FB1B7DECB2C2A2C9CAA1FBB917A81472"
#elif OSX
                "FB1B7DECB2C2A2C9CAA1FBB917A81472"
#else
                "8E61943EEA4008E8921618B76FB4C870",
                "8E61943EEA4008E8921618B76FB4C870"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "LameMP3",
                null,
#if LINUX
                "E73399A30F5497FD72E1AEE0A3E76EAF",
                "FB1B7DECB2C2A2C9CAA1FBB917A81472"
#elif OSX
                "FB1B7DECB2C2A2C9CAA1FBB917A81472"
#else
                "8E61943EEA4008E8921618B76FB4C870",
                "8E61943EEA4008E8921618B76FB4C870"
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
                "EED1BD58A65E3CA92EEC176F156DEF24",
                "32EEC2B69A048975FB3BD034E8B392A4"
#elif OSX
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
                "8EEAF387756C09E867E45CAEF8649C53",
                "24F1B744BDA5C6A94B9FE6136DEC4614"
#elif OSX
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
                "EED1BD58A65E3CA92EEC176F156DEF24",
                "32EEC2B69A048975FB3BD034E8B392A4"
#elif OSX
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
                "A7C95E4A6B3071F8C9498D4A705E2685",
                "1B5C266B6D799765BA1272BE28A7C435"
#elif OSX
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
                "EED1BD58A65E3CA92EEC176F156DEF24",
                "32EEC2B69A048975FB3BD034E8B392A4"
#elif OSX
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
                "2773A9EAC3A6E07FA1C71DD5FC730267",
                "5737ED221E55314FD5B9FA167C1C1651"
#elif OSX
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
                "D05EA326944389E530612EAF314BB588",
                "D270226F14E850B208624CD2AC59512D"
#elif OSX
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
                "B46B30D5E331FE64F24C620CFD5C717D",
                "10E44CEE38E66E9737677BE52E7A286D"
#elif OSX
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
                "B46B30D5E331FE64F24C620CFD5C717D",
                "10E44CEE38E66E9737677BE52E7A286D"
#elif OSX
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
                "B46B30D5E331FE64F24C620CFD5C717D",
                "10E44CEE38E66E9737677BE52E7A286D"
#elif OSX
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
                "B46B30D5E331FE64F24C620CFD5C717D",
                "10E44CEE38E66E9737677BE52E7A286D"
#elif OSX
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
                "472A428EDB8AD18369EEC4F748F80A59",
                "65D418A236D86A8CE33E07A76C98DF08"
#elif OSX
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
#if LINUX
                "E539FECE8D80128F11BA73148B92209F",
                "5DE234656056DFDAAD30E4DA9FD26366"
#elif OSX
                "5DE234656056DFDAAD30E4DA9FD26366"
#else
                "5DE234656056DFDAAD30E4DA9FD26366",
                "5DE234656056DFDAAD30E4DA9FD26366"
#endif
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
#if LINUX
                "30520C93D354F17F637671B99BE75083",
                "2BBC83E74AB1A4EB150BC6E1EB9920B5"
#elif OSX
                "2BBC83E74AB1A4EB150BC6E1EB9920B5"
#else
                "2BBC83E74AB1A4EB150BC6E1EB9920B5",
                "2BBC83E74AB1A4EB150BC6E1EB9920B5"
#endif
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
#if LINUX
                "464C9EE0662B447224805438FAC9D10E",
                "BEB5029A08011BCEDFFA99173B763E7F"
#elif OSX
                "BEB5029A08011BCEDFFA99173B763E7F"
#else
                "BEB5029A08011BCEDFFA99173B763E7F",
                "BEB5029A08011BCEDFFA99173B763E7F"
#endif
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
#if LINUX
                "9CD9C9FCE1E42F79EDBAA84BA1B8D8C4",
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
#elif OSX
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
#else
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6",
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
#endif
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
#if LINUX
                "9CD9C9FCE1E42F79EDBAA84BA1B8D8C4",
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
#elif OSX
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
#else
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6",
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
#endif
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
#if LINUX
                "5BE352395ADE20729E8F68977C3043C7",
                "EACCA2FD6404ACA1AB46027FAE6A667B"
#elif OSX
                "EACCA2FD6404ACA1AB46027FAE6A667B"
#else
                "EACCA2FD6404ACA1AB46027FAE6A667B",
                "EACCA2FD6404ACA1AB46027FAE6A667B"
#endif
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
                "B46B30D5E331FE64F24C620CFD5C717D",
                "10E44CEE38E66E9737677BE52E7A286D"
#elif OSX
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
#if LINUX
                "9CD9C9FCE1E42F79EDBAA84BA1B8D8C4",
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
#elif OSX
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
#else
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6",
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
#endif
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
                "B46B30D5E331FE64F24C620CFD5C717D",
                "10E44CEE38E66E9737677BE52E7A286D"
#elif OSX
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
                "C6F6B42CCBC2435DC612F643A76E46FC",
                "8EFA63733A5527E08092CAD86E86F76E"
#elif OSX
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
                "F1C20FE496BD523FFC1088A44091D84F",
                "FB96F0590FB1A603C847DEF075D8FC5F"
#elif OSX
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
                "169D4509DF879ADC61BBACA4C3CEDF49",
                "169D4509DF879ADC61BBACA4C3CEDF49"
#elif OSX
                "C4CB008A88BB92E54CFCF5EB8CC6E6A4"
#else
                "48FB0655BD5730B8DE83A69CE6606989",
                "48FB0655BD5730B8DE83A69CE6606989"
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
                "2022FC0C77829D1CE73D57912FE7929E",
                "2022FC0C77829D1CE73D57912FE7929E"
#elif OSX
                "CB5B49E015E1C1C8BD09459E860BBF4E"
#else
                "01D92AFE644FCA99676BDB7F2719384D",
                "01D92AFE644FCA99676BDB7F2719384D"
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
                "62D12380155023C53AD1C679524B73EB",
                "62D12380155023C53AD1C679524B73EB"
#elif OSX
                "CDC01A83DE71D5A96FD39991C8C2885B"
#else
                "DA4FD2B1DF8E97DACEB0818C3C5D4A61",
                "DA4FD2B1DF8E97DACEB0818C3C5D4A61"
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
                "73C7A548A304C9A6668E9BADD05EF3D8",
                "73C7A548A304C9A6668E9BADD05EF3D8"
#elif OSX
                "0FB3B4487F7D0A107FFF847C92520D8E"
#else
                "A9472CF99A5029F29C3E3440B2A725FE",
                "A9472CF99A5029F29C3E3440B2A725FE"
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
                "EC24791D57666C738DF852B537A7E93F",
                "EC24791D57666C738DF852B537A7E93F"
#elif OSX
                "37FA56FB113CC847DD3054FD87B17D82"
#else
                "5C7139C872A467371512CFDF4CA2801F",
                "5C7139C872A467371512CFDF4CA2801F"
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
                "FDAB2DD681F09A84236ECDF050B3086B",
                "FDAB2DD681F09A84236ECDF050B3086B"
#elif OSX
                "860F43037C8ECDC02233CDC1844601D3"
#else
                "45B8F1212F569A3715646040BB1CADA9",
                "45B8F1212F569A3715646040BB1CADA9"
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
                "AF6DC6FC3EB19867B56B0FB55C4D7410",
                "AF6DC6FC3EB19867B56B0FB55C4D7410"
#elif OSX
                "8D77247996A63F24623793E9A8CD9427"
#else
                "3ABCC84E2DA559211D5CB8C4D5807810",
                "3ABCC84E2DA559211D5CB8C4D5807810"
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
                "AF6DC6FC3EB19867B56B0FB55C4D7410",
                "AF6DC6FC3EB19867B56B0FB55C4D7410"
#elif OSX
                "8D77247996A63F24623793E9A8CD9427"
#else
                "3ABCC84E2DA559211D5CB8C4D5807810",
                "3ABCC84E2DA559211D5CB8C4D5807810"
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
                "1EF6D099FFC64B6E2B54C87F0BFFE948",
                "1EF6D099FFC64B6E2B54C87F0BFFE948"
#elif OSX
                "AD6CC65EA1BF0DE4A66DA6026D09C7E7"
#else
                "C06D2E290A5AF84194267144414AD2FB",
                "C06D2E290A5AF84194267144414AD2FB"
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
                "50C94F4625B7E1B168212A564D3FCEE6",
                "50C94F4625B7E1B168212A564D3FCEE6"
#elif OSX
                "0BF600C8D2D7AF3BFA6ACD4055B2F1FB"
#else
                "0E53AD3D8B3B9139D50F04AE438F9D5E",
                "0E53AD3D8B3B9139D50F04AE438F9D5E"
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
                "62D12380155023C53AD1C679524B73EB",
                "62D12380155023C53AD1C679524B73EB"
#elif OSX
                "CDC01A83DE71D5A96FD39991C8C2885B"
#else
                "DA4FD2B1DF8E97DACEB0818C3C5D4A61",
                "DA4FD2B1DF8E97DACEB0818C3C5D4A61"
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
                "06BB339324CC08B4F9DB9AB549382313",
                "06BB339324CC08B4F9DB9AB549382313"
#elif OSX
                "17F6ED99258BC832478E7CAA273108BC"
#else
                "7D05638316212F739F6BF15ED894AF24",
                "7D05638316212F739F6BF15ED894AF24"
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
                "FBD64FFF01984F2860E72F97841D4E45",
                "FBD64FFF01984F2860E72F97841D4E45"
#elif OSX
                "5C5BF2D883D82E8673217524A66AFEF1"
#else
                "D46EE71C3FF5B1C61D016972BE9BBC46",
                "D46EE71C3FF5B1C61D016972BE9BBC46"
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
                "F403579CAB6AFD7FE440A96D2051BA57",
                "F403579CAB6AFD7FE440A96D2051BA57"
#elif OSX
                "4C98EB3D79436A6B768D1E9634088681"
#else
                "FB561924D6828C62E1BC4A26245B5364",
                "FB561924D6828C62E1BC4A26245B5364"
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
                "75FEA74C38068EC55547F91B7B921951",
                "75FEA74C38068EC55547F91B7B921951"
#elif OSX
                "CB77612ABAD41C28D894B70831BA25E3"
#else
                "52D18D24F46C4D647510012AB0ECD2D4",
                "52D18D24F46C4D647510012AB0ECD2D4"
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
                "EFB37C1BF555256EF774A346D8A7E726",
                "EFB37C1BF555256EF774A346D8A7E726"
#elif OSX
                "582862413672D5290AC69019BEC4E576"
#else
                "B0B8AAE55E06B1026CE5A854FE2A12BC",
                "B0B8AAE55E06B1026CE5A854FE2A12BC"
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
                "EFB37C1BF555256EF774A346D8A7E726",
                "EFB37C1BF555256EF774A346D8A7E726"
#elif OSX
                "582862413672D5290AC69019BEC4E576"
#else
                "B0B8AAE55E06B1026CE5A854FE2A12BC",
                "B0B8AAE55E06B1026CE5A854FE2A12BC"
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
                "76519119096606316467F962AAF6DDD8",
                "76519119096606316467F962AAF6DDD8"
#elif OSX
                "B1598421FF7FCE6C8D69CB8DE68EBD1A"
#else
                "507FC9D4D061124B1D0597B530039F29",
                "507FC9D4D061124B1D0597B530039F29"
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
                "62D12380155023C53AD1C679524B73EB",
                "62D12380155023C53AD1C679524B73EB"
#elif OSX
                "CDC01A83DE71D5A96FD39991C8C2885B"
#else
                "DA4FD2B1DF8E97DACEB0818C3C5D4A61",
                "DA4FD2B1DF8E97DACEB0818C3C5D4A61"
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
                "EFB37C1BF555256EF774A346D8A7E726",
                "EFB37C1BF555256EF774A346D8A7E726"
#elif OSX
                "582862413672D5290AC69019BEC4E576"
#else
                "B0B8AAE55E06B1026CE5A854FE2A12BC",
                "B0B8AAE55E06B1026CE5A854FE2A12BC"
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
                "62D12380155023C53AD1C679524B73EB",
                "62D12380155023C53AD1C679524B73EB"
#elif OSX
                "CDC01A83DE71D5A96FD39991C8C2885B"
#else
                "DA4FD2B1DF8E97DACEB0818C3C5D4A61",
                "DA4FD2B1DF8E97DACEB0818C3C5D4A61"
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
                "7508A275052C3AD7057208132BE5F233",
                "7508A275052C3AD7057208132BE5F233"
#elif OSX
                "8C54CD37E0EF253C482EB0E486ACCDB3"
#else
                "80D0C8F78A4575912410660568BBA99D",
                "80D0C8F78A4575912410660568BBA99D"
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
                "1313B8C8D0EE8933D5B518D6F3E933A2",
                "1313B8C8D0EE8933D5B518D6F3E933A2"
#elif OSX
                "B91F6ED893D23D6F27C693F2F844D56C"
#else
                "3AF8982E501F01E50A502704324CD270",
                "3AF8982E501F01E50A502704324CD270"
#endif
            },

            #endregion

            #region Opus Encoding

            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "D085A7142C47B6898B3CCB6C5EDCBA55",
                "D085A7142C47B6898B3CCB6C5EDCBA55"
#elif OSX
                "110D3A7864F59FD6C7C03421E9A8A9C3"
#else
                "8BB7F2763D133BD534AE9C20104AFA2E",
                "05B065E87CE35617089DC8A86ED5AD19"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "46B412FA052C97474B60DDDCF2CA8052",
                "46B412FA052C97474B60DDDCF2CA8052"
#elif OSX
                "88BBD2418DFF710EFDDEDE2EDFC41FA3"
#else
                "BE719CEBED7270CD6DC580007E20D5F2",
                "C8C92433A8093BB8C7AE3AE6F84653B3"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "94AADBE44ECF34AF98982F17BCCE7C97",
                "94AADBE44ECF34AF98982F17BCCE7C97"
#elif OSX
                "0130D94C0E77473D536579E7E8FAB95E"
#else
                "3CAB512976D0D62B45E2C9239B19735D",
                "F82CC6FE2C86F5E7CD4A4F8F634E2DE9"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "C9024DED60E2039752C7B372DFFC7149",
                "C9024DED60E2039752C7B372DFFC7149"
#elif OSX
                "281726A47418C362530F5982AFD6E873"
#else
                "3C5EF551E97AEE3A916F1B39BCD5D3C2",
                "BD5F090F921BCC80F05FCBF5725D8E0E"
#endif
            },

            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "22AA639A1AA309F9C343F8D8EBDB1B6D",
                "22AA639A1AA309F9C343F8D8EBDB1B6D"
#elif OSX
                "FDDB125D6D018A13213D90A81354B932"
#else
                "9E31F9F58A41EA377740E7AC43E57939",
                "D0FC8A33EE164AC29D79435A4ED779B1"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "1B7298B4EA52190612430642D2CD0694",
                "1B7298B4EA52190612430642D2CD0694"
#elif OSX
                "E9C6468C1ED6CB1BF1A87F63C1F46990"
#else
                "ED5EA746CA5182DE57E0D25695D17F4E",
                "E59BFB7E77545D36723E7B8EB5AF1B48"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "476D30594189602BF6E4755E990D0615",
                "476D30594189602BF6E4755E990D0615"
#elif OSX
                "4BAF73AAD856D60891428C923B424C22"
#else
                "17D464F4D94C17DD99AC0017BAB9228F",
                "B52AE38B2D30A7ADE63492CCB2F000EC"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
#if LINUX
                "476D30594189602BF6E4755E990D0615",
                "476D30594189602BF6E4755E990D0615"
#elif OSX
                "4BAF73AAD856D60891428C923B424C22"
#else
                "17D464F4D94C17DD99AC0017BAB9228F",
                "B52AE38B2D30A7ADE63492CCB2F000EC"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Minimum serial #
                    ["SerialNumber"] = int.MinValue
                },
#if LINUX
                "DE6D30BCAC9DA6340DA28542E75C5E01",
                "DE6D30BCAC9DA6340DA28542E75C5E01"
#elif OSX
                "DD9F06CBF786584277CBC8CAF2FE081E"
#else
                "0E458849DBB876C84D23FA8B1AE58E7D",
                "3804E9A98DDBD216A3AED6F638C5644E"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Maximum serial #
                    ["SerialNumber"] = int.MaxValue
                },
#if LINUX
                "D0DB0EE3ABC3950E3009663461F631DB",
                "D0DB0EE3ABC3950E3009663461F631DB"
#elif OSX
                "5395834138A28EB01905B8BB1BB82728"
#else
                "049CE3FC2614A25BDE9E311CCEC4E995",
                "5480E19A91B0DC0891094B43168FA839"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Minimum bit rate
                    ["BitRate"] = 5,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "7556A3E6FF92CEF2D0ECD5654322DEAB",
                "7556A3E6FF92CEF2D0ECD5654322DEAB"
#elif OSX
                "133CB3DC2A123817FF98113C6D15274A"
#else
                "FC89452B5F0EA49E3D738CE67FBF8B1C",
                "D345EE9B84E231E3ACB17C7AC0972154"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Maximum bit rate
                    ["BitRate"] = 512,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "C031F42E8AF2D482E302264E132502E1",
                "C031F42E8AF2D482E302264E132502E1"
#elif OSX
                "81C9FDC8D8272F3E113987D0FC045764"
#else
                "6B422669A0FCB242E0E15204F5FDCC47",
                "4A9455EB95B3C9CBCB2538D0C888267D"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Variable control mode (default, explicit)
                    ["ControlMode"] = "Variable",
                    ["SerialNumber"] = 1
                },
#if LINUX
                "94AADBE44ECF34AF98982F17BCCE7C97",
                "94AADBE44ECF34AF98982F17BCCE7C97"
#elif OSX
                "0130D94C0E77473D536579E7E8FAB95E"
#else
                "3CAB512976D0D62B45E2C9239B19735D",
                "F82CC6FE2C86F5E7CD4A4F8F634E2DE9"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Constrained VBR mode
                    ["ControlMode"] = "Constrained",
                    ["SerialNumber"] = 1
                },
#if LINUX
                "940955E239CF3D26B417F6B532EBC726",
                "940955E239CF3D26B417F6B532EBC726"
#elif OSX
                "DEBF5CBE7D61DFE8A5698BD416D8EE99"
#else
                "38E90E75D928371AB9C1CFF243C731B4",
                "E0219613A0740FACAAC162D7B9FD0517"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // CBR mode
                    ["ControlMode"] = "Constant",
                    ["SerialNumber"] = 1
                },
#if LINUX
                "9533FF6E154EE45B8ED522C3B5A25865",
                "9533FF6E154EE45B8ED522C3B5A25865"
#elif OSX
                "70F280880A48118B2094B97DA0B6E77F"
#else
                "0A4FC1F40FD76797222FC8CACCA83AD9",
                "F3CF4988FB774A297832863156B8ED8D"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Low bit rate, Music signal type (default)
                    ["BitRate"] = 32,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "201BD7EF36658DB3DC9658C67E9AF71E",
                "201BD7EF36658DB3DC9658C67E9AF71E"
#elif OSX
                "2D3346BD8CB70FABA865771A3BE8D644"
#else
                "8FC7BCF02EDB42E9785797FD2C9A71D6",
                "8FC7BCF02EDB42E9785797FD2C9A71D6"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Low bit rate, Music signal type (explicit)
                    ["SignalType"] = "Music",
                    ["BitRate"] = 32,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "201BD7EF36658DB3DC9658C67E9AF71E",
                "201BD7EF36658DB3DC9658C67E9AF71E"
#elif OSX
                "2D3346BD8CB70FABA865771A3BE8D644"
#else
                "8FC7BCF02EDB42E9785797FD2C9A71D6",
                "8FC7BCF02EDB42E9785797FD2C9A71D6"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Low bit rate, Speech signal type
                    ["SignalType"] = "Speech",
                    ["BitRate"] = 32,
                    ["SerialNumber"] = 1
                },
#if LINUX
                "8AA73947B00AABCB3602F660E69263C5",
                "8AA73947B00AABCB3602F660E69263C5"
#elif OSX
                "0041E1CC1A4C05537A05AD209E07D604"
#else
                "F7583CA097F3B8D7EE50587DB0C9B883",
                "0C29F2F289C7817FC177201ECCA21BC9"
#endif
            },

            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
#if LINUX
                "94AADBE44ECF34AF98982F17BCCE7C97",
                "94AADBE44ECF34AF98982F17BCCE7C97"
#elif OSX
                "0130D94C0E77473D536579E7E8FAB95E"
#else
                "3CAB512976D0D62B45E2C9239B19735D",
                "F82CC6FE2C86F5E7CD4A4F8F634E2DE9"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new TestSettingDictionary
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
#if LINUX
                "945B2B47707ED2C2DC2D30BD4AF75A34",
                "945B2B47707ED2C2DC2D30BD4AF75A34"
#elif OSX
                "959B3FC639ED4AF783652B9735FF78D1"
#else
                "2B5AD4B688767C703DC63A83C3F96074",
                "AC629D5D7F3B004E75BE9F85D4B1DBD3"
#endif
            },

            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new TestSettingDictionary
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album",
                    ["SerialNumber"] = 1
                },
#if LINUX
                "2D5A8223552F6A731413BD1DD181040A",
                "2D5A8223552F6A731413BD1DD181040A"
#elif OSX
                "91B625446C9F0998BFF40C3AA4FEC24A"
#else
                "509504BD68BD34AC79FECEA9073BAFB3",
                "45911349F86066B37C0DBC7D6AC43890"
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