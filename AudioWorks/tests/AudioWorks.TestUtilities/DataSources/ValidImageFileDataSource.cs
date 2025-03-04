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
using Xunit;

namespace AudioWorks.TestUtilities.DataSources
{
    public static class ValidImageFileDataSource
    {
        static readonly IEnumerable<TheoryDataRow<string, int, int, int, bool, string, string>> _data =
        [
            new(
                "Bitmap 24-bit 1280 x 935.bmp",
                1280,
                935,
                24,
                true,
                "image/png",
                "9315650A78F4292A5527586198C4F3C8"
            ),
            new(
                "PNG 24-bit 1280 x 935.png",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            ),
            new(
                "JPEG 24-bit 1280 x 935.jpg",
                1280,
                935,
                24,
                false,
                "image/jpeg",
                "4BFBE209E1183AE63DBBED12EEE773B8"
            )
        ];

        public static IEnumerable<TheoryDataRow<string>> FileNames =>
            _data.Select(item => new TheoryDataRow<string>(item.Data.Item1));

        public static IEnumerable<TheoryDataRow<string, int>> FileNamesAndWidth =>
            _data.Select(item => new TheoryDataRow<string, int>(item.Data.Item1, item.Data.Item2));

        public static IEnumerable<TheoryDataRow<string, int>> FileNamesAndHeight =>
            _data.Select(item => new TheoryDataRow<string, int>(item.Data.Item1, item.Data.Item3));

        public static IEnumerable<TheoryDataRow<string, int>> FileNamesAndColorDepth =>
            _data.Select(item => new TheoryDataRow<string, int>(item.Data.Item1, item.Data.Item4));

        public static IEnumerable<TheoryDataRow<string, bool>> FileNamesAndLossless =>
            _data.Select(item => new TheoryDataRow<string, bool>(item.Data.Item1, item.Data.Item5));

        public static IEnumerable<TheoryDataRow<string, string>> FileNamesAndMimeType =>
            _data.Select(item => new TheoryDataRow<string, string>(item.Data.Item1, item.Data.Item6));

        public static IEnumerable<TheoryDataRow<string, string>> FileNamesAndDataHash =>
            _data.Select(item => new TheoryDataRow<string, string>(item.Data.Item1, item.Data.Item7));
    }
}
