using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AudioWorks.TestUtilities;
using JetBrains.Annotations;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Common.Tests
{
    public sealed class AudioInvalidExceptionTests
    {
        public AudioInvalidExceptionTests([NotNull] ITestOutputHelper outputHelper)
        {
            LoggingManager.LoggerFactory.AddProvider(new XUnitLoggerProvider(outputHelper));
        }

        [Fact(DisplayName = "AudioInvalidException is an AudioException")]
        public void IsAudioException()
        {
            Assert.IsAssignableFrom<AudioException>(new AudioInvalidException());
        }

        [Fact(DisplayName = "AudioInvalidException's Path property is properly serialized")]
        public void PathIsSerialized()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioInvalidException(null, "Foo"));
                stream.Position = 0;
                Assert.Equal("Foo", ((AudioInvalidException) formatter.Deserialize(stream)).Path);
            }
        }
    }
}
