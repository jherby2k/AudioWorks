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
using System.Linq;
using AudioWorks.Api.Tests.DataTypes;
using Xunit;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class EncodeValidFileDataSource
    {
        static readonly TheoryData<string, string, TestSettingDictionary, string[]> _data = new()
        {
            #region Wave Encoding

            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "818EE6CBF16F76F923D33650E7A52708"
                }
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "509B83828F13945E4121E4C4897A8649"
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "5D4B869CD72BE208BC7B47F35E13BE9A"
                }
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "EFBC44B9FA9C04449D67ECD16CB7F3D8"
                }
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "D55BD1987676A7D6C2A04BF09C10F64F"
                }
            },
            {
                "A-law 44100Hz Stereo.wav",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "47B031C7D8246F557E0CB37E4DB3F528"
                }
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "FCA4C53C5F98B061CE9C9186A303D816"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "509B83828F13945E4121E4C4897A8649"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "5D4B869CD72BE208BC7B47F35E13BE9A"
                }
            },
            {
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "EFBC44B9FA9C04449D67ECD16CB7F3D8"
                }
            },
            {
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "D55BD1987676A7D6C2A04BF09C10F64F"
                }
            },

#if !LINUX
            {
                "ALAC 16-bit 44100Hz Mono.m4a",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "509B83828F13945E4121E4C4897A8649"
                }
            },
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "5D4B869CD72BE208BC7B47F35E13BE9A"
                }
            },
            {
                "ALAC 16-bit 48000Hz Stereo.m4a",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "EFBC44B9FA9C04449D67ECD16CB7F3D8"
                }
            },
            {
                "ALAC 24-bit 96000Hz Stereo.m4a",
                "Wave",
                new TestSettingDictionary(),
                new[]
                {
                    "D55BD1987676A7D6C2A04BF09C10F64F"
                }
            },
#endif

            #endregion

            #region FLAC Encoding

            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary(),
                new[]
                {
                    "44AA2E52CED28503D02D51957B19DF74", // FLAC 1.3.2 (Ubuntu 18.04)
                    "42070347011D5067A9D962DA3237EF63", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "BB7D8149E9AC3C96C79996C2CE7BFB78" // FLAC 1.4.1
                }
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "FLAC",
                new TestSettingDictionary(),
                new[]
                {
                    "588ACB7827AF0D1A6A18751EEFEA3604", // FLAC 1.3.2 (Ubuntu 18.04)
                    "0771EF09959F087FACE194A4479F5107", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "CD9B7C00E3EFF10763F51414E1630AD5" // FLAC 1.4.1
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary(),
                new[]
                {
                    "3983A342A074A7E8871FEF4FBE0AC73F", // FLAC 1.3.2 (Ubuntu 18.04)
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "6C23634B915BA1E7FEA6C5062D06A201" // FLAC 1.4.1
                }
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary(),
                new[]
                {
                    "8A532C4C9D61AF027BC6F684C59FE9A6", // FLAC 1.3.2 (Ubuntu 18.04)
                    "F0F075E05A3AFB67403CCF373932BCCA", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "9745D84285DFDFA68BD8D3362784581E" // FLAC 1.4.1
                }
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary(),
                new[]
                {
                    "4A4DE0494E31D82F446421C876FB10EA", // FLAC 1.3.2 (Ubuntu 18.04)
                    "D3A7B834DCE97F0709AEFCA45A24F5B6", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "488D7D6E95DD334D25C3C7B8B3E9C7A8" // FLAC 1.4.1
                }
            },
            {
                "A-law 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary(),
                new[]
                {
                    "A34A2A6CD402C98AB46C4BF4CE901A8E", // FLAC 1.3.2 (Ubuntu 18.04)
                    "10F6AC75659ECFE81D3C07D8D3074538", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "A7716A5C7639D750E2739E3855C60A9B" // FLAC 1.4.1
                }
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary(),
                new[]
                {
                    "B4E17B8DD9733AAB81EC9FA27D4F0D81", // FLAC 1.3.2 (Ubuntu 18.04)
                    "FCBAB8A8C261456CF6F87E603B237426", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "78128DB57EA3FD0E7387161F7C811086" // FLAC 1.4.1
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "FLAC",
                new TestSettingDictionary(),
                new[]
                {
                    "A6B18F2B4DDC51DC37154410E701251F", // FLAC 1.3.2 (Ubuntu 18.04)
                    "2F2F341FEECB7842F7FA9CE6CB110C67", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "E2DD7730726FE5ED62DB1295D74ADC88" // FLAC 1.4.1
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "FLAC",
                new TestSettingDictionary(),
                new[]
                {
                    "CC3E8D9A5B48AE40CB9D0EDD38D433B5", // FLAC 1.3.2 (Ubuntu 18.04)
                    "A48820F5E30B5C21A881E01209257E21", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "CAF95E65F1C131A065E5351B7B2EB5CD" // FLAC 1.4.1
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "FLAC",
                new TestSettingDictionary(),
                new[]
                {
                    "93016EE621E1F515BDAF0D405917DA25", // FLAC 1.3.2 (Ubuntu 18.04)
                    "D90693A520FA14AC987272ACB6CD8996", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "850E0ADBC12F3682209D5A4BA1C3DAE2" // FLAC 1.4.1
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Default compression
                    ["CompressionLevel"] = 5
                },
                new[]
                {
                    "3983A342A074A7E8871FEF4FBE0AC73F", // FLAC 1.3.2 (Ubuntu 18.04)
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "6C23634B915BA1E7FEA6C5062D06A201" // FLAC 1.4.1
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Minimum compression
                    ["CompressionLevel"] = 0
                },
                new[]
                {
                    "D352B276E4712ABBA3A8F1B9CA8BAB55", // FLAC 1.3.2 (Ubuntu 18.04)
                    "A58022B124B427771041A96F65D8DF21", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "1C0C702068210D78CC9391A938097F96" // FLAC 1.4.1
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Maximum compression
                    ["CompressionLevel"] = 8
                },
                new[]
                {
                    "C73F21F10850A4542EEA2435226F1DEB", // FLAC 1.3.2 (Ubuntu 18.04)
                    "F341E56E68A0A168B779A4EBFD41422D", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "F33C025E3D6E116750A574FFFD907C65" // FLAC 1.4.1
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Default seek point interval
                    ["SeekPointInterval"] = 10
                },
                new[]
                {
                    "3983A342A074A7E8871FEF4FBE0AC73F", // FLAC 1.3.2 (Ubuntu 18.04)
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "6C23634B915BA1E7FEA6C5062D06A201" // FLAC 1.4.1
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Disabled seek points
                    ["SeekPointInterval"] = 0
                },
                new[]
                {
                    "7DBB3E3E8079E60932AA5F8B4D9CD57C", // FLAC 1.3.2 (Ubuntu 18.04)
                    "986464F3AC48E00D00B8ECF3AF3FD6BC", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "8B399070A685ADC38EA67F9C3B5D6F21" // FLAC 1.4.1
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Maximum seek point interval
                    ["SeekPointInterval"] = 600
                },
                new[]
                {
                    "3983A342A074A7E8871FEF4FBE0AC73F", // FLAC 1.3.2 (Ubuntu 18.04)
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "6C23634B915BA1E7FEA6C5062D06A201" // FLAC 1.4.1
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Default padding
                    ["Padding"] = 8192
                },
                new[]
                {
                    "3983A342A074A7E8871FEF4FBE0AC73F", // FLAC 1.3.2 (Ubuntu 18.04)
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "6C23634B915BA1E7FEA6C5062D06A201" // FLAC 1.4.1
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Disabled padding
                    ["Padding"] = 0
                },
                new[]
                {
                    "FFB7D9F0F4CDF37EDBA799FE371424A7", // FLAC 1.3.2 (Ubuntu 18.04)
                    "662592BD8B3853B6FEC4E188F7D0F246", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "3DB90E9E679EE1D72C36175E45E63FC9" // FLAC 1.4.1
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    // Maximum padding
                    ["Padding"] = 16_775_369
                },
                new[]
                {
                    "F03F417B853C560705CD424AD329EFBC", // FLAC 1.3.2 (Ubuntu 18.04)
                    "455753A51355171BF22CCC78647235B4", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "4DF51A1E2BD691F901F4446FF22BF029" // FLAC 1.4.1
                }
            },

            #endregion

#if !LINUX

            #region ALAC Encoding

            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "24C88B615C59F3054FF0A44C677987FA", // MacOS 11
                    "50F7F27DBCCE5874118C3DE9B0F0306D"
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "04244493BA087CD69BAD989927BD1595", // MacOS 11
                    "4A2E22037B18F3318920EA47BA76825C"
                }
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "8522CA9ADFA8E200FDF5936AEF62EA43", // MacOS 11
                    "C299C20C8EF4ED5B6B5664E6B81C3244"
                }
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "1BB62FEE2C66905CFBB6FEC048BF9772", // MacOS 11
                    "26442948986C55394D8AE960E66101C3"
                }
            },
            {
                "A-law 44100Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "B55FDF0750DE71AA26781CA565222D05", // MacOS 11
                    "40626FB4389C8CF567C0BF74621036BD"
                }
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "8D7D0F67E43BECA4B9EE8AC0D552C01F", // MacOS 11
                    "26B6F5A519F858AB695C5090E7B98451"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "C88E17FEC4F9AE6C0F8ED21E423E60D3", // MacOS 11
                    "C8E2DD6861F837C845A52A4C34523C85"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "826418694704C310B1FFFDE1D1874839", // MacOS 11
                    "FAF8B7679D0B2446D83BA248CB491410"
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "ALAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "E170FAAB89D07557FC15F472715168A0", // MacOS 11
                    "38406F719F6EF9E5F5D4E7862AA5C351"
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    // Different creation time
                    ["CreationTime"] = new DateTime(2016, 12, 1),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "B9F0B2D3BCB6FD612829084D8C42C2AA", // MacOS 11
                    "2F72E377036957C669D858AEA26DF62F"
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new TestSettingDictionary
                {
                    // Different modification time
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2018, 12, 1)
                },
                new[]
                {
                    "EF39C30FA5D1106F655DC55806D8CB44", // MacOS 11
                    "F57326FFFD308ED69B83F7F451938D55"
                }
            },
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
                new[]
                {
                    "04244493BA087CD69BAD989927BD1595", // MacOS 11
                    "4A2E22037B18F3318920EA47BA76825C"
                }
            },
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
                new[]
                {
                    "771DAECD27700570C845116672E7DACC", // MacOS 11
                    "03305CCE91A686386908415EF35BDE0D"
                }
            },
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
                new[]
                {
                    "DF3747EF579BFC71DC15CC0399E8F347", // MacOS 11
                    "815E83D61745D4E117E12D31543C47BF"
                }
            },

            #endregion

            #region Apple AAC Encoding

            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "F62EB3988683FAFF83599C61A18E4BB5", // MacOS 10.15
                    "1E20EEB7DE9491EFC9B946EE9947A437", // MacOS 11 on Intel
                    "7E21540C472F59D17BB67D400BFD7991", // MacOS 11 on ARM
                    "09CD8B8C8E9D8BC09121D8C9F871F9B7", // 32-bit Windows on Intel
                    "CF5AD69DADDCCE22612CD6FA8FB21897", // 32-bit Windows on AMD
                    "75D127D9FCD7720CBE92C0670A93A880", // 64-bit Windows on Intel
                    "F1E4C1E4386B0DD4B233284E4E2363D8" // 64-bit Windows on AMD
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "EAD8D0B06C252C21C49920DCF360CAAF", // MacOS 10.15
                    "A1FE40A8416A31D87160E49797AAF469", // MacOS 11 on Intel
                    "439F73E38831F4A10B5BD4F91F732A15", // MacOS 11 on ARM
                    "9A0F6E1984B428F236E1209C13AED4D1", // 32-bit Windows on Intel
                    "7FABBF9DDF1A16701E57C6DD190485E0", // 32-bit Windows on AMD
                    "1D0F379EC9C47267569F88729569D407", // 64-bit Windows on Intel
                    "C40C18B162E7429F60C62DF6A23071D6" // 64-bit Windows on AMD
                }
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "F1DD4F4527CCD1F347EEE1008EA2CC92", // MacOS 10.15
                    "16D9FC9ACF0A6F1CE04BACE0368C297C", // MacOS 11 on Intel
                    "13BB443AE1F220DF897615F96EF0A0D2", // MacOS 11 on ARM
                    "CB39DFBF414790022574435C2D30297D", // 32-bit Windows on Intel
                    "EB0DA4A098888A34C2F77A2A65D2E337", // 32-bit Windows on AMD
                    "E0A80A6B32CD5A8FA5C62B44F28C4A87", // 64-bit Windows on Intel
                    "CA58A161F472C9038E29DB08BC8B41CC" // 64-bit Windows on AMD
                }
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "D8CFD4140F419E15B4227A3E079F0A8D", // MacOS 10.15
                    "9EEEFD7C9BD1965C842964F88243E361", // MacOS 11 on Intel
                    "88908D8E665C16C5B6762CD1B14F8B7C", // MacOS 11 on ARM
                    "E0C34EA1479C8979D3AF3A2C98D4E699", // 32-bit Windows on Intel
                    "06469BD31CF3F0B799D9E52BBEA00C72", // 32-bit Windows on AMD
                    "ED307F76DD052720321284BAD8876AB2", // 64-bit Windows on Intel
                    "155983F671E0ACD36DC482A283A8EDBE" // 64-bit Windows on AMD
                }
            },
            {
                "A-law 44100Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "E75BB5AB85B9091CF800762611718B5E", // MacOS 10.15
                    "DF9FAF5172A3A2B98A40A45AE96AE020", // MacOS 11 on Intel
                    "18E802BDE2DDECAAEF5628312EC47EC9", // MacOS 11 on ARM
                    "6E08F885FEC4094041F6A0B4A02F10AB", // 32-bit Windows on Intel
                    "194D40FBCAE58B4A01095DD89CE70A2D", // 32-bit Windows on AMD
                    "369DAA1350BB9C45BAF84F7769221F00", // 64-bit Windows on Intel
                    "B90925A2DA13F80969FB7E6FEF7E81E0" // 64-bit Windows on AMD
                }
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "D44290597054C7643B8A492DA3F0F97F", // MacOS 10.15
                    "0885887CE776F7DF0B62938F0B1B5A37", // MacOS 11 on Intel
                    "7B13752769B0DCF9A5054C462D18CB40", // MacOS 11 on Intel
                    "D41235E8E642C5773C499DCE06A72CC8", // 32-bit Windows on Intel
                    "C7FD479EFD340F30F3DE645205FA3BC5", // 32-bit Windows on AMD
                    "A86E9A3D4A9479A44F852FA42BA0C9C2", // 64-bit Windows on Intel
                    "437164350E18B65669094C81C9335C4E" // 64-bit Windows on AMD
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "C57AA6ADEDD247E629980DC8E78610A8", // MacOS 10.15
                    "C06FCDFF1A3A1D4FCD4ACAB7AA962957", // MacOS 11 on Intel
                    "04ABF599CC89FC3478FAFD96DD393DBA", // MacOS 11 on ARM
                    "7BAD797AA7C5F71C7168C24077271029", // 32-bit Windows on Intel
                    "01DF45A55B786EBDEFCFFFFFD58187DF", // 32-bit Windows on AMD
                    "102A8F21E39D364419B9CF5BFB386631", // 64-bit Windows on Intel
                    "EB34F55D3D94E569E99C12BD8FCF5BBF" // 64-bit Windows on AMD
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "0E0C4050A3325A97DCC8782850A085C1", // MacOS 10.15
                    "B3E88B401D50081F75096694932AFA63", // MacOS 11 on Intel
                    "B1BD1E3A78B5E47CAAE8462B13720EDE", // MacOS 11 on ARM
                    "9AC3DEF9B464D0E1AB2D4F91C1A08B83", // 32-bit Windows on Intel
                    "E0B28BC27F9F86696F724F2D6E2540AA", // Legacy .NET on 32-bit Windows / AMD
                    "B6A891E92CF4D1FF747716CAB6F617DA", // .NET Core 3.0+ on 32-bit Windows / AMD
                    "2863A63E2060267B6A6151CA90239BC6", // Legacy .NET on 64-bit Windows / Intel
                    "940C0D0056C23E7F8DCDCC09C51E1475", // .NET Core 3.0+ on 64-bit Windows / Intel
                    "067296B4F0A8B3DCE4AF3C48EA9B0C66", // Legacy .NET on 64-bit Windows / AMD
                    "CEFB86217E9AA94C807AE03273151C6E" // .NET Core 3.0+ on 64-bit Windows / AMD
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "AppleAAC",
                new TestSettingDictionary
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                new[]
                {
                    "0E0C4050A3325A97DCC8782850A085C1", // MacOS 10.15
                    "B3E88B401D50081F75096694932AFA63", // MacOS 11 on Intel
                    "B1BD1E3A78B5E47CAAE8462B13720EDE", // MacOS 11 on ARM
                    "9AC3DEF9B464D0E1AB2D4F91C1A08B83", // 32-bit Windows on Intel
                    "4BB812252790CAEDE8CE8547E7BD546A", // 32-bit Windows on AMD
                    "2863A63E2060267B6A6151CA90239BC6", // 64-bit Windows on Intel
                    "76BFBCEB5644AEA27C774D00D35A2B66" // 64-bit Windows on AMD
                }
            },
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
                new[]
                {
                    "EAD8D0B06C252C21C49920DCF360CAAF", // MacOS 10.15
                    "A1FE40A8416A31D87160E49797AAF469", // MacOS 11 on Intel
                    "439F73E38831F4A10B5BD4F91F732A15", // MacOS 11 on ARM
                    "9A0F6E1984B428F236E1209C13AED4D1", // 32-bit Windows on Intel
                    "7FABBF9DDF1A16701E57C6DD190485E0", // 32-bit Windows on AMD
                    "1D0F379EC9C47267569F88729569D407", // 64-bit Windows on Intel
                    "C40C18B162E7429F60C62DF6A23071D6" // 64-bit Windows on AMD
                }
            },
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
                new[]
                {
                    "86088ED15687B386696E2A86CB26B90B", // MacOS 10.15
                    "EA0A363DB54E3435F680C2308EF2A3D9", // MacOS 11 on Intel
                    "E11E1E49987AA13BA2DE91237256AAAF", // MacOS 11 on ARM
                    "78299761793D1A6EC79CBB9233156FD8", // 32-bit Windows on Intel
                    "E53BA332FDCFBE927A81040DB480688B", // 32-bit Windows on AMD
                    "93D67A9C673E7ABE3929846DBE5DBF97", // 64-bit Windows on Intel
                    "FC6792AF620BF4CFB49222B1747B4859" // 64-bit Windows on AMD
                }
            },
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
                new[]
                {
                    "3ED7A24F4D3C2337506DDE23FAFFF996", // MacOS 10.15
                    "A2061435078552AF063273BF4498D456", // MacOS 11 on Intel
                    "D565DE5B4A908E077EEC1BB5B75C05C2", // MacOS 11 on ARM
                    "7EDD94F25082AEEE82B2AA87E795AB6D", // 32-bit Windows on Intel
                    "9EFB3B60246E65F1C90A0880CF8905D9", // 32-bit Windows on AMD
                    "A1CD6AC102BA40A728B2C7E00B1E786D", // 64-bit Windows on Intel
                    "288B9B3FE4FCC212491B9A370757FC46" // 64-bit Windows on AMD
                }
            },
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
                new[]
                {
                    "090AD76F6A073DB5A6FCB206FE76B3B3", // MacOS 10.15
                    "A8FB56E50B8131AD68A311FFCEE0F6FB", // MacOS 11 on Intel
                    "DAA69EC398F6E9E31B67B12DD735331F", // MacOS 11 on ARM
                    "0177BB1DEB19854CA8495C4CBBB25366", // 32-bit Windows on Intel
                    "4EE479F602DD0FB162B19540B683B2BF", // 32-bit Windows on AMD
                    "38D28BD3802566CB30D3B824D7FF593F", // 64-bit Windows on Intel
                    "2D6F2B1043E1276764605C70E94A9EAE" // 64-bit Windows on AMD
                }
            },
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
                new[]
                {
                    "7DA8554E0BC588114FC632ACF751039D", // MacOS 10.15
                    "C9B699DB0247E0F95DE4C3A39582C4D0", // MacOS 11
                    "9E77C0824474E3600F1A919715609A1B", // 32-bit Windows
                    "2321A80FDC5F36A1860523948548F8E5" // 64-bit Windows
                }
            },
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
                new[]
                {
                    "13D8C2E82652D483B473B373FC56C753", // MacOS 10.15
                    "391A6E071EE1085676EDB8BAC8888937", // MacOS 11 on Intel
                    "01245B8ECCFC4C4DA1F261F691A3FF37", // MacOS 11 on ARM
                    "EBD496E30A953A8D0FE11C2609EFABC3", // 32-bit Windows on Intel
                    "AFEC3388275B59A08EFD11A9B32904FD", // 32-bit Windows on AMD
                    "2AD5FC82A78732A66B8F04387D7D412B", // 64-bit Windows on Intel
                    "58E298B32860D9A30E1F31B3D164A3AC" // 64-bit Windows on AMD
                }
            },
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
                new[]
                {
                    "CACCC7C8EFADCEA67B61FBD88040CEF2", // MacOS 10.15
                    "63FF38E19B0CAEAD684B9F03E9126D11", // MacOS 11 on Intel
                    "8B21060F2783BBD1029CB5E38489EE6F", // MacOS 11 on ARM
                    "DE5F94EC1EACB75A3D049AE9960A7ACB", // 32-bit Windows on Intel
                    "C3A45A0F87C7E3A8BDAD6526CFA00ABF", // 32-bit Windows on AMD
                    "298A2B946AA53102FD025DDD9D273B21", // 64-bit Windows on Intel
                    "8853121A36B39C0A607B5FE219A8EFD8" // 64-bit Windows on AMD
                }
            },
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
                new[]
                {

                    "2192CBAE8336E9611C536D086A1DB531", // MacOS 10.15
                    "35BDD732D1DEAF05CDAA969A37758E09", // MacOS 11 on Intel
                    "0557E7171818C5DC73C175F1F8DC7535", // MacOS 11 on ARM
                    "B26C14FD53A4027C26FA3A57CB96AF4C", // 32-bit Windows
                    "96E46C6CF7126E26E58224D5F55850F2", // 64-bit Windows on Intel
                    "60DEF5D19AF12B0962777B0B38CBA79A" // 64-bit Windows on AMD
                }
            },
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
                new[]
                {
                    "2192CBAE8336E9611C536D086A1DB531", // MacOS 10.15
                    "35BDD732D1DEAF05CDAA969A37758E09", // MacOS 11 on Intel
                    "0557E7171818C5DC73C175F1F8DC7535", // MacOS 11 on ARM
                    "B26C14FD53A4027C26FA3A57CB96AF4C", // 32-bit Windows
                    "96E46C6CF7126E26E58224D5F55850F2", // 64-bit Windows on Intel
                    "60DEF5D19AF12B0962777B0B38CBA79A" // 64-bit Windows on AMD
                }
            },
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
                new[]
                {
                    "2EE6CD80BF1B68CE6C7F3C22FC96DB6B", // MacOS 10.15
                    "2689DD85934B68279881E8CA660419CC", // MacOS 11 on Intel
                    "C17ED28BC045C9E5010DF361ADE59515", // MacOS 11 on ARM
                    "B65D496ADABF3DBCDB24136A9655C295", // 32-bit Windows
                    "D4A9A3FFC75AC0383B68BADA43E23C3D", // 64-bit Windows on Intel
                    "2B2F9D57CFA1F3A0D63D261AD4750468" // 64-bit Windows on AMD
                }
            },
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
                new[]
                {
                    "66F3C02A4A016EDB29216A1917D6E54A", // MacOS 10.15
                    "1A4D28ABAD6B47007B7FB06B526DDCAB", // MacOS 11 on Intel
                    "FB38869E1A10C837554BCD0CCDBC660A", // MacOS 11 on ARM
                    "365D7E965534C8690B4694B27D0CF1C9", // 32-bit Windows on Intel
                    "BCA45E90590A453EF4DBDCE3950C9CC4", // 32-bit Windows on AMD
                    "08686D04EFF88BC663C469F2DD224020", // 64-bit Windows on Intel
                    "85FBE50C3C18EFAB46DE754068F359BC" // 64-bit Windows on AMD
                }
            },
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
                new[]
                {
                    "EAD8D0B06C252C21C49920DCF360CAAF", // MacOS 10.15
                    "A1FE40A8416A31D87160E49797AAF469", // MacOS 11 on Intel
                    "439F73E38831F4A10B5BD4F91F732A15", // MacOS 11 on ARM
                    "9A0F6E1984B428F236E1209C13AED4D1", // 32-bit Windows on Intel
                    "7FABBF9DDF1A16701E57C6DD190485E0", // 32-bit Windows on AMD
                    "1D0F379EC9C47267569F88729569D407", // 64-bit Windows on Intel
                    "C40C18B162E7429F60C62DF6A23071D6" // 64-bit Windows on AMD
                }
            },
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
                new[]
                {
                    "32C00BC525D1101491AE423027AFF09B", // MacOS 10.15
                    "248F69EF80DD2185FD4953BA8FC564C3", // MacOS 11 on Intel
                    "F245069996E7B90FBC76C04515E7DB2C", // MacOS 11 on ARM
                    "DDA8DBB070EA36F77455A41A2628B6AA", // 32-bit Windows on Intel
                    "9EBD64EEF7F1CB540012892515A3B0F5", // 32-bit Windows on AMD
                    "B49EC8F6428A1CDEBA4F0728FC1BF8E5", // 64-bit Windows on Intel
                    "A65D4DA9DB98F2DEF066508818C680CD" // 64-bit Windows on AMD
                }
            },
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
                new[]
                {
                    "B60568438DAB722DF989DA528B5461AC", // MacOS 10.15
                    "56419864ABD22CED84B79209BEFED0E6", // MacOS 11 on Intel
                    "AF4D1BAF1D9D5DFB5F4B6E5D5D50F423", // MacOS 11 on ARM
                    "5502D724D98AA24FE49FA8AFB0FC63A6", // 32-bit Windows on Intel
                    "838B5CABD1F8E0077559E4DF504842DC", // 32-bit Windows on AMD
                    "19940A1BA1D575D9E165584C24A955F4", // 64-bit Windows on Intel
                    "8107A1714777E243320A2E72F8F1D6D7" // 64-bit Windows on AMD
                }
            },

            #endregion

#endif

            #region Lame MP3 Encoding

            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary(),
                new[]
                {
                    "F2BD0875E273743A8908F96DCCFDFC44", // Lame 3.100 (Ubuntu and MacOS)
                    "7CB68FB7ACC70E8CD928E7DB437B16FE" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "LameMP3",
                new TestSettingDictionary(),
                new[]
                {
                    "1CB5B915B3A72CBE76087E16F96A0A3E", // Lame 3.100 (Ubuntu and MacOS)
                    "537DE5BA83AAF6542B2E29C74D405EC2" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary(),
                new[]
                {
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary(),
                new[]
                {
                    "1454732B48913F2A3898164BA366DA01", // Lame 3.100 (Ubuntu and MacOS)
                    "D6C2622620E83D442C80AADBE6B45921" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary(),
                new[]
                {
                    "AD56C3A1ACD627DBDA4B5A28AFE0355D", // Lame 3.100 (Ubuntu and MacOS)
                    "5BCC0ED414809596507ECFEDEBD4454D" // Lame 3.100 (Windows)
                }
            },
            {
                "A-law 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary(),
                new[]
                {
                    "0190385E444B8576C297E1DE837279F1", // Lame 3.100 (Ubuntu and MacOS)
                    "C43AEE67905D09300EE49323D6330426" // Lame 3.100 (Windows)
                }
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary(),
                new[]
                {
                    "3CE431DA62AC5204B9FAE63BD8E2B4A8", // Lame 3.100 (Ubuntu and MacOS)
                    "CC60AD39342F059B4F590988F192FE8D" // Lame 3.100 (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary(),
                new[]
                {
                    "F737A24D4F60E5B3229689CC15FF10EE", // Lame 3.100 (Ubuntu and MacOS)
                    "EA2FE7549FB7A1971265FA27B88D0285" // Lame 3.100 (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "LameMP3",
                new TestSettingDictionary(),
                new[]
                {
                    "FB1B7DECB2C2A2C9CAA1FBB917A81472", // Lame 3.100 (MacOS and Legacy .NET on Ubuntu)
                    "C50EBCF351F8931F36802CF099BFF826", // Lame 3.100 (.NET Core 3.0+ on Ubuntu)
                    "D888321C2C776E245434E50FDF61A6E8", // Lame 3.100 (Legacy .NET on 32-bit Windows)
                    "A286239868A51F5CD05540C198EA96B2", // Lame 3.100 (Legacy .NET on 64-bit Windows)
                    "8BCAB166E1DEE1873AFEFC4DCCCBE333" // Lame 3.100 (.NET Core 3.0+ on Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "LameMP3",
                new TestSettingDictionary(),
                new[]
                {
                    "FB1B7DECB2C2A2C9CAA1FBB917A81472", // Lame 3.100 (Ubuntu and MacOS)
                    "A4306E31052226EFD081D5D5FA80F62B" // Lame 3.100 (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Default tag version
                    ["TagVersion"] = "2.3"
                },
                new[]
                {
                    "F737A24D4F60E5B3229689CC15FF10EE", // Lame 3.100 (Ubuntu and MacOS)
                    "EA2FE7549FB7A1971265FA27B88D0285" // Lame 3.100 (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Tag version 2.4
                    ["TagVersion"] = "2.4"
                },
                new[]
                {
                    "F69CCDFC32565F97130CBAEABFF0D13C", // Lame 3.100 (Ubuntu and MacOS)
                    "BAB786013527E61F4719BBC1F6682C92" // Lame 3.100 (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Default tag encoding
                    ["TagEncoding"] = "Latin1"
                },
                new[]
                {
                    "F737A24D4F60E5B3229689CC15FF10EE", // Lame 3.100 (Ubuntu and MacOS)
                    "EA2FE7549FB7A1971265FA27B88D0285" // Lame 3.100 (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // UTF-16 tag encoding
                    ["TagEncoding"] = "UTF16"
                },
                new[]
                {
                    "EA1232E970C83FCDDE00D4C1D51F0446", // Lame 3.100 (Ubuntu and MacOS)
                    "B3402C50D24A82004D50DC0172E81BC1" // Lame 3.100 (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // UTF-8 tag encoding, implicit tag version 2.4
                    ["TagEncoding"] = "UTF8"
                },
                new[]
                {
                    "388108BB7EE76567E9869F4CE9786CE9", // Lame 3.100 (Ubuntu and MacOS)
                    "B0AC00BF6DFDA60BAD712FD3F9DFED21" // Lame 3.100 (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // UTF-8 tag encoding, explicit tag version 2.4
                    ["TagVersion"] = "2.4",
                    ["TagEncoding"] = "UTF8"
                },
                new[]
                {
                    "388108BB7EE76567E9869F4CE9786CE9", // Lame 3.100 (Ubuntu and MacOS)
                    "B0AC00BF6DFDA60BAD712FD3F9DFED21" // Lame 3.100 (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // UTF-8 tag encoding, ignored tag version 2.3
                    ["TagVersion"] = "2.3",
                    ["TagEncoding"] = "UTF8"
                },
                new[]
                {
                    "388108BB7EE76567E9869F4CE9786CE9", // Lame 3.100 (Ubuntu and MacOS)
                    "B0AC00BF6DFDA60BAD712FD3F9DFED21" // Lame 3.100 (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Default tag padding (explicit)
                    ["TagPadding"] = 2048
                },
                new[]
                {
                    "F737A24D4F60E5B3229689CC15FF10EE", // Lame 3.100 (Ubuntu and MacOS)
                    "EA2FE7549FB7A1971265FA27B88D0285" // Lame 3.100 (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Tag padding disabled
                    ["TagPadding"] = 0
                },
                new[]
                {
                    "ED3A9531742553641B112C0D0A41F099", // Lame 3.100 (Ubuntu and MacOS)
                    "ADA8A213D1219A92937453878EEA3D18" // Lame 3.100 (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Maximum tag padding
                    ["TagPadding"] = 16_777_216
                },
                new[]
                {
                    "6645726904A761FDF324711CFD21D477", // Lame 3.100 (Ubuntu and MacOS)
                    "47A50D9D488F78F43577BB5BBA0BD783" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Tag version does nothing without metadata
                    ["TagVersion"] = "2.4"
                },
                new[]
                {
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Tag encoding does nothing without metadata
                    ["TagEncoding"] = "UTF16"
                },
                new[]
                {
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Tag padding does nothing without metadata
                    ["TagPadding"] = 100
                },
                new[]
                {
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Default VBR quality
                    ["VBRQuality"] = 3
                },
                new[]
                {
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Minimum VBR quality
                    ["VBRQuality"] = 9
                },
                new[]
                {
                    "65D418A236D86A8CE33E07A76C98DF08", // Lame 3.100 (Ubuntu and MacOS)
                    "BB8B33BD589DA49D751C883B8A0FF653" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Maximum VBR quality
                    ["VBRQuality"] = 0
                },
                new[]
                {
                    "5DE234656056DFDAAD30E4DA9FD26366", // Lame 3.100 (Ubuntu and MacOS)
                    "3B10B6430B2A823C58F16953F5B33E9C" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Minimum bit rate
                    ["BitRate"] = 8
                },
                new[]
                {
                    "2BBC83E74AB1A4EB150BC6E1EB9920B5", // Lame 3.100 (Ubuntu and MacOS)
                    "2ACEB0816512B4300D13E9329F76D752" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Maximum bit rate
                    ["BitRate"] = 320
                },
                new[]
                {
                    "BEB5029A08011BCEDFFA99173B763E7F", // Lame 3.100 (Ubuntu and MacOS)
                    "E9525D2505684DDB3F9FDDE7B550577E" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Forced bit rate disabled (default)
                    ["BitRate"] = 128
                },
                new[]
                {
                    "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6", // Lame 3.100 (Ubuntu and MacOS)
                    "54E8544993C1E818C72DE5AC00DEABF5" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Forced bit rate explicitly disabled
                    ["BitRate"] = 128,
                    ["ForceCBR"] = false
                },
                new[]
                {
                    "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6", // Lame 3.100 (Ubuntu and MacOS)
                    "54E8544993C1E818C72DE5AC00DEABF5" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Forced bit rate enabled
                    ["BitRate"] = 128,
                    ["ForceCBR"] = true
                },
                new[]
                {
                    "EACCA2FD6404ACA1AB46027FAE6A667B", // Lame 3.100 (Ubuntu and MacOS)
                    "AAE7CB7E1E4EAAF5ED19F8B986647298" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Forced bit rate ignored without bit rate
                    ["ForceCBR"] = true
                },
                new[]
                {
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // VBR quality ignored with bit rate
                    ["VBRQuality"] = 3,
                    ["BitRate"] = 128
                },
                new[]
                {
                    "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6", // Lame 3.100 (Ubuntu and MacOS)
                    "54E8544993C1E818C72DE5AC00DEABF5" // Lame 3.100 (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track"
                },
                new[]
                {
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track"
                },
                new[]
                {
                    "8D5A9BA2D974BCDEFB50A3DA7E134335", // Lame 3.100 (MacOS)
                    "49CB061F4DE93D7F88D3B656458C7003", // Lame 3.100 (Ubuntu)
                    "3AE3B2DA82CE86ABDA69CFBB06EBDB6E", // Lame 3.100 (Legacy .NET on Windows)
                    "F3F89801873BDC7D9E403C160051B457" // Lame 3.100 (.NET Core 3.0+ on Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album"
                },
                new[]
                {
                    "94E8EF913253B83D35E760A8C81C18BA", // Lame 3.100 (MacOS)
                    "E804988EFC1EA58704E6C78B42CE1DF6", // Lame 3.100 (Ubuntu)
                    "85C1262D46F863436B5C55B71C8C0B31", // Lame 3.100 (Legacy .NET on Windows)
                    "026C2587049104F0FFF72EB8E49F87AB" // Lame 3.100 (.NET Core 3.0+ on Windows)
                }
            },

            #endregion

            #region Ogg Vorbis Encoding

            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "00502CA1BD9BE8137FCF75518D8EC5AC", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "2BEFC8DFC5C86F305FBB1126CC2B2D0D", // Vorbis 1.3.7 (MacOS on Intel)
                    "F187C28A4A37DF469CF96EBC8565CC21", // Vorbis 1.3.7 (MacOS on ARM)
                    "E6FBBBCE3847BC9C9EB45A89A7D7DDA6" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "1A3784BD4B5A5F7324F0F19119EC8829", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "EFC7217481D26BB0828C6E4BE7D2D414", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "E8B8006C5EC2A3D50555B85F367F4922", // Vorbis 1.3.7 (MacOS on Intel)
                    "4759435E55E2DF81B20F6DD929330EE5", // Vorbis 1.3.7 (MacOS on ARM)
                    "62C6F8889AA6CBE4A80750EFF33D9FDA", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "3C12BC37F57BB2C4542A92ABD52EA27D" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "F041B26255F501A83ECD8C6C7EBCE55D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "C2C9FE067F40025DDF41B868350F28A5", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "7757FDE8D2A84D6AA7406F9CA3D840D3", // Vorbis 1.3.7 (MacOS on Intel)
                    "E912A2A65C6272335DF116937A0549A6", // Vorbis 1.3.7 (MacOS on ARM)
                    "57E0997F13613A8C64306230A031D912" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                }
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "AA03C92A5ED981484D17EA96D87B9331", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "BDCF3199D9F7457000B8D2BDFB757579", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "6D4108CB330E72038EA67B4D7B335AF9", // Vorbis 1.3.7 (MacOS on Intel)
                    "C1D824314622416360560019BD79F519", // Vorbis 1.3.7 (MacOS on ARM)
                    "A75601BC8CABA65EADD866211A1EF536" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                }
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "5C6A8AC9FE704864E8ABC4F1654F65D7", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "F796497F75425D1928C66B88F3F24232", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "239E03B5B24AA65E6335303DF50FB3A2", // Vorbis 1.3.7 (MacOS on Intel)
                    "2A14B596BBE04A8391249A4276753F10", // Vorbis 1.3.7 (MacOS on ARM)
                    "5EF3AF9F00AB434F1A0A08482DE2C0DE" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                }
            },
            {
                "A-law 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "9153B9D26FA0A06AD1515B5660055CA3", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "6DD98044F88A9535595D2B2D16D1A787", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "D48672C42031B4419ADC0F420D503A47", // Vorbis 1.3.7 (MacOS on Intel)
                    "2C8DB7597ED25C3494B89E5FBCC75508", // Vorbis 1.3.7 (MacOS on ARM)
                    "DB770934E3E57E832BFFA1C1C85E065F" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                }
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "9A93109BCB1025791DCA8B63E9EFE3B6", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "3E40EB8AEB86CA5E3C76525E1E03D0FD", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "4721899D9DDAF7394B1A679F753F81CF", // Vorbis 1.3.7 (MacOS on Intel)
                    "856219EF1D9C77A64FE0994B7373C848", // Vorbis 1.3.7 (MacOS on ARM)
                    "4E69AF464154872795B7AD87BA762870", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "38EB06F6C7055EC41221129C2440E823" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "91ADF87CA8AE3D1669EA65D30BB083D8", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "19AF57343851DC074CDD23039C577BC2", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "F6D78BE21567DD9244848160A2BD3889", // Vorbis 1.3.7 (MacOS on Intel)
                    "ED7CAFD6D69FFF643E01219D1E9696E7", // Vorbis 1.3.7 (MacOS on ARM)
                    "AB097FDBAA6B8516129A17ED1C5BDC21" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "67F854EBB448041C99902D721EB029C0", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "BD1777F28C923D15E00840C2725EB14A", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "AD172F94045E0970A71F8C439EEEA6C9", // Vorbis 1.3.7 (MacOS on Intel)
                    "CD4B255CDC3C689C8A2242FDE5F912C7", // Vorbis 1.3.7 (MacOS on ARM)
                    "7A82FD0ADD477568D0FA4B84BE1CFDD9", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on 32-bit Windows)
                    "45E719C489D35DD1CC902143B578553D", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on 64-bit Windows)
                    "467CEB127C359E6B75C045FAD1F7989A" // Vorbis 1.3.7 AoTuV + Lancer (.NET Core 3.0+ on Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "Vorbis",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "0AA286B6A6896263E9182853FFA2DD52", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "8A4B07CF14C30DF85BCBDCD91A597424", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "AD172F94045E0970A71F8C439EEEA6C9", // Vorbis 1.3.7 (MacOS on Intel)
                    "CD4B255CDC3C689C8A2242FDE5F912C7", // Vorbis 1.3.7 (MacOS on ARM)
                    "F25060DBEAB99B219CDB5EC54D37AD1C" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Minimum serial #
                    ["SerialNumber"] = int.MinValue
                },
                new[]
                {
                    "ABC75E49AF00624847D66A441068199F", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "8EB74661170103394BB0737E102B978A", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "88C186B7AA3DC3A55CD430FFF8AADCB1", // Vorbis 1.3.7 (MacOS on Intel)
                    "E7BE0862210C0010541BE605901D4A6D", // Vorbis 1.3.7 (MacOS on ARM)
                    "8224BAEA06E788DF37EFC01CCCD479C8" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Maximum serial #
                    ["SerialNumber"] = int.MaxValue
                },
                new[]
                {
                    "9B499871B3788480810EB00DDF08054F", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "171C2CA1CDB8C004B15A4D1D6C2C7375", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "74D8326F97A08EBCCC7FF754FE37464F", // Vorbis 1.3.7 (MacOS on Intel)
                    "9DEA359E04D151021DC995764B8C428E", // Vorbis 1.3.7 (MacOS on ARM)
                    "C77B164D4E60453D679F35551FD3BF02" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Default quality (explicit)
                    ["Quality"] = 5,
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "F041B26255F501A83ECD8C6C7EBCE55D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "C2C9FE067F40025DDF41B868350F28A5", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "7757FDE8D2A84D6AA7406F9CA3D840D3", // Vorbis 1.3.7 (MacOS on Intel)
                    "E912A2A65C6272335DF116937A0549A6", // Vorbis 1.3.7 (MacOS on ARM)
                    "57E0997F13613A8C64306230A031D912" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Minimum quality
                    ["Quality"] = -1,
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "E3B0B6AE0805022FAA88EC50199E9D05", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "DFDCDBBC24E1DC2EE1411CE40D7DF3FE", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "9DEF0BAB12400A25E06EA7CA8C32CCC6", // Vorbis 1.3.7 (MacOS on Intel)
                    "483C59AF294602813322BFF28BF554A5", // Vorbis 1.3.7 (MacOS on ARM)
                    "79C966C3D6728C49723640C0D7B9330B", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "55B7584D23DE2667374ABB9C3C571875" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)

                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Maximum quality
                    ["Quality"] = 10,
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "08A958E00D3797F33ABB2F098556DE43", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "EC722CED6079EC8609552686C911FFFE", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "1C21FEE55AC987FE8ACA1865353DC833", // Vorbis 1.3.7 (MacOS on Intel)
                    "25AEE91A747018638A34DC630D24B3FC", // Vorbis 1.3.7 (MacOS on ARM)
                    "4B2B694BD0D42994F4A1911FBCB2ABF8", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "9E45D7B37055FD59650839F5BEAB1ED0" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Minimum bit rate
                    ["BitRate"] = 45,
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "749518EA6F95A089787DD414855DDBC0", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "68B19B197BDAFA73A45EBF67CD961CA9", // Vorbis 1.3.7 (MacOS on Intel)
                    "1CBA6651221C34E9ECDB314A653D05E0", // Vorbis 1.3.7 (MacOS on ARM)
                    "CAEAE4C932830A8C0A41BC5C79DC80D5", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "53E545072EC0FDA11D100BA1DE9EBC0A" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Maximum bit rate
                    ["BitRate"] = 500,
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "0E233828AF5FD1010188AC4C63DF40BC", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "057D14E2F3F8C9F2FD44EE29222A7BD3", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "D37BB84F2008B1B467D54C618495C4CE", // Vorbis 1.3.7 (MacOS on Intel)
                    "9631DB55B39F79E82ECC05E27EE8928B", // Vorbis 1.3.7 (MacOS on ARM)
                    "215FA0E953F4BB520A46A3B44B68CC92", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "8E0D0279037DC9DCC286050A2930427A" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Forced bit rate disabled (default)
                    ["BitRate"] = 128,
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "70ECB3839EC4DC7A7ECEA48241E2407D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "577FA5EA45715260728A894592EEAED9", // Vorbis 1.3.7 (MacOS on Intel)
                    "5008E6BEB9E483B5190D778129D5C1C4", // Vorbis 1.3.7 (MacOS on ARM)
                    "84E389F08890621CF00AF8DD2D7C77DB", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "EC75AF6E1F0EFEA7E87ED7B40EFA415A" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                }
            },
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
                new[]
                {
                    "70ECB3839EC4DC7A7ECEA48241E2407D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "577FA5EA45715260728A894592EEAED9", // Vorbis 1.3.7 (MacOS on Intel)
                    "5008E6BEB9E483B5190D778129D5C1C4", // Vorbis 1.3.7 (MacOS on ARM)
                    "84E389F08890621CF00AF8DD2D7C77DB", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "EC75AF6E1F0EFEA7E87ED7B40EFA415A" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                }
            },
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
                new[]
                {
                    "E67F4AB32814D96B1BA65652E5E11E56", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "D23564C9639B7C6490E59163C37B9C83", // Vorbis 1.3.7 (MacOS on Intel)
                    "D4974D63DD5B7326BD2143801C3DC610", // Vorbis 1.3.7 (MacOS on ARM)
                    "C84819FCFA2F25FCDB3E5490E54949B4", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "3FA511E9C941DFB1E61A98418C27F383" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Forced bit rate ignored without bit rate
                    ["ForceCBR"] = true,
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "F041B26255F501A83ECD8C6C7EBCE55D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "C2C9FE067F40025DDF41B868350F28A5", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "7757FDE8D2A84D6AA7406F9CA3D840D3", // Vorbis 1.3.7 (MacOS on Intel)
                    "E912A2A65C6272335DF116937A0549A6", // Vorbis 1.3.7 (MacOS on ARM)
                    "57E0997F13613A8C64306230A031D912" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                }
            },
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
                new[]
                {
                    "70ECB3839EC4DC7A7ECEA48241E2407D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "577FA5EA45715260728A894592EEAED9", // Vorbis 1.3.7 (MacOS on Intel)
                    "5008E6BEB9E483B5190D778129D5C1C4", // Vorbis 1.3.7 (MacOS on ARM)
                    "84E389F08890621CF00AF8DD2D7C77DB", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "EC75AF6E1F0EFEA7E87ED7B40EFA415A" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new TestSettingDictionary
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "F041B26255F501A83ECD8C6C7EBCE55D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "C2C9FE067F40025DDF41B868350F28A5", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "7757FDE8D2A84D6AA7406F9CA3D840D3", // Vorbis 1.3.7 (MacOS on Intel)
                    "E912A2A65C6272335DF116937A0549A6", // Vorbis 1.3.7 (MacOS on ARM)
                    "57E0997F13613A8C64306230A031D912" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "39443EC16B23C3B5159CD199E83AD092", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "466D9535B74B53EA088F13FF9720268C", // Vorbis 1.3.7 (MacOS on Intel)
                    "A637C5C3C53C717092D518B1F47963D5", // Vorbis 1.3.7 (MacOS on ARM)
                    "CE264095289A42A1FB038E6F44D5F007", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on Windows / Intel)
                    "E698F1678A8FBC0A6834B6E7230743DA", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on Windows / AMD)
                    "805F62BDFE149898E21C9448F4335BAC", // Vorbis 1.3.7 AoTuV + Lancer (.NET Core 3.0+ on Windows / Intel)
                    "64478AA7F8E2052608AE10624A3C396B" // Vorbis 1.3.7 AoTuV + Lancer (.NET Core 3.0+ on Windows / AMD)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new TestSettingDictionary
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album",
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "7FF678CC731484D0B16E7FC01301EE7E", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "9F4297E686E3A938051AB8C753476521", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "488A2980F20619FE0206CFBB1767CBAC", // Vorbis 1.3.7 (MacOS on Intel)
                    "8F7096C15F10B9EB3CA0DFA0E044877E", // Vorbis 1.3.7 (MacOS on ARM)
                    "4BCFD0F13F55B657F36304BBE3F41D39", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on Windows / Intel)
                    "F4C820158442E1ECEEBBCEFF42855E5F", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on Windows / AMD)
                    "34BA39848B7D78D7FE1D2B30999DF6A9", // Vorbis 1.3.7 AoTuV + Lancer (.NET Core 3.0+ on Windows / Intel)
                    "F1F317505EE9AD557FBF2DB31777AAAB" // Vorbis 1.3.7 AoTuV + Lancer (.NET Core 3.0+ on Windows / AMD)
                }
            },

            #endregion

            #region Opus Encoding

            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "D085A7142C47B6898B3CCB6C5EDCBA55", // Opus 1.1.2 (Ubuntu 18.04)
                    "5E705C21EEB25418F69F54E33CF65156", // Opus 1.3.1 (Ubuntu 20.04)
                    "2D31A0D707D3A9AB865DB00095EB08AE", // Opus 1.3.1 (MacOS on Intel)
                    "FB641D1DA3D800E22BD22C7983D83325", // Opus 1.3.1 (MacOS on ARM)
                    "8BB7F2763D133BD534AE9C20104AFA2E", // Opus 1.3.1 (32-bit Windows on Intel)
                    "6FB30E2C70A48F63A6596FCDE747E47A", // Opus 1.3.1 (32-bit Windows on AMD)
                    "05B065E87CE35617089DC8A86ED5AD19", // Opus 1.3.1 (64-bit Windows on Intel)
                    "3E1D8B9F272D68F84209CA487120EFB1" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "46B412FA052C97474B60DDDCF2CA8052", // Opus 1.1.2 (Ubuntu 18.04)
                    "E170CC2F31A402071E26892426AEC65B", // Opus 1.3.1 (Ubuntu 20.04)
                    "C22E87F617B51970785E9A4C43C9FC48", // Opus 1.3.1 (MacOS on Intel)
                    "86C499288EADAAB469F3E7FA63628AD1", // Opus 1.3.1 (MacOS on ARM)
                    "BE719CEBED7270CD6DC580007E20D5F2", // Opus 1.3.1 (32-bit Windows on Intel)
                    "B68DA72E32DD0B6A89138679E8716876", // Opus 1.3.1 (32-bit Windows on AMD)
                    "C8C92433A8093BB8C7AE3AE6F84653B3" // Opus 1.3.1 (64-bit Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "94AADBE44ECF34AF98982F17BCCE7C97", // Opus 1.1.2 (Ubuntu 18.04)
                    "9DA2DC35F1B9FBC9FAEF343788BB9E41", // Opus 1.3.1 (Ubuntu 20.04)
                    "0C6BF7ECB9F757DB8F8AF485137BD2C8", // Opus 1.3.1 (MacOS on Intel)
                    "DF1CE487259E093896FB4BDB74A6B1E3", // Opus 1.3.1 (MacOS on ARM)
                    "3CAB512976D0D62B45E2C9239B19735D", // Opus 1.3.1 (32-bit Windows on Intel)
                    "63CCEA9D4CF6847E5D6D29245372E93D", // Opus 1.3.1 (32-bit Windows on AMD)
                    "F82CC6FE2C86F5E7CD4A4F8F634E2DE9", // Opus 1.3.1 (64-bit Windows on Intel)
                    "A6ACF878EA0F60CF33EA9D214F814608" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "C9024DED60E2039752C7B372DFFC7149", // Opus 1.1.2 (Ubuntu 18.04)
                    "BD5F090F921BCC80F05FCBF5725D8E0E", // Opus 1.3.1 (Windows, Ubuntu 20.04 and MacOS on Intel)
                    "5074AE45A08E36CAF8FC0AB1B50F117C", // Opus 1.3.1 (MacOS on ARM)
                    "3C5EF551E97AEE3A916F1B39BCD5D3C2", // Opus 1.3.1 (32-bit Windows on Intel)
                    "B9B4ECB9629E3DCA58EC09AC5560A1F9", // Opus 1.3.1 (32-bit Windows on AMD)
                    "FDE97144EB743A401CD274613B32D085" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "22AA639A1AA309F9C343F8D8EBDB1B6D", // Opus 1.1.2 (Ubuntu 18.04)
                    "F7CB34EAF50E121D9474AE51E43BBB5B", // Opus 1.3.1 (Ubuntu 20.04)
                    "8A8FA9C452D9EBBCF8554EE3E270A538", // Opus 1.3.1 (MacOS on Intel)
                    "FC74891978B6213CDF3807C6D0130E82", // Opus 1.3.1 (MacOS on ARM)
                    "9E31F9F58A41EA377740E7AC43E57939", // Opus 1.3.1 (32-bit Windows on Intel)
                    "6D4CD77579FC6041A28997D9A409DCB4", // Opus 1.3.1 (32-bit Windows on AMD)
                    "D0FC8A33EE164AC29D79435A4ED779B1", // Opus 1.3.1 (64-bit Windows on Intel)
                    "EF01321114364EB70260DA0AC94553DB" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "A-law 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "64C832F3C0D8A175084FB138D0A3525D", // Opus 1.1.2 (Ubuntu 18.04)
                    "CD27FFC8398F52FB2F5984085D2215AC", // Opus 1.3.1 (MacOS and Ubuntu 20.04 on Intel)
                    "2018DEFDE09A503E0734CD57B0E19FC4", // Opus 1.3.1 (Ubuntu 20.04 on AMD)
                    "4190DC99A4B49887AA3E5BC9E15DC1C8", // Opus 1.3.1 (MacOS on ARM)
                    "F06773DFABF471946AF2B3D55E881795", // Opus 1.3.1 (32-bit Windows on Intel)
                    "B540644D968E93C4EF7BA2BE7C84A5AA", // Opus 1.3.1 (32-bit Windows on AMD)
                    "AA2881B010E43964FC90B6DB03BCA969", // Opus 1.3.1 (64-bit Windows on Intel)
                    "29E4A664D34C52A907E85C52D485FEAB" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "852C615283FA436C87460A6DD0A5ABE5", // Opus 1.1.2 (Ubuntu 18.04)
                    "9ED53495B32D47496C1C88B75D2FEF5F", // Opus 1.3.1 (MacOS and Ubuntu 20.04 on Intel)
                    "EF85B330CBC8A8931C2827A81F2E1532", // Opus 1.3.1 (Ubuntu 20.04 on AMD)
                    "5A928AED6645BDF1A8CBA3C2EEB7E90F", // Opus 1.3.1 (MacOS on ARM)
                    "86E35D927296E4954BAE384D4B28F247", // Opus 1.3.1 (32-bit Windows on Intel)
                    "84171D7E57590C886785D15C4E89D14E", // Opus 1.3.1 (32-bit Windows on AMD)
                    "143CFA3D5684C47EC62A5BEE19B59493", // Opus 1.3.1 (64-bit Windows on Intel)
                    "73888454CF0F7F104EF389C6A603973A" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "1B7298B4EA52190612430642D2CD0694", // Opus 1.1.2 (Ubuntu 18.04)
                    "5A2C444BCD2F8D898E369DFA78D2336D", // Opus 1.3.1 (Ubuntu 20.04)
                    "5501D7D9730B0722C310D5263AFBEB77", // Opus 1.3.1 (MacOS on Intel)
                    "CB28DA52EF8451F944D8ED642D438292", // Opus 1.3.1 (MacOS on ARM)
                    "ED5EA746CA5182DE57E0D25695D17F4E", // Opus 1.3.1 (32-bit Windows on Intel)
                    "F6AE6DD4A2E565AD94622055C7DD2DD6", // Opus 1.3.1 (32-bit Windows on AMD)
                    "E59BFB7E77545D36723E7B8EB5AF1B48", // Opus 1.3.1 (64-bit Windows on Intel)
                    "0E862732E81AF56E0AF64B9878BEFED8" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "E497EF951D153E53CD3B2C3D84E5851C", // Opus 1.1.2 (Ubuntu 18.04)
                    "F11DE8AF370B04150CBF6E08FA1E2045", // Opus 1.3.1 (Ubuntu 20.04 on Intel)
                    "AFE05CB9E10ECE0CE4A267B01E91AA7C", // Opus 1.3.1 (Ubuntu 20.04 on AMD)
                    "0ADBC105EA51C62CB1ED53B978B33415", // Opus 1.3.1 (MacOS on Intel)
                    "2D7ABAB485382CFA8AD0DA8437304185", // Opus 1.3.1 (MacOS on ARM)
                    "17D464F4D94C17DD99AC0017BAB9228F", // Opus 1.3.1 (32-bit Windows on Intel)
                    "EEFBE37F619828E4EB4889D2B795C0EA", // Opus 1.3.1 (Legacy .NET on 32-bit Windows / AMD)
                    "9DC6E32817E85882E936B8428C28C040", // Opus 1.3.1 (.NET Core 3.0+ on 32-bit Windows / AMD)
                    "B52AE38B2D30A7ADE63492CCB2F000EC", // Opus 1.3.1 (Legacy .NET on 64-bit Windows / Intel)
                    "58FBBA36C5E504D4BD93C427C369DB0A", // Opus 1.3.1 (.NET Core 3.0+ on 64-bit Windows / Intel)
                    "D9A87559E197DC01457A4E175CF0D3DB", // Opus 1.3.1 (Legacy .NET on 64-bit Windows / AMD)
                    "47BD4ADA27ABF79100C4126048FBC42A" // Opus 1.3.1 (.NET Core 3.0+ on 64-bit Windows / AMD)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "Opus",
                new TestSettingDictionary
                {
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "476D30594189602BF6E4755E990D0615", // Opus 1.1.2 (Ubuntu 18.04)
                    "E170CC2F31A402071E26892426AEC65B", // Opus 1.3.1 (Ubuntu 20.04)
                    "0ADBC105EA51C62CB1ED53B978B33415", // Opus 1.3.1 (MacOS on Intel)
                    "2D7ABAB485382CFA8AD0DA8437304185", // Opus 1.3.1 (MacOS on ARM)
                    "17D464F4D94C17DD99AC0017BAB9228F", // Opus 1.3.1 (32-bit Windows on Intel)
                    "37FAD87A9C5A0E42FEEBE2B5CC5547DF", // Opus 1.3.1 (32-bit Windows on AMD)
                    "B52AE38B2D30A7ADE63492CCB2F000EC", // Opus 1.3.1 (64-bit Windows on Intel)
                    "285F9F24FA0B405F86C61AC20BE81333" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Minimum serial #
                    ["SerialNumber"] = int.MinValue
                },
                new[]
                {
                    "DE6D30BCAC9DA6340DA28542E75C5E01", // Opus 1.1.2 (Ubuntu 18.04)
                    "503CA160DEC071D27F058FAE56273C29", // Opus 1.3.1 (Ubuntu 20.04)
                    "CDE65307AACEA4F40F22FF925622D14E", // Opus 1.3.1 (MacOS on Intel)
                    "F2FDC3B2D196D551919F3FC85120DA15", // Opus 1.3.1 (MacOS on ARM)
                    "0E458849DBB876C84D23FA8B1AE58E7D", // Opus 1.3.1 (32-bit Windows on Intel)
                    "7BB78CF68981EC0D8A4FF3006F9D56D9", // Opus 1.3.1 (32-bit Windows on AMD)
                    "3804E9A98DDBD216A3AED6F638C5644E", // Opus 1.3.1 (64-bit Windows on Intel)
                    "211289691B8D3B688C68ADAE31CF17FA" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Maximum serial #
                    ["SerialNumber"] = int.MaxValue
                },
                new[]
                {
                    "D0DB0EE3ABC3950E3009663461F631DB", // Opus 1.1.2 (Ubuntu 18.04)
                    "E83E5634833A567959243B09B9AF666B", // Opus 1.3.1 (Ubuntu 20.04)
                    "3AA89123958BA0DF1611539BC909DD0B", // Opus 1.3.1 (MacOS on Intel)
                    "A7F53EB258E068E6B9B6C6A1D756E342", // Opus 1.3.1 (MacOS on ARM)
                    "049CE3FC2614A25BDE9E311CCEC4E995", // Opus 1.3.1 (32-bit Windows on Intel)
                    "2D4B0717D5E4BA6494A5A50131FABB2E", // Opus 1.3.1 (32-bit Windows on AMD)
                    "5480E19A91B0DC0891094B43168FA839", // Opus 1.3.1 (64-bit Windows on Intel)
                    "F07749A5027B6F7994D04ED84E75AC8E" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Minimum bit rate
                    ["BitRate"] = 5,
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "7556A3E6FF92CEF2D0ECD5654322DEAB", // Opus 1.1.2 (Ubuntu 18.04)
                    "1F81AFD1976BAB56F31A4599A8AD8FF1", // Opus 1.3.1 (Ubuntu 20.04 and MacOS)
                    "FC89452B5F0EA49E3D738CE67FBF8B1C", // Opus 1.3.1 (32-bit Windows)
                    "D345EE9B84E231E3ACB17C7AC0972154" // Opus 1.3.1 (64-bit Windows)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Maximum bit rate
                    ["BitRate"] = 512,
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "C031F42E8AF2D482E302264E132502E1", // Opus 1.1.2 (Ubuntu 18.04)
                    "2DBEDA9428C2522EFF60481B5446FA47", // Opus 1.3.1 (Ubuntu 20.04)
                    "785D39E686216F4958B8103B62E9E321", // Opus 1.3.1 (MacOS on Intel)
                    "4F1E3A4F58E62CB2F9DF5D0C3AED689D", // Opus 1.3.1 (MacOS on ARM)
                    "6B422669A0FCB242E0E15204F5FDCC47", // Opus 1.3.1 (32-bit Windows on Intel)
                    "DFF654F2AF639F992FA9BC378F80FE20", // Opus 1.3.1 (32-bit Windows on AMD)
                    "4A9455EB95B3C9CBCB2538D0C888267D", // Opus 1.3.1 (64-bit Windows on Intel)
                    "0E9C8DE567A09A13D1D35A14BBB04A2D" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Variable control mode (default, explicit)
                    ["ControlMode"] = "Variable",
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "94AADBE44ECF34AF98982F17BCCE7C97", // Opus 1.1.2 (Ubuntu 18.04)
                    "9DA2DC35F1B9FBC9FAEF343788BB9E41", // Opus 1.3.1 (Ubuntu 20.04)
                    "0C6BF7ECB9F757DB8F8AF485137BD2C8", // Opus 1.3.1 (MacOS on Intel)
                    "DF1CE487259E093896FB4BDB74A6B1E3", // Opus 1.3.1 (MacOS on ARM)
                    "3CAB512976D0D62B45E2C9239B19735D", // Opus 1.3.1 (32-bit Windows on Intel)
                    "63CCEA9D4CF6847E5D6D29245372E93D", // Opus 1.3.1 (32-bit Windows on AMD)
                    "F82CC6FE2C86F5E7CD4A4F8F634E2DE9", // Opus 1.3.1 (64-bit Windows on Intel)
                    "A6ACF878EA0F60CF33EA9D214F814608" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Constrained VBR mode
                    ["ControlMode"] = "Constrained",
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "940955E239CF3D26B417F6B532EBC726", // Opus 1.1.2 (Ubuntu 18.04)
                    "713E85741D7499561FA4424B5EF74EE7", // Opus 1.3.1 (Ubuntu 20.04)
                    "B98AC6052E465C5A226D8D2905B535EC", // Opus 1.3.1 (MacOS on Intel)
                    "532F344E999193C4F59D1CDBF511C7DE", // Opus 1.3.1 (MacOS on ARM)
                    "38E90E75D928371AB9C1CFF243C731B4", // Opus 1.3.1 (32-bit Windows on Intel)
                    "7DB85D28D9AEF13C7D8BAB7578EC3019", // Opus 1.3.1 (32-bit Windows on AMD)
                    "E0219613A0740FACAAC162D7B9FD0517", // Opus 1.3.1 (64-bit Windows on Intel)
                    "2FEF54092E6C1442909D4B3A8F697632" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // CBR mode
                    ["ControlMode"] = "Constant",
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "9533FF6E154EE45B8ED522C3B5A25865", // Opus 1.1.2 (Ubuntu 18.04)
                    "9948DAE5FCE1CF7D6298C293E3D71B0F", // Opus 1.3.1 (Ubuntu 20.04)
                    "687B2402BF33EDB0EAD683235E309BC8", // Opus 1.3.1 (MacOS on Intel)
                    "D9E874A3511506C2A8021FF77DEAED23", // Opus 1.3.1 (MacOS on ARM)
                    "0A4FC1F40FD76797222FC8CACCA83AD9", // Opus 1.3.1 (32-bit Windows on Intel)
                    "15F2F64E2FD60C135DE87B563DA028DF", // Opus 1.3.1 (32-bit Windows on AMD)
                    "F3CF4988FB774A297832863156B8ED8D", // Opus 1.3.1 (64-bit Windows on Intel)
                    "CD6404BF5B59DE41CE66F7D2C8838015" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // Low bit rate, Music signal type (default)
                    ["BitRate"] = 32,
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "201BD7EF36658DB3DC9658C67E9AF71E", // Opus 1.1.2 (Ubuntu 18.04)
                    "B1D166A87EF5F03FDC80E8571B9FC70E", // Opus 1.3.1 (MacOS on ARM)
                    "8FC7BCF02EDB42E9785797FD2C9A71D6", // Opus 1.3.1 (MacOS, Ubuntu 20.04 and Windows on Intel)
                    "BD1629D7E9272CAA8AEAC20FD576B7C6" // Opus 1.3.1 (Windows on AMD)
                }
            },
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
                new[]
                {
                    "201BD7EF36658DB3DC9658C67E9AF71E", // Opus 1.1.2 (Ubuntu 18.04)
                    "B1D166A87EF5F03FDC80E8571B9FC70E", // Opus 1.3.1 (MacOS on ARM)
                    "8FC7BCF02EDB42E9785797FD2C9A71D6", // Opus 1.3.1 (MacOS, Ubuntu 20.04 and Windows on Intel)
                    "BD1629D7E9272CAA8AEAC20FD576B7C6" // Opus 1.3.1 (Windows on AMD)
                }
            },
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
                new[]
                {
                    "8AA73947B00AABCB3602F660E69263C5", // Opus 1.1.2 (Ubuntu 18.04)
                    "56A43E329A39A3CC568348116F5134F3", // Opus 1.3.1 (Ubuntu 20.04)
                    "84D72BBEF86EA7611518CF2862FC94BD", // Opus 1.3.1 (MacOS on Intel)
                    "3FBE634F53A90A6E5E26325A49BD20E5", // Opus 1.3.1 (MacOS on ARM)
                    "F7583CA097F3B8D7EE50587DB0C9B883", // Opus 1.3.1 (32-bit Windows on Intel)
                    "CE832392AA59C20C39D1C246A96E31AC", // Opus 1.3.1 (32-bit Windows on AMD)
                    "0C29F2F289C7817FC177201ECCA21BC9", // Opus 1.3.1 (64-bit Windows on Intel)
                    "EE24D1F3BCA4485768E29F8F842B8009" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new TestSettingDictionary
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "94AADBE44ECF34AF98982F17BCCE7C97", // Opus 1.1.2 (Ubuntu 18.04)
                    "9DA2DC35F1B9FBC9FAEF343788BB9E41", // Opus 1.3.1 (Ubuntu 20.04)
                    "0C6BF7ECB9F757DB8F8AF485137BD2C8", // Opus 1.3.1 (MacOS on Intel)
                    "DF1CE487259E093896FB4BDB74A6B1E3", // Opus 1.3.1 (MacOS on ARM)
                    "3CAB512976D0D62B45E2C9239B19735D", // Opus 1.3.1 (32-bit Windows on Intel)
                    "63CCEA9D4CF6847E5D6D29245372E93D", // Opus 1.3.1 (32-bit Windows on AMD)
                    "F82CC6FE2C86F5E7CD4A4F8F634E2DE9", // Opus 1.3.1 (64-bit Windows on Intel)
                    "A6ACF878EA0F60CF33EA9D214F814608" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new TestSettingDictionary
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "945B2B47707ED2C2DC2D30BD4AF75A34", // Opus 1.1.2 (Ubuntu 18.04)
                    "1CE1BF9EA0D643C0CDB9B8C8A1492B58", // Opus 1.3.1 (Ubuntu 20.04)
                    "B6029E9FC954A8722A1B10700F80EA19", // Opus 1.3.1 (MacOS on Intel)
                    "A9E1172F6E94329BC82ACAD85B83F3F0", // Opus 1.3.1 (MacOS on ARM)
                    "2B5AD4B688767C703DC63A83C3F96074", // Opus 1.3.1 (32-bit Windows on Intel)
                    "CE9599BAE9112521A52EC0749041D72C", // Opus 1.3.1 (32-bit Windows on AMD)
                    "AC629D5D7F3B004E75BE9F85D4B1DBD3", // Opus 1.3.1 (64-bit Windows on Intel)
                    "2844C8E28786F962FE6C611991FC3FFD" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new TestSettingDictionary
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album",
                    ["SerialNumber"] = 1
                },
                new[]
                {
                    "2D5A8223552F6A731413BD1DD181040A", // Opus 1.1.2 (Ubuntu 18.04)
                    "5B7586B7F6F2DFCCA28E3A1D4C75690A", // Opus 1.3.1 (Ubuntu 20.04)
                    "EF6A93BC1F10A15E5F45255F36E90F3A", // Opus 1.3.1 (MacOS on Intel)
                    "8AF396D9A1F7648C2FDC7DFCC1AF35F6", // Opus 1.3.1 (MacOS on ARM)
                    "509504BD68BD34AC79FECEA9073BAFB3", // Opus 1.3.1 (32-bit Windows on Intel)
                    "0AF4C3DCD598108BEA2671C1B784A10F", // Opus 1.3.1 (32-bit Windows on AMD)
                    "45911349F86066B37C0DBC7D6AC43890", // Opus 1.3.1 (64-bit Windows on Intel)
                    "2DA574E2734B174C0844847AAEF7643A" // Opus 1.3.1 (64-bit Windows on AMD)
                }
            }

            #endregion
        };

        public static TheoryData<int, string, string, TestSettingDictionary, string[]> Data
        {
            get
            {
                var results = new TheoryData<int, string, string, TestSettingDictionary, string[]>();
                foreach (var result in _data.Select((item, index) => (index,
                             (string) item[0],
                             (string) item[1],
                             (TestSettingDictionary) item[2],
                             (string[]) item[3])))
                    results.Add(result.index, result.Item2, result.Item3, result.Item4, result.Item5);
                return results;
            }
        }
    }
}