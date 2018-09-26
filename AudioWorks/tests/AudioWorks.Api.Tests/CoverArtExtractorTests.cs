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
using AudioWorks.Common;
using AudioWorks.TestUtilities;
using JetBrains.Annotations;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests
{
    public sealed class CoverArtExtractorTests
    {
        static CoverArtExtractorTests()
        {
            XunitLoggerProvider.Instance.Enable(LoggingManager.LoggerFactory);
        }

        public CoverArtExtractorTests([NotNull] ITestOutputHelper outputHelper)
        {
            XunitLoggerProvider.Instance.OutputHelper = outputHelper;
        }

        [Fact(DisplayName = "CoverArtExtractor's constructor throws an exception if encodedDirectoryName references an invalid metadata field")]
        public void ConstructorEncodedDirectoryNameInvalidThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new CoverArtExtractor("{Invalid}"));
        }
    }
}
