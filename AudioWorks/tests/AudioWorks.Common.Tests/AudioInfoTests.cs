using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace AudioWorks.Common.Tests
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public sealed class AudioInfoTests
    {
        [Fact(DisplayName = "AudioInfo throws an exception if the Format is null")]
        public void FormatNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => AudioInfo.CreateForLossless(null, 2, 16, 44100));
        }

        [Fact(DisplayName = "AudioInfo's Format property is properly serialized")]
        public void FormatIsSerialized()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, AudioInfo.CreateForLossless("Test", 2, 16, 44100));
                stream.Position = 0;

                Assert.Equal("Test", ((AudioInfo) formatter.Deserialize(stream)).Format);
            }
        }

        [Fact(DisplayName = "AudioInfo throws an exception if Channels is less than 1")]
        public void ChannelsTooLowThrowsException()
        {
            Assert.Throws<AudioInvalidException>(() => AudioInfo.CreateForLossless("Test", 0, 16, 44100));
        }

        [Fact(DisplayName = "AudioInfo throws an exception if Channels is greater than 2")]
        public void ChannelsTooHighThrowsException()
        {
            Assert.Throws<AudioUnsupportedException>(() => AudioInfo.CreateForLossless("Test", 3, 16, 44100));
        }

        [Fact(DisplayName = "AudioInfo's Channels property is properly serialized")]
        public void ChannelsIsSerialized()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, AudioInfo.CreateForLossless("Test", 2, 16, 44100));
                stream.Position = 0;

                Assert.Equal(2, ((AudioInfo) formatter.Deserialize(stream)).Channels);
            }
        }

        [Fact(DisplayName = "AudioInfo throws an exception if BitsPerSample is greater than 32")]
        public void BitsPerSampleTooHighThrowsException()
        {
            Assert.Throws<AudioUnsupportedException>(() => AudioInfo.CreateForLossless("Test", 2, 33, 44100));
        }

        [Fact(DisplayName = "AudioInfo throws an exception if BitsPerSample is less than 1")]
        public void BitsPerSampleTooLowThrowsException()
        {
            Assert.Throws<AudioInvalidException>(() => AudioInfo.CreateForLossless("Test", 2, 0, 44100));
        }

        [Fact(DisplayName = "AudioInfo's BitsPerSample property is properly serialized")]
        public void BitsPerSampleIsSerialized()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, AudioInfo.CreateForLossless("Test", 2, 16, 44100));
                stream.Position = 0;

                Assert.Equal(16, ((AudioInfo) formatter.Deserialize(stream)).BitsPerSample);
            }
        }

        [Fact(DisplayName = "AudioInfo throws an exception if SampleRate is less than 1")]
        public void SampleRateTooLowThrowsException()
        {
            Assert.Throws<AudioInvalidException>(() => AudioInfo.CreateForLossless("Test", 2, 16, 0));
        }

        [Fact(DisplayName = "AudioInfo's SampleRate property is properly serialized")]
        public void SampleRateIsSerialized()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, AudioInfo.CreateForLossless("Test", 2, 16, 44100));
                stream.Position = 0;

                Assert.Equal(44100, ((AudioInfo) formatter.Deserialize(stream)).SampleRate);
            }
        }

        [Fact(DisplayName = "AudioInfo throws an exception if FrameCount is negative")]
        public void FrameCountNegativeThrowsException()
        {
            Assert.Throws<AudioInvalidException>(() => AudioInfo.CreateForLossless("Test", 2, 16, 44100, -1));
        }

        [Fact(DisplayName = "AudioInfo's FrameCount property is properly serialized")]
        public void FrameCountIsSerialized()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, AudioInfo.CreateForLossless("Test", 2, 16, 44100, 1000));
                stream.Position = 0;

                Assert.Equal(1000, ((AudioInfo) formatter.Deserialize(stream)).FrameCount);
            }
        }

        [Fact(DisplayName = "AudioInfo throws an exception if BitRate is negative")]
        public void BitRateNegativeThrowsException()
        {
            Assert.Throws<AudioInvalidException>(() => AudioInfo.CreateForLossy("Test", 2, 44100, 0, -1));
        }

        [Fact(DisplayName = "AudioInfo's BitRate property is properly serialized")]
        public void BitRateIsSerialized()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, AudioInfo.CreateForLossy("Test", 2, 44100, 0, 1000));
                stream.Position = 0;

                Assert.Equal(1000, ((AudioInfo) formatter.Deserialize(stream)).BitRate);
            }
        }
    }
}
