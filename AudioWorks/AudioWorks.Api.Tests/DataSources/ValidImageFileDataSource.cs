using System.Collections.Generic;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests.DataSources
{
    public static class ValidImageFileDataSource
    {
        [NotNull, ItemNotNull] static readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                "Bitmap 24-bit 1280 x 935.bmp"
            },

            new object[]
            {
                "PNG 24-bit 1280 x 935.png"
            },

            new object[]
            {
                "JPEG 24-bit 1280 x 935.jpg"
            }
        };

        [NotNull, ItemNotNull]
        public static IEnumerable<object[]> Data
        {
            [UsedImplicitly] get => _data;
        }
    }
}
