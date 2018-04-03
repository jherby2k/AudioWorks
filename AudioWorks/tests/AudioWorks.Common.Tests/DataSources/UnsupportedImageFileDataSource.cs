using System.Collections.Generic;
using JetBrains.Annotations;

namespace AudioWorks.Common.Tests.DataSources
{
    public static class UnsupportedImageFileDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[] { "Text.txt" }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Data
        {
            [UsedImplicitly] get => _data;
        }
    }
}