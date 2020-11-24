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
    static class Sync
    {
        internal static uint Unsafe(Stream source, Stream destination, uint size)
        {
            using (var writer = new BinaryWriter(destination, Encoding.UTF8, true))
            using (var reader = new BinaryReader(source, Encoding.UTF8, true))
            {
                byte last = 0;
                uint syncs = 0, count = 0;

                while (count < size)
                {
                    var val = reader.ReadByte();
                    if (last == 0xFF && val == 0x00)
                        syncs++;
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

                destination.Position = 0;
                return syncs;
            }
        }
    }
}