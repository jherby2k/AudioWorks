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
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Common;
using AudioWorks.TestUtilities;
using JetBrains.Annotations;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public sealed class CoverArtExtractorTests
    {
        public CoverArtExtractorTests([NotNull] ITestOutputHelper outputHelper)
        {
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;
        }

        [Fact(DisplayName = "CoverArtExtractor's constructor throws an exception if encodedDirectoryName references an invalid metadata field")]
        public void ConstructorEncodedDirectoryNameInvalidThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new CoverArtExtractor("{Invalid}"));
        }

        [Theory(DisplayName = "CoverArtExtractor's Extract method creates the expected image file")]
        [MemberData(nameof(ValidFileWithCoverArtDataSource.IndexedFileNamesAndDataHash), MemberType = typeof(ValidFileWithCoverArtDataSource))]
        public void ExtractCreatesExpectedImageFile(
            int index,
            [NotNull] string sourceFileName,
            [NotNull] string expectedHash)
        {
            var path = Path.Combine("Output", "Extract");
            Directory.CreateDirectory(path);

            var result = new CoverArtExtractor(
                    path,
                    $"{index:00} - {Path.GetFileNameWithoutExtension(sourceFileName)}",
                    true)
                .Extract(new TaggedAudioFile(
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        sourceFileName)));

            Assert.Equal(expectedHash, result == null ? null : HashUtility.CalculateHash(result.FullName));
        }
    }
}
