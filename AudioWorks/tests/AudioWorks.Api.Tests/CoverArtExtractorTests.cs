using System;
using Xunit;

namespace AudioWorks.Api.Tests
{
    public sealed class CoverArtExtractorTests
    {
        [Fact(DisplayName = "CoverArtExtractor's constructor throws an exception if encodedDirectoryName references an invalid metadata field")]
        public void ConstructorEncodedDirectoryNameInvalidThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new CoverArtExtractor("{Invalid}"));
        }
    }
}
