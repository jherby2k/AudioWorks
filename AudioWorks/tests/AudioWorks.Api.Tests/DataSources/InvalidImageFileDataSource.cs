using System.Collections.Generic;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class InvalidImageFileDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[] { "Not an Image.bmp" }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Data
        {
            [UsedImplicitly] get => _data;
        }
    }
}