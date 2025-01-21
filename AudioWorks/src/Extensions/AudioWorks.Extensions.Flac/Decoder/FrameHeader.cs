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

using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac.Decoder
{
    [StructLayout(LayoutKind.Explicit)]
    readonly struct FrameHeader
    {
        [FieldOffset(0)] internal readonly uint BlockSize;

        [FieldOffset(4)] readonly uint SampleRate;

        [FieldOffset(8)] internal readonly uint Channels;

        [FieldOffset(12)] readonly int ChannelAssignment;

        [FieldOffset(16)] internal readonly uint BitsPerSample;

        [FieldOffset(20)] readonly int NumberType;

        [FieldOffset(24)] readonly uint FrameNumber;

        [FieldOffset(24)] readonly ulong SampleNumber;

        [FieldOffset(32)] readonly byte Crc;
    }
}