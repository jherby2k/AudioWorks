using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AudioWorks.Common.Tests.DataSources;
using JetBrains.Annotations;
using Xunit;

namespace AudioWorks.Common.Tests
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public sealed class CoverArtFactoryTests
    {
        [Fact(DisplayName = "CoverArtFactory's Create method throws an exception if the path is null")]
        public void CreateDataNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => CoverArtFactory.GetOrCreate((byte[]) null));
        }

        [Fact(DisplayName = "CoverArtFactory's Create method throws an exception if the path is null")]
        public void CreatePathNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => CoverArtFactory.GetOrCreate((string) null));
        }

        [Fact(DisplayName = "CoverArtFactory's Create method throws an exception if the path cannot be found")]
        public void CreatePathNotFoundThrowsException()
        {
            Assert.Throws<FileNotFoundException>(() => CoverArtFactory.GetOrCreate("Foo"));
        }

        [Theory(DisplayName = "CoverArtFactory's Create method throws an exception if the path is an unsupported file")]
        [MemberData(nameof(UnsupportedImageFileDataSource.Data), MemberType = typeof(UnsupportedImageFileDataSource))]
        public void CreatePathUnsupportedThrowsException([NotNull] string fileName)
        {
            Assert.Throws<ImageUnsupportedException>(() =>
                CoverArtFactory.GetOrCreate(Path.Combine(
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
                CoverArtFactory.GetOrCreate(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                    "TestFiles",
                    "Invalid",
                    fileName)));
        }
    }
}
