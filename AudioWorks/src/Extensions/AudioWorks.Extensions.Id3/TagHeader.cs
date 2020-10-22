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
        static readonly byte[] _3di = { 0x33, 0x44, 0x49 }; //"3DI" footer tag

        byte _id3Flags;
        uint _paddingSize;

        internal static uint HeaderSize => 10;

        internal byte Version { get; set; } = 4;

        internal byte Revision { get; set; }

        internal uint TagSize { get; set; }

        internal uint TagSizeWithHeaderFooter => TagSize + HeaderSize + (Footer ? HeaderSize : 0);

        internal bool Unsync
        {
            get => (_id3Flags & 0x80) > 0;
            set
            {
                if (value)
                    _id3Flags |= 0x80;
                else
                    unchecked
                    {
                        _id3Flags &= (byte) ~0x80;
                    }
            }
        }

        internal bool ExtendedHeader
        {
            get => (_id3Flags & 0x40) > 0;
            set
            {
                if (value)
                    _id3Flags |= 0x40;
                else
                    unchecked
                    {
                        _id3Flags &= (byte) ~0x40;
                    }
            }
        }

        internal bool Experimental
        {
            get => (_id3Flags & 0x20) > 0;
            set
            {
                if (value)
                    _id3Flags |= 0x20;
                else
                    unchecked
                    {
                        _id3Flags &= (byte) ~0x20;
                    }
            }
        }

        internal bool Footer
        {
            get => (_id3Flags & 0x10) > 0;
            set
            {
                if (value)
                {
                    _id3Flags |= 0x10;
                    _paddingSize = 0;
                }
                else
                    unchecked
                    {
                        _id3Flags &= (byte) ~0x10;
                    }
            }
        }

        internal bool Padding => _paddingSize > 0;

        internal uint PaddingSize
        {
            get => _paddingSize;
            set
            {
                if (value > 0)
                    Footer = false;
                _paddingSize = value;
            }
        }

        internal void Serialize(Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                //TODO: Validate version and revision we support
                writer.Write(_id3); // ID3v2/file identifier
                writer.Write(Version); // ID3v2 version, e.g. 3 or 4
                writer.Write(Revision); // ID3v2 revision, e.g. 0
                writer.Write(_id3Flags); // ID3v2 flags
                writer.Write(Swap.UInt32(Sync.Safe(TagSize + _paddingSize)));
            }
        }

        internal void SerializeFooter(Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                //TODO: Validate version and revision we support
                writer.Write(_3di); // ID3v2/file footer identifier; ID3 backwards.
                writer.Write(Version); // ID3v2 version, e.g. 3 or 4
                writer.Write(Revision); // ID3v2 revision, e.g. 0
                writer.Write(_id3Flags); // ID3v2 flags
                writer.Write(Swap.UInt32(Sync.Safe(TagSize)));
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
                Revision = reader.ReadByte();
                if (Revision == 0xff)
                    throw new InvalidTagException("Corrupt header, invalid ID3v2 revision.");

                // Get the id3v2 flag byte, only read what I understand
                _id3Flags = (byte) (0xf0 & reader.ReadByte());
                // Get the id3v2 size, swap and un-sync the integer
                TagSize = Swap.UInt32(Sync.UnsafeBigEndian(reader.ReadUInt32()));
                if (TagSize == 0)
                    throw new InvalidTagException("Corrupt header, tag size can't be zero.");
            }
        }
    }
}