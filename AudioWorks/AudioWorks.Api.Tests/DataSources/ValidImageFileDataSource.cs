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
                935,
                24,
                true,
                "image/png",
                "AE46988A9D44F2797469B8B79CA77A33"
            },

            new object[]
            {
                "PNG 24-bit 1280 x 935.png",
                1280,
                935,
                24,
                true,
                "image/png",
                "6895D34A230827FE1F7C3141AC0454FA"
            },

            new object[]
            {
                "JPEG 24-bit 1280 x 935.jpg",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "0E19A69A74AF427BD6F7CCE15B1D5AB9"
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
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[2] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndColorDepth
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[3] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndLossless
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[4] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndMimeType
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[5] });
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> FileNamesAndDataHash
        {
            [UsedImplicitly] get => _data.Select(item => new[] { item[0], item[6] });
        }
    }
}
