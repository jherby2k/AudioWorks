﻿/* Copyright © 2018 Jeremy Herbison

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
    public static class SaveMetadataUnsupportedFileDataSource
    {
        static readonly IEnumerable<TheoryDataRow<string>> _data =
        [
            "LPCM 16-bit 44100Hz Stereo.wav"
        ];

        public static IEnumerable<TheoryDataRow<int, string>> Data =>
            _data.Select((item, index) => new TheoryDataRow<int, string>(index, item.Data) { Skip = item.Skip });
    }
}
