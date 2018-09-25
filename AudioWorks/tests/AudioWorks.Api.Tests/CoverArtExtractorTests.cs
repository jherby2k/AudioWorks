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
        public CoverArtExtractorTests([NotNull] ITestOutputHelper outputHelper)
        {
            LoggingManager.LoggerFactory.AddProvider(new XUnitLoggerProvider(outputHelper));
        }

        [Fact(DisplayName = "CoverArtExtractor's constructor throws an exception if encodedDirectoryName references an invalid metadata field")]
        public void ConstructorEncodedDirectoryNameInvalidThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new CoverArtExtractor("{Invalid}"));
        }
    }
}
