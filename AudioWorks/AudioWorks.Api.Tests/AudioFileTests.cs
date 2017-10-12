using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Common;
using JetBrains.Annotations;
using ObjectsComparer;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public sealed class AudioFileTests
    {
        [Fact(DisplayName = "AudioFile's constructor throws an exception if the path is null")]
        public void ConstructorPathNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioFile(null));
        }

        [Fact(DisplayName = "AudioFile's constructor throws an exception if the path cannot be found")]
        public void ConstructorPathNotFoundThrowsException()
        {
            Assert.Throws<FileNotFoundException>(() =>
                new AudioFile("Foo"));
        }

        [Theory(DisplayName = "AudioFile's constructor throws an exception if the path is an unsupported file")]
        [MemberData(nameof(UnsupportedFileDataSource.Data), MemberType = typeof(UnsupportedFileDataSource))]
        public void ConstructorPathUnsupportedThrowsException([NotNull] string fileName)
        {
            Assert.Throws<AudioUnsupportedException>(() =>
                new AudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                    "TestFiles",
                    "Unsupported",
                    fileName)));
        }

        [Theory(DisplayName = "AudioFile's constructor throws an exception if the Path is an invalid file")]
        [MemberData(nameof(InvalidFileDataSource.Data), MemberType = typeof(InvalidFileDataSource))]
        public void ConstructorPathInvalidThrowsException([NotNull] string fileName)
        {
            Assert.Throws<AudioInvalidException>(() =>
                new AudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                    "TestFiles",
                    "Invalid",
                    fileName)));
        }

        [Theory(DisplayName = "AudioFile has the expected FileInfo property value")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedFileInfo([NotNull] string fileName)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(
                path,
                new AudioFile(path).FileInfo.FullName);
        }

        [Theory(DisplayName = "AudioFile's AudioInfo property is set")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void HasAudioInfo([NotNull] string fileName)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.IsAssignableFrom<AudioInfo>(
                new AudioFile(path).AudioInfo);
        }

        [Theory(DisplayName = "AudioFile's FileInfo property is properly serialized")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void FileInfoIsSerialized([NotNull] string fileName)
        {
            var audioFile = new AudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName));

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, audioFile);
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal(audioFile.FileInfo.FullName, ((AudioFile)formatter.Deserialize(stream)).FileInfo.FullName);
            }
        }

        [Theory(DisplayName = "AudioFile's AudioInfo property is properly serialized")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void AudioInfoIsSerialized([NotNull] string fileName)
        {
            var audioFile = new AudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName));

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, audioFile);
                stream.Seek(0, SeekOrigin.Begin);
                Assert.True(new Comparer<AudioInfo>().Compare(
                    audioFile.AudioInfo,
                    ((AudioFile) formatter.Deserialize(stream)).AudioInfo));
            }
        }
    }
}
