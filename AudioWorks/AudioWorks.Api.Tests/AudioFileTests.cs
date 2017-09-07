using AudioWorks.Common;
using JetBrains.Annotations;
using System.IO;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public sealed class AudioFileTests
    {
        [Theory(DisplayName = "AudioFile has the expected FileInfo property value")]
        [ClassData(typeof(ValidTestFilesClassData))]
        public void AudioFileHasExpectedFileInfo([NotNull] string fileName, [NotNull] AudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(path, AudioFileFactory.Create(path).FileInfo.FullName);
        }

        [Theory(DisplayName = "AudioFile's AudioInfo property is set")]
        [ClassData(typeof(ValidTestFilesClassData))]
        public void AudioFileHasAudioInfo([NotNull] string fileName, [NotNull] AudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.IsType<AudioInfo>(AudioFileFactory.Create(path).AudioInfo);
        }
    }
}
