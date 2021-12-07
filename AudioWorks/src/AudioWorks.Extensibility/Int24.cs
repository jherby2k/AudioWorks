﻿/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Runtime.InteropServices;

namespace AudioWorks.Extensibility
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct Int24
    {
        readonly byte _byte1;
        readonly byte _byte2;
        readonly byte _byte3;

        internal Int24(int value)
        {
            _byte1 = (byte) value;
            _byte2 = (byte) ((value >> 8) & 0xFF);
            _byte3 = (byte) ((value >> 16) & 0xFF);
        }

        public static implicit operator int(Int24 value) =>
            value._byte1 | value._byte2 << 8 | ((sbyte) value._byte3 << 16);
    }
}