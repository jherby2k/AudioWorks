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

namespace AudioWorks.Extensions.Flac.Metadata
{
    abstract class VorbisCommentMetadataObject : MetadataObject
    {
        internal VorbisCommentMetadataObject()
            : base(MetadataType.VorbisComment)
        {
        }

        internal void Append(string key, string value)
        {
            LibFlac.MetadataObjectVorbisCommentEntryFromNameValuePair(out var entry, key, value);

            // The comment takes ownership of the new entry if 'copy' is false
            LibFlac.MetadataObjectVorbisCommentAppendComment(Handle, entry, false);
        }
    }
}