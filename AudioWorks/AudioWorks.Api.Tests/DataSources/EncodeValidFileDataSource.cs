﻿using System.Collections.Generic;
using System.Linq;
using AudioWorks.Api.Tests.DataTypes;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class EncodeValidFileDataSource
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
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
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
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "FLAC",
                null,
                "A6B18F2B4DDC51DC37154410E701251F"
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
                "F03F417B853C560705CD424AD329EFBC"
            },

            // Lame encoding
            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "LameMP3",
                null,
                "7CB68FB7ACC70E8CD928E7DB437B16FE"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "LameMP3",
                null,
                "C02CA44F3E1CCA3D8BA0DE922C49946E"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                null,
                "34C345AB6BDA4A4C172D74046EC683D7"
            },
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "LameMP3",
                null,
                "A333E74AFF4107E6C6C987AB27DF4B36"
            },
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "LameMP3",
                null,
                "C0204097396B92D06E2B1BEBA90D0BD9"
            },
            new object[]
            {
                "FLAC Level 5 16-bit 44100Hz Stereo (Tagged using defaults).flac",
                "LameMP3",
                null,
                "55F290095FDCE602C43380CC4F5D1101"
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
                "BEB5029A08011BCEDFFA99173B763E7F"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "LameMP3",
                new TestSettingDictionary
                {
                    // Forced bit rate
                    ["BitRate"] = 128,
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
                    // Forced bit rate ignored without bit rate
                    ["ForceCBR"] = true
                },
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
                "AD0C6C5DE14F77D2CFEE3F27EEA6B0C6"
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