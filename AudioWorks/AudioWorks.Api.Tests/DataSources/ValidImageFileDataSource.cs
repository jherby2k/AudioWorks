using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class ValidImageFileDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                "Bitmap 24-bit 1280 x 935.bmp",
                1280,
                935
            },

            new object[]
            {
                "PNG 24-bit 1280 x 935.png",
                1280,
                935
            },

            new object[]
            {
                "JPEG 24-bit 1280 x 935.jpg",
                1280,
                935
            }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNames
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndWidth
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[1] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndHeight
        {
            [UsedImplicitly]
            get => _data.Select(item => new[] { item[0], item[2] });
        }
    }
}
