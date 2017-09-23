using AudioWorks.Common;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace AudioWorks.Api.Tests
{
    public static class TestFilesUnsupportedSaveMetadataDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[] { "LPCM 16-bit 44100Hz Stereo.wav" }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Data
        {
            [UsedImplicitly] get => _data;
        }
    }
}
