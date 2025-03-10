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
using System.Text;

namespace AudioWorks.Extensions.Id3
{
    [Frame("COMM")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class FrameFullText() : FrameText("COMM"), IFrameDescription
    {
        public string Description { get; set; } = string.Empty;

        internal string Language { get; set; } = "eng";

        internal override void Parse(Span<byte> frame)
        {
            var index = 0;
            TextType = (TextType) frame[index];
            index++;

            // Invalid frame
            if (frame.Length - index < 3)
                return;

            Language = Encoding.ASCII.GetString(frame.Slice(index, 3));
            index += 3;

            if (frame.Length - index < 1)
                return;

            Description = TextBuilder.ReadText(frame, ref index, TextType);
            Text = TextBuilder.ReadTextEnd(frame[index..], TextType);
        }

        internal override void Write(Stream output)
        {
            output.WriteByte((byte) TextType);
            TextBuilder.WriteText(output, Language, TextType.Ascii, false);
            TextBuilder.WriteText(output, Description, TextType);
            TextBuilder.WriteText(output, Text, TextType);
        }
    }
}