using AudioWorks.TestUtilities;
using JetBrains.Annotations;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Common.Tests
{
    public sealed class AudioMetadataInvalidExceptionTests
    {
        public AudioMetadataInvalidExceptionTests([NotNull] ITestOutputHelper outputHelper)
        {
            LoggingManager.LoggerFactory.AddProvider(new XUnitLoggerProvider(outputHelper));
        }

        [Fact(DisplayName = "AudioMetadataInvalidException is an AudioException")]
        public void IsException()
        {
            Assert.IsAssignableFrom<AudioException>(new AudioInvalidException());
        }
    }
}
