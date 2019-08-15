/* Copyright © 2018 Jeremy Herbison

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

namespace AudioWorks.Extensions.Mp4
{
    sealed class FreeAtom : WritableAtom
    {
        readonly uint _size;

        internal FreeAtom(uint size) => _size = size;

        internal override void Write(Stream output)
        {
            if (_size <= 8) return;

            using (var writer = new Mp4Writer(output))
            {
                // Write the atom header
                writer.WriteBigEndian(_size);
                writer.WriteBigEndian(0x66726565);  // 'free'

                writer.WriteZeros((int) _size - 8);
            }
        }
    }
}