﻿/* Copyright © 2018 Jeremy Herbison

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
                new(),
                [
                    "818EE6CBF16F76F923D33650E7A52708"
                ]
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "Wave",
                new(),
                [
                    "509B83828F13945E4121E4C4897A8649"
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Wave",
                new(),
                [
                    "5D4B869CD72BE208BC7B47F35E13BE9A"
                ]
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Wave",
                new(),
                [
                    "EFBC44B9FA9C04449D67ECD16CB7F3D8"
                ]
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Wave",
                new(),
                [
                    "D55BD1987676A7D6C2A04BF09C10F64F"
                ]
            },
            {
                "A-law 44100Hz Stereo.wav",
                "Wave",
                new(),
                [
                    "47B031C7D8246F557E0CB37E4DB3F528"
                ]
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "Wave",
                new(),
                [
                    "FCA4C53C5F98B061CE9C9186A303D816"
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Mono.flac",
                "Wave",
                new(),
                [
                    "509B83828F13945E4121E4C4897A8649"
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo.flac",
                "Wave",
                new(),
                [
                    "5D4B869CD72BE208BC7B47F35E13BE9A"
                ]
            },
            {
                "FLAC Level 5 16-bit 48000Hz Stereo.flac",
                "Wave",
                new(),
                [
                    "EFBC44B9FA9C04449D67ECD16CB7F3D8"
                ]
            },
            {
                "FLAC Level 5 24-bit 96000Hz Stereo.flac",
                "Wave",
                new(),
                [
                    "D55BD1987676A7D6C2A04BF09C10F64F"
                ]
            },

#if !LINUX
            {
                "ALAC 16-bit 44100Hz Mono.m4a",
                "Wave",
                new(),
                [
                    "509B83828F13945E4121E4C4897A8649"
                ]
            },
            {
                "ALAC 16-bit 44100Hz Stereo.m4a",
                "Wave",
                new(),
                [
                    "5D4B869CD72BE208BC7B47F35E13BE9A"
                ]
            },
            {
                "ALAC 16-bit 48000Hz Stereo.m4a",
                "Wave",
                new(),
                [
                    "EFBC44B9FA9C04449D67ECD16CB7F3D8"
                ]
            },
            {
                "ALAC 24-bit 96000Hz Stereo.m4a",
                "Wave",
                new(),
                [
                    "D55BD1987676A7D6C2A04BF09C10F64F"
                ]
            },
#endif

            #endregion

            #region FLAC Encoding

            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "FLAC",
                new(),
                [
                    "42070347011D5067A9D962DA3237EF63", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "5C1655ACED0E208FC231C92C678FD87B" // FLAC 1.4.3
                ]
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "FLAC",
                new(),
                [
                    "0771EF09959F087FACE194A4479F5107", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "0B2A54ACAA983961889A83EF85942C5D" // FLAC 1.4.3
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new(),
                [
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "F86AA899D869835AB05C17699F7F30DC" // FLAC 1.4.3
                ]
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "FLAC",
                new(),
                [
                    "F0F075E05A3AFB67403CCF373932BCCA", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "4CF8F4F9A26506718F891820268284DD" // FLAC 1.4.3
                ]
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "FLAC",
                new(),
                [
                    "D3A7B834DCE97F0709AEFCA45A24F5B6", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "99355C1C3DD53802AB84425043CF1831" // FLAC 1.4.3
                ]
            },
            {
                "A-law 44100Hz Stereo.wav",
                "FLAC",
                new(),
                [
                    "10F6AC75659ECFE81D3C07D8D3074538", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "F7A6A7DC6610B277E302387A9E38FB32" // FLAC 1.4.3
                ]
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "FLAC",
                new(),
                [
                    "FCBAB8A8C261456CF6F87E603B237426", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "2ED7D0E99EA9072FB4415FF1510D3056" // FLAC 1.4.3
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "FLAC",
                new(),
                [
                    "2F2F341FEECB7842F7FA9CE6CB110C67", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "D75493F3FFF379D7A14F5B0BFA5AFB8A" // FLAC 1.4.3
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "FLAC",
                new(),
                [
                    "A48820F5E30B5C21A881E01209257E21", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "A7E0AA5D079DCBF97B3C03B76FA2E645" // FLAC 1.4.3
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "FLAC",
                new(),
                [
                    "D90693A520FA14AC987272ACB6CD8996", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "1ED46D08800E000E7D4FFD6B3E776C2C" // FLAC 1.4.3
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Default compression
                    ["CompressionLevel"] = 5
                },
                [
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "F86AA899D869835AB05C17699F7F30DC" // FLAC 1.4.3
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Minimum compression
                    ["CompressionLevel"] = 0
                },
                [
                    "A58022B124B427771041A96F65D8DF21", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "FED4E3CA9AB4BF04C528E16D6D19EB44" // FLAC 1.4.3
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Maximum compression
                    ["CompressionLevel"] = 8
                },
                [
                    "F341E56E68A0A168B779A4EBFD41422D", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "3C563D2DAF004BF8C01139C6D5A7CA8F" // FLAC 1.4.3
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Default seek point interval
                    ["SeekPointInterval"] = 10
                },
                [
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "F86AA899D869835AB05C17699F7F30DC" // FLAC 1.4.3
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Disabled seek points
                    ["SeekPointInterval"] = 0
                },
                [
                    "986464F3AC48E00D00B8ECF3AF3FD6BC", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "46D403F89E109773996223F339570D74" // FLAC 1.4.3
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Maximum seek point interval
                    ["SeekPointInterval"] = 600
                },
                [
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "F86AA899D869835AB05C17699F7F30DC" // FLAC 1.4.3
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Default padding
                    ["Padding"] = 8192
                },
                [
                    "734954C2D360CD6D6C4F7FE23F6970AF", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "F86AA899D869835AB05C17699F7F30DC" // FLAC 1.4.3
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Disabled padding
                    ["Padding"] = 0
                },
                [
                    "662592BD8B3853B6FEC4E188F7D0F246", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "A1A977792BBC0D92B12674A42AAE614F" // FLAC 1.4.3
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new()
                {
                    // Maximum padding
                    ["Padding"] = 16_775_369
                },
                [
                    "455753A51355171BF22CCC78647235B4", // FLAC 1.3.3 (Ubuntu 20.04 and 22.04)
                    "B74DC40E795B96904E4DE801F98F1162" // FLAC 1.4.3
                ]
            },

            #endregion

#if !LINUX

            #region ALAC Encoding

            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "24C88B615C59F3054FF0A44C677987FA", // MacOS 11
                    "50F7F27DBCCE5874118C3DE9B0F0306D"
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "04244493BA087CD69BAD989927BD1595", // MacOS 11
                    "4A2E22037B18F3318920EA47BA76825C"
                ]
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "8522CA9ADFA8E200FDF5936AEF62EA43", // MacOS 11
                    "C299C20C8EF4ED5B6B5664E6B81C3244"
                ]
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "1BB62FEE2C66905CFBB6FEC048BF9772", // MacOS 11
                    "26442948986C55394D8AE960E66101C3"
                ]
            },
            {
                "A-law 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "B55FDF0750DE71AA26781CA565222D05", // MacOS 11
                    "40626FB4389C8CF567C0BF74621036BD"
                ]
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "8D7D0F67E43BECA4B9EE8AC0D552C01F", // MacOS 11
                    "26B6F5A519F858AB695C5090E7B98451"
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "C88E17FEC4F9AE6C0F8ED21E423E60D3", // MacOS 11
                    "C8E2DD6861F837C845A52A4C34523C85"
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "826418694704C310B1FFFDE1D1874839", // MacOS 11
                    "FAF8B7679D0B2446D83BA248CB491410"
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "ALAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "E170FAAB89D07557FC15F472715168A0", // MacOS 11
                    "38406F719F6EF9E5F5D4E7862AA5C351"
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    // Different creation time
                    ["CreationTime"] = new DateTime(2016, 12, 1),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "B9F0B2D3BCB6FD612829084D8C42C2AA", // MacOS 11
                    "2F72E377036957C669D858AEA26DF62F"
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    // Different modification time
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2018, 12, 1)
                },
                [
                    "EF39C30FA5D1106F655DC55806D8CB44", // MacOS 11
                    "F57326FFFD308ED69B83F7F451938D55"
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    // Default padding (explicit)
                    ["Padding"] = 2048,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "04244493BA087CD69BAD989927BD1595", // MacOS 11
                    "4A2E22037B18F3318920EA47BA76825C"
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    // Disabled padding
                    ["Padding"] = 0,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "771DAECD27700570C845116672E7DACC", // MacOS 11
                    "03305CCE91A686386908415EF35BDE0D"
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "ALAC",
                new()
                {
                    // Maximum padding
                    ["Padding"] = 16_777_216,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "DF3747EF579BFC71DC15CC0399E8F347", // MacOS 11
                    "815E83D61745D4E117E12D31543C47BF"
                ]
            },

            #endregion

            #region Apple AAC Encoding

            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "D921E1D8B26ED8C0425B5F3DB207E221", // MacOS on Intel
                    "7E21540C472F59D17BB67D400BFD7991", // MacOS on ARM
                    "09CD8B8C8E9D8BC09121D8C9F871F9B7", // 32-bit Windows on Intel
                    "CF5AD69DADDCCE22612CD6FA8FB21897", // 32-bit Windows on AMD
                    "D281CFECEEBE5A14D0D3D953D12F71DC", // 64-bit Windows on Intel
                    "F1E4C1E4386B0DD4B233284E4E2363D8" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "395581695DE218B82419F68BA6D2E11E", // MacOS on Intel
                    "439F73E38831F4A10B5BD4F91F732A15", // MacOS on ARM
                    "9A0F6E1984B428F236E1209C13AED4D1", // 32-bit Windows on Intel
                    "7FABBF9DDF1A16701E57C6DD190485E0", // 32-bit Windows on AMD
                    "47189DECF29E68A40F60645F97714BE3", // 64-bit Windows on Intel
                    "C40C18B162E7429F60C62DF6A23071D6" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "9B639E598FBF1E82299E60D0260D9017", // MacOS on Intel
                    "13BB443AE1F220DF897615F96EF0A0D2", // MacOS on ARM
                    "CB39DFBF414790022574435C2D30297D", // 32-bit Windows on Intel
                    "EB0DA4A098888A34C2F77A2A65D2E337", // 32-bit Windows on AMD
                    "589C93B14B5A2C8EF39239949A7729FF", // 64-bit Windows on Intel
                    "CA58A161F472C9038E29DB08BC8B41CC" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "EC5C3156FCA0EE4883D221DE30D01F2C", // MacOS on Intel
                    "88908D8E665C16C5B6762CD1B14F8B7C", // MacOS on ARM
                    "E0C34EA1479C8979D3AF3A2C98D4E699", // 32-bit Windows on Intel
                    "06469BD31CF3F0B799D9E52BBEA00C72", // 32-bit Windows on AMD
                    "BA760ADA182CA749797AC1B978266CB1", // 64-bit Windows on Intel
                    "155983F671E0ACD36DC482A283A8EDBE" // 64-bit Windows on AMD
                ]
            },
            {
                "A-law 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "A31D5E1A431F346AB36174FC4862F6F3", // MacOS on Intel
                    "18E802BDE2DDECAAEF5628312EC47EC9", // MacOS on ARM
                    "6E08F885FEC4094041F6A0B4A02F10AB", // 32-bit Windows on Intel
                    "194D40FBCAE58B4A01095DD89CE70A2D", // 32-bit Windows on AMD
                    "061ECBD66434D647CFFC03337B4EFC5A", // 64-bit Windows on Intel
                    "B90925A2DA13F80969FB7E6FEF7E81E0" // 64-bit Windows on AMD
                ]
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "E29517B127D5D1EEFB28C08E746A32BF", // MacOS on Intel
                    "7B13752769B0DCF9A5054C462D18CB40", // MacOS on AMD
                    "D41235E8E642C5773C499DCE06A72CC8", // 32-bit Windows on Intel
                    "C7FD479EFD340F30F3DE645205FA3BC5", // 32-bit Windows on AMD
                    "03B3AE83638F10833E9A6ECC83FD720B", // 64-bit Windows on Intel
                    "437164350E18B65669094C81C9335C4E" // 64-bit Windows on AMD
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "BB98985FF7FF60D8681833A88093B2DE", // MacOS on Intel
                    "04ABF599CC89FC3478FAFD96DD393DBA", // MacOS on ARM
                    "7BAD797AA7C5F71C7168C24077271029", // 32-bit Windows on Intel
                    "01DF45A55B786EBDEFCFFFFFD58187DF", // 32-bit Windows on AMD
                    "A3DC2D21D29A05456284B0B8C09E1F94", // 64-bit Windows on Intel
                    "EB34F55D3D94E569E99C12BD8FCF5BBF" // 64-bit Windows on AMD
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "4FFFB1DD9D5BEB99B2DEFFF8A80DC933", // MacOS on Intel
                    "B1BD1E3A78B5E47CAAE8462B13720EDE", // MacOS on ARM
                    "61EC671C23EB3AC1A03029EB0BCDF257", // Legacy .NET on 32-bit Windows / Intel
                    "937750181287505A92B933F8A815D4C2", // .NET Core 3.0+ on 32-bit Windows / Intel
                    "E0B28BC27F9F86696F724F2D6E2540AA", // Legacy .NET on 32-bit Windows / AMD
                    "B6A891E92CF4D1FF747716CAB6F617DA", // .NET Core 3.0+ on 32-bit Windows / AMD
                    "D3113CF2C5283D2879F3DBEB617D050B", // Legacy .NET on 64-bit Windows / Intel
                    "A0705FB3CDB8E21E2477DE72DC0E2C7A", // .NET Core 3.0+ on 64-bit Windows / Intel
                    "067296B4F0A8B3DCE4AF3C48EA9B0C66", // Legacy .NET on 64-bit Windows / AMD
                    "CEFB86217E9AA94C807AE03273151C6E" // .NET Core 3.0+ on 64-bit Windows / AMD
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "AppleAAC",
                new()
                {
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "F1585045E793FEAA6CB69253475843F8", // MacOS on Intel
                    "B1BD1E3A78B5E47CAAE8462B13720EDE", // MacOS on ARM
                    "9AC3DEF9B464D0E1AB2D4F91C1A08B83", // 32-bit Windows on Intel
                    "4BB812252790CAEDE8CE8547E7BD546A", // 32-bit Windows on AMD
                    "DC77F4678649D575CE3E91DB950CDF55", // 64-bit Windows on Intel
                    "76BFBCEB5644AEA27C774D00D35A2B66" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    // Default VBR quality
                    ["VBRQuality"] = 9,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "395581695DE218B82419F68BA6D2E11E", // MacOS on Intel
                    "439F73E38831F4A10B5BD4F91F732A15", // MacOS on ARM
                    "9A0F6E1984B428F236E1209C13AED4D1", // 32-bit Windows on Intel
                    "7FABBF9DDF1A16701E57C6DD190485E0", // 32-bit Windows on AMD
                    "47189DECF29E68A40F60645F97714BE3", // 64-bit Windows on Intel
                    "C40C18B162E7429F60C62DF6A23071D6" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    // Minimum VBR quality
                    ["VBRQuality"] = 0,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "CBDAABA391433EDAF676BFEDCE1C0E2C", // MacOS on Intel
                    "E11E1E49987AA13BA2DE91237256AAAF", // MacOS on ARM
                    "78299761793D1A6EC79CBB9233156FD8", // 32-bit Windows on Intel
                    "E53BA332FDCFBE927A81040DB480688B", // 32-bit Windows on AMD
                    "93D67A9C673E7ABE3929846DBE5DBF97", // 64-bit Windows on Intel
                    "FC6792AF620BF4CFB49222B1747B4859" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    // Maximum VBR quality
                    ["VBRQuality"] = 14,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "888CFC0621E764E94FA60D0018E8E559", // MacOS on Intel
                    "D565DE5B4A908E077EEC1BB5B75C05C2", // MacOS on ARM
                    "7EDD94F25082AEEE82B2AA87E795AB6D", // 32-bit Windows on Intel
                    "9EFB3B60246E65F1C90A0880CF8905D9", // 32-bit Windows on AMD
                    "560039278EF9183F0FB2C47E5744E475", // 64-bit Windows on Intel
                    "288B9B3FE4FCC212491B9A370757FC46" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    // Minimum bit rate (stereo is automatically increased to 64)
                    ["BitRate"] = 32,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "67A06D818B49EBE273108839976FF1C3", // MacOS on Intel
                    "DAA69EC398F6E9E31B67B12DD735331F", // MacOS on ARM
                    "0177BB1DEB19854CA8495C4CBBB25366", // 32-bit Windows on Intel
                    "4EE479F602DD0FB162B19540B683B2BF", // 32-bit Windows on AMD
                    "DB44ACD7770861D4A3C6D7EE644C5E1C", // 64-bit Windows on Intel
                    "2D6F2B1043E1276764605C70E94A9EAE" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    // Minimum bit rate (mono)
                    ["BitRate"] = 32,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "C9B699DB0247E0F95DE4C3A39582C4D0", // MacOS
                    "9E77C0824474E3600F1A919715609A1B", // 32-bit Windows
                    "2321A80FDC5F36A1860523948548F8E5" // 64-bit Windows
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    // Maximum bit rate (stereo)
                    ["BitRate"] = 320,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "A466A0B76F8B02177B203CC21656D97C", // MacOS on Intel
                    "01245B8ECCFC4C4DA1F261F691A3FF37", // MacOS on ARM
                    "EBD496E30A953A8D0FE11C2609EFABC3", // 32-bit Windows on Intel
                    "AFEC3388275B59A08EFD11A9B32904FD", // 32-bit Windows on AMD
                    "A67A5F8D1A55CD2A29EEFA54E583AEA1", // 64-bit Windows on Intel
                    "58E298B32860D9A30E1F31B3D164A3AC" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    // Maximum bit rate (mono is automatically reduced to 256)
                    ["BitRate"] = 320,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "184DADDB750C82434E8B491A4E146DD5", // MacOS on Intel
                    "8B21060F2783BBD1029CB5E38489EE6F", // MacOS on ARM
                    "DE5F94EC1EACB75A3D049AE9960A7ACB", // 32-bit Windows on Intel
                    "C3A45A0F87C7E3A8BDAD6526CFA00ABF", // 32-bit Windows on AMD
                    "8F6858F8F86AA821789D926E0B4F63B6", // 64-bit Windows on Intel
                    "8853121A36B39C0A607B5FE219A8EFD8" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    // Constrained bit rate mode (default)
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [

                    "2E531801B06854D07EE21F128D15021D", // MacOS on Intel
                    "0557E7171818C5DC73C175F1F8DC7535", // MacOS on ARM
                    "B26C14FD53A4027C26FA3A57CB96AF4C", // 32-bit Windows
                    "EEEAF1FB2801EF0FB49B9B87350B5587", // 64-bit Windows on Intel
                    "60DEF5D19AF12B0962777B0B38CBA79A" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    // Constrained bit rate mode (explicit)
                    ["ControlMode"] = "Constrained",
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "2E531801B06854D07EE21F128D15021D", // MacOS on Intel
                    "0557E7171818C5DC73C175F1F8DC7535", // MacOS on ARM
                    "B26C14FD53A4027C26FA3A57CB96AF4C", // 32-bit Windows
                    "EEEAF1FB2801EF0FB49B9B87350B5587", // 64-bit Windows on Intel
                    "60DEF5D19AF12B0962777B0B38CBA79A" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    // Average bit rate mode
                    ["ControlMode"] = "Average",
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "5121C9A7D75A0718B62D7A83CAD441FE", // MacOS on Intel
                    "C17ED28BC045C9E5010DF361ADE59515", // MacOS on ARM
                    "B65D496ADABF3DBCDB24136A9655C295", // 32-bit Windows
                    "6AD4BD76918C74B976FD7774163CD7ED", // 64-bit Windows on Intel
                    "2B2F9D57CFA1F3A0D63D261AD4750468" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "AppleAAC",
                new()
                {
                    // Constant bit rate mode
                    ["ControlMode"] = "Constant",
                    ["BitRate"] = 128,
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "8FEA4642FF03F72EF3F4E35386EEDAE4", // MacOS on Intel
                    "FB38869E1A10C837554BCD0CCDBC660A", // MacOS on ARM
                    "365D7E965534C8690B4694B27D0CF1C9", // 32-bit Windows on Intel
                    "BCA45E90590A453EF4DBDCE3950C9CC4", // 32-bit Windows on AMD
                    "4E99289AFF43EA387442E30EAFB7305A", // 64-bit Windows on Intel
                    "85FBE50C3C18EFAB46DE754068F359BC" // 64-bit Windows on AMD
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "AppleAAC",
                new()
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track",
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "395581695DE218B82419F68BA6D2E11E", // MacOS on Intel
                    "439F73E38831F4A10B5BD4F91F732A15", // MacOS on ARM
                    "9A0F6E1984B428F236E1209C13AED4D1", // 32-bit Windows on Intel
                    "7FABBF9DDF1A16701E57C6DD190485E0", // 32-bit Windows on AMD
                    "47189DECF29E68A40F60645F97714BE3", // 64-bit Windows on Intel
                    "C40C18B162E7429F60C62DF6A23071D6" // 64-bit Windows on AMD
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "AppleAAC",
                new()
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track",
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "B189CBFFA17095BD978B3D9E1978C722", // MacOS on Intel
                    "F245069996E7B90FBC76C04515E7DB2C", // MacOS on ARM
                    "DDA8DBB070EA36F77455A41A2628B6AA", // 32-bit Windows on Intel
                    "9EBD64EEF7F1CB540012892515A3B0F5", // 32-bit Windows on AMD
                    "14145EA9D279E2FA457AD85F19DC0896", // 64-bit Windows on Intel
                    "A65D4DA9DB98F2DEF066508818C680CD" // 64-bit Windows on AMD
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "AppleAAC",
                new()
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album",
                    ["CreationTime"] = new DateTime(2017, 1, 31),
                    ["ModificationTime"] = new DateTime(2017, 1, 31)
                },
                [
                    "334BA4C5FA784B2899782C2173F34076", // MacOS on Intel
                    "AF4D1BAF1D9D5DFB5F4B6E5D5D50F423", // MacOS on ARM
                    "5502D724D98AA24FE49FA8AFB0FC63A6", // 32-bit Windows on Intel
                    "838B5CABD1F8E0077559E4DF504842DC", // 32-bit Windows on AMD
                    "90D1426E435372B957E6558E4DC5D7FD", // 64-bit Windows on Intel
                    "8107A1714777E243320A2E72F8F1D6D7" // 64-bit Windows on AMD
                ]
            },

            #endregion

#endif

            #region Lame MP3 Encoding

            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "LameMP3",
                new(),
                [
                    "F2BD0875E273743A8908F96DCCFDFC44", // Lame 3.100 (Ubuntu and MacOS)
                    "7CB68FB7ACC70E8CD928E7DB437B16FE" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "LameMP3",
                new(),
                [
                    "1CB5B915B3A72CBE76087E16F96A0A3E", // Lame 3.100 (Ubuntu and MacOS)
                    "537DE5BA83AAF6542B2E29C74D405EC2" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new(),
                [
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "LameMP3",
                new(),
                [
                    "1454732B48913F2A3898164BA366DA01", // Lame 3.100 (Ubuntu and MacOS)
                    "D6C2622620E83D442C80AADBE6B45921" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "LameMP3",
                new(),
                [
                    "AD56C3A1ACD627DBDA4B5A28AFE0355D", // Lame 3.100 (Ubuntu and MacOS)
                    "5BCC0ED414809596507ECFEDEBD4454D" // Lame 3.100 (Windows)
                ]
            },
            {
                "A-law 44100Hz Stereo.wav",
                "LameMP3",
                new(),
                [
                    "0190385E444B8576C297E1DE837279F1", // Lame 3.100 (Ubuntu and MacOS)
                    "C43AEE67905D09300EE49323D6330426" // Lame 3.100 (Windows)
                ]
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "LameMP3",
                new(),
                [
                    "3CE431DA62AC5204B9FAE63BD8E2B4A8", // Lame 3.100 (Ubuntu and MacOS)
                    "CC60AD39342F059B4F590988F192FE8D" // Lame 3.100 (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new(),
                [
                    "F737A24D4F60E5B3229689CC15FF10EE", // Lame 3.100 (Ubuntu and MacOS)
                    "EA2FE7549FB7A1971265FA27B88D0285" // Lame 3.100 (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "LameMP3",
                new(),
                [
                    "FB1B7DECB2C2A2C9CAA1FBB917A81472", // Lame 3.100 (MacOS and Legacy .NET on Ubuntu)
                    "BA2607DC22DBB4BA6DD592CCA6E7C994", // Lame 3.100 (.NET Core 3.0+ on Ubuntu)
                    "D888321C2C776E245434E50FDF61A6E8", // Lame 3.100 (Legacy .NET on 32-bit Windows)
                    "A286239868A51F5CD05540C198EA96B2", // Lame 3.100 (Legacy .NET on 64-bit Windows)
                    "8BCAB166E1DEE1873AFEFC4DCCCBE333" // Lame 3.100 (.NET Core 3.0+ on Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "LameMP3",
                new(),
                [
                    "FB1B7DECB2C2A2C9CAA1FBB917A81472", // Lame 3.100 (Ubuntu and MacOS)
                    "A4306E31052226EFD081D5D5FA80F62B" // Lame 3.100 (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Default tag version
                    ["TagVersion"] = "2.3"
                },
                [
                    "F737A24D4F60E5B3229689CC15FF10EE", // Lame 3.100 (Ubuntu and MacOS)
                    "EA2FE7549FB7A1971265FA27B88D0285" // Lame 3.100 (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Tag version 2.4
                    ["TagVersion"] = "2.4"
                },
                [
                    "F69CCDFC32565F97130CBAEABFF0D13C", // Lame 3.100 (Ubuntu and MacOS)
                    "BAB786013527E61F4719BBC1F6682C92" // Lame 3.100 (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Default tag encoding
                    ["TagEncoding"] = "Latin1"
                },
                [
                    "F737A24D4F60E5B3229689CC15FF10EE", // Lame 3.100 (Ubuntu and MacOS)
                    "EA2FE7549FB7A1971265FA27B88D0285" // Lame 3.100 (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // UTF-16 tag encoding
                    ["TagEncoding"] = "UTF16"
                },
                [
                    "EA1232E970C83FCDDE00D4C1D51F0446", // Lame 3.100 (Ubuntu and MacOS)
                    "B3402C50D24A82004D50DC0172E81BC1" // Lame 3.100 (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // UTF-8 tag encoding, implicit tag version 2.4
                    ["TagEncoding"] = "UTF8"
                },
                [
                    "388108BB7EE76567E9869F4CE9786CE9", // Lame 3.100 (Ubuntu and MacOS)
                    "B0AC00BF6DFDA60BAD712FD3F9DFED21" // Lame 3.100 (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // UTF-8 tag encoding, explicit tag version 2.4
                    ["TagVersion"] = "2.4",
                    ["TagEncoding"] = "UTF8"
                },
                [
                    "388108BB7EE76567E9869F4CE9786CE9", // Lame 3.100 (Ubuntu and MacOS)
                    "B0AC00BF6DFDA60BAD712FD3F9DFED21" // Lame 3.100 (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // UTF-8 tag encoding, ignored tag version 2.3
                    ["TagVersion"] = "2.3",
                    ["TagEncoding"] = "UTF8"
                },
                [
                    "388108BB7EE76567E9869F4CE9786CE9", // Lame 3.100 (Ubuntu and MacOS)
                    "B0AC00BF6DFDA60BAD712FD3F9DFED21" // Lame 3.100 (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Default tag padding (explicit)
                    ["TagPadding"] = 2048
                },
                [
                    "F737A24D4F60E5B3229689CC15FF10EE", // Lame 3.100 (Ubuntu and MacOS)
                    "EA2FE7549FB7A1971265FA27B88D0285" // Lame 3.100 (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Tag padding disabled
                    ["TagPadding"] = 0
                },
                [
                    "ED3A9531742553641B112C0D0A41F099", // Lame 3.100 (Ubuntu and MacOS)
                    "ADA8A213D1219A92937453878EEA3D18" // Lame 3.100 (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Maximum tag padding
                    ["TagPadding"] = 16_777_216
                },
                [
                    "6645726904A761FDF324711CFD21D477", // Lame 3.100 (Ubuntu and MacOS)
                    "47A50D9D488F78F43577BB5BBA0BD783" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Tag version does nothing without metadata
                    ["TagVersion"] = "2.4"
                },
                [
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Tag encoding does nothing without metadata
                    ["TagEncoding"] = "UTF16"
                },
                [
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Tag padding does nothing without metadata
                    ["TagPadding"] = 100
                },
                [
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Default VBR quality
                    ["VBRQuality"] = 3
                },
                [
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Minimum VBR quality
                    ["VBRQuality"] = 9
                },
                [
                    "65D418A236D86A8CE33E07A76C98DF08", // Lame 3.100 (Ubuntu and MacOS)
                    "BB8B33BD589DA49D751C883B8A0FF653" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Maximum VBR quality
                    ["VBRQuality"] = 0
                },
                [
                    "5DE234656056DFDAAD30E4DA9FD26366", // Lame 3.100 (Ubuntu and MacOS)
                    "3B10B6430B2A823C58F16953F5B33E9C" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Minimum bit rate
                    ["BitRate"] = 8
                },
                [
                    "2BBC83E74AB1A4EB150BC6E1EB9920B5", // Lame 3.100 (Ubuntu and MacOS)
                    "2ACEB0816512B4300D13E9329F76D752" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Maximum bit rate
                    ["BitRate"] = 320
                },
                [
                    "BEB5029A08011BCEDFFA99173B763E7F", // Lame 3.100 (Ubuntu and MacOS)
                    "E9525D2505684DDB3F9FDDE7B550577E" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Forced bit rate disabled (default)
                    ["BitRate"] = 128
                },
                [
                    "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6", // Lame 3.100 (Ubuntu and MacOS)
                    "54E8544993C1E818C72DE5AC00DEABF5" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Forced bit rate explicitly disabled
                    ["BitRate"] = 128,
                    ["ForceCBR"] = false
                },
                [
                    "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6", // Lame 3.100 (Ubuntu and MacOS)
                    "54E8544993C1E818C72DE5AC00DEABF5" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Forced bit rate enabled
                    ["BitRate"] = 128,
                    ["ForceCBR"] = true
                },
                [
                    "EACCA2FD6404ACA1AB46027FAE6A667B", // Lame 3.100 (Ubuntu and MacOS)
                    "AAE7CB7E1E4EAAF5ED19F8B986647298" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // Forced bit rate ignored without bit rate
                    ["ForceCBR"] = true
                },
                [
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // VBR quality ignored with bit rate
                    ["VBRQuality"] = 3,
                    ["BitRate"] = 128
                },
                [
                    "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6", // Lame 3.100 (Ubuntu and MacOS)
                    "54E8544993C1E818C72DE5AC00DEABF5" // Lame 3.100 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new()
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track"
                },
                [
                    "10E44CEE38E66E9737677BE52E7A286D", // Lame 3.100 (Ubuntu and MacOS)
                    "EF2FAA877F1DE84DC87015F841103263" // Lame 3.100 (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track"
                },
                [
                    "8D5A9BA2D974BCDEFB50A3DA7E134335", // Lame 3.100 (MacOS)
                    "49CB061F4DE93D7F88D3B656458C7003", // Lame 3.100 (Ubuntu)
                    "3AE3B2DA82CE86ABDA69CFBB06EBDB6E", // Lame 3.100 (Legacy .NET on Windows)
                    "F3F89801873BDC7D9E403C160051B457" // Lame 3.100 (.NET Core 3.0+ on Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                new()
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album"
                },
                [
                    "94E8EF913253B83D35E760A8C81C18BA", // Lame 3.100 (MacOS)
                    "E804988EFC1EA58704E6C78B42CE1DF6", // Lame 3.100 (Ubuntu)
                    "85C1262D46F863436B5C55B71C8C0B31", // Lame 3.100 (Legacy .NET on Windows)
                    "026C2587049104F0FFF72EB8E49F87AB" // Lame 3.100 (.NET Core 3.0+ on Windows)
                ]
            },

            #endregion

            #region Ogg Vorbis Encoding

            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "00502CA1BD9BE8137FCF75518D8EC5AC", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "2BEFC8DFC5C86F305FBB1126CC2B2D0D", // Vorbis 1.3.7 (MacOS on Intel)
                    "F187C28A4A37DF469CF96EBC8565CC21", // Vorbis 1.3.7 (MacOS on ARM)
                    "E6FBBBCE3847BC9C9EB45A89A7D7DDA6" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "1A3784BD4B5A5F7324F0F19119EC8829", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "EFC7217481D26BB0828C6E4BE7D2D414", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "E8B8006C5EC2A3D50555B85F367F4922", // Vorbis 1.3.7 (MacOS on Intel)
                    "4759435E55E2DF81B20F6DD929330EE5", // Vorbis 1.3.7 (MacOS on ARM)
                    "62C6F8889AA6CBE4A80750EFF33D9FDA", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "3C12BC37F57BB2C4542A92ABD52EA27D" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "F041B26255F501A83ECD8C6C7EBCE55D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "C2C9FE067F40025DDF41B868350F28A5", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "7757FDE8D2A84D6AA7406F9CA3D840D3", // Vorbis 1.3.7 (MacOS on Intel)
                    "E912A2A65C6272335DF116937A0549A6", // Vorbis 1.3.7 (MacOS on ARM)
                    "57E0997F13613A8C64306230A031D912" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "AA03C92A5ED981484D17EA96D87B9331", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "BDCF3199D9F7457000B8D2BDFB757579", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "6D4108CB330E72038EA67B4D7B335AF9", // Vorbis 1.3.7 (MacOS on Intel)
                    "C1D824314622416360560019BD79F519", // Vorbis 1.3.7 (MacOS on ARM)
                    "A75601BC8CABA65EADD866211A1EF536" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "5C6A8AC9FE704864E8ABC4F1654F65D7", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "F796497F75425D1928C66B88F3F24232", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "239E03B5B24AA65E6335303DF50FB3A2", // Vorbis 1.3.7 (MacOS on Intel)
                    "2A14B596BBE04A8391249A4276753F10", // Vorbis 1.3.7 (MacOS on ARM)
                    "5EF3AF9F00AB434F1A0A08482DE2C0DE" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            },
            {
                "A-law 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "9153B9D26FA0A06AD1515B5660055CA3", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "6DD98044F88A9535595D2B2D16D1A787", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "D48672C42031B4419ADC0F420D503A47", // Vorbis 1.3.7 (MacOS on Intel)
                    "2C8DB7597ED25C3494B89E5FBCC75508", // Vorbis 1.3.7 (MacOS on ARM)
                    "DB770934E3E57E832BFFA1C1C85E065F" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "9A93109BCB1025791DCA8B63E9EFE3B6", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "3E40EB8AEB86CA5E3C76525E1E03D0FD", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "4721899D9DDAF7394B1A679F753F81CF", // Vorbis 1.3.7 (MacOS on Intel)
                    "856219EF1D9C77A64FE0994B7373C848", // Vorbis 1.3.7 (MacOS on ARM)
                    "4E69AF464154872795B7AD87BA762870", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "38EB06F6C7055EC41221129C2440E823" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "91ADF87CA8AE3D1669EA65D30BB083D8", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "19AF57343851DC074CDD23039C577BC2", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "F6D78BE21567DD9244848160A2BD3889", // Vorbis 1.3.7 (MacOS on Intel)
                    "ED7CAFD6D69FFF643E01219D1E9696E7", // Vorbis 1.3.7 (MacOS on ARM)
                    "AB097FDBAA6B8516129A17ED1C5BDC21" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "234B57CFDB48E7C3B793D17436AA53E9", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "BD1777F28C923D15E00840C2725EB14A", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "ECE596D89D40FBD4573DD17588F5C962", // Vorbis 1.3.7 (MacOS on Intel)
                    "CD4B255CDC3C689C8A2242FDE5F912C7", // Vorbis 1.3.7 (MacOS on ARM)
                    "7A82FD0ADD477568D0FA4B84BE1CFDD9", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on 32-bit Windows)
                    "45E719C489D35DD1CC902143B578553D", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on 64-bit Windows)
                    "467CEB127C359E6B75C045FAD1F7989A" // Vorbis 1.3.7 AoTuV + Lancer (.NET Core 3.0+ on Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "Vorbis",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "0AA286B6A6896263E9182853FFA2DD52", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "8A4B07CF14C30DF85BCBDCD91A597424", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "AD172F94045E0970A71F8C439EEEA6C9", // Vorbis 1.3.7 (MacOS on Intel)
                    "CD4B255CDC3C689C8A2242FDE5F912C7", // Vorbis 1.3.7 (MacOS on ARM)
                    "F25060DBEAB99B219CDB5EC54D37AD1C" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Minimum serial #
                    ["SerialNumber"] = int.MinValue
                },
                [
                    "ABC75E49AF00624847D66A441068199F", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "8EB74661170103394BB0737E102B978A", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "88C186B7AA3DC3A55CD430FFF8AADCB1", // Vorbis 1.3.7 (MacOS on Intel)
                    "E7BE0862210C0010541BE605901D4A6D", // Vorbis 1.3.7 (MacOS on ARM)
                    "8224BAEA06E788DF37EFC01CCCD479C8" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Maximum serial #
                    ["SerialNumber"] = int.MaxValue
                },
                [
                    "9B499871B3788480810EB00DDF08054F", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "171C2CA1CDB8C004B15A4D1D6C2C7375", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "74D8326F97A08EBCCC7FF754FE37464F", // Vorbis 1.3.7 (MacOS on Intel)
                    "9DEA359E04D151021DC995764B8C428E", // Vorbis 1.3.7 (MacOS on ARM)
                    "C77B164D4E60453D679F35551FD3BF02" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Default quality (explicit)
                    ["Quality"] = 5,
                    ["SerialNumber"] = 1
                },
                [
                    "F041B26255F501A83ECD8C6C7EBCE55D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "C2C9FE067F40025DDF41B868350F28A5", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "7757FDE8D2A84D6AA7406F9CA3D840D3", // Vorbis 1.3.7 (MacOS on Intel)
                    "E912A2A65C6272335DF116937A0549A6", // Vorbis 1.3.7 (MacOS on ARM)
                    "57E0997F13613A8C64306230A031D912" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Minimum quality
                    ["Quality"] = -1,
                    ["SerialNumber"] = 1
                },
                [
                    "E3B0B6AE0805022FAA88EC50199E9D05", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "DFDCDBBC24E1DC2EE1411CE40D7DF3FE", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "9DEF0BAB12400A25E06EA7CA8C32CCC6", // Vorbis 1.3.7 (MacOS on Intel)
                    "483C59AF294602813322BFF28BF554A5", // Vorbis 1.3.7 (MacOS on ARM)
                    "79C966C3D6728C49723640C0D7B9330B", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "55B7584D23DE2667374ABB9C3C571875" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)

                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Maximum quality
                    ["Quality"] = 10,
                    ["SerialNumber"] = 1
                },
                [
                    "08A958E00D3797F33ABB2F098556DE43", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "EC722CED6079EC8609552686C911FFFE", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "1C21FEE55AC987FE8ACA1865353DC833", // Vorbis 1.3.7 (MacOS on Intel)
                    "25AEE91A747018638A34DC630D24B3FC", // Vorbis 1.3.7 (MacOS on ARM)
                    "4B2B694BD0D42994F4A1911FBCB2ABF8", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "9E45D7B37055FD59650839F5BEAB1ED0" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Minimum bit rate
                    ["BitRate"] = 45,
                    ["SerialNumber"] = 1
                },
                [
                    "749518EA6F95A089787DD414855DDBC0", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "68B19B197BDAFA73A45EBF67CD961CA9", // Vorbis 1.3.7 (MacOS on Intel)
                    "1CBA6651221C34E9ECDB314A653D05E0", // Vorbis 1.3.7 (MacOS on ARM)
                    "CAEAE4C932830A8C0A41BC5C79DC80D5", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "53E545072EC0FDA11D100BA1DE9EBC0A" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Maximum bit rate
                    ["BitRate"] = 500,
                    ["SerialNumber"] = 1
                },
                [
                    "0E233828AF5FD1010188AC4C63DF40BC", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "057D14E2F3F8C9F2FD44EE29222A7BD3", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "D37BB84F2008B1B467D54C618495C4CE", // Vorbis 1.3.7 (MacOS on Intel)
                    "9631DB55B39F79E82ECC05E27EE8928B", // Vorbis 1.3.7 (MacOS on ARM)
                    "215FA0E953F4BB520A46A3B44B68CC92", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "8E0D0279037DC9DCC286050A2930427A" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Forced bit rate disabled (default)
                    ["BitRate"] = 128,
                    ["SerialNumber"] = 1
                },
                [
                    "70ECB3839EC4DC7A7ECEA48241E2407D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "577FA5EA45715260728A894592EEAED9", // Vorbis 1.3.7 (MacOS on Intel)
                    "5008E6BEB9E483B5190D778129D5C1C4", // Vorbis 1.3.7 (MacOS on ARM)
                    "84E389F08890621CF00AF8DD2D7C77DB", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "EC75AF6E1F0EFEA7E87ED7B40EFA415A" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Forced bit rate disabled (explicit)
                    ["BitRate"] = 128,
                    ["ForceCBR"] = false,
                    ["SerialNumber"] = 1
                },
                [
                    "70ECB3839EC4DC7A7ECEA48241E2407D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "577FA5EA45715260728A894592EEAED9", // Vorbis 1.3.7 (MacOS on Intel)
                    "5008E6BEB9E483B5190D778129D5C1C4", // Vorbis 1.3.7 (MacOS on ARM)
                    "84E389F08890621CF00AF8DD2D7C77DB", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "EC75AF6E1F0EFEA7E87ED7B40EFA415A" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Forced bit rate enabled
                    ["BitRate"] = 128,
                    ["ForceCBR"] = true,
                    ["SerialNumber"] = 1
                },
                [
                    "E67F4AB32814D96B1BA65652E5E11E56", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "D23564C9639B7C6490E59163C37B9C83", // Vorbis 1.3.7 (MacOS on Intel)
                    "D4974D63DD5B7326BD2143801C3DC610", // Vorbis 1.3.7 (MacOS on ARM)
                    "C84819FCFA2F25FCDB3E5490E54949B4", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "3FA511E9C941DFB1E61A98418C27F383" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Forced bit rate ignored without bit rate
                    ["ForceCBR"] = true,
                    ["SerialNumber"] = 1
                },
                [
                    "F041B26255F501A83ECD8C6C7EBCE55D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "C2C9FE067F40025DDF41B868350F28A5", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "7757FDE8D2A84D6AA7406F9CA3D840D3", // Vorbis 1.3.7 (MacOS on Intel)
                    "E912A2A65C6272335DF116937A0549A6", // Vorbis 1.3.7 (MacOS on ARM)
                    "57E0997F13613A8C64306230A031D912" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // Quality ignored with bit rate
                    ["Quality"] = 3,
                    ["BitRate"] = 128,
                    ["SerialNumber"] = 1
                },
                [
                    "70ECB3839EC4DC7A7ECEA48241E2407D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "577FA5EA45715260728A894592EEAED9", // Vorbis 1.3.7 (MacOS on Intel)
                    "5008E6BEB9E483B5190D778129D5C1C4", // Vorbis 1.3.7 (MacOS on ARM)
                    "84E389F08890621CF00AF8DD2D7C77DB", // Vorbis 1.3.7 AoTuV + Lancer (Windows on Intel)
                    "EC75AF6E1F0EFEA7E87ED7B40EFA415A" // Vorbis 1.3.7 AoTuV + Lancer (Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Vorbis",
                new()
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                [
                    "F041B26255F501A83ECD8C6C7EBCE55D", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "C2C9FE067F40025DDF41B868350F28A5", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "7757FDE8D2A84D6AA7406F9CA3D840D3", // Vorbis 1.3.7 (MacOS on Intel)
                    "E912A2A65C6272335DF116937A0549A6", // Vorbis 1.3.7 (MacOS on ARM)
                    "57E0997F13613A8C64306230A031D912" // Vorbis 1.3.7 AoTuV + Lancer (Windows)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new()
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                [
                    "39443EC16B23C3B5159CD199E83AD092", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu)
                    "466D9535B74B53EA088F13FF9720268C", // Vorbis 1.3.7 (MacOS on Intel)
                    "A637C5C3C53C717092D518B1F47963D5", // Vorbis 1.3.7 (MacOS on ARM)
                    "CE264095289A42A1FB038E6F44D5F007", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on Windows / Intel)
                    "E698F1678A8FBC0A6834B6E7230743DA", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on Windows / AMD)
                    "805F62BDFE149898E21C9448F4335BAC", // Vorbis 1.3.7 AoTuV + Lancer (.NET Core 3.0+ on Windows / Intel)
                    "64478AA7F8E2052608AE10624A3C396B" // Vorbis 1.3.7 AoTuV + Lancer (.NET Core 3.0+ on Windows / AMD)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Vorbis",
                new()
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album",
                    ["SerialNumber"] = 1
                },
                [
                    "7FF678CC731484D0B16E7FC01301EE7E", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on Intel)
                    "9F4297E686E3A938051AB8C753476521", // Vorbis 1.3.7 AoTuV + Lancer (Ubuntu on AMD)
                    "488A2980F20619FE0206CFBB1767CBAC", // Vorbis 1.3.7 (MacOS on Intel)
                    "8F7096C15F10B9EB3CA0DFA0E044877E", // Vorbis 1.3.7 (MacOS on ARM)
                    "4BCFD0F13F55B657F36304BBE3F41D39", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on Windows / Intel)
                    "F4C820158442E1ECEEBBCEFF42855E5F", // Vorbis 1.3.7 AoTuV + Lancer (Legacy .NET on Windows / AMD)
                    "34BA39848B7D78D7FE1D2B30999DF6A9", // Vorbis 1.3.7 AoTuV + Lancer (.NET Core 3.0+ on Windows / Intel)
                    "F1F317505EE9AD557FBF2DB31777AAAB" // Vorbis 1.3.7 AoTuV + Lancer (.NET Core 3.0+ on Windows / AMD)
                ]
            },

            #endregion

            #region Opus Encoding

            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "2D31A0D707D3A9AB865DB00095EB08AE", // Opus 1.3.1 (Ubuntu 22.04)
                    "4477BA0E09EF52AC239EE55FF44DA2A0", // Opus 1.5.2 (MacOS on Intel)
                    "FB641D1DA3D800E22BD22C7983D83325", // Opus 1.3.1 (MacOS on ARM)
                    "47359005DE5203C8AF0FDDE7F34713D9", // Opus 1.5.2 (32-bit Windows on Intel)
                    "ADB9E5F7EADCD228D63F97351F7456B0", // Opus 1.5.2 (32-bit Windows on AMD)
                    "A2EDE18F0E554FDC9EE5DD9C62622236", // Opus 1.5.2 (64-bit Windows on Intel)
                    "BA67EF14D54C77C508A44C0B08D01261" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "C22E87F617B51970785E9A4C43C9FC48", // Opus 1.3.1 (Ubuntu 22.04)
                    "A96ECE9F6FC5F2977202F2489A105967", // Opus 1.5.2 (MacOS on Intel)
                    "86C499288EADAAB469F3E7FA63628AD1", // Opus 1.3.1 (MacOS on ARM)
                    "6F0174BCFBF9AE02ACFCC0B2E3B834B8", // Opus 1.5.2 (Windows on Intel)
                    "D40EF4C25A4F36F96A715D2C2D5084BF" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "0C6BF7ECB9F757DB8F8AF485137BD2C8", // Opus 1.3.1 (Ubuntu 22.04)
                    "54A2DC75ED7879D6B890D50F4CB34603", // Opus 1.5.2 (MacOS on Intel)
                    "DF1CE487259E093896FB4BDB74A6B1E3", // Opus 1.3.1 (MacOS on ARM)
                    "E105170CA90D2D427A6431ACB8D5A16C", // Opus 1.5.2 (32-bit Windows on Intel)
                    "8A9F6B6464C10B76DC880BAF4DF1AA3B", // Opus 1.5.2 (32-bit Windows on AMD)
                    "BF310174502CDAD081F13998FB901865", // Opus 1.5.2 (64-bit Windows on Intel)
                    "AB19CD2D3E02D331600240F8FC9D56CC" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "BD5F090F921BCC80F05FCBF5725D8E0E", // Opus 1.3.1 (Ubuntu 22.04)
                    "FB5BA2E0CF0A5738942A622FEE822D4E", // Opus 1.5.2 (MacOS on Intel)
                    "5074AE45A08E36CAF8FC0AB1B50F117C", // Opus 1.3.1 (MacOS on ARM)
                    "AB158A17CCED018828790DAA396BAB93", // Opus 1.5.2 (32-bit Windows on Intel)
                    "348B339AB9B26FC3185112CFFC784479", // Opus 1.5.2 (32-bit Windows on AMD)
                    "F4D51B165CDACF9CC1740A023FC832F7", // Opus 1.5.2 (64-bit Windows on Intel)
                    "7136AEEC7C21613DC83A006AA79DC260" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "8A8FA9C452D9EBBCF8554EE3E270A538", // Opus 1.3.1 (Ubuntu 22.04)
                    "2821951FD04D0E164FBF6E6604B85F86", // Opus 1.5.2 (MacOS on Intel)
                    "FC74891978B6213CDF3807C6D0130E82", // Opus 1.3.1 (MacOS on ARM)
                    "DA42257349612217D56C05879F45DD52", // Opus 1.5.2 (32-bit Windows on Intel)
                    "66F597B809F3331DEA291B8F147A3B6B", // Opus 1.5.2 (32-bit Windows on AMD)
                    "48D4A34C2C1BB5B6F1230BFE64764ED6", // Opus 1.5.2 (64-bit Windows on Intel)
                    "FD5836D8C677561A65CA77CB9A2898EA" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "A-law 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "CD27FFC8398F52FB2F5984085D2215AC", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "2018DEFDE09A503E0734CD57B0E19FC4", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "5A972613B661C285E44AB07D8416DC0A", // Opus 1.5.2 (MacOS on Intel)
                    "4190DC99A4B49887AA3E5BC9E15DC1C8", // Opus 1.3.1 (MacOS on ARM)
                    "8BB79D2D7415D4082679D416B2F41D93", // Opus 1.5.2 (32-bit Windows on Intel)
                    "CA97D1381E6BC36AB0A1880339C6A0E7", // Opus 1.5.2 (32-bit Windows on AMD)
                    "AE7BD2C79045C6DD3AFD6D20A98902D6", // Opus 1.5.2 (64-bit Windows on Intel)
                    "0BBEC9BAABF1C61003A65E6174CE7A71" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "µ-law 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "9ED53495B32D47496C1C88B75D2FEF5F", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "EF85B330CBC8A8931C2827A81F2E1532", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "D432F9BAABD834950984D4DFB1F446FA", // Opus 1.5.2 (MacOS on Intel)
                    "5A928AED6645BDF1A8CBA3C2EEB7E90F", // Opus 1.3.1 (MacOS on ARM)
                    "F8C93268C74883B727D78FBDB38F831F", // Opus 1.5.2 (32-bit Windows on Intel)
                    "7691A7F85D7F6FBFC3C3C1AA4F1D291A", // Opus 1.5.2 (32-bit Windows on AMD)
                    "DCB3078A6720C49D9726C64C0DCCCD7E", // Opus 1.5.2 (64-bit Windows on Intel)
                    "7300DC516256ED2274BDCE4F782872E2" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "5501D7D9730B0722C310D5263AFBEB77", // Opus 1.3.1 (Ubuntu 22.04)
                    "C110F6EFE538B370E49ECBAE4B40C8C2", // Opus 1.5.2 (MacOS on Intel)
                    "CB28DA52EF8451F944D8ED642D438292", // Opus 1.3.1 (MacOS on ARM)
                    "C461EAB699527B90A919D2B26979A30A", // Opus 1.5.2 (32-bit Windows on Intel)
                    "F8E8DE9329CE7869197512B31ABCB9EC", // Opus 1.5.2 (32-bit Windows on AMD)
                    "F6F88CF0DC907202CE5F91CC706A8ADD", // Opus 1.5.2 (64-bit Windows on Intel)
                    "3C64C7A9670C2DACEE22ED461A1FC1C5" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - PNG).flac",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "5D472AE652EB5A8ADCAE3E6CEF843B45", // Opus 1.3.1 (Ubuntu 22.04 on Intel)
                    "AFE05CB9E10ECE0CE4A267B01E91AA7C", // Opus 1.3.1 (Ubuntu 22.04 on AMD)
                    "1F562788793531FA94B5BC715886C983", // Opus 1.5.2 (MacOS on Intel)
                    "2D7ABAB485382CFA8AD0DA8437304185", // Opus 1.3.1 (MacOS on ARM)
                    "A7DBF7182ACFDB5C1A608F24C89BEAB3", // Opus 1.5.2 (Legacy .NET on 32-bit Windows / Intel)
                    "F24CCDB06BAFA257E2276FC09D2E3100", // Opus 1.5.2 (.NET Core 3.0+ on 32-bit Windows / Intel)
                    "2005EA5C8571256292391346D1153012", // Opus 1.5.2 (Legacy .NET on 32-bit Windows / AMD)
                    "14EB0889A6F92036165A3676F9D7E240", // Opus 1.5.2 (.NET Core 3.0+ on 32-bit Windows / AMD)
                    "E2E73DC93259B672C7307ACE9B5AA14E", // Opus 1.5.2 (Legacy .NET on 64-bit Windows / Intel)
                    "4044A896DE26E13DB1979ACBCF85084A", // Opus 1.5.2 (.NET Core 3.0+ on 64-bit Windows / Intel)
                    "A948C31D62A79D98DE51E05CF03BE8AD", // Opus 1.5.2 (Legacy .NET on 64-bit Windows / AMD)
                    "0440A8288CD63A81D633ADDC2EC4435F" // Opus 1.5.2 (.NET Core 3.0+ on 64-bit Windows / AMD)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (PICTURE block - JPEG).flac",
                "Opus",
                new()
                {
                    ["SerialNumber"] = 1
                },
                [
                    "0ADBC105EA51C62CB1ED53B978B33415", // Opus 1.3.1 (Ubuntu 22.04)
                    "BA2DB653CD22442197C80828E35B694E", // Opus 1.5.2 (MacOS on Intel)
                    "2D7ABAB485382CFA8AD0DA8437304185", // Opus 1.3.1 (MacOS on ARM)
                    "AA83D462884ABCC559AFAB31D08652CA", // Opus 1.5.2 (32-bit Windows on Intel)
                    "2B14B7A07466F519B4411D1E21A16DEB", // Opus 1.5.2 (32-bit Windows on AMD)
                    "BDD151A5D31382F430EA2139D18DD46A", // Opus 1.5.2 (64-bit Windows on Intel)
                    "9A0971BA58A017AC3C01ED4AAC60FFE7" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Minimum serial #
                    ["SerialNumber"] = int.MinValue
                },
                [
                    "CDE65307AACEA4F40F22FF925622D14E", // Opus 1.3.1 (Ubuntu 22.04)
                    "6C30D9BB7917BAEB1934A89C4874ADAC", // Opus 1.5.2 (MacOS on Intel)
                    "F2FDC3B2D196D551919F3FC85120DA15", // Opus 1.3.1 (MacOS on ARM)
                    "C768F0F87DAD97B1FF7C0A03FE6C2950", // Opus 1.5.2 (32-bit Windows on Intel)
                    "42E4106E48A192A9BC6D2EDE7B9C9A49", // Opus 1.5.2 (32-bit Windows on AMD)
                    "828B1323F1F7F6E9589249130098DB64", // Opus 1.5.2 (64-bit Windows on Intel)
                    "67D4E04F3A749124E32EE6ED7134865B" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Maximum serial #
                    ["SerialNumber"] = int.MaxValue
                },
                [
                    "3AA89123958BA0DF1611539BC909DD0B", // Opus 1.3.1 (Ubuntu 22.04)
                    "49452D1FB45FAB63ACCA4D7B98FC3421", // Opus 1.5.2 (MacOS on Intel)
                    "A7F53EB258E068E6B9B6C6A1D756E342", // Opus 1.3.1 (MacOS on ARM)
                    "B3D8AECD20CEC76CD140DA44979A60E4", // Opus 1.5.2 (32-bit Windows on Intel)
                    "F16D71E1988E3DF6E8D02C555D080E5F", // Opus 1.5.2 (32-bit Windows on AMD)
                    "4195302C28B204B94F945E14A4A9672B", // Opus 1.5.2 (64-bit Windows on Intel)
                    "D2DE03FC8DCCAE320BB7FF8432A6EF31" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Minimum bit rate
                    ["BitRate"] = 5,
                    ["SerialNumber"] = 1
                },
                [
                    "1F81AFD1976BAB56F31A4599A8AD8FF1", // Opus 1.3.1 (Ubuntu 22.04)
                    "39009C873167E72F3ACD4DC0085A3183", // Opus 1.5.2 (MacOS)
                    "A531829330D15594D94053B54404E137" // Opus 1.5.2 (Windows)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Maximum bit rate
                    ["BitRate"] = 512,
                    ["SerialNumber"] = 1
                },
                [
                    "785D39E686216F4958B8103B62E9E321", // Opus 1.3.1 (Ubuntu 22.04)
                    "015FC5B8C914D876461F7C481B313083", // Opus 1.5.2 (MacOS on Intel)
                    "4F1E3A4F58E62CB2F9DF5D0C3AED689D", // Opus 1.3.1 (MacOS on ARM)
                    "6B422669A0FCB242E0E15204F5FDCC47", // Opus 1.3.1 (32-bit Windows on Intel)
                    "C8CBDCD05703BDC8E61072ECE4A34521", // Opus 1.5.2 (32-bit Windows on AMD)
                    "CA4D9FA683B85ADCF4132828402D9994", // Opus 1.5.2 (64-bit Windows on Intel)
                    "39E7219F6438D312C01F244231E3CCD3" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Variable control mode (default, explicit)
                    ["ControlMode"] = "Variable",
                    ["SerialNumber"] = 1
                },
                [
                    "0C6BF7ECB9F757DB8F8AF485137BD2C8", // Opus 1.3.1 (Ubuntu 22.04)
                    "54A2DC75ED7879D6B890D50F4CB34603", // Opus 1.5.2 (MacOS on Intel)
                    "DF1CE487259E093896FB4BDB74A6B1E3", // Opus 1.3.1 (MacOS on ARM)
                    "E105170CA90D2D427A6431ACB8D5A16C", // Opus 1.5.2 (32-bit Windows on Intel)
                    "8A9F6B6464C10B76DC880BAF4DF1AA3B", // Opus 1.5.2 (32-bit Windows on AMD)
                    "BF310174502CDAD081F13998FB901865", // Opus 1.5.2 (64-bit Windows on Intel)
                    "AB19CD2D3E02D331600240F8FC9D56CC" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Constrained VBR mode
                    ["ControlMode"] = "Constrained",
                    ["SerialNumber"] = 1
                },
                [
                    "B98AC6052E465C5A226D8D2905B535EC", // Opus 1.3.1 (Ubuntu 22.04)
                    "6EED767F4AD005A03CCE4535E62836D3", // Opus 1.5.2 (MacOS on Intel)
                    "532F344E999193C4F59D1CDBF511C7DE", // Opus 1.3.1 (MacOS on ARM)
                    "93110D453D9EF4DF0EF57B0B758092D7", // Opus 1.5.2 (32-bit Windows on Intel)
                    "A74FA40F2DD860E515BE360C42558A2D", // Opus 1.5.2 (32-bit Windows on AMD)
                    "1C8382776908484524B1F8287A133A1F", // Opus 1.5.2 (64-bit Windows on Intel)
                    "9262170234B68A66E7FC8CEFC012EB63" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // CBR mode
                    ["ControlMode"] = "Constant",
                    ["SerialNumber"] = 1
                },
                [
                    "687B2402BF33EDB0EAD683235E309BC8", // Opus 1.3.1 (Ubuntu 22.04)
                    "69CB6F9E36304A413E772A64F8930EF8", // Opus 1.5.2 (MacOS on Intel)
                    "D9E874A3511506C2A8021FF77DEAED23", // Opus 1.3.1 (MacOS on ARM)
                    "3CE799E222F941AC4BE899AB1B5236B4", // Opus 1.5.2 (32-bit Windows on Intel)
                    "7554FB04DC120221749C1EE818CA6906", // Opus 1.5.2 (32-bit Windows on AMD)
                    "682884B45E806A93559FD12B759992D7", // Opus 1.5.2 (64-bit Windows on Intel)
                    "F4228369593D13A85161D84CFCB9E749" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Low bit rate, Music signal type (default)
                    ["BitRate"] = 32,
                    ["SerialNumber"] = 1
                },
                [
                    "8FC7BCF02EDB42E9785797FD2C9A71D6", // Opus 1.3.1 (Ubuntu 22.04)
                    "8A770140717C859BE50E5A65C9FD0591", // Opus 1.5.2 (MacOS on Intel)
                    "B1D166A87EF5F03FDC80E8571B9FC70E", // Opus 1.3.1 (MacOS on ARM)
                    "47CA0622B6C24FEF2BDA687457AF6EE3", // Opus 1.5.2 (Windows on Intel)
                    "D56071729C7BE8B2B93AF0B8BD3AC3AB" // Opus 1.5.2 (Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Low bit rate, Music signal type (explicit)
                    ["SignalType"] = "Music",
                    ["BitRate"] = 32,
                    ["SerialNumber"] = 1
                },
                [
                    "8FC7BCF02EDB42E9785797FD2C9A71D6", // Opus 1.3.1 (Ubuntu 22.04)
                    "8A770140717C859BE50E5A65C9FD0591", // Opus 1.5.2 (MacOS on Intel)
                    "B1D166A87EF5F03FDC80E8571B9FC70E", // Opus 1.3.1 (MacOS on ARM)
                    "47CA0622B6C24FEF2BDA687457AF6EE3", // Opus 1.5.2 (Windows on Intel)
                    "D56071729C7BE8B2B93AF0B8BD3AC3AB" // Opus 1.5.2 (Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // Low bit rate, Speech signal type
                    ["SignalType"] = "Speech",
                    ["BitRate"] = 32,
                    ["SerialNumber"] = 1
                },
                [
                    "84D72BBEF86EA7611518CF2862FC94BD", // Opus 1.3.1 (Ubuntu 22.04)
                    "D6B5EDF579DBAFF63BD7892C9BA58ADD", // Opus 1.5.2 (MacOS on Intel)
                    "3FBE634F53A90A6E5E26325A49BD20E5", // Opus 1.3.1 (MacOS on ARM)
                    "C1E6A103E4499035344A3B913611C0E0", // Opus 1.5.2 (Windows on Intel)
                    "B2F7F592DD68C92B20D577E4D9AEE7AB" // Opus 1.5.2 (Windows on AMD)
                ]
            },
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "Opus",
                new()
                {
                    // TrackGain requested but not available
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                [
                    "0C6BF7ECB9F757DB8F8AF485137BD2C8", // Opus 1.3.1 (Ubuntu 22.04)
                    "54A2DC75ED7879D6B890D50F4CB34603", // Opus 1.5.2 (MacOS on Intel)
                    "DF1CE487259E093896FB4BDB74A6B1E3", // Opus 1.3.1 (MacOS on ARM)
                    "E105170CA90D2D427A6431ACB8D5A16C", // Opus 1.5.2 (32-bit Windows on Intel)
                    "8A9F6B6464C10B76DC880BAF4DF1AA3B", // Opus 1.5.2 (32-bit Windows on AMD)
                    "BF310174502CDAD081F13998FB901865", // Opus 1.5.2 (64-bit Windows on Intel)
                    "AB19CD2D3E02D331600240F8FC9D56CC" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new()
                {
                    // Scaled to TrackGain
                    ["ApplyGain"] = "Track",
                    ["SerialNumber"] = 1
                },
                [
                    "B6029E9FC954A8722A1B10700F80EA19", // Opus 1.3.1 (Ubuntu 22.04)
                    "33E6FD8F8DC37F80C467DF3EF820D424", // Opus 1.5.2 (MacOS on Intel)
                    "A9E1172F6E94329BC82ACAD85B83F3F0", // Opus 1.3.1 (MacOS on ARM)
                    "E199F6D24770B47A9AF906F3ECA8E863", // Opus 1.5.2 (32-bit Windows on Intel)
                    "C05063029AD8645A38F639F92B573B81", // Opus 1.5.2 (32-bit Windows on AMD)
                    "D6414CE58A6E7C07C17C9CC11A8CD966", // Opus 1.5.2 (64-bit Windows on Intel)
                    "CA783515424077C81C2654A5F4037B0D" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            },
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "Opus",
                new()
                {
                    // Scaled to AlbumGain
                    ["ApplyGain"] = "Album",
                    ["SerialNumber"] = 1
                },
                [
                    "EF6A93BC1F10A15E5F45255F36E90F3A", // Opus 1.3.1 (Ubuntu 22.04)
                    "070E63A0E8A8A6F69B5C4DF9A7D1C82A", // Opus 1.5.2 (MacOS on Intel)
                    "8AF396D9A1F7648C2FDC7DFCC1AF35F6", // Opus 1.3.1 (MacOS on ARM)
                    "E8199A697B010856D7ED9F4C67A82481", // Opus 1.5.2 (32-bit Windows on Intel)
                    "5A71D9D8779039B3C5EB689A58C3C5FB", // Opus 1.5.2 (32-bit Windows on AMD)
                    "1C7A4661C5DD9C5CD8FC8CED950E2C80", // Opus 1.5.2 (64-bit Windows on Intel)
                    "09059F0610AB711E2F3B1B8EAB838A67" // Opus 1.5.2 (64-bit Windows on AMD)
                ]
            }

            #endregion
        };

        public static TheoryData<int, string, string, TestSettingDictionary, string[]> Data =>
            new(_data.Select((item, index) =>
                (index, item.Data.Item1, item.Data.Item2, item.Data.Item3, item.Data.Item4)));
    }
}