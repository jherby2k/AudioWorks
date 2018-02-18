using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using AutoMapper;
using JetBrains.Annotations;
using ObjectsComparer;
using Xunit;

namespace AudioWorks.Api.Tests
{
    public sealed class TaggedAudioFileTests
    {
        static TaggedAudioFileTests()
        {
            Mapper.Initialize(config => config.CreateMap<AudioMetadata, AudioMetadata>());
        }

        [Fact(DisplayName = "TaggedAudioFile's constructor throws an exception if the path is null")]
        public void ConstructorPathNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new TaggedAudioFile(null));
        }

        [Fact(DisplayName = "TaggedAudioFile's constructor throws an exception if the path cannot be found")]
        public void ConstructorPathNotFoundThrowsException()
        {
            Assert.Throws<FileNotFoundException>(() => new TaggedAudioFile("Foo"));
        }

        [Theory(DisplayName = "TaggedAudioFile's constructor throws an exception if the path is an unsupported file")]
        [MemberData(nameof(UnsupportedFileDataSource.Data), MemberType = typeof(UnsupportedFileDataSource))]
        public void ConstructorPathUnsupportedThrowsException([NotNull] string fileName)
        {
            Assert.Throws<AudioUnsupportedException>(() =>
                new TaggedAudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
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
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
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
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
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
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                    "TestFiles",
                    "Valid",
                    fileName)).Metadata = null);
        }

        [Theory(DisplayName = "TaggedAudioFile's Rename method renames the file")]
        [MemberData(nameof(RenameValidFileDataSource.Data), MemberType = typeof(RenameValidFileDataSource))]
        public void RenameRenamesFile(
            [NotNull] string fileName,
            [NotNull] TestAudioMetadata metadata,
            [NotNull] string name,
            [NotNull] string expectedFileName)
        {
            var path = Path.Combine("Output", "Rename", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName), path, true);
            var audioFile = new TaggedAudioFile(path) { Metadata = metadata };

            audioFile.Rename(name, true);

            Assert.Equal(expectedFileName, Path.GetFileName(audioFile.Path));
        }

        [Theory(DisplayName = "TaggedAudioFile's LoadMetadata method refreshes the Metadata property")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void LoadMetadataRefreshesMetadata([NotNull] string fileName)
        {
            var audioFile = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName));
            var expectedMetadata = Mapper.Map<AudioMetadata>(audioFile.Metadata);

            audioFile.Metadata = new AudioMetadata { Title = "Modified" };
            audioFile.LoadMetadata();

            Assert.True(new Comparer().Compare(expectedMetadata, audioFile.Metadata, out var differences), string.Join(' ', differences));
        }

        [Theory(DisplayName = "TaggedAudioFile's SaveMetadata method creates the expected output")]
        [MemberData(nameof(SaveMetadataValidFileSource.Data), MemberType = typeof(SaveMetadataValidFileSource))]
        public void SaveMetadataCreatesExpectedOutput(
            int index,
            [NotNull] string fileName,
            [NotNull] TestAudioMetadata metadata,
            [CanBeNull] string imageFileName,
            [CanBeNull] TestSettingDictionary settings,
            [NotNull] string expectedHash)
        {
            var sourceDirectory = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid");
            var path = Path.Combine("Output", "SaveMetadata", "Valid", $"{index:00} - {fileName}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(sourceDirectory, fileName), path, true);
            var audioFile = new TaggedAudioFile(path) { Metadata = metadata };
            if (imageFileName != null)
                audioFile.Metadata.CoverArt = CoverArtFactory.Create(Path.Combine(sourceDirectory, imageFileName));

            audioFile.SaveMetadata(settings);

            Assert.Equal(expectedHash, HashUtility.CalculateHash(audioFile));
        }

        [Theory(DisplayName = "TaggedAudioFile's SaveMetadata method throws an exception if the file is unsupported")]
        [MemberData(nameof(SaveMetadataUnsupportedFileDataSource.Data), MemberType = typeof(SaveMetadataUnsupportedFileDataSource))]
        public void SaveMetadataUnsupportedFileThrowsException(int index, [NotNull] string fileName)
        {
            var path = Path.Combine("Output", "SaveMetadata", "Unsupported", $"{index:00} - {fileName}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName), path, true);
            var audioFile = new TaggedAudioFile(path);

            Assert.Throws<AudioUnsupportedException>(() => audioFile.SaveMetadata());
        }

        [Theory(DisplayName = "TaggedAudioFile's SaveMetadata method throws an exception if an unexpected setting is provided")]
        [MemberData(nameof(SaveMetadataInvalidSettingsDataSource.Data), MemberType = typeof(SaveMetadataInvalidSettingsDataSource))]
        public void SaveMetadataUnexpectedSettingThrowsException(
            int index,
            [NotNull] string fileName,
            [CanBeNull] TestSettingDictionary settings)
        {
            var path = Path.Combine("Output", "SaveMetadata", "UnexpectedSetting", $"{index:00} - {fileName}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName), path, true);

            Assert.Throws<ArgumentException>(() => new TaggedAudioFile(path).SaveMetadata(settings));
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property is properly serialized")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void MetadataIsSerialized([NotNull] string fileName)
        {
            var audioFile = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName));
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, audioFile);
                stream.Position = 0;

                Assert.True(new Comparer<AudioMetadata>().Compare(
                    audioFile.Metadata,
                    ((TaggedAudioFile) formatter.Deserialize(stream)).Metadata));
            }
        }
    }
}
