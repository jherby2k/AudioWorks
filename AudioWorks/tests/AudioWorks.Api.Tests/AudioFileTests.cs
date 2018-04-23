using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using JetBrains.Annotations;
using ObjectsComparer;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public sealed class AudioFileTests
    {
        [Fact(DisplayName = "AudioFile's constructor throws an exception if the path is null")]
        public void ConstructorPathNullThrowsException()
        {
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
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
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
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                    "TestFiles",
                    "Invalid",
                    fileName)));
        }

        [Theory(DisplayName = "AudioFile has the expected Path property value")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedPath([NotNull] string fileName)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName);

            Assert.Equal(path, new AudioFile(path).Path);
        }

        [Theory(DisplayName = "AudioFile's Info property is not null")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void InfoNotNull([NotNull] string fileName)
        {
            Assert.NotNull(
                new AudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Info);
        }

        [Theory(DisplayName = "AudioFile's Path property is properly serialized")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void PathIsSerialized([NotNull] string fileName)
        {
            var audioFile = new AudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName));
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, audioFile);
                stream.Position = 0;

                Assert.Equal(audioFile.Path, ((AudioFile) formatter.Deserialize(stream)).Path);
            }
        }

        [Theory(DisplayName = "AudioFile's Info property is properly serialized")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void InfoIsSerialized([NotNull] string fileName)
        {
            var audioFile = new AudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName));
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, audioFile);
                stream.Position = 0;

                Assert.True(new Comparer<AudioInfo>().Compare(
                    audioFile.Info,
                    ((AudioFile) formatter.Deserialize(stream)).Info));
            }
        }

        [Theory(DisplayName = "AudioFile's Info property has the expected Format")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedFormat([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            Assert.Equal(expectedAudioInfo.Format, new AudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Info.Format);
        }

        [Theory(DisplayName = "AudioFile's Info property has the expected Channels")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedChannels([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            Assert.Equal(expectedAudioInfo.Channels, new AudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Info.Channels);
        }

        [Theory(DisplayName = "AudioFile's Info property has the expected BitsPerSample")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedBitsPerSample([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            Assert.Equal(expectedAudioInfo.BitsPerSample, new AudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Info.BitsPerSample);
        }

        [Theory(DisplayName = "AudioFile's Info property has the expected SampleRate")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedSampleRate([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            Assert.Equal(expectedAudioInfo.SampleRate, new AudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Info.SampleRate);
        }

        [Theory(DisplayName = "AudioFile's Info property has the expected BitRate")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedBitRate([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            Assert.Equal(expectedAudioInfo.BitRate, new AudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Info.BitRate);
        }

        [Theory(DisplayName = "AudioFile's Info property has the expected FrameCount")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedFrameCount([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            Assert.Equal(expectedAudioInfo.SampleCount, new AudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Info.FrameCount);
        }

        [Theory(DisplayName = "AudioFile's Info property has the expected PlayLength")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedPlayLength([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            Assert.Equal(
                expectedAudioInfo.SampleCount == 0
                    ? TimeSpan.Zero
                    : new TimeSpan(0, 0,
                        (int) Math.Round(expectedAudioInfo.SampleCount / (double) expectedAudioInfo.SampleRate)),
                new AudioFile(Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                    .Info.PlayLength);
        }
    }
}
