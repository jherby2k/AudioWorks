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
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace AudioWorks.Extensions.Id3
{
    static class Sync
    {
        internal static uint Unsafe(uint val)
        {
            Span<byte> value = stackalloc byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(value, val);
            if (value[0] > 0x7f || value[1] > 0x7f || value[2] > 0x7f || value[3] > 0x7f)
                throw new InvalidTagException("Sync-safe value corrupted");

            Span<byte> sync = stackalloc byte[4];
            sync[0] = (byte) (((value[0] >> 0) & 0x7f) | ((value[1] & 0x01) << 7));
            sync[1] = (byte) (((value[1] >> 1) & 0x3f) | ((value[2] & 0x03) << 6));
            sync[2] = (byte) (((value[2] >> 2) & 0x1f) | ((value[3] & 0x07) << 5));
            sync[3] = (byte) ((value[3] >> 3) & 0x0f);
            return BinaryPrimitives.ReadUInt32LittleEndian(sync);
        }

        internal static uint Safe(uint val)
        {
            if (val > 0x10000000)
                throw new OverflowException("value is too large for a sync-safe integer");

            Span<byte> value = stackalloc byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(value, val);

            Span<byte> sync = stackalloc byte[4];
            sync[0] = (byte) ((value[0] >> 0) & 0x7f);
            sync[1] = (byte) (((value[0] >> 7) & 0x01) | (value[1] << 1) & 0x7f);
            sync[2] = (byte) (((value[1] >> 6) & 0x03) | (value[2] << 2) & 0x7f);
            sync[3] = (byte) (((value[2] >> 5) & 0x07) | (value[3] << 3) & 0x7f);
            return BinaryPrimitives.ReadUInt32LittleEndian(sync);
        }

        internal static uint UnsafeBigEndian(uint val)
        {
            Span<byte> value = stackalloc byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(value, val);
            if (value[0] > 0x7f || value[1] > 0x7f || value[2] > 0x7f || value[3] > 0x7f)
                throw new InvalidTagException("Sync-safe value corrupted");

            Span<byte> sync = stackalloc byte[4];
            sync[3] = (byte) (((value[3] >> 0) & 0x7f) | ((value[2] & 0x01) << 7));
            sync[2] = (byte) (((value[2] >> 1) & 0x3f) | ((value[1] & 0x03) << 6));
            sync[1] = (byte) (((value[1] >> 2) & 0x1f) | ((value[0] & 0x07) << 5));
            sync[0] = (byte) ((value[0] >> 3) & 0x0f);
            return BinaryPrimitives.ReadUInt32LittleEndian(sync);
        }

        internal static uint Unsafe(Stream src, Stream dst, uint size)
        {
            using (var writer = new BinaryWriter(dst, Encoding.UTF8, true))
            using (var reader = new BinaryReader(src, Encoding.UTF8, true))
            {
                byte last = 0;
                uint syncs = 0, count = 0;

                while (count < size)
                {
                    var val = reader.ReadByte();
                    if (last == 0xFF && val == 0x00)
                        syncs++; // skip the sync byte
                    else
                        writer.Write(val);
                    last = val;
                    count++;
                }

                if (last == 0xFF)
                {
                    writer.Write((byte) 0x00);
                    syncs++;
                }

                dst.Position = 0;
                return syncs; //bytes removed from stream
            }
        }

        internal static uint Safe(Stream src, Stream dst, uint count)
        {
            using (var writer = new BinaryWriter(dst, Encoding.UTF8, true))
            using (var reader = new BinaryReader(src, Encoding.UTF8, true))
            {
                byte last = 0;
                uint syncs = 0;

                while (count > 0)
                {
                    var val = reader.ReadByte();
                    if (last == 0xFF && (val == 0x00 || val >= 0xE0))
                    {
                        writer.Write((byte) 0x00);
                        syncs++;
                    }

                    last = val;
                    writer.Write(val);
                    count--;
                }

                if (last == 0xFF)
                {
                    writer.Write((byte) 0x00);
                    syncs++;
                }

                return syncs; // bytes added to the stream
            }
        }
    }
}