﻿/* Copyright © 2020 Jeremy Herbison

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
using System.Collections.ObjectModel;
using System.IO;

namespace AudioWorks.Extensions.Id3
{
    class TagModel : Collection<FrameBase>
    {
        internal bool IsValid => Count > 0;

        internal TagHeader Header { get; } = new TagHeader();

        internal TagExtendedHeader ExtendedHeader { get; } = new TagExtendedHeader();

        internal void AddRange(IEnumerable<FrameBase> frames)
        {
            foreach (var frame in frames)
                Add(frame);
        }

        internal void UpdateSize()
        {
            if (!IsValid)
                Header.TagSize = 0; // clear the TagSize stored in the tagModel

            // TODO: there must be a better way of obtaining this!!
            using (Stream stream = new MemoryStream())
                TagManager.Serialize(this, stream);
        }
    }
}