using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Commands.Tests
{
    [UsedImplicitly]
    public class UnsupportedTestFilesFixture
    {
        [NotNull]
        internal string DirectoryName { get; } = "UnsupportedTestFiles";

        public UnsupportedTestFilesFixture()
        {
            Directory.CreateDirectory(DirectoryName);
            foreach (var file in new DirectoryInfo($@"..\..\..\{DirectoryName}").GetFiles())
                file.CopyTo(Path.Combine(DirectoryName, file.Name), true);
        }
    }
}