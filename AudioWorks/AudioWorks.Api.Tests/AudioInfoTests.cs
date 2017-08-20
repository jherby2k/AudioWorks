using AudioWorks.Common;
using JetBrains.Annotations;
using System.IO;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public class AudioInfoTests
    {
        [Theory(DisplayName = "AudioInfo has the expected Description property value")]
        [ClassData(typeof(ValidTestFilesClassData))]
        public void AudioInfoHasExpectedDescription([NotNull] string fileName, [NotNull] AudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(expectedAudioInfo.Description, AudioFileFactory.Create(path).AudioInfo.Description);
        }

        [Theory(DisplayName = "AudioInfo has the expected Channel property value")]
        [ClassData(typeof(ValidTestFilesClassData))]
        public void AudioInfoHasExpectedChannels([NotNull] string fileName, [NotNull] AudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(expectedAudioInfo.Channels, AudioFileFactory.Create(path).AudioInfo.Channels);
        }

        [Theory(DisplayName = "AudioInfo has the expected BitsPerSample property value")]
        [ClassData(typeof(ValidTestFilesClassData))]
        public void AudioInfoHasExpectedBitsPerSample([NotNull] string fileName, [NotNull] AudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(expectedAudioInfo.BitsPerSample, AudioFileFactory.Create(path).AudioInfo.BitsPerSample);
        }

        [Theory(DisplayName = "AudioInfo has the expected SampleRate property value")]
        [ClassData(typeof(ValidTestFilesClassData))]
        public void AudioInfoHasExpectedSampleRate([NotNull] string fileName, [NotNull] AudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(expectedAudioInfo.SampleRate, AudioFileFactory.Create(path).AudioInfo.SampleRate);
        }

        [Theory(DisplayName = "AudioInfo has the expected SampleCount property value")]
        [ClassData(typeof(ValidTestFilesClassData))]
        public void AudioInfoHasExpectedSampleCount([NotNull] string fileName, [NotNull] AudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(expectedAudioInfo.SampleCount, AudioFileFactory.Create(path).AudioInfo.SampleCount);
        }
    }
}
