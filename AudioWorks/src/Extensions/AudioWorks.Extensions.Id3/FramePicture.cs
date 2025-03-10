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
    [Frame("APIC")]
    public sealed class FramePicture() : FrameBase("APIC"), IFrameDescription
    {
        internal TextType TextType { get; set; } = TextType.Ascii;

        internal string Mime { get; set; } = string.Empty;

        internal PictureType PictureType { get; set; }

        public string Description { get; set; } = string.Empty;

        internal byte[] PictureData { get; set; } = [];

        internal override void Parse(Span<byte> frame)
        {
            var index = 0;
            TextType = (TextType) frame[index++];
            Mime = TextBuilder.ReadText(frame, ref index, TextType.Ascii);
            PictureType = (PictureType) frame[index++];
            Description = TextBuilder.ReadText(frame, ref index, TextType);

            PictureData = new byte[frame.Length - index];
            frame[index..].CopyTo(PictureData);
        }

        internal override void Write(Stream output)
        {
            output.WriteByte((byte) TextType);
            TextBuilder.WriteText(output, Mime, TextType.Ascii);
            output.WriteByte((byte) PictureType);
            TextBuilder.WriteText(output, Description, TextType);
            output.Write(PictureData, 0, PictureData.Length);
        }
    }
}