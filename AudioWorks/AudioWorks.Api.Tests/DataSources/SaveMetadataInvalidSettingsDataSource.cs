using AudioWorks.Common;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class SaveMetadataInvalidSettingsDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new SettingDictionary
                {
                    ["Foo"] = "Bar"
                }
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new SettingDictionary
                {
                    ["Version"] = "1.5"
                }
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new SettingDictionary
                {
                    ["Padding"] = "Foo"
                }
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new SettingDictionary
                {
                    ["Padding"] = -1
                }
            },
            new object[]
            {
                "Lame CBR 128 44100Hz Stereo.mp3",
                new SettingDictionary
                {
                    ["Padding"] = int.MaxValue
                }
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
