using Xunit;

namespace AudioWorks.Common.Tests
{
    public sealed class AudioMetadataInvalidExceptionTests
    {
        [Fact(DisplayName = "AudioMetadataInvalidException is an AudioException")]
        public void IsException()
        {
            Assert.IsAssignableFrom<AudioException>(new AudioInvalidException());
        }
    }
}
