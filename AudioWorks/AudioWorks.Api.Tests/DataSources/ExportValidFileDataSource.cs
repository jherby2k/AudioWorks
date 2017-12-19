using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class ExportValidFileDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
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