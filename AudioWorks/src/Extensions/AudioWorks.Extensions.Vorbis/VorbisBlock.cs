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

using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    [StructLayout(LayoutKind.Sequential)]
    readonly struct VorbisBlock
    {
        readonly IntPtr Pcm;

        readonly OggPackBuffer OggPackBuffer;

#if WINDOWS
        readonly int Lw;

        readonly int W;

        readonly int Nw;
#else
        readonly long Lw;

        readonly long W;

        readonly long Nw;
#endif

        readonly int PcmEnd;

        readonly int Mode;

        readonly int EoffLag;

        readonly long GranulePosition;

        readonly long Sequence;

        readonly IntPtr DspState;

        readonly IntPtr LocalStore;

#if WINDOWS
        readonly int LocalTop;

        readonly int LocalAlloc;

        readonly int TotalUse;
#else
        readonly long LocalTop;

        readonly long LocalAlloc;

        readonly long TotalUse;
#endif

        readonly IntPtr Reap;

#if WINDOWS
        readonly int GlueBits;

        readonly int TimeBits;

        readonly int FloorBits;

        readonly int ResBits;
#else
        readonly long GlueBits;

        readonly long TimeBits;

        readonly long FloorBits;

        readonly long ResBits;
#endif

        readonly IntPtr Internal;
    }
}