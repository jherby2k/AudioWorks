using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.IO;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public class AudioFileFactoryTests
    {
        [Fact(DisplayName = "AudioFileFactory.Create throws an exception if the Path is null")]
        public void CreatePathNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws(typeof(ArgumentNullException), () => AudioFileFactory.Create(null));
        }

        [Fact(DisplayName = "AudioFileFactory.Create throws an exception if the Path cannot be found")]
        public void CreatePathNotFoundThrowsException()
        {
            Assert.Throws(typeof(FileNotFoundException), () => AudioFileFactory.Create("Foo"));
        }

        [Theory(DisplayName = "AudioFileFactory.Create throws an exception if the Path is an unsupported file")]
        [ClassData(typeof(UnsupportedTestFilesClassData))]
        public void CreatePathUnsupportedThrowsException([NotNull] string fileName)
        {
            Assert.Throws(typeof(UnsupportedFileException), () => AudioFileFactory.Create(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Unsupported",
                fileName)));
        }

        [Theory(DisplayName = "AudioFileFactory.Create throws an exception if the Path is an invalid file")]
        [ClassData(typeof(InvalidTestFilesClassData))]
        public void CreatePathInvalidThrowsException([NotNull] string fileName)
        {
            Assert.Throws(typeof(InvalidFileException), () => AudioFileFactory.Create(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Invalid",
                fileName)));
        }

        [Theory(DisplayName = "AudioFileFactory.Create returns an AudioFile")]
        [ClassData(typeof(ValidTestFilesClassData))]
        public void CreateReturnsAudioFile([NotNull] string fileName)
        {
            Assert.IsType<AudioFile>(AudioFileFactory.Create(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName)));
        }
    }
}
