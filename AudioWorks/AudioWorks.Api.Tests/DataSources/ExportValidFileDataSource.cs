using System.Collections.Generic;
using System.Linq;
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
                "158095449AD4C8BCA6EECDDC2FD82667"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Mono.wav",
                "FLAC",
                null,
                "7386711EF1FF2CFDD3237D1D0B6BD015"
            },
            new object[]
            {
                "LPCM 16-bit 44100Hz Stereo.wav",
                "FLAC",
                null,
                "6D50D234121EF64A84FF03BB557B025B"
            },
            new object[]
            {
                "LPCM 16-bit 48000Hz Stereo.wav",
                "FLAC",
                null,
                "802B4CD402BED63278D4148275739DEE"
            },
            new object[]
            {
                "LPCM 24-bit 96000Hz Stereo.wav",
                "FLAC",
                null,
                "42B3494C2B3971EBECD0C02289471CB8"
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