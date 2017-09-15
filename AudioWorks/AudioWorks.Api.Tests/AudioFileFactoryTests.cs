using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.IO;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public sealed class AudioFileFactoryTests
    {
        [Fact(DisplayName = "AudioFileFactory.Create throws an exception if the Path is null")]
        public void CreatePathNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                AudioFileFactory.Create(null));
        }

        [Fact(DisplayName = "AudioFileFactory.Create throws an exception if the Path cannot be found")]
        public void CreatePathNotFoundThrowsException()
        {
            Assert.Throws<FileNotFoundException>(() =>
                AudioFileFactory.Create("Foo"));
        }

        [Theory(DisplayName = "AudioFileFactory.Create throws an exception if the Path is an unsupported file")]
        [MemberData(nameof(TestFilesUnsupportedDataSource.FileNames), MemberType = typeof(TestFilesUnsupportedDataSource))]
        public void CreatePathUnsupportedThrowsException([NotNull] string fileName)
        {
            Assert.Throws<AudioUnsupportedException>(() =>
                AudioFileFactory.Create(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                    "TestFiles",
                    "Unsupported",
                    fileName)));
        }

        [Theory(DisplayName = "AudioFileFactory.Create throws an exception if the Path is an invalid file")]
        [MemberData(nameof(TestFilesInvalidDataSource.FileNames), MemberType = typeof(TestFilesInvalidDataSource))]
        public void CreatePathInvalidThrowsException([NotNull] string fileName)
        {
            Assert.Throws<AudioInvalidException>(() =>
                AudioFileFactory.Create(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                    "TestFiles",
                    "Invalid",
                    fileName)));
        }

        [Theory(DisplayName = "AudioFileFactory.Create returns an AudioFile")]
        [MemberData(nameof(TestFilesValidDataSource.FileNames), MemberType = typeof(TestFilesValidDataSource))]
        public void CreateReturnsAudioFile([NotNull] string fileName)
        {
            Assert.IsType<AudioFile>(
                AudioFileFactory.Create(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                    "TestFiles",
                    "Valid",
                    fileName)));
        }
    }
}
