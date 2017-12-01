using System;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    sealed class MetadataIterator : IDisposable
    {
        [NotNull] readonly MetadataIteratorHandle _handle = SafeNativeMethods.MetadataIteratorNew();

        internal MetadataIterator([NotNull] MetadataChainHandle chainHandle)
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

        internal void InsertBlockAfter([NotNull] MetadataBlock metadataBlock)
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