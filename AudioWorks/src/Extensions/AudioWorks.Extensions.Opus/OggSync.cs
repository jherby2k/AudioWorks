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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Opus
{
    sealed class OggSync : IDisposable
    {
        readonly IntPtr _state;

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method always returns 0")]
        internal unsafe OggSync()
        {
            _state = Marshal.AllocHGlobal(sizeof(OggSyncState));
            SafeNativeMethods.OggSyncInit(_state);
        }

        internal bool PageOut(out OggPage page) => SafeNativeMethods.OggSyncPageOut(_state, out page) == 1;

        internal IntPtr Buffer(int size) =>
            SafeNativeMethods.OggSyncBuffer(_state, new(size));

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void Wrote(int bytes) =>
            SafeNativeMethods.OggSyncWrote(_state, new(bytes));

        public void Dispose()
        {
            FreeUnmanaged();
            GC.SuppressFinalize(this);
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method always returns 0")]
        void FreeUnmanaged()
        {
            SafeNativeMethods.OggSyncClear(_state);
            Marshal.FreeHGlobal(_state);
        }

        ~OggSync() => FreeUnmanaged();
    }
}