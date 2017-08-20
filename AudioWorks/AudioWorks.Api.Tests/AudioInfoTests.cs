using AudioWorks.Common;
using JetBrains.Annotations;
using System.IO;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public class AudioInfoTests
    {
        [Theory(DisplayName = "AudioInfo has the expected Description property value")]
        [ClassData(typeof(ValidTestFilesClassData))]
        public void AudioInfoHasExpectedDescription([NotNull] string fileName, [NotNull] AudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(expectedAudioInfo.Description, AudioFileFactory.Create(path).AudioInfo.Description);
        }
    }
}
