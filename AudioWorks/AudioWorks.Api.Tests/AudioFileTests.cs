using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.IO;
using System.Security.Cryptography;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public sealed class AudioFileTests
    {
        [Theory(DisplayName = "AudioFile has the expected FileInfo property value")]
        [MemberData(nameof(TestFilesValidDataSource.FileNames), MemberType = typeof(TestFilesValidDataSource))]
        public void HasExpectedFileInfo([NotNull] string fileName)
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
        public void HasAudioInfo([NotNull] string fileName)
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
        public void HasMetadata([NotNull] string fileName)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.IsAssignableFrom<AudioMetadata>(
                AudioFileFactory.Create(path).Metadata);
        }

        [Fact(DisplayName = "AudioFile's Metadata property throws an exception when set to null")]
        public void MetadataNullThrowsException()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            var audioFile = new AudioFile(null, null, null, null);
            Assert.Throws<ArgumentNullException>(() => audioFile.Metadata = null);
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Theory(DisplayName = "AudioFile's SaveMetadata method creates the expected output")]
        [MemberData(nameof(TestFilesValidSaveMetadataDataSource.Data), MemberType = typeof(TestFilesValidSaveMetadataDataSource))]
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
            var audioFile = AudioFileFactory.Create(path);
            audioFile.Metadata = metadata;
            audioFile.SaveMetadata(settings);
            Assert.Equal(expectedHash, CalculateHash(audioFile));
        }

        [Theory(DisplayName = "AudioFile's SaveMetadata method throws an exception if the file is unsupported")]
        [MemberData(nameof(TestFilesUnsupportedSaveMetadataDataSource.Data), MemberType = typeof(TestFilesUnsupportedSaveMetadataDataSource))]
        public void SaveMetadataUnsupportedFileThrowsException(int index, [NotNull] string fileName)
        {
            var path = Path.Combine("Output", "SaveMetadata", "Unsupported", $"{index:00} - {fileName}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName), path, true);
            var audioFile = AudioFileFactory.Create(path);
            Assert.Throws<AudioUnsupportedException>(() =>
                audioFile.SaveMetadata());
        }

        [Theory(DisplayName = "AudioFile's SaveMetadata method throws an exception if an unexpected setting is provided")]
        [MemberData(nameof(TestFilesValidSettingsInvalidSaveMetadataDataSource.Data), MemberType = typeof(TestFilesValidSettingsInvalidSaveMetadataDataSource))]
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
                AudioFileFactory.Create(path).SaveMetadata(settings));
        }

        [Pure, NotNull]
        static string CalculateHash([NotNull] AudioFile audioFile)
        {
            using (var md5 = MD5.Create())
            using (var fileStream = audioFile.FileInfo.OpenRead())
                return BitConverter.ToString(md5.ComputeHash(fileStream))
                    .Replace("-", string.Empty);
        }
    }
}
