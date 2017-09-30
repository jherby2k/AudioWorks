using JetBrains.Annotations;
using System;

namespace AudioWorks.Extensions.Flac
{
    abstract class NativeMetadataBlock : IDisposable
    {
        [NotNull]
        internal NativeMetadataBlockHandle Handle { get; }

        protected NativeMetadataBlock(MetadataType metadataType)
        {
            Handle = SafeNativeMethods.MetadataObjectNew(metadataType);
        }

        public void Dispose()
        {
            Handle.Dispose();
        }
    }
}