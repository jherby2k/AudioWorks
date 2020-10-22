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
    [Frame("T")]
    internal class FrameText : FrameBase
    {
        internal TextType TextType { get; set; } = TextType.Ascii;

        internal string Text { get; set; }

        internal FrameText(string frameId)
            : base(frameId)
        {
        }

        internal override void Parse(byte[] frame)
        {
            var index = 0;
            TextType = (TextType) frame[index++];
            Text = TextBuilder.ReadTextEnd(frame, index, TextType);
        }

        internal override byte[] Make()
        {
            using (var buffer = new MemoryStream())
            using (var writer = new BinaryWriter(buffer, Encoding.UTF8, true))
            {
                writer.Write((byte) TextType);
                writer.Write(TextBuilder.WriteTextEnd(Text, TextType));
                return buffer.ToArray();
            }
        }

        public override string ToString()
        {
            return Text;
        }
    }
}