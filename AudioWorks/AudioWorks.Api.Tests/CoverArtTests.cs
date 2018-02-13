using System;
using System.IO;
using AudioWorks.Api.Tests.DataSources;
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
    }
}
