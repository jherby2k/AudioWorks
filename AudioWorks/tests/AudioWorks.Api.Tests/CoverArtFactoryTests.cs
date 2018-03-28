using System;
using System.IO;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Common;
using JetBrains.Annotations;
using Xunit;

namespace AudioWorks.Api.Tests
{
    public sealed class CoverArtFactoryTests
    {
        [Fact(DisplayName = "CoverArtFactory's Create method throws an exception if the path is null")]
        public void CreateDataNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => CoverArtFactory.Create((byte[]) null));
        }

        [Fact(DisplayName = "CoverArtFactory's Create method throws an exception if the path is null")]
        public void CreatePathNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => CoverArtFactory.Create((string) null));
        }

        [Fact(DisplayName = "CoverArtFactory's Create method throws an exception if the path cannot be found")]
        public void CreatePathNotFoundThrowsException()
        {
            Assert.Throws<FileNotFoundException>(() => CoverArtFactory.Create("Foo"));
        }

        [Theory(DisplayName = "CoverArtFactory's Create method throws an exception if the path is an unsupported file")]
        [MemberData(nameof(UnsupportedImageFileDataSource.Data), MemberType = typeof(UnsupportedImageFileDataSource))]
        public void CreatePathUnsupportedThrowsException([NotNull] string fileName)
        {
            Assert.Throws<ImageUnsupportedException>(() =>
                CoverArtFactory.Create(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                    "TestFiles",
                    "Unsupported",
                    fileName)));
        }

        [Theory(DisplayName = "CoverArtFactory's Create method throws an exception if the path is an unsupported file")]
        [MemberData(nameof(InvalidImageFileDataSource.Data), MemberType = typeof(InvalidImageFileDataSource))]
        public void CreatePathInvalidThrowsException([NotNull] string fileName)
        {
            Assert.Throws<ImageInvalidException>(() =>
                CoverArtFactory.Create(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                    "TestFiles",
                    "Invalid",
                    fileName)));
        }
    }
}
