using JetBrains.Annotations;
using System;

namespace AudioWorks.Extensions.Flac
{
    sealed class NativeMetadataIterator : IDisposable
    {
        [NotNull] readonly NativeMetadataIteratorHandle _handle = SafeNativeMethods.MetadataIteratorNew();

        internal NativeMetadataIterator([NotNull] NativeMetadataChainHandle chainHandle)
        {
            SafeNativeMethods.MetadataIteratorInit(_handle, chainHandle);
        }

        internal bool Next()
        {
            return SafeNativeMethods.MetadataIteratorNext(_handle);
        }

        internal IntPtr GetBlock()
        {
            return SafeNativeMethods.MetadataIteratorGetBlock(_handle);
        }

        internal void InsertBlockAfter([NotNull] NativeMetadataBlock metadataBlock)
        {
            // The iterator takes ownership of the handle
            SafeNativeMethods.MetadataIteratorInsertBlockAfter(_handle, metadataBlock.Handle);
            metadataBlock.Handle.DropOwnership();
        }

        internal void DeleteBlock(bool replaceWithPadding)
        {
            SafeNativeMethods.MetadataIteratorDeleteBlock(_handle, replaceWithPadding);
        }

        public void Dispose()
        {
            _handle.Dispose();
        }
    }
}