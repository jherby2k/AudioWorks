/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
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
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests
{
    public sealed class TaggedAudioFileTests
    {
        readonly IMapper _mapper;

        public TaggedAudioFileTests(ITestOutputHelper outputHelper)
        {
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;
            _mapper = new MapperConfiguration(config => config.CreateMap<AudioMetadata, AudioMetadata>()).CreateMapper();
        }

        [Fact(DisplayName = "TaggedAudioFile's constructor throws an exception if the path is null")]
        [SuppressMessage("Performance", "CS8625:Cannot convert null literal to non-nullable reference type")]
        public void ConstructorPathNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new TaggedAudioFile(null));

        [Fact(DisplayName = "TaggedAudioFile's constructor throws an exception if the path cannot be found")]
        public void ConstructorPathNotFoundThrowsException() =>
            Assert.Throws<FileNotFoundException>(() => new TaggedAudioFile("Foo"));

        [Theory(DisplayName = "TaggedAudioFile's constructor throws an exception if the path is an unsupported file")]
        [MemberData(nameof(UnsupportedFileDataSource.Data), MemberType = typeof(UnsupportedFileDataSource))]
        public void ConstructorPathUnsupportedThrowsException(string fileName) =>
            Assert.Throws<AudioUnsupportedException>(() =>
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Unsupported", fileName)));

        [Theory(DisplayName = "TaggedAudioFile's constructor throws an exception if the Path is an invalid file")]
        [MemberData(nameof(InvalidFileDataSource.Data), MemberType = typeof(InvalidFileDataSource))]
        public void ConstructorPathInvalidThrowsException(string fileName) =>
            Assert.Throws<AudioInvalidException>(() =>
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Invalid", fileName)));

        [Theory(DisplayName = "TaggedAudioFile's Metadata property is not null")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void MetadataNotNull(string fileName) =>
            Assert.NotNull(
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Title")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedTitle(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.Title,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.Title);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Artist")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedArtist(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.Artist,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.Artist);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Album")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedAlbum(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.Album,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.Album);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected AlbumArtist")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedAlbumArtist(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.AlbumArtist,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.AlbumArtist);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Composer")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedComposer(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.Composer,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.Composer);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Genre")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedGenre(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.Genre,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.Genre);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Comment")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedComment(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.Comment,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.Comment);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Day")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedDay(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.Day,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.Day);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Month")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedMonth(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.Month,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.Month);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected Year")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedYear(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.Year,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.Year);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected TrackNumber")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedTrackNumber(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.TrackNumber,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.TrackNumber);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected TrackCount")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedTrackCount(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.TrackCount,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.TrackCount);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected TrackPeak")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedTrackPeak(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.TrackPeak,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.TrackPeak);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected AlbumPeak")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedAlbumPeak(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.AlbumPeak,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.AlbumPeak);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected TrackGain")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedTrackGain(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.TrackGain,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.TrackGain);

        [Theory(DisplayName = "TaggedAudioFile's Metadata property has the expected AlbumGain")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void MetadataHasExpectedAlbumGain(string fileName, TestAudioMetadata expectedMetadata) =>
            Assert.Equal(
                expectedMetadata.AlbumGain,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.AlbumGain);

        [Theory(DisplayName = "TaggedAudioFile's Metadata.CoverArt property has the expected Width")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.FileNamesAndWidth),
            MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void CoverArtHasExpectedWidth(string fileName, int expectedWidth) =>
            Assert.Equal(
                expectedWidth,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.CoverArt?
                    .Width ?? 0);

        [Theory(DisplayName = "TaggedAudioFile's Metadata.CoverArt property has the expected Height")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.FileNamesAndHeight),
            MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void CoverArtHasExpectedHeight(string fileName, int expectedHeight) =>
            Assert.Equal(
                expectedHeight,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.CoverArt?
                    .Height ?? 0);

        [Theory(DisplayName = "TaggedAudioFile's Metadata.CoverArt property has the expected ColorDepth")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.FileNamesAndColorDepth),
            MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void CoverArtHasExpectedColorDepth(string fileName, int expectedColorDepth) =>
            Assert.Equal(
                expectedColorDepth,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(),"Valid", fileName)).Metadata.CoverArt?
                    .ColorDepth ?? 0);

        [Theory(DisplayName = "TaggedAudioFile's Metadata.CoverArt property has the expected Lossless value")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.FileNamesAndLossless),
            MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void CoverArtHasExpectedLossless(string fileName, bool expectedLossless) =>
            Assert.Equal(
                expectedLossless,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.CoverArt?
                    .Lossless ?? false);

        [Theory(DisplayName = "TaggedAudioFile's Metadata.CoverArt property has the expected MimeType")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.FileNamesAndMimeType),
            MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void CoverArtHasExpectedMimeType(string fileName, string expectedMimeType) =>
            Assert.Equal(
                expectedMimeType,
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.CoverArt?
                    .MimeType ?? string.Empty);

        [Theory(DisplayName = "TaggedAudioFile's Metadata.CoverArt.GetData method returns the expected value")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.FileNamesAndDataHash),
            MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void CoverArtGetDataReturnsExpectedValue(string fileName, string expectedHash)
        {
            var result = new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Metadata.CoverArt?
                .Data.ToArray();

            Assert.Equal(expectedHash, result == null ? string.Empty : HashUtility.CalculateHash(result));
        }

        [Theory(DisplayName = "TaggedAudioFile's Rename method throws an exception if the name is null")]
        [MemberData(nameof(RenameValidFileDataSource.FileNames), MemberType = typeof(RenameValidFileDataSource))]
        [SuppressMessage("Performance", "CS8625:Cannot convert null literal to non-nullable reference type")]
        public void RenameNullNameThrowsException(string fileName)
        {
            var path = Path.Combine("Output", "Rename", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName), path, true);

            Assert.Throws<ArgumentNullException>(() =>
                new TaggedAudioFile(path).Rename(null, true));
        }

        [Theory(DisplayName = "TaggedAudioFile's Rename method renames the file")]
        [MemberData(nameof(RenameValidFileDataSource.Data), MemberType = typeof(RenameValidFileDataSource))]
        public void RenameRenamesFile(string fileName, TestAudioMetadata metadata, string name, string expectedFileName)
        {
            var path = Path.Combine("Output", "Rename", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName), path, true);
            var audioFile = new TaggedAudioFile(path);
            _mapper.Map(metadata, audioFile.Metadata);

            audioFile.Rename(name, true);

            Assert.Equal(expectedFileName, Path.GetFileName(audioFile.Path));
        }

        [Theory(DisplayName = "TaggedAudioFile's LoadMetadata method refreshes the Metadata property")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void LoadMetadataRefreshesMetadata(string fileName)
        {
            var audioFile = new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName));
            var expectedMetadata = new AudioMetadata(audioFile.Metadata);

            audioFile.Metadata.Title = "Modified";
            audioFile.LoadMetadata();

            Assert.True(new MetadataComparer().Equals(expectedMetadata, audioFile.Metadata));
        }

        [Theory(DisplayName = "TaggedAudioFile's SaveMetadata method creates the expected output")]
        [MemberData(nameof(SaveMetadataValidFileSource.Data), MemberType = typeof(SaveMetadataValidFileSource))]
        public void SaveMetadataCreatesExpectedOutput(
            int index,
            string fileName,
            TestAudioMetadata metadata,
            string imageFileName,
            TestSettingDictionary settings,
            string[] validHashes)
        {
            var sourceDirectory = Path.Combine(PathUtility.GetTestFileRoot(), "Valid");
            var path = Path.Combine("Output", "SaveMetadata", "Valid", $"{index:000} - {fileName}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(sourceDirectory, fileName), path, true);
            var audioFile = new TaggedAudioFile(path);
            _mapper.Map(metadata, audioFile.Metadata);
            if (!string.IsNullOrEmpty(imageFileName))
                audioFile.Metadata.CoverArt = CoverArtFactory.GetOrCreate(Path.Combine(sourceDirectory, imageFileName));

            audioFile.SaveMetadata(settings);

            Assert.Contains(HashUtility.CalculateHash(audioFile.Path), validHashes);
        }

        [Theory(DisplayName = "TaggedAudioFile's SaveMetadata method throws an exception if the file is unsupported")]
        [MemberData(nameof(SaveMetadataUnsupportedFileDataSource.Data), MemberType = typeof(SaveMetadataUnsupportedFileDataSource))]
        public void SaveMetadataUnsupportedFileThrowsException(int index, string fileName)
        {
            var path = Path.Combine("Output", "SaveMetadata", "Unsupported", $"{index:000} - {fileName}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName), path, true);
            var audioFile = new TaggedAudioFile(path);

            Assert.Throws<AudioUnsupportedException>(() => audioFile.SaveMetadata());
        }

        [Theory(DisplayName = "TaggedAudioFile's SaveMetadata method throws an exception if an unexpected setting is provided")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void SaveMetadataUnexpectedSettingThrowsException(string fileName) =>
            Assert.Throws<ArgumentException>(() =>
                new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName))
                    .SaveMetadata(new SettingDictionary { ["Foo"] = "Bar" }));

        [Theory(DisplayName = "TaggedAudioFile's Metadata property is properly serialized")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void MetadataIsSerialized(string fileName)
        {
            var audioFile = new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName));
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, audioFile);
                stream.Position = 0;

                Assert.True(new MetadataComparer().Equals(
                    audioFile.Metadata,
                    ((TaggedAudioFile) formatter.Deserialize(stream)).Metadata));
            }
        }
    }
}
