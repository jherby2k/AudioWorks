using System.IO;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    sealed class DirectoryNameSubstituter : MetadataSubstituter
    {
        internal DirectoryNameSubstituter([NotNull] string encodedDirectoryName)
            : base(encodedDirectoryName, Path.GetInvalidPathChars())
        {
        }
    }
}