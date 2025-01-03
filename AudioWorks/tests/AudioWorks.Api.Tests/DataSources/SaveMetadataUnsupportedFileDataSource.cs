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

namespace AudioWorks.Api.Tests.DataSources
{
    public static class SaveMetadataUnsupportedFileDataSource
    {
        static readonly TheoryData<string> _data = new()
        {
            "LPCM 16-bit 44100Hz Stereo.wav"
        };

        public static TheoryData<int, string> Data
        {
            get
            {
                var results = new TheoryData<int, string>();
                foreach (var result in _data.Cast<string>().Select((item, index) => (index, item)))
                    results.Add(result.Item1, result.Item2);
                return results;
            }
        }
    }
}
