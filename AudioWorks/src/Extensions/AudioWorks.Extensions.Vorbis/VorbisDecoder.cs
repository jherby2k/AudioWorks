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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Vorbis
{
    sealed class VorbisDecoder : IDisposable
    {
        readonly IntPtr _info = Marshal.AllocHGlobal(Marshal.SizeOf<VorbisInfo>());

        internal VorbisDecoder() => SafeNativeMethods.VorbisInfoInit(_info);

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void HeaderIn(in VorbisComment comment, in OggPacket packet) =>
            SafeNativeMethods.VorbisSynthesisHeaderIn(_info, comment, packet);

        internal VorbisInfo GetInfo() => Marshal.PtrToStructure<VorbisInfo>(_info);

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void FreeUnmanaged()
        {
            SafeNativeMethods.VorbisInfoClear(_info);
            Marshal.FreeHGlobal(_info);
        }

        ~VorbisDecoder() => FreeUnmanaged();
    }
}