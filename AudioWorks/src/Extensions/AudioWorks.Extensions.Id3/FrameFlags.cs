/* Copyright © 2020 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

namespace AudioWorks.Extensions.Id3
{
    sealed class FrameFlags
    {
        internal bool Compression  { get; }

        internal bool Encryption  { get; }

        internal bool Grouping  { get; }

        internal bool Unsynchronisation  { get; }

        internal bool DataLengthIndicator { get; }

        internal FrameFlags(ushort data, int version)
        {
            Compression = version == 4 ? (data & 0b0000_1000) > 0 : (data & 0b1000_0000) > 0;
            Encryption = version == 4 ? (data & 0b0000_0100) > 0 : (data & 0b0100_0000) > 0;
            Grouping = version == 4 ? (data & 0b0100_0000) > 0 : (data & 0b0010_0000) > 0;
            Unsynchronisation = version == 4 && (data & 0b0000_0010) > 0;
            DataLengthIndicator = version == 4 && (data & 0b0000_0001) > 0;
        }
    }
}