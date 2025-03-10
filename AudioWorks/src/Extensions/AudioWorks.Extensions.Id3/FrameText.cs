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

using System;
using System.IO;

namespace AudioWorks.Extensions.Id3
{
    [Frame("T")]
    public class FrameText(string frameId) : FrameBase(frameId)
    {
        internal TextType TextType { get; set; } = TextType.Ascii;

        internal string Text { get; set; } = string.Empty;

        // ReSharper disable once MemberCanBeProtected.Global

        internal override void Parse(Span<byte> frame)
        {
            var index = 0;
            TextType = (TextType) frame[index++];
            Text = TextBuilder.ReadTextEnd(frame[index..], TextType);
        }

        internal override void Write(Stream output)
        {
            output.WriteByte((byte) TextType);
            TextBuilder.WriteText(output, Text, TextType);
        }
    }
}