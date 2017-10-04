using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public sealed class AudioMetadataTests
    {
        [Fact(DisplayName = "AudioMetadata throws an exception if the Title is null")]
        public void TitleNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata{ Title = null });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid Title")]
        public void AcceptsValidTitle()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Title = "Test Title" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Title")]
        public void AcceptsEmptyTitle()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Title = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Title property defaults to an empty string")]
        public void TitleDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().Title);
        }

        [Fact(DisplayName = "AudioMetadata's Title property is properly serialized")]
        public void TitleIsSerialized()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioMetadata { Title = "Test Title" });
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal("Test Title", ((AudioMetadata) formatter.Deserialize(stream)).Title);
            }
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Artist is null")]
        public void ArtistNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Artist = null });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid Artist")]
        public void AcceptsValidArtist()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Artist = "Test Artist" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Artist")]
        public void AcceptsEmptyArtist()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Artist = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Artist property defaults to an empty string")]
        public void ArtistDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().Artist);
        }

        [Fact(DisplayName = "AudioMetadata's Artist property is properly serialized")]
        public void ArtistIsSerialized()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioMetadata { Artist = "Test Artist" });
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal("Test Artist", ((AudioMetadata) formatter.Deserialize(stream)).Artist);
            }
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Album is null")]
        public void AlbumNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Album = null });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid Album")]
        public void AcceptsValidAlbum()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Album = "Test Album" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Album")]
        public void AcceptsEmptyAlbum()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Album = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Album property defaults to an empty string")]
        public void AlbumDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().Album);
        }

        [Fact(DisplayName = "AudioMetadata's Album property is properly serialized")]
        public void AlbumIsSerialized()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioMetadata { Album = "Test Album" });
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal("Test Album", ((AudioMetadata) formatter.Deserialize(stream)).Album);
            }
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Genre is null")]
        public void GenreNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Genre = null });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid Genre")]
        public void AcceptsValidGenre()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Genre = "Test Genre" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Genre")]
        public void AcceptsEmptyGenre()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Genre = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Genre property defaults to an empty string")]
        public void GenreDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().Genre);
        }

        [Fact(DisplayName = "AudioMetadata's Genre property is properly serialized")]
        public void GenreIsSerialized()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioMetadata { Genre = "Test Genre" });
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal("Test Genre", ((AudioMetadata) formatter.Deserialize(stream)).Genre);
            }
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Comment is null")]
        public void CommentNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Comment = null });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid Comment")]
        public void AcceptsValidComment()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Comment = "Test Comment" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Comment")]
        public void AcceptsEmptyComment()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Comment = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Comment property defaults to an empty string")]
        public void CommentDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().Comment);
        }

        [Fact(DisplayName = "AudioMetadata's Comment property is properly serialized")]
        public void CommentIsSerialized()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioMetadata { Comment = "Test Comment" });
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal("Test Comment", ((AudioMetadata) formatter.Deserialize(stream)).Comment);
            }
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Day is null")]
        public void DayNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Day = null });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Day is zero")]
        public void DayZeroThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { Day = "0" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Day is greater than 31")]
        public void DayTooHighThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { Day = "32" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Day isn't numeric")]
        public void DayNotNumericThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { Day = "##" });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid Day")]
        public void AcceptsValidDay()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Day = "31" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Day")]
        public void AcceptsEmptyDay()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Day = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Day property defaults to an empty string")]
        public void DayDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().Day);
        }

        [Fact(DisplayName = "AudioMetadata normalizes the Day property to 2 digits")]
        public void NormalizesDay()
        {
            Assert.Equal("01", new AudioMetadata { Day = "1" }.Day);
        }

        [Fact(DisplayName = "AudioMetadata's Day property is properly serialized")]
        public void DayIsSerialized()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioMetadata { Day = "31" });
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal("31", ((AudioMetadata) formatter.Deserialize(stream)).Day);
            }
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Month is null")]
        public void MonthNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Month = null });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Month is zero")]
        public void MonthZeroThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { Month = "0" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Month is greater than 12")]
        public void MonthTooHighThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { Month = "13" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Month isn't numeric")]
        public void MonthNotNumericThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { Month = "##" });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid Month")]
        public void AcceptsValidMonth()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Month = "12" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Month")]
        public void AcceptsEmptyMonth()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Month = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Month property defaults to an empty string")]
        public void MonthDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().Month);
        }

        [Fact(DisplayName = "AudioMetadata normalizes the Month property to 2 digits")]
        public void NormalizesMonth()
        {
            Assert.Equal("01", new AudioMetadata { Month = "1" }.Month);
        }

        [Fact(DisplayName = "AudioMetadata's Month property is properly serialized")]
        public void MonthIsSerialized()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioMetadata { Month = "01" });
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal("01", ((AudioMetadata) formatter.Deserialize(stream)).Month);
            }
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Year is null")]
        public void YearNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Year = null });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Year is less than 4 characters")]
        public void YearTooShortThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { Year = "999" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Year is more than 4 characters")]
        public void YearTooLongThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { Year = "10000" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Year isn't numeric")]
        public void YearNotNumericThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { Year = "Test" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Year starts with a zero")]
        public void YearStartsWithZeroThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { Year = "0100" });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid Year")]
        public void AcceptsValidYear()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Year = "2000" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty Year")]
        public void AcceptsEmptyYear()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { Year = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's Year property defaults to an empty string")]
        public void YearDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().Year);
        }

        [Fact(DisplayName = "AudioMetadata's Year property is properly serialized")]
        public void YearIsSerialized()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioMetadata { Year = "2017" });
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal("2017", ((AudioMetadata) formatter.Deserialize(stream)).Year);
            }
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackNumber is null")]
        public void TrackNumberNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { TrackNumber = null });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackNumber is zero")]
        public void TrackNumberZeroThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { TrackNumber = "0" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackNumber is more than 2 characters")]
        public void TrackNumberTooLongThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { TrackNumber = "100" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackNumber isn't numeric")]
        public void TrackNumberNotNumericThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { TrackNumber = "##" });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid TrackNumber")]
        public void AcceptsValidTrackNumber()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { TrackNumber = "1" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata normalizes TrackNumber to 2 digits")]
        public void NormalizesTrackNumber()
        {
            Assert.Equal("01", new AudioMetadata { TrackNumber = "1" }.TrackNumber);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty TrackNumber")]
        public void AcceptsEmptyTrackNumber()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { TrackNumber = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's TrackNumber property defaults to an empty string")]
        public void TrackNumberDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().TrackNumber);
        }

        [Fact(DisplayName = "AudioMetadata's TrackNumber property is properly serialized")]
        public void TrackNumberIsSerialized()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioMetadata { TrackNumber = "01" });
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal("01", ((AudioMetadata)formatter.Deserialize(stream)).TrackNumber);
            }
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackCount is null")]
        public void TrackCountNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { TrackCount = null });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackCount is zero")]
        public void TrackCountZeroThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { TrackCount = "0" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackCount is more than 2 characters")]
        public void TrackCountTooLongThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { TrackCount = "100" });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the TrackCount isn't numeric")]
        public void TrackCountNotNumericThrowsException()
        {
            Assert.Throws<AudioMetadataInvalidException>(() =>
                new AudioMetadata { TrackCount = "##" });
        }

        [Fact(DisplayName = "AudioMetadata accepts a valid TrackCount")]
        public void AcceptsValidTrackCount()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { TrackCount = "1" };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata normalizes TrackCount to 2 digits")]
        public void NormalizesTrackCount()
        {
            Assert.Equal("01", new AudioMetadata { TrackCount = "1" }.TrackCount);
        }

        [Fact(DisplayName = "AudioMetadata accepts an empty TrackCount")]
        public void AcceptsEmptyTrackCount()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AudioMetadata { TrackCount = string.Empty };
            Assert.True(true);
        }

        [Fact(DisplayName = "AudioMetadata's TrackCount property defaults to an empty string")]
        public void TrackCountDefaultsToEmpty()
        {
            Assert.Equal(string.Empty, new AudioMetadata().TrackCount);
        }

        [Fact(DisplayName = "AudioMetadata's TrackCount property is properly serialized")]
        public void TrackCountIsSerialized()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioMetadata { TrackCount = "12" });
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal("12", ((AudioMetadata) formatter.Deserialize(stream)).TrackCount);
            }
        }

        [Theory(DisplayName = "AudioMetadata has the expected Title property value")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedTitle([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
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
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedArtist([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
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
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedAlbum([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
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

        [Theory(DisplayName = "AudioMetadata has the expected Genre property value")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedGenre([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(
                expectedMetadata.Genre,
                AudioFileFactory.Create(path).Metadata.Genre);
        }

        [Theory(DisplayName = "AudioMetadata has the expected Comment property value")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedComment([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
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

        [Theory(DisplayName = "AudioMetadata has the expected Day property value")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedDay([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(
                expectedMetadata.Day,
                AudioFileFactory.Create(path).Metadata.Day);
        }

        [Theory(DisplayName = "AudioMetadata has the expected Month property value")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedMonth([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(
                expectedMetadata.Month,
                AudioFileFactory.Create(path).Metadata.Month);
        }

        [Theory(DisplayName = "AudioMetadata has the expected Year property value")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedYear([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
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
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedTrackNumber([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
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
        [MemberData(nameof(ValidFileDataSource.FileNamesAndMetadata), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedTrackCount([NotNull] string fileName, [NotNull] AudioMetadata expectedMetadata)
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
