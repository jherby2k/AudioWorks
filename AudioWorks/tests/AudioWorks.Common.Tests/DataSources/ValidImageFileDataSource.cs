using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace AudioWorks.Common.Tests.DataSources
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
#if LINUX || NET471
                "C7B06AE783981771FA3806BBFF114EFF"
#else
                "85E02F6C2BCF8112E16E63660CADFE02"
#endif
            },

            new object[]
            {
                "PNG 24-bit 1280 x 935.png",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            },

            new object[]
            {
                "JPEG 24-bit 1280 x 935.jpg",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
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
