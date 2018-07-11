using System.IO;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    sealed class EncodedFileName : EncodedString
    {
        internal EncodedFileName([NotNull] string encodedDirectoryName)
            : base(encodedDirectoryName, Path.GetInvalidFileNameChars())
        {
        }
    }
}