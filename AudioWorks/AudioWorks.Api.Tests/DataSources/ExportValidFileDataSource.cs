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
                "EDD9B72838A464EA044EC708BD3D6DAC"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "FLAC",
                null,
                "1F576BE36A17F1C89C46D0AE170DDA2D"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                null,
                "3EF30DBEE1A041F82B660ED62AFF9C98"
            },
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "FLAC",
                null,
                "29E0BB750298EEFCAE1B8D1FDD7B6D88"
            },
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "FLAC",
                null,
                "E0EA787ABB3779F207C6FEC852489A8A"
            },
            new object[]
            {
                "LPCM 8-bit 8000Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["CompressionLevel"] = "8"
                },
                "B207A1CD8777A198E9C13734C8AECAE4"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["CompressionLevel"] = "8"
                },
                "224AA2ADCF9FC568C6BC4A52EFD104FE"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["CompressionLevel"] = "8"
                },
                "824E258162C14654E104DEC97FC531FF"
            },
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["CompressionLevel"] = "8"
                },
                "5A0FAFF509BD74E1084480FC05BD7D96"
            },
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "FLAC",
                new TestSettingDictionary
                {
                    ["CompressionLevel"] = "8"
                },
                "E36EEB92754EB9D334599F07831D8263"
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