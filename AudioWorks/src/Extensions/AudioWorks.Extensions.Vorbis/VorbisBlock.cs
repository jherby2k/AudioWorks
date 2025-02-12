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

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct VorbisBlock
    {
        readonly nint Pcm;

        readonly OggPackBuffer OggPackBuffer;

        readonly CLong Lw;

        readonly CLong W;

        readonly CLong Nw;

        readonly int PcmEnd;

        readonly int Mode;

        readonly int EoffLag;

        readonly long GranulePosition;

        readonly long Sequence;

        readonly nint DspState;

        readonly nint LocalStore;

        readonly CLong LocalTop;

        readonly CLong LocalAlloc;

        readonly CLong TotalUse;

        readonly nint Reap;

        readonly CLong GlueBits;

        readonly CLong TimeBits;

        readonly CLong FloorBits;

        readonly CLong ResBits;

        readonly nint Internal;
    }
}