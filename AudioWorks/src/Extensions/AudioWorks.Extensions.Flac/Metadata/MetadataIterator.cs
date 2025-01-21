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

namespace AudioWorks.Extensions.Flac.Metadata
{
    sealed class MetadataIterator : IDisposable
    {
        readonly MetadataIteratorHandle _handle = LibFlac.MetadataIteratorNew();

        internal MetadataIterator(MetadataChainHandle chainHandle) =>
            LibFlac.MetadataIteratorInit(_handle, chainHandle);

        internal bool Next() => LibFlac.MetadataIteratorNext(_handle);

        internal nint GetBlock() => LibFlac.MetadataIteratorGetBlock(_handle);

        internal void InsertBlockAfter(MetadataObject metadataObject)
        {
            // The iterator takes ownership of the handle
            LibFlac.MetadataIteratorInsertBlockAfter(_handle, metadataObject.Handle);
            metadataObject.Handle.DropOwnership();
        }

        internal void DeleteBlock(bool replaceWithPadding) =>
            LibFlac.MetadataIteratorDeleteBlock(_handle, replaceWithPadding);

        public void Dispose() => _handle.Dispose();
    }
}