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
    public sealed class CoverArtTests
    {
        public CoverArtTests([NotNull] ITestOutputHelper outputHelper)
        {
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;
        }

        [Theory(DisplayName = "CoverArt has the expected Width property value")]
        [MemberData(nameof(ValidImageFileDataSource.FileNamesAndWidth),
            MemberType = typeof(ValidImageFileDataSource))]
        public void HasExpectedWidth([NotNull] string fileName, int expectedWidth)
        {
            Assert.Equal(expectedWidth, CoverArtFactory.GetOrCreate(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Width);
        }

        [Theory(DisplayName = "CoverArt has the expected Height property value")]
        [MemberData(nameof(ValidImageFileDataSource.FileNamesAndHeight),
            MemberType = typeof(ValidImageFileDataSource))]
        public void HasExpectedHeight([NotNull] string fileName, int expectedHeight)
        {
            Assert.Equal(expectedHeight, CoverArtFactory.GetOrCreate(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Height);
        }

        [Theory(DisplayName = "CoverArt has the expected ColorDepth property value")]
        [MemberData(nameof(ValidImageFileDataSource.FileNamesAndColorDepth),
            MemberType = typeof(ValidImageFileDataSource))]
        public void HasExpectedColorDepth([NotNull] string fileName, int expectedColorDepth)
        {
            Assert.Equal(expectedColorDepth, CoverArtFactory.GetOrCreate(
                    Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName))
                .ColorDepth);
        }

        [Theory(DisplayName = "CoverArt has the expected Lossless property value")]
        [MemberData(nameof(ValidImageFileDataSource.FileNamesAndLossless),
            MemberType = typeof(ValidImageFileDataSource))]
        public void HasExpectedLossless([NotNull] string fileName, bool expectedLossless)
        {
            Assert.Equal(expectedLossless, CoverArtFactory.GetOrCreate(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Lossless);
        }

        [Theory(DisplayName = "CoverArt has the expected MimeType property value")]
        [MemberData(nameof(ValidImageFileDataSource.FileNamesAndMimeType),
            MemberType = typeof(ValidImageFileDataSource))]
        public void HasExpectedMimeType([NotNull] string fileName, [NotNull] string mimeType)
        {
            Assert.Equal(mimeType, CoverArtFactory.GetOrCreate(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .MimeType);
        }

        [Theory(DisplayName = "GetData returns the expected value")]
        [MemberData(nameof(ValidImageFileDataSource.FileNamesAndDataHash),
            MemberType = typeof(ValidImageFileDataSource))]
        public void GetDataReturnsExpectedValue([NotNull] string fileName, [NotNull] string expectedHash)
        {
            Assert.Equal(expectedHash, HashUtility.CalculateHash(CoverArtFactory.GetOrCreate(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName))
                .Data.ToArray()));
        }
    }
}
