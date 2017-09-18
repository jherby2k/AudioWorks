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
        [MemberData(nameof(TestFilesValidDataSource.FileNames), MemberType = typeof(TestFilesValidDataSource))]
        public void AudioFileHasExpectedFileInfo([NotNull] string fileName)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(
                path,
                AudioFileFactory.Create(path).FileInfo.FullName);
        }

        [Theory(DisplayName = "AudioFile's AudioInfo property is set")]
        [MemberData(nameof(TestFilesValidDataSource.FileNames), MemberType = typeof(TestFilesValidDataSource))]
        public void AudioFileHasAudioInfo([NotNull] string fileName)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.IsAssignableFrom<AudioInfo>(
                AudioFileFactory.Create(path).AudioInfo);
        }

        [Theory(DisplayName = "AudioFile's Metadata property is set")]
        [MemberData(nameof(TestFilesValidDataSource.FileNames), MemberType = typeof(TestFilesValidDataSource))]
        public void AudioFileHasMetadata([NotNull] string fileName)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.IsAssignableFrom<AudioMetadata>(
                AudioFileFactory.Create(path).Metadata);
        }
    }
}
