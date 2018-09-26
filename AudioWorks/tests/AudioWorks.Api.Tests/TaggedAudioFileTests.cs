/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using AudioWorks.TestUtilities;
using AutoMapper;
using JetBrains.Annotations;
using ObjectsComparer;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public sealed class TaggedAudioFileTests
    {
        static TaggedAudioFileTests()
        {
            XunitLoggerProvider.Instance.Enable(LoggingManager.LoggerFactory);
            Mapper.Initialize(config => config.CreateMap<AudioMetadata, AudioMetadata>());
        }

        public TaggedAudioFileTests([NotNull] ITestOutputHelper outputHelper)
        {
            XunitLoggerProvider.Instance.OutputHelper = outputHelper;
        }

        [Fact(DisplayName = "TaggedAudioFile's constructor throws an exception if the path is null")]
        public void ConstructorPathNullThrowsException()
        {
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

        [Theory(DisplayName = "TaggedAudioFile's Metadata property is not null")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void MetadataNotNull([NotNull] string fileName)
        {
            Assert.NotNull(
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
            Assert.Throws<ArgumentNullException>(() =>
                new TaggedAudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                    "TestFiles",
                    "Valid",
                    fileName)).Metadata = null);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Title")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedTitle([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.Title, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.Title);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Artist")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedArtist([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.Artist, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.Artist);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Album")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedAlbum([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.Album, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.Album);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected AlbumArtist")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedAlbumArtist([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.AlbumArtist, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.AlbumArtist);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Composer")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedComposer([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.Composer, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.Composer);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Genre")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedGenre([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.Genre, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.Genre);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Comment")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedComment([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.Comment, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.Comment);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Day")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedDay([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.Day, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.Day);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Month")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedMonth([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.Month, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.Month);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Year")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedYear([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.Year, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.Year);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected TrackNumber")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedTrackNumber([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.TrackNumber, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.TrackNumber);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected TrackCount")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedTrackCount([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.TrackCount, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.TrackCount);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected TrackPeak")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedTrackPeak([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.TrackPeak, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.TrackPeak);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected AlbumPeak")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedAlbumPeak([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.AlbumPeak, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.AlbumPeak);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected TrackGain")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedTrackGain([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.TrackGain, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.TrackGain);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected AlbumGain")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedAlbumGain([NotNull] string fileName, [NotNull] TestAudioMetadata expectedMetadata)
        {
            Assert.Equal(expectedMetadata.AlbumGain, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.AlbumGain);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata.CoverArt property has the expected Width")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.FileNamesAndWidth),
            MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void CoverArtHasExpectedWidth([NotNull] string fileName, int expectedWidth)
        {
            Assert.Equal(expectedWidth, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.CoverArt?.Width);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata.CoverArt property has the expected Height")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.FileNamesAndHeight),
            MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void CoverArtHasExpectedHeight([NotNull] string fileName, int expectedHeight)
        {
            Assert.Equal(expectedHeight, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.CoverArt?.Height);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata.CoverArt property has the expected ColorDepth")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.FileNamesAndColorDepth),
            MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void CoverArtHasExpectedColorDepth([NotNull] string fileName, int expectedColorDepth)
        {
            Assert.Equal(expectedColorDepth, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.CoverArt?.ColorDepth);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata.CoverArt property has the expected Lossless value")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.FileNamesAndLossless),
            MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void CoverArtHasExpectedLossless([NotNull] string fileName, bool expectedLossless)
        {
            Assert.Equal(expectedLossless, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.CoverArt?.Lossless);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata.CoverArt property has the expected MimeType")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.FileNamesAndMimeType),
            MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void CoverArtHasExpectedMimeType([NotNull] string fileName, [NotNull] string expectedMimeType)
        {
            Assert.Equal(expectedMimeType, new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.CoverArt?.MimeType);
        }

        [Theory(DisplayName = "TaggedAudioFile's Metadata.CoverArt.GetData method returns the expected value")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.FileNamesAndDataHash),
            MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void CoverArtGetDataReturnsExpectedValue([NotNull] string fileName, [NotNull] string expectedHash)
        {
            Assert.Equal(expectedHash, HashUtility.CalculateHash(new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Metadata.CoverArt?.Data.ToArray()));
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

            Assert.True(new Comparer().Compare(expectedMetadata, audioFile.Metadata, out var differences), string.Join(" ", differences));
        }

        [Theory(DisplayName = "TaggedAudioFile's SaveMetadata method creates the expected output")]
        [MemberData(nameof(SaveMetadataValidFileSource.Data), MemberType = typeof(SaveMetadataValidFileSource))]
        public void SaveMetadataCreatesExpectedOutput(
            int index,
            [NotNull] string fileName,
            [NotNull] TestAudioMetadata metadata,
            [CanBeNull] string imageFileName,
            [CanBeNull] TestSettingDictionary settings,
#if LINUX
            [NotNull] string expectedUbuntu1604Hash,
            [NotNull] string expectedUbuntu1804Hash)
#else
            [NotNull] string expectedHash)
#endif
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
                audioFile.Metadata.CoverArt = CoverArtFactory.GetOrCreate(Path.Combine(sourceDirectory, imageFileName));

            audioFile.SaveMetadata(settings);

#if LINUX
            Assert.Equal(LinuxUtility.GetRelease().StartsWith("Ubuntu 16.04", StringComparison.Ordinal)
                ? expectedUbuntu1604Hash
                : expectedUbuntu1804Hash,
                HashUtility.CalculateHash(audioFile.Path));
#else
            Assert.Equal(expectedHash, HashUtility.CalculateHash(audioFile.Path));
#endif
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
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void SaveMetadataUnexpectedSettingThrowsException([NotNull] string fileName)
        {
            Assert.Throws<ArgumentException>(() => new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName)).SaveMetadata(new SettingDictionary { ["Foo"] = "Bar" }));
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
