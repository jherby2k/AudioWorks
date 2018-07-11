using System.IO;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    sealed class EncodedDirectoryName : EncodedString
    {
        internal EncodedDirectoryName([NotNull] string encodedDirectoryName)
            : base(encodedDirectoryName, Path.GetInvalidPathChars())
        {
        }
    }
}