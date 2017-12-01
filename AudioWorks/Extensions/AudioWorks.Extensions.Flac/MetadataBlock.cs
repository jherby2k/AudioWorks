using System;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    abstract class MetadataBlock : IDisposable
    {
        [NotNull]
        internal MetadataBlockHandle Handle { get; }

        protected MetadataBlock(MetadataType metadataType)
        {
            Handle = SafeNativeMethods.MetadataObjectNew(metadataType);
        }

        public void Dispose()
        {
            Handle.Dispose();
        }
    }
}