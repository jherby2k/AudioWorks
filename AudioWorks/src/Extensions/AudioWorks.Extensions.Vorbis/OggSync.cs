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
    sealed class OggSync : IDisposable
    {
        readonly nint _state;

        internal unsafe OggSync()
        {
            _state = Marshal.AllocHGlobal(sizeof(OggSyncState));
            _ = LibOgg.SyncInit(_state);
        }

        internal bool PageOut(out OggPage page) => LibOgg.SyncPageOut(_state, out page) == 1;

        internal unsafe void* Buffer(int size) =>
            LibOgg.SyncBuffer(_state, new(size));

        internal void Wrote(int bytes) =>
            _ = LibOgg.SyncWrote(_state, new(bytes));

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        void FreeUnmanaged()
        {
            _ = LibOgg.SyncClear(_state);
            Marshal.FreeHGlobal(_state);
        }

        ~OggSync() => FreeUnmanaged();
    }
}