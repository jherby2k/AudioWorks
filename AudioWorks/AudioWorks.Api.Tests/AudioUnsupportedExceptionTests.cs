using AudioWorks.Common;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace AudioWorks.Api.Tests
{
    public sealed class AudioUnsupportedExceptionTests
    {
        [Fact(DisplayName = "AudioUnsupportedException is an Exception")]
        public void IsException()
        {
            Assert.IsAssignableFrom<Exception>(new AudioUnsupportedException());
        }

        [Fact(DisplayName = "AudioUnsupportedException's FileInfo property is properly serialized")]
        public void FileInfoIsSerialized()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioUnsupportedException(null, new FileInfo("Foo")));
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Equal("Foo", ((AudioUnsupportedException) formatter.Deserialize(stream)).FileInfo?.Name);
            }
        }

        [Fact(DisplayName = "AudioUnsupportedException serializes correctly when FileInfo is null")]
        public void FileInfoNullSerializesCorrectly()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AudioUnsupportedException());
                stream.Seek(0, SeekOrigin.Begin);
                Assert.Null(((AudioUnsupportedException) formatter.Deserialize(stream)).FileInfo);
            }
        }
    }
}
