using AudioWorks.Common;
using System;
using System.IO;
using JetBrains.Annotations;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public sealed class AudioMetadataTests
    {
        [Fact(DisplayName = "AudioMetadata throws an exception if the Title is null")]
        public void AudioMetadataTitleNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata{ Title = null });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid Title")]
        public void AudioMetadataAcceptsValidTitle()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Title = "Test Title" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Title")]
        public void AudioMetadataAcceptsEmptyTitle()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Title = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Title property defaults to an empty string")]
        public void AudioMetadataTitleDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().Title);
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Artist is null")]
        public void AudioMetadataArtistNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Artist = null });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid Artist")]
        public void AudioMetadataAcceptsValidArtist()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Artist = "Test Artist" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Artist")]
        public void AudioMetadataAcceptsEmptyArtist()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Artist = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Artist property defaults to an empty string")]
        public void AudioMetadataArtistDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().Artist);
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Album is null")]
        public void AudioMetadataAlbumNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Album = null });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid Album")]
        public void AudioMetadataAcceptsValidAlbum()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Album = "Test Album" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Album")]
        public void AudioMetadataAcceptsEmptyAlbum()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Album = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Album property defaults to an empty string")]
        public void AudioMetadataAlbumDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().Album);
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Comment is null")]
        public void AudioMetadataCommentNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Comment = null });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid Comment")]
        public void AudioMetadataAcceptsValidComment()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Comment = "Test Comment" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Comment")]
        public void AudioMetadataAcceptsEmptyComment()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Comment = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Comment property defaults to an empty string")]
        public void AudioMetadataCommentDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().Comment);
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Year is null")]
        public void AudioMetadataYearNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Year = null });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Year is less than 4 characters")]
        public void AudioMetadataYearTooShortThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new AudioMetadata { Year = "999" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Year is more than 4 characters")]
        public void AudioMetadataYearTooLongThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new AudioMetadata { Year = "10000" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Year isn't numeric")]
        public void AudioMetadataYearNotNumericThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new AudioMetadata { Year = "Test" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Year starts with a zero")]
        public void AudioMetadataYearStartsWithZeroThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new AudioMetadata { Year = "0100" });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid Year")]
        public void AudioMetadataAcceptsValidYear()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Year = "2000" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Year")]
        public void AudioMetadataAcceptsEmptyYear()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Year = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Year property defaults to an empty string")]
        public void AudioMetadataYearDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().Year);
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackNumber is null")]
        public void AudioMetadataTrackNumberNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { TrackNumber = null });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackNumber is zero")]
        public void AudioMetadataTrackNumberZeroThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new AudioMetadata { TrackNumber = "0" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackNumber is more than 2 characters")]
        public void AudioMetadataTrackNumberTooLongThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new AudioMetadata { TrackNumber = "100" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackNumber isn't numeric")]
        public void AudioMetadataTrackNumberNotNumericThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new AudioMetadata { TrackNumber = "##" });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid TrackNumber")]
        public void AudioMetadataAcceptsValidTrackNumber()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { TrackNumber = "1" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata normalizes TrackNumber to 2 digits")]
        public void AudioMetadataNormalizesTrackNumber()
        {
            Assert.True(new AudioMetadata { TrackNumber = "1" }.TrackNumber == "01");
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty TrackNumber")]
        public void AudioMetadataAcceptsEmptyTrackNumber()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { TrackNumber = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's TrackNumber property defaults to an empty string")]
        public void AudioMetadataTrackNumberDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().TrackNumber);
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackCount is null")]
        public void AudioMetadataTrackCountNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { TrackCount = null });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackCount is zero")]
        public void AudioMetadataTrackCountZeroThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new AudioMetadata { TrackCount = "0" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackCount is more than 2 characters")]
        public void AudioMetadataTrackCountTooLongThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new AudioMetadata { TrackCount = "100" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackCount isn't numeric")]
        public void AudioMetadataTrackCountNotNumericThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new AudioMetadata { TrackCount = "##" });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid TrackCount")]
        public void AudioMetadataAcceptsValidTrackCount()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { TrackCount = "1" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata normalizes TrackCount to 2 digits")]
        public void AudioMetadataNormalizesTrackCount()
        {
            Assert.True(new AudioMetadata { TrackCount = "1" }.TrackCount == "01");
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty TrackCount")]
        public void AudioMetadataAcceptsEmptyTrackCount()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { TrackCount = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's TrackCount property defaults to an empty string")]
        public void AudioMetadataTrackCountDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().TrackCount);
        }

        [Theory(DisplayName = "AudioMetadata has the expected Title property value")]
        [MemberData(nameof(TestFilesValidDataSource.FileNamesAndMetadata), MemberType = typeof(TestFilesValidDataSource))]
        public void AudioInfoHasExpectedTitle([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(
                expectedMetadata.Title,
                AudioFileFactory.Create(path).Metadata.Title);
        }

        [Theory(DisplayName = "AudioMetadata has the expected Artist property value")]
        [MemberData(nameof(TestFilesValidDataSource.FileNamesAndMetadata), MemberType = typeof(TestFilesValidDataSource))]
        public void AudioInfoHasExpectedArtist([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(
                expectedMetadata.Artist,
                AudioFileFactory.Create(path).Metadata.Artist);
        }

        [Theory(DisplayName = "AudioMetadata has the expected Album property value")]
        [MemberData(nameof(TestFilesValidDataSource.FileNamesAndMetadata), MemberType = typeof(TestFilesValidDataSource))]
        public void AudioInfoHasExpectedAlbum([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(
                expectedMetadata.Album,
                AudioFileFactory.Create(path).Metadata.Album);
        }

        [Theory(DisplayName = "AudioMetadata has the expected Comment property value")]
        [MemberData(nameof(TestFilesValidDataSource.FileNamesAndMetadata), MemberType = typeof(TestFilesValidDataSource))]
        public void AudioInfoHasExpectedComment([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(
                expectedMetadata.Comment,
                AudioFileFactory.Create(path).Metadata.Comment);
        }

        [Theory(DisplayName = "AudioMetadata has the expected Year property value")]
        [MemberData(nameof(TestFilesValidDataSource.FileNamesAndMetadata), MemberType = typeof(TestFilesValidDataSource))]
        public void AudioInfoHasExpectedYear([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(
                expectedMetadata.Year,
                AudioFileFactory.Create(path).Metadata.Year);
        }

        [Theory(DisplayName = "AudioMetadata has the expected TrackNumber property value")]
        [MemberData(nameof(TestFilesValidDataSource.FileNamesAndMetadata), MemberType = typeof(TestFilesValidDataSource))]
        public void AudioInfoHasExpectedTrackNumber([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(
                expectedMetadata.TrackNumber,
                AudioFileFactory.Create(path).Metadata.TrackNumber);
        }

        [Theory(DisplayName = "AudioMetadata has the expected TrackCount property value")]
        [MemberData(nameof(TestFilesValidDataSource.FileNamesAndMetadata), MemberType = typeof(TestFilesValidDataSource))]
        public void AudioInfoHasExpectedTrackCount([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(
                expectedMetadata.TrackCount,
                AudioFileFactory.Create(path).Metadata.TrackCount);
        }
    }
}
