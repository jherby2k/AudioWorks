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
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    sealed class MetadataIterator : IDisposable
    {
        [NotNull] readonly MetadataIteratorHandle _handle = SafeNativeMethods.MetadataIteratorNew();

        internal MetadataIterator([NotNull] MetadataChainHandle chainHandle) =>
            SafeNativeMethods.MetadataIteratorInit(_handle, chainHandle);

        internal bool Next() => SafeNativeMethods.MetadataIteratorNext(_handle);

        internal IntPtr GetBlock() => SafeNativeMethods.MetadataIteratorGetBlock(_handle);

        internal void InsertBlockAfter([NotNull] MetadataBlock metadataBlock)
        {
            // The iterator takes ownership of the handle
            SafeNativeMethods.MetadataIteratorInsertBlockAfter(_handle, metadataBlock.Handle);
            metadataBlock.Handle.DropOwnership();
        }

        internal void DeleteBlock(bool replaceWithPadding) =>
            SafeNativeMethods.MetadataIteratorDeleteBlock(_handle, replaceWithPadding);

        public void Dispose() => _handle.Dispose();
    }
}