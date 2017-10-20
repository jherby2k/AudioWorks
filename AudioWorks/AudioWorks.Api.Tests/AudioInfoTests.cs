using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public sealed class AudioInfoTests
    {
        [Fact(DisplayName = "AudioInfo throws an exception if the Format is null")]
        public void FormatNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => AudioInfo.CreateForLossless(null, 2, 16, 44100));
        }

        [Fact(DisplayName = "AudioInfo's Format property is properly serialized")]
        public void FormatIsSerialized()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, AudioInfo.CreateForLossless("Test", 2, 16, 44100));
                stream.Seek(0, SeekOrigin.Begin);

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
                stream.Seek(0, SeekOrigin.Begin);

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
                stream.Seek(0, SeekOrigin.Begin);

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
                stream.Seek(0, SeekOrigin.Begin);

                Assert.Equal(44100, ((AudioInfo) formatter.Deserialize(stream)).SampleRate);
            }
        }

        [Fact(DisplayName = "AudioInfo throws an exception if SampleCount is negative")]
        public void SampleCountNegativeThrowsException()
        {
            Assert.Throws<AudioInvalidException>(() => AudioInfo.CreateForLossless("Test", 2, 16, 44100, -1));
        }

        [Fact(DisplayName = "AudioInfo's SampleCount property is properly serialized")]
        public void SampleCountIsSerialized()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, AudioInfo.CreateForLossless("Test", 2, 16, 44100, 1000));
                stream.Seek(0, SeekOrigin.Begin);

                Assert.Equal(1000, ((AudioInfo) formatter.Deserialize(stream)).SampleCount);
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
                stream.Seek(0, SeekOrigin.Begin);

                Assert.Equal(1000, ((AudioInfo) formatter.Deserialize(stream)).BitRate);
            }
        }

        [Theory(DisplayName = "AudioInfo has the expected Format property value")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedFormat([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);

            Assert.Equal(expectedAudioInfo.Format, new AudioFile(path).Info.Format);
        }

        [Theory(DisplayName = "AudioInfo has the expected Channel property value")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedChannels([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);

            Assert.Equal(expectedAudioInfo.Channels, new AudioFile(path).Info.Channels);
        }

        [Theory(DisplayName = "AudioInfo has the expected BitsPerSample property value")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedBitsPerSample([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);

            Assert.Equal(expectedAudioInfo.BitsPerSample, new AudioFile(path).Info.BitsPerSample);
        }

        [Theory(DisplayName = "AudioInfo has the expected SampleRate property value")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedSampleRate([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);

            Assert.Equal(expectedAudioInfo.SampleRate, new AudioFile(path).Info.SampleRate);
        }

        [Theory(DisplayName = "AudioInfo has the expected BitRate property value")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedBitRate([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);

            Assert.Equal(expectedAudioInfo.BitRate, new AudioFile(path).Info.BitRate);
        }

        [Theory(DisplayName = "AudioInfo has the expected SampleCount property value")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedSampleCount([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);

            Assert.Equal(expectedAudioInfo.SampleCount, new AudioFile(path).Info.SampleCount);
        }

        [Theory(DisplayName = "AudioInfo has the expected PlayLength property value")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedPlayLength([NotNull] string fileName, [NotNull] TestAudioInfo expectedAudioInfo)
        {
            var path = Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName);

            Assert.Equal(
                expectedAudioInfo.SampleCount == 0
                    ? TimeSpan.Zero
                    : new TimeSpan(0, 0,
                        (int) Math.Round(expectedAudioInfo.SampleCount / (double) expectedAudioInfo.SampleRate)),
                new AudioFile(path).Info.PlayLength);
        }
    }
}
