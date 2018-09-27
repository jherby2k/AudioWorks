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
using AudioWorks.Common.Tests.DataSources;
using AudioWorks.TestUtilities;
using JetBrains.Annotations;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Common.Tests
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public sealed class CoverArtFactoryTests
    {
        public CoverArtFactoryTests([NotNull] ITestOutputHelper outputHelper)
        {
            LoggingManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;
        }

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
