using JetBrains.Annotations;
using System.Collections.Generic;

namespace AudioWorks.Api.Tests
{
    public static class TestFilesUnsupportedDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[] { "Not Audio.txt" },
            new object[] { "MS ADPCM.wav" },
            new object[] { "Speex.ogg" },
            new object[] { "Lame MP3.m4a" }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNames
        {
            [UsedImplicitly] get => _data;
        }
    }
}