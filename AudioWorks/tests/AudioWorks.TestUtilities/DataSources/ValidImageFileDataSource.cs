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

using System.Linq;
using Xunit;

namespace AudioWorks.TestUtilities.DataSources
{
    public static class ValidImageFileDataSource
    {
        static readonly TheoryData<string, int, int, int, bool, string, string> _data = new()
        {
            {
                "Bitmap 24-bit 1280 x 935.bmp",
                1280,
                935,
                24,
                true,
                "image/png",
                "9315650A78F4292A5527586198C4F3C8"
            },
            {
                "PNG 24-bit 1280 x 935.png",
                1280,
                935,
                24,
                true,
                "image/png",
                "85E02F6C2BCF8112E16E63660CADFE02"
            },
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

        public static TheoryData<string> FileNames =>
            new(_data.Select(item => item.Data.Item1));

        public static TheoryData<string, int> FileNamesAndWidth =>
            new(_data.Select(item => (item.Data.Item1, item.Data.Item2)));

        public static TheoryData<string, int> FileNamesAndHeight =>
            new(_data.Select(item => (item.Data.Item1, item.Data.Item3)));

        public static TheoryData<string, int> FileNamesAndColorDepth =>
            new(_data.Select(item => (item.Data.Item1, item.Data.Item4)));

        public static TheoryData<string, bool> FileNamesAndLossless =>
            new(_data.Select(item => (item.Data.Item1, item.Data.Item5)));

        public static TheoryData<string, string> FileNamesAndMimeType =>
            new(_data.Select(item => (item.Data.Item1, item.Data.Item6)));

        public static TheoryData<string, string> FileNamesAndDataHash =>
            new(_data.Select(item => (item.Data.Item1, item.Data.Item7)));
    }
}
