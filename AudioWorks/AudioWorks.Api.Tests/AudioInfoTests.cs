using System;
using AudioWorks.Common;
using JetBrains.Annotations;
using System.IO;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public class AudioInfoTests
    {
        [Fact(DisplayName = "AudioInfo throws an exception if the Description is null")]
        public void AudioInfoDescriptionNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new AudioInfo(null, 2, 16, 44100, 0));
        }

        [Fact(DisplayName = "AudioInfo throws an exception if Channels is greater than 2")]
        public void AudioInfoChannelsTooHighThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AudioInfo("Test", 3, 16, 44100, 0));
        }

        [Fact(DisplayName = "AudioInfo throws an exception if Channels is less than 1")]
        public void AudioInfoChannelsTooLowThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AudioInfo("Test", 0, 16, 44100, 0));
        }

        [Fact(DisplayName = "AudioInfo throws an exception if BitsPerSample is less than 1")]
        public void AudioInfoBitsPerSampleTooLowThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AudioInfo("Test", 2, 0, 44100, 0));
        }

        [Fact(DisplayName = "AudioInfo throws an exception if BitsPerSample is greater than 32")]
        public void AudioInfoBitsPerSampleTooHighThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AudioInfo("Test", 2, 33, 44100, 0));
        }

        [Fact(DisplayName = "AudioInfo throws an exception if SampleRate is less than 1")]
        public void AudioInfoSampleRateTooLowThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AudioInfo("Test", 2, 16, 0, 0));
        }

        [Fact(DisplayName = "AudioInfo throws an exception if SampleCount is negative")]
        public void AudioInfoSampleCountNegativeThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AudioInfo("Test", 2, 16, 44100, -1));
        }

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

        [Theory(DisplayName = "AudioInfo has the expected PlayLength property value")]
        [ClassData(typeof(ValidTestFilesClassData))]
        public void AudioInfoHasExpectedPlayLength([NotNull] string fileName, [NotNull] AudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);
            Assert.Equal(expectedAudioInfo.PlayLength, AudioFileFactory.Create(path).AudioInfo.PlayLength);
        }
    }
}
