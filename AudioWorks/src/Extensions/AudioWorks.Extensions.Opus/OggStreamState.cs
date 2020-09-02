/* Copyright © 2019 Jeremy Herbison

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

namespace AudioWorks.Extensions.Opus
{
    [StructLayout(LayoutKind.Sequential)]
    unsafe struct OggStreamState
    {
        readonly IntPtr BodyData;

#if WINDOWS
        readonly int BodyStorage;

        readonly int BodyFill;

        readonly int BodyReturned;
#else
        readonly long BodyStorage;

        readonly long BodyFill;

        readonly long BodyReturned;
#endif

        readonly IntPtr LacingValues;

        readonly IntPtr GranuleValues;

#if WINDOWS
        readonly int LacingStorage;

        readonly int LacingFill;

        readonly int LacingPacket;

        readonly int LacingReturned;
#else
        readonly long LacingStorage;

        readonly long LacingFill;

        readonly long LacingPacket;

        readonly long LacingReturned;
#endif

        fixed byte Header[282];

        readonly int HeaderFill;

        readonly int EndOfStream;

        readonly int BeginningOfStream;

#if WINDOWS
        internal readonly int SerialNumber;

        readonly int PageNumber;
#else
        internal readonly long SerialNumber;

        readonly long PageNumber;
#endif

        readonly long PacketNumber;

        readonly long GranulePosition;
    }
}