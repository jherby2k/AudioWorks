﻿using AudioWorks.Common;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace AudioWorks.Api.Tests
{
    public static class TestFilesValidSettingsInvalidSaveMetadataDataSource
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
