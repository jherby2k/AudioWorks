using System;
using System.IO;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Common;
using JetBrains.Annotations;
using Xunit;

namespace AudioWorks.Api.Tests
{
    public sealed class CoverArtTests
    {
        [Fact(DisplayName = "CoverArt's constructor throws an exception if the path is null")]
        public void ConstructorPathNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new CoverArt(null));
        }

        [Fact(DisplayName = "CoverArt's constructor throws an exception if the path cannot be found")]
        public void ConstructorPathNotFoundThrowsException()
        {
            Assert.Throws<FileNotFoundException>(() => new CoverArt("Foo"));
        }

        [Theory(DisplayName = "CoverArt's constructor throws an exception if the path is an unsupported file")]
        [MemberData(nameof(UnsupportedImageFileDataSource.Data), MemberType = typeof(UnsupportedImageFileDataSource))]
        public void ConstructorPathUnsupportedThrowsException([NotNull] string fileName)
        {
            Assert.Throws<ImageUnsupportedException>(() =>
                new CoverArt(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                    "TestFiles",
                    "Unsupported",
                    fileName)));
        }

        [Theory(DisplayName = "CoverArt's constructor throws an exception if the path is an unsupported file")]
        [MemberData(nameof(InvalidImageFileDataSource.Data), MemberType = typeof(InvalidImageFileDataSource))]
        public void ConstructorPathInvalidThrowsException([NotNull] string fileName)
        {
            Assert.Throws<ImageInvalidException>(() =>
                new CoverArt(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                    "TestFiles",
                    "Invalid",
                    fileName)));
        }

        [Theory(DisplayName = "CoverArt has the expected Format property value")]
        [MemberData(nameof(ValidImageFileDataSource.FileNamesAndFormat), MemberType = typeof(ValidImageFileDataSource))]
        public void HasExpectedFormat(
            [NotNull] string fileName,
            [NotNull] string format)
        {
            Assert.Equal(format, new CoverArt(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName)).Format);
        }

        [Theory(DisplayName = "CoverArt has the expected MimeType property value")]
        [MemberData(nameof(ValidImageFileDataSource.FileNamesAndMimeType), MemberType = typeof(ValidImageFileDataSource))]
        public void HasExpectedMimeType(
            [NotNull] string fileName,
            [NotNull] string mimeType)
        {
            Assert.Equal(mimeType, new CoverArt(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName)).MimeType);
        }

        [Theory(DisplayName = "CoverArt has the expected Width property value")]
        [MemberData(nameof(ValidImageFileDataSource.FileNamesAndWidth), MemberType = typeof(ValidImageFileDataSource))]
        public void HasExpectedWidth(
            [NotNull] string fileName,
            int expectedWidth)
        {
            Assert.Equal(expectedWidth, new CoverArt(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName)).Width);
        }

        [Theory(DisplayName = "CoverArt has the expected Height property value")]
        [MemberData(nameof(ValidImageFileDataSource.FileNamesAndHeight), MemberType = typeof(ValidImageFileDataSource))]
        public void HasExpectedHeight(
            [NotNull] string fileName,
            int expectedHeight)
        {
            Assert.Equal(expectedHeight, new CoverArt(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName)).Height);
        }

        [Theory(DisplayName = "GetData returns the expected value")]
        [MemberData(nameof(ValidImageFileDataSource.FileNamesAndDataHash), MemberType = typeof(ValidImageFileDataSource))]
        public void GetDataReturnsExpectedValue(
            [NotNull] string fileName,
            [NotNull] string expectedHash)
        {
            Assert.Equal(expectedHash, HashUtility.CalculateHash(new CoverArt(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName)).GetData()));
        }
    }
}
