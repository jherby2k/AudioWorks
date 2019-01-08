/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

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
#if WINDOWS && !NETSTANDARD2_0
                "85E02F6C2BCF8112E16E63660CADFE02"
#else
                "C7B06AE783981771FA3806BBFF114EFF"
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
