/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Collections.Generic;
using System.Linq;

namespace AudioWorks.Common.Tests.DataSources
{
    public static class ValidImageFileDataSource
    {
        static readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                "Bitmap 24-bit 1280 x 935.bmp",
                1280,
                935,
                24,
                true,
                "image/png",
#if WINDOWS && NETCOREAPP2_1
                "85E02F6C2BCF8112E16E63660CADFE02"
#elif WINDOWS && NETCOREAPP
                "368F0A80FDB2080365F923D9CD9BBE5F"
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

        public static IEnumerable<object[]> FileNames => _data.Select(item => new[] { item[0] });

        public static IEnumerable<object[]> FileNamesAndWidth => _data.Select(item => new[] { item[0], item[1] });

        public static IEnumerable<object[]> FileNamesAndHeight => _data.Select(item => new[] { item[0], item[2] });

        public static IEnumerable<object[]> FileNamesAndColorDepth => _data.Select(item => new[] { item[0], item[3] });

        public static IEnumerable<object[]> FileNamesAndLossless => _data.Select(item => new[] { item[0], item[4] });

        public static IEnumerable<object[]> FileNamesAndMimeType => _data.Select(item => new[] { item[0], item[5] });

        public static IEnumerable<object[]> FileNamesAndDataHash => _data.Select(item => new[] { item[0], item[6] });
    }
}
