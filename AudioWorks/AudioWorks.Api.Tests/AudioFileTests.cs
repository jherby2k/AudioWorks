using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Common;
using JetBrains.Annotations;
using ObjectsComparer;
using Xunit;

namespace AudioWorks.Api.Tests
{
    public sealed class AudioFileTests
    {
        [Fact(DisplayName = "AudioFile's constructor throws an exception if the path is null")]
        public void ConstructorPathNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new AudioFile(null));
        }

        [Fact(DisplayName = "AudioFile's constructor throws an exception if the path cannot be found")]
        public void ConstructorPathNotFoundThrowsException()
        {
            Assert.Throws<FileNotFoundException>(() => new AudioFile("Foo"));
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

        [Theory(DisplayName = "AudioFile has the expected Path property value")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedPath([NotNull] string fileName)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);

            Assert.Equal(path, new AudioFile(path).Path);
        }

        [Theory(DisplayName = "AudioFile's Info property is set")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void HasInfo([NotNull] string fileName)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);

            Assert.IsAssignableFrom<AudioInfo>(new AudioFile(path).Info);
        }

        [Theory(DisplayName = "AudioFile's Path property is properly serialized")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void PathIsSerialized([NotNull] string fileName)
        {
            var audioFile = new AudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName));
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, audioFile);
                stream.Seek(0, SeekOrigin.Begin);

                Assert.Equal(audioFile.Path, ((AudioFile) formatter.Deserialize(stream)).Path);
            }
        }

        [Theory(DisplayName = "AudioFile's Info property is properly serialized")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void InfoIsSerialized([NotNull] string fileName)
        {
            var audioFile = new AudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName));
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, audioFile);
                stream.Seek(0, SeekOrigin.Begin);

                Assert.True(new Comparer<AudioInfo>().Compare(
                    audioFile.Info,
                    ((AudioFile) formatter.Deserialize(stream)).Info));
            }
        }
    }
}
