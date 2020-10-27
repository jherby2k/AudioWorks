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
using AudioWorks.Common;

namespace AudioWorks.Extensions.Id3
{
    sealed class TagExtendedHeader
    {
        internal uint Size { get; private set; }

        internal void Deserialize(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
                Size = Swap.UInt32(Sync.UnsafeBigEndian(reader.ReadUInt32()));
            if (Size < 6)
                throw new AudioInvalidException("Corrupt id3 extended header.");

            // Seek past it for now
            stream.Seek((int) Size, SeekOrigin.Current);
        }
    }
}