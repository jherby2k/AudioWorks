using JetBrains.Annotations;
using System.IO;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public class AudioFileTests
    {
        [Theory(DisplayName = "AudioFile has the expected FileInfo property value")]
        [ClassData(typeof(ValidTestFilesClassData))]
        public void AudioFileHasExpectedFileInfo([NotNull] string fileName)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(path, AudioFileFactory.Create(path).FileInfo.FullName);
        }
    }
}
