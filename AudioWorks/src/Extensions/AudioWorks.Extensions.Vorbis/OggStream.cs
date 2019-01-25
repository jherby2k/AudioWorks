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
    sealed class OggStream : IDisposable
    {
        readonly IntPtr _state;

#if WINDOWS
        internal int SerialNumber => Marshal.PtrToStructure<OggStreamState>(_state).SerialNumber;
#else
        internal long SerialNumber => Marshal.PtrToStructure<OggStreamState>(_state).SerialNumber;
#endif

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
#if WINDOWS
        internal OggStream(int serialNumber)
#else
        internal OggStream(long serialNumber)
#endif
        {
            _state = Marshal.AllocHGlobal(Marshal.SizeOf<OggStreamState>());
            SafeNativeMethods.OggStreamInit(_state, serialNumber);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void PageIn(in OggPage page)
        {
            SafeNativeMethods.OggStreamPageIn(_state, page);
        }

        internal bool PageOut(out OggPage page)
        {
            return SafeNativeMethods.OggStreamPageOut(_state, out page) != 0;
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void PacketIn(in OggPacket packet)
        {
            SafeNativeMethods.OggStreamPacketIn(_state, packet);
        }

        internal bool PacketOut(out OggPacket packet)
        {
            return SafeNativeMethods.OggStreamPacketOut(_state, out packet) == 1;
        }

        internal bool Flush(out OggPage page)
        {
            return SafeNativeMethods.OggStreamFlush(_state, out page) != 0;
        }

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void FreeUnmanaged()
        {
            SafeNativeMethods.OggStreamClear(_state);
            Marshal.FreeHGlobal(_state);
        }

        ~OggStream()
        {
            FreeUnmanaged();
        }
    }
}