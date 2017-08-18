using System;
using System.IO;
using Xunit;

namespace AudioWorks.Api.Tests
{
    public class AudioFileFactoryTests
    {
        [Fact(DisplayName = "AudioFileFactory.Create throws an exception when fileName is null")]
        public void CreatePathNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws(typeof(ArgumentNullException), () => AudioFileFactory.Create(null));
        }

        [Fact(DisplayName = "AudioFileFactory.Create throws an exception when fileName cannot be found")]
        public void CreatePathNotFoundThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws(typeof(FileNotFoundException), () => AudioFileFactory.Create(""));
        }
    }
}
