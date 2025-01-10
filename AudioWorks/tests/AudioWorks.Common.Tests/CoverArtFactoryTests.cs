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
using System.IO;
using AudioWorks.Common.Tests.DataSources;
using AudioWorks.TestUtilities;
using Xunit;

namespace AudioWorks.Common.Tests
{
    public sealed class CoverArtFactoryTests
    {
        public CoverArtFactoryTests(ITestOutputHelper outputHelper) =>
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;

        [Fact(DisplayName = "CoverArtFactory's Create method throws an exception if the path is null")]
        public void CreateDataNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => CoverArtFactory.GetOrCreate((byte[]?) null));

        [Fact(DisplayName = "CoverArtFactory's Create method throws an exception if the path is null")]
        public void CreatePathNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => CoverArtFactory.GetOrCreate((string?) null!));

        [Fact(DisplayName = "CoverArtFactory's Create method throws an exception if the path cannot be found")]
        public void CreatePathNotFoundThrowsException() =>
            Assert.Throws<FileNotFoundException>(() => CoverArtFactory.GetOrCreate("Foo"));

        [Theory(DisplayName = "CoverArtFactory's Create method throws an exception if the path is an unsupported file")]
        [MemberData(nameof(UnsupportedImageFileDataSource.Data), MemberType = typeof(UnsupportedImageFileDataSource))]
        public void CreatePathUnsupportedThrowsException(string fileName) =>
            Assert.Throws<ImageUnsupportedException>(() =>
                CoverArtFactory.GetOrCreate(Path.Combine(PathUtility.GetTestFileRoot(), "Unsupported", fileName)));

        [Theory(DisplayName = "CoverArtFactory's Create method throws an exception if the path is an unsupported file")]
        [MemberData(nameof(InvalidImageFileDataSource.Data), MemberType = typeof(InvalidImageFileDataSource))]
        public void CreatePathInvalidThrowsException(string fileName) =>
            Assert.Throws<ImageInvalidException>(() =>
                CoverArtFactory.GetOrCreate(Path.Combine(PathUtility.GetTestFileRoot(), "Invalid", fileName)));
    }
}
