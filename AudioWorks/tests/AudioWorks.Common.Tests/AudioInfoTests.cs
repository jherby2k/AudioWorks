/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Text.Json;
using AudioWorks.TestUtilities;
using Xunit;

namespace AudioWorks.Common.Tests
{
    public sealed class AudioInfoTests
    {
        public AudioInfoTests(ITestOutputHelper outputHelper) =>
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;

        [Fact(DisplayName = "AudioInfo throws an exception if the Format is null")]
        public void FormatNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => AudioInfo.CreateForLossless(null!, 2, 16, 44100));

        [Fact(DisplayName = "AudioInfo's Format property is properly serialized")]
        public void FormatIsSerialized() =>
            Assert.Equal("Test", JsonSerializer.Deserialize<AudioInfo>(JsonSerializer.Serialize(
                AudioInfo.CreateForLossless("Test", 2, 16, 44100)))?.Format);

        [Fact(DisplayName = "AudioInfo throws an exception if Channels is less than 1")]
        public void ChannelsTooLowThrowsException() =>
            Assert.Throws<AudioInvalidException>(() => AudioInfo.CreateForLossless("Test", 0, 16, 44100));

        [Fact(DisplayName = "AudioInfo throws an exception if Channels is greater than 2")]
        public void ChannelsTooHighThrowsException() =>
            Assert.Throws<AudioUnsupportedException>(() => AudioInfo.CreateForLossless("Test", 3, 16, 44100));

        [Fact(DisplayName = "AudioInfo's Channels property is properly serialized")]
        public void ChannelsIsSerialized() =>
            Assert.Equal(2, JsonSerializer.Deserialize<AudioInfo>(JsonSerializer.Serialize(
                AudioInfo.CreateForLossless("Test", 2, 16, 44100)))?.Channels);

        [Fact(DisplayName = "AudioInfo throws an exception if BitsPerSample is greater than 32")]
        public void BitsPerSampleTooHighThrowsException() =>
            Assert.Throws<AudioUnsupportedException>(() => AudioInfo.CreateForLossless("Test", 2, 33, 44100));

        [Fact(DisplayName = "AudioInfo throws an exception if BitsPerSample is less than 1")]
        public void BitsPerSampleTooLowThrowsException() =>
            Assert.Throws<AudioInvalidException>(() => AudioInfo.CreateForLossless("Test", 2, 0, 44100));

        [Fact(DisplayName = "AudioInfo's BitsPerSample property is properly serialized")]
        public void BitsPerSampleIsSerialized() =>
            Assert.Equal(16, JsonSerializer.Deserialize<AudioInfo>(JsonSerializer.Serialize(
                AudioInfo.CreateForLossless("Test", 2, 16, 44100)))?.BitsPerSample);

        [Fact(DisplayName = "AudioInfo throws an exception if SampleRate is less than 1")]
        public void SampleRateTooLowThrowsException() =>
            Assert.Throws<AudioInvalidException>(() => AudioInfo.CreateForLossless("Test", 2, 16, 0));

        [Fact(DisplayName = "AudioInfo's SampleRate property is properly serialized")]
        public void SampleRateIsSerialized() =>
            Assert.Equal(44100, JsonSerializer.Deserialize<AudioInfo>(JsonSerializer.Serialize(
                AudioInfo.CreateForLossless("Test", 2, 16, 44100)))?.SampleRate);

        [Fact(DisplayName = "AudioInfo throws an exception if FrameCount is negative")]
        public void FrameCountNegativeThrowsException() =>
            Assert.Throws<AudioInvalidException>(() => AudioInfo.CreateForLossless("Test", 2, 16, 44100, -1));

        [Fact(DisplayName = "AudioInfo's FrameCount property is properly serialized")]
        public void FrameCountIsSerialized() =>
            Assert.Equal(1000, JsonSerializer.Deserialize<AudioInfo>(JsonSerializer.Serialize(
                AudioInfo.CreateForLossless("Test", 2, 16, 44100, 1000)))?.FrameCount);

        [Fact(DisplayName = "AudioInfo throws an exception if BitRate is negative")]
        public void BitRateNegativeThrowsException() =>
            Assert.Throws<AudioInvalidException>(() => AudioInfo.CreateForLossy("Test", 2, 44100, 0, -1));

        [Fact(DisplayName = "AudioInfo's BitRate property is properly serialized")]
        public void BitRateIsSerialized() =>
            Assert.Equal(1000, JsonSerializer.Deserialize<AudioInfo>(JsonSerializer.Serialize(
                AudioInfo.CreateForLossy("Test", 2, 44100, 0, 1000)))?.BitRate);
    }
}
