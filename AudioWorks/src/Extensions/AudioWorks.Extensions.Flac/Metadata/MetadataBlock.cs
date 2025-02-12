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

using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac.Metadata
{
    [StructLayout(LayoutKind.Explicit)]
    readonly struct MetadataBlock
    {
        [FieldOffset(0)]
        internal readonly MetadataType Type;

        [FieldOffset(4)]
        readonly bool IsLast;

        [FieldOffset(8)]
        readonly uint Length;

        [FieldOffset(16)]
        internal readonly StreamInfo StreamInfo;

        [FieldOffset(16)]
        internal readonly VorbisComment VorbisComment;

        [FieldOffset(16)]
        internal readonly Picture Picture;
    }
}