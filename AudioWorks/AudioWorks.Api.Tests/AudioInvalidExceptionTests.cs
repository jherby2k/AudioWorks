using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AudioWorks.Common;
using Xunit;

namespace AudioWorks.Api.Tests
{
    public sealed class AudioInvalidExceptionTests
    {
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
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal("Foo", ((AudioInvalidException) formatter.Deserialize(stream)).Path);
            }
        }
    }
}
