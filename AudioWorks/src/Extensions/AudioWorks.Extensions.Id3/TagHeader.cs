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

using System;
using System.IO;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Id3
{
    sealed class TagHeader
    {
        static readonly byte[] _id3 = [0x49, 0x44, 0x33]; //"ID3" tag

        bool _hasFooter;

        internal static uint HeaderSize => 10;

        internal byte Version { get; set; } = 3;

        internal uint TagSize { get; set; }

        internal uint TagSizeWithHeaderFooter => TagSize + HeaderSize + (_hasFooter ? HeaderSize : 0);

        internal bool Unsynchronisation { get; private set; }

        internal bool HasExtendedHeader { get; private set; }

        internal uint PaddingSize { get; set; }

        internal void Serialize(Stream stream)
        {
            using (var writer = new TagWriter(stream))
            {
                writer.Write(_id3);
                writer.Write(Version);
                writer.Write((byte) 0);
                writer.Write((byte) 0);
                writer.WriteSyncSafe(TagSize + PaddingSize);
            }
        }

        internal void Deserialize(Stream stream)
        {
            using (var reader = new TagReader(stream))
            {
                // Read the tag identifier
#if NETSTANDARD2_0
                var idTag = reader.ReadBytes(3);
#else
                Span<byte> idTag = stackalloc byte[3];
                reader.Read(idTag);
#endif
                if (_id3.AsSpan().SequenceCompareTo(idTag) != 0)
                    throw new TagNotFoundException("ID3v2 tag identifier was not found.");

                // Get the id3v2 version byte
                Version = reader.ReadByte();
                if (Version == 0xFF)
                    throw new AudioInvalidException("Corrupt header: invalid ID3v2 version.");

                // Get the id3v2 revision byte
                if (reader.ReadByte() == 0xFF)
                    throw new AudioInvalidException("Corrupt header: invalid ID3v2 revision.");

                // Parse the flag byte
                var id3Flags = (byte) (0xF0 & reader.ReadByte());
                Unsynchronisation = (id3Flags & 0b1000_0000) > 0;
                HasExtendedHeader = (id3Flags & 0b0100_0000) > 0;
                _hasFooter = (id3Flags & 0b0001_0000) > 0;

                // Get the id3v2 size, swap and un-sync the integer
                TagSize = reader.ReadUInt32SyncSafe();
                if (TagSize == 0)
                    throw new AudioInvalidException("Corrupt header: tag size can't be zero.");
            }
        }
    }
}