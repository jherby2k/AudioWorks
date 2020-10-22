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

using System.IO;
using System.Text;

namespace AudioWorks.Extensions.Id3
{
    [Frame("APIC")]
    class FramePicture : FrameBase, IFrameDescription
    {
        internal TextType TextEncoding { get; set; } = TextType.Ascii;

        internal string Mime { get; set; }

        internal PictureType PictureType { get; set; }

        public string Description { get; set; }

        internal byte[] PictureData { get; set; }

        internal FramePicture(string frameId)
            : base(frameId)
        {
        }

        internal override void Parse(byte[] frame)
        {
            var index = 0;
            TextEncoding = (TextType) frame[index++];
            Mime = TextBuilder.ReadAscii(frame, ref index);
            PictureType = (PictureType) frame[index++];
            Description = TextBuilder.ReadText(frame, ref index, TextEncoding);
            PictureData = Memory.Extract(frame, index, frame.Length - index);
        }

        internal override byte[] Make()
        {
            using (var buffer = new MemoryStream())
            using (var writer = new BinaryWriter(buffer, Encoding.UTF8, true))
            {
                writer.Write((byte) TextEncoding);
                writer.Write(TextBuilder.WriteAscii(Mime));
                writer.Write((byte) PictureType);
                writer.Write(TextBuilder.WriteText(Description, TextEncoding));
                writer.Write(PictureData);
                return buffer.ToArray();
            }
        }

        public override string ToString()
        {
            return Description;
        }
    }
}