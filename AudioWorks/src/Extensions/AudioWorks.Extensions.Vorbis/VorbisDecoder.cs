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
    sealed unsafe class VorbisDecoder : IDisposable
    {
        readonly nint _info = Marshal.AllocHGlobal(sizeof(VorbisInfo));

        internal VorbisDecoder() => LibVorbis.InfoInit(_info);

        internal void HeaderIn(in VorbisComment comment, in OggPacket packet) =>
            _ = LibVorbis.SynthesisHeaderIn(_info, comment, packet);

        internal VorbisInfo GetInfo() => Marshal.PtrToStructure<VorbisInfo>(_info);

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void FreeUnmanaged()
        {
            LibVorbis.InfoClear(_info);
            Marshal.FreeHGlobal(_info);
        }

        ~VorbisDecoder() => FreeUnmanaged();
    }
}