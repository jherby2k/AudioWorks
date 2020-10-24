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
    sealed class TagHeader
    {
        static readonly byte[] _id3 = { 0x49, 0x44, 0x33 }; //"ID3" tag

        bool _hasFooter;

        internal static uint HeaderSize => 10;

        internal byte Version { get; set; } = 3;

        internal uint TagSize { get; set; }

        internal uint TagSizeWithHeaderFooter => TagSize + HeaderSize + (_hasFooter ? HeaderSize : 0);

        internal bool Unsync { get; private set; }

        internal bool HasExtendedHeader { get; private set; }

        internal uint PaddingSize { get; set; }

        internal void Serialize(Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                //TODO: Validate version and revision we support
                writer.Write(_id3); // ID3v2/file identifier
                writer.Write(Version); // ID3v2 version, e.g. 3 or 4
                writer.Write((byte) 0); // ID3v2 revision
                writer.Write((byte) 0); // ID3v2 flags
                writer.Write(Swap.UInt32(Sync.Safe(TagSize + PaddingSize)));
            }
        }

        internal void Deserialize(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
            {
                var idTag = new byte[3];

                // Read the tag identifier
                reader.Read(idTag, 0, 3);
                if (!Memory.Compare(_id3, idTag))
                    throw new TagNotFoundException("ID3v2 tag identifier was not found");

                // Get the id3v2 version byte
                Version = reader.ReadByte();
                if (Version == 0xff)
                    throw new InvalidTagException("Corrupt header, invalid ID3v2 version.");

                // Get the id3v2 revision byte
                if (reader.ReadByte() == 0xff)
                    throw new InvalidTagException("Corrupt header, invalid ID3v2 revision.");

                // Parse the flag byte
                var id3Flags = (byte) (0xf0 & reader.ReadByte());
                Unsync = (id3Flags & 0x80) > 0;
                HasExtendedHeader = (id3Flags & 0x40) > 0;
                _hasFooter = (id3Flags & 0x10) > 0;

                // Get the id3v2 size, swap and un-sync the integer
                TagSize = Swap.UInt32(Sync.UnsafeBigEndian(reader.ReadUInt32()));
                if (TagSize == 0)
                    throw new InvalidTagException("Corrupt header, tag size can't be zero.");
            }
        }
    }
}