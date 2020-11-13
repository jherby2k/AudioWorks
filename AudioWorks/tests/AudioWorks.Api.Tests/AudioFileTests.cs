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
using System.IO;
using System.Text.Json;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using AudioWorks.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests
{
    public sealed class AudioFileTests
    {
        public AudioFileTests(ITestOutputHelper outputHelper) =>
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;

        [Fact(DisplayName = "AudioFile's constructor throws an exception if the path is null")]
        public void ConstructorPathNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioFile(null!));

        [Fact(DisplayName = "AudioFile's constructor throws an exception if the path cannot be found")]
        public void ConstructorPathNotFoundThrowsException() =>
            Assert.Throws<FileNotFoundException>(() => new AudioFile("Foo"));

        [Theory(DisplayName = "AudioFile's constructor throws an exception if the path is an unsupported file")]
        [MemberData(nameof(UnsupportedFileDataSource.Data), MemberType = typeof(UnsupportedFileDataSource))]
        public void ConstructorPathUnsupportedThrowsException(string fileName) =>
            Assert.Throws<AudioUnsupportedException>(() =>
                new AudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Unsupported", fileName)));

        [Theory(DisplayName = "AudioFile's constructor throws an exception if the Path is an invalid file")]
        [MemberData(nameof(InvalidFileDataSource.Data), MemberType = typeof(InvalidFileDataSource))]
        public void ConstructorPathInvalidThrowsException(string fileName) =>
            Assert.Throws<AudioInvalidException>(() =>
                new AudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Invalid", fileName)));

        [Theory(DisplayName = "AudioFile has the expected Path property value")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void HasExpectedPath(string fileName)
        {
            var path = Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName);

            Assert.Equal(path, new AudioFile(path).Path);
        }

        [Theory(DisplayName = "AudioFile's Info property is not null")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void InfoNotNull(string fileName) =>
            Assert.NotNull(new AudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Info);

        [Theory(DisplayName = "AudioFile's Path property is properly serialized")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void PathIsSerialized(string fileName)
        {
            var audioFile = new AudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName));

            Assert.Equal(audioFile.Path,
                JsonSerializer.Deserialize<AudioFile>(JsonSerializer.Serialize(audioFile))?.Path);
        }

        [Theory(DisplayName = "AudioFile's Info property is properly serialized")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void InfoIsSerialized(string fileName)
        {
            var audioFile = new AudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName));

            Assert.True(new AudioInfoComparer().Equals(
                audioFile.Info,
                JsonSerializer.Deserialize<AudioFile>(JsonSerializer.Serialize(audioFile))?.Info));
        }

        [Theory(DisplayName = "AudioFile's Info property has the expected Format")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedFormat(string fileName, TestAudioInfo expectedAudioInfo) =>
            Assert.Equal(
                expectedAudioInfo.Format,
                new AudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Info.Format);

        [Theory(DisplayName = "AudioFile's Info property has the expected Channels")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedChannels(string fileName, TestAudioInfo expectedAudioInfo) =>
            Assert.Equal(
                expectedAudioInfo.Channels,
                new AudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Info.Channels);

        [Theory(DisplayName = "AudioFile's Info property has the expected BitsPerSample")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedBitsPerSample(string fileName, TestAudioInfo expectedAudioInfo) =>
            Assert.Equal(
                expectedAudioInfo.BitsPerSample,
                new AudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Info.BitsPerSample);

        [Theory(DisplayName = "AudioFile's Info property has the expected SampleRate")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedSampleRate(string fileName, TestAudioInfo expectedAudioInfo) =>
            Assert.Equal(
                expectedAudioInfo.SampleRate,
                new AudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Info.SampleRate);

        [Theory(DisplayName = "AudioFile's Info property has the expected BitRate")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedBitRate(string fileName, TestAudioInfo expectedAudioInfo) =>
            Assert.Equal(
                expectedAudioInfo.BitRate,
                new AudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Info.BitRate);

        [Theory(DisplayName = "AudioFile's Info property has the expected FrameCount")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedFrameCount(string fileName, TestAudioInfo expectedAudioInfo) =>
            Assert.Equal(
                expectedAudioInfo.SampleCount,
                new AudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Info.FrameCount);

        [Theory(DisplayName = "AudioFile's Info property has the expected PlayLength")]
        [MemberData(nameof(ValidFileDataSource.FileNamesAndAudioInfo), MemberType = typeof(ValidFileDataSource))]
        public void InfoHasExpectedPlayLength(string fileName, TestAudioInfo expectedAudioInfo) =>
            Assert.Equal(
                expectedAudioInfo.SampleCount == 0
                    ? TimeSpan.Zero
                    : new TimeSpan(0, 0,
                        (int) Math.Round(expectedAudioInfo.SampleCount / (double) expectedAudioInfo.SampleRate)),
                new AudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)).Info.PlayLength);

        [Theory(DisplayName = "AudioFile's Rename method throws an exception if the name is null")]
        [MemberData(nameof(RenameValidFileDataSource.FileNames), MemberType = typeof(RenameValidFileDataSource))]
        public void RenameNullNameThrowsException(string fileName)
        {
            var path = Path.Combine("Output", "Rename", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.Copy(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName), path, true);

            Assert.Throws<ArgumentNullException>(() =>
                new AudioFile(path).Rename(null!, true));
        }
    }
}
