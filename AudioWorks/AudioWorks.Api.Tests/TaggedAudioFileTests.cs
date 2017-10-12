using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using ObjectsComparer;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public sealed class TaggedAudioFileTests
    {
        [Fact(DisplayName = "TaggedAudioFile's constructor throws an exception if the path is null")]
        public void ConstructorPathNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new TaggedAudioFile(null));
        }

        [Fact(DisplayName = "TaggedAudioFile's constructor throws an exception if the path cannot be found")]
        public void ConstructorPathNotFoundThrowsException()
        {
            Assert.Throws<FileNotFoundException>(() =>
                new TaggedAudioFile("Foo"));
        }

        [Theory(DisplayName = "TaggedAudioFile's constructor throws an exception if the path is an unsupported file")]
        [MemberData(nameof(UnsupportedFileDataSource.Data), MemberType = typeof(UnsupportedFileDataSource))]
        public void ConstructorPathUnsupportedThrowsException([NotNull] string fileName)
        {
            Assert.Throws<AudioUnsupportedException>(() =>
                new TaggedAudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                    "TestFiles",
                    "Unsupported",
                    fileName)));
        }

        [Theory(DisplayName = "TaggedAudioFile's constructor throws an exception if the Path is an invalid file")]
        [MemberData(nameof(InvalidFileDataSource.Data), MemberType = typeof(InvalidFileDataSource))]
        public void ConstructorPathInvalidThrowsException([NotNull] string fileName)
        {
            Assert.Throws<AudioInvalidException>(() =>
                new TaggedAudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                    "TestFiles",
                    "Invalid",
                    fileName)));
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property is set")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void HasMetadata([NotNull] string fileName)
        {
            Assert.IsAssignableFrom<AudioMetadata>(
                new TaggedAudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                    "TestFiles",
                    "Valid",
                    fileName)).Metadata);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property throws an exception when set to null")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void MetadataNullThrowsException([NotNull] string fileName)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new TaggedAudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                    "TestFiles",
                    "Valid",
                    fileName)).Metadata = null);
        }

        [Theory(DisplayName = "TaggedAudioFile's SaveMetadata method creates the expected output")]
        [MemberData(nameof(SaveMetadataDataValidFileSource.Data), MemberType = typeof(SaveMetadataDataValidFileSource))]
        public void SaveMetadataCreatesExpectedOutput(
            int index,
            [NotNull] string fileName,
            [NotNull] AudioMetadata metadata,
            [CanBeNull] SettingDictionary settings,
            [NotNull] string expectedHash)
        {
            var path = Path.Combine("Output", "SaveMetadata", "Valid", $"{index:00} - {fileName}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName), path, true);
            var audioFile = new TaggedAudioFile(path) { Metadata = metadata };
            audioFile.SaveMetadata(settings);
            Assert.Equal(expectedHash, CalculateHash(audioFile));
        }

        [Theory(DisplayName = "TaggedAudioFile's SaveMetadata method throws an exception if the file is unsupported")]
        [MemberData(nameof(SaveMetadataUnsupportedFileDataSource.Data), MemberType = typeof(SaveMetadataUnsupportedFileDataSource))]
        public void SaveMetadataUnsupportedFileThrowsException(int index, [NotNull] string fileName)
        {
            var path = Path.Combine("Output", "SaveMetadata", "Unsupported", $"{index:00} - {fileName}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName), path, true);
            var audioFile = new TaggedAudioFile(path);
            Assert.Throws<AudioUnsupportedException>(() =>
                audioFile.SaveMetadata());
        }

        [Theory(DisplayName = "TaggedAudioFile's SaveMetadata method throws an exception if an unexpected setting is provided")]
        [MemberData(nameof(SaveMetadataInvalidSettingsDataSource.Data), MemberType = typeof(SaveMetadataInvalidSettingsDataSource))]
        public void SaveMetadataUnexpectedSettingThrowsException(
            int index,
            [NotNull] string fileName,
            [CanBeNull] SettingDictionary settings)
        {
            var path = Path.Combine("Output", "SaveMetadata", "UnexpectedSetting", $"{index:00} - {fileName}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName), path, true);
            Assert.Throws<ArgumentException>(() =>
                new TaggedAudioFile(path).SaveMetadata(settings));
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property is properly serialized")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void MetadataIsSerialized([NotNull] string fileName)
        {
            var audioFile = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName));

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, audioFile);
                stream.Seek(0, SeekOrigin.Begin);
                Assert.True(new Comparer<AudioMetadata>().Compare(
                    audioFile.Metadata,
                    ((TaggedAudioFile) formatter.Deserialize(stream)).Metadata));
            }
        }

        [Pure, NotNull]
        static string CalculateHash([NotNull] IAudioFile audioFile)
        {
            using (var md5 = MD5.Create())
            using (var fileStream = audioFile.FileInfo.OpenRead())
                return BitConverter.ToString(md5.ComputeHash(fileStream))
                    .Replace("-", string.Empty);
        }
    }
}
