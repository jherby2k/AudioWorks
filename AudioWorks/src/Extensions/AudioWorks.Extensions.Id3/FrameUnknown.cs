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
    class FrameUnknown : FrameBase
    {
        byte[] _data;

        public FrameUnknown(string frameId)
            : base(frameId)
        {
        }

        internal override void Parse(byte[] frame) => _data = frame;

        internal override byte[] Make() => _data;
    }
}