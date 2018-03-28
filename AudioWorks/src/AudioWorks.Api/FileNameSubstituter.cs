using System.IO;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    sealed class FileNameSubstituter : MetadataSubstituter
    {
        internal FileNameSubstituter([NotNull] string encodedDirectoryName)
            : base(encodedDirectoryName, Path.GetInvalidFileNameChars())
        {
        }
    }
}