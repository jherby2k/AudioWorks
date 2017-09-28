using AudioWorks.Common;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace AudioWorks.Api.Tests
{
    public sealed class AudioInvalidExceptionTests
    {
        [Fact(DisplayName = "AudioInvalidException is an AudioException")]
        public void IsException()
        {
            Assert.IsAssignableFrom<AudioException>(new AudioInvalidException());
        }

        [Fact(DisplayName = "AudioInvalidException's FileInfo property is properly serialized")]
        public void FileInfoIsSerialized()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioInvalidException(null, new FileInfo("Foo")));
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal("Foo", ((AudioInvalidException) formatter.Deserialize(stream)).FileInfo?.Name);
            }
        }

        [Fact(DisplayName = "AudioInvalidException serializes correctly when FileInfo is null")]
        public void FileInfoNullSerializesCorrectly()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioInvalidException());
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Null(((AudioInvalidException) formatter.Deserialize(stream)).FileInfo);
            }
        }
    }
}
