/* Copyright � 2018 Jeremy Herbison

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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AudioWorks.Common;
using AudioWorks.TestUtilities;
using AudioWorks.TestUtilities.DataSources;
using Xunit;

namespace AudioWorks.Api.Tests
{
    public sealed class AudioFileEncoderTests
    {
        [Fact(DisplayName = "AudioFileEncoder's constructor throws an exception if the name is null")]
        public void ConstructorNameNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() =>
                new AudioFileEncoder(null!));

        [Fact(DisplayName = "AudioFileEncoder's constructor throws an exception if the name is unsupported")]
        public void ConstructorNameUnsupportedThrowsException() =>
            Assert.Throws<ArgumentException>(() =>
                new AudioFileEncoder("Foo"));

        [Fact(DisplayName =
            "AudioFileEncoder's constructor throws an exception if encodedDirectoryName references an invalid metadata field")]
        public void ConstructorEncodedDirectoryNameInvalidThrowsException() =>
            Assert.Throws<ArgumentException>(() =>
                new AudioFileEncoder("Wave") { EncodedDirectoryName = "{Invalid}" });

        [Fact(DisplayName = "AudioFileEncoder's constructor throws an exception if an unexpected setting is provided")]
        public void ConstructorUnexpectedSettingThrowsException() =>
            Assert.Throws<ArgumentException>(() =>
                new AudioFileEncoder("Wave") { Settings = new() { ["Foo"] = "Bar" } });

        [Fact(DisplayName =
            "AudioFileEncoder's MaxDegreeOfParallelism property throws an exception if it is less than 1")]
        public void MaxDegreeOfParallelismTooLowThrowsException() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new AudioFileEncoder("Wave") { MaxDegreeOfParallelism = 0 });

        [Fact(DisplayName = "AudioFileEncoder's Settings property throws an exception if an unexpected setting is provided")]
        public void SettingsUnexpectedSettingThrowsException() =>
            Assert.Throws<ArgumentException>(() =>
                new AudioFileEncoder("Wave").Settings["Foo"] = "Bar");

        [Fact(DisplayName = "AudioFileEncoder's Encode method throws an exception if an audio file is null")]
        public async Task EncodeAsyncNullAudioFileThrowsException() =>
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                new AudioFileEncoder("Wave").EncodeAsync(null!)).ConfigureAwait(true);

        [Theory(DisplayName = "AudioFileEncoder's Encode method creates the expected audio file")]
        [MemberData(nameof(EncodeValidFileDataSource.Data), MemberType = typeof(EncodeValidFileDataSource))]
        public async Task EncodeAsyncCreatesExpectedAudioFile(
            int index,
            string sourceFileName,
            string encoderName,
            SettingDictionary settings,
            string[] validHashes)
        {
            var results = (await new AudioFileEncoder(encoderName)
                {
                    EncodedDirectoryName = Path.Combine("Output", "Encode", "Valid"),
                    EncodedFileName = $"{index:000} - {Path.GetFileNameWithoutExtension(sourceFileName)}",
                    Settings = settings,
                    Overwrite = true
                }.EncodeAsync(new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", sourceFileName)))
                .ConfigureAwait(true)).ToArray();

            Assert.Single(results);

            var resultPath = results[0].Path;
            var resultData = await File.ReadAllBytesAsync(resultPath, CancellationToken.None);

            TestContext.Current.AddAttachment(
                Path.GetFileNameWithoutExtension(resultPath),
                resultData,
                PathUtility.GetMime(resultPath));

            Assert.Contains(HashUtility.CalculateHash(resultData), validHashes);
        }

        [Theory(DisplayName = "AudioFileEncoder's Encode method creates the expected audio file without optimization")]
        [MemberData(nameof(EncodeValidFileDataSource.Data), MemberType = typeof(EncodeValidFileDataSource))]
        public async Task EncodeAsyncCreatesExpectedAudioFileWithoutOptimization(
            int index,
            string sourceFileName,
            string encoderName,
            SettingDictionary settings,
            string[] validHashes)
        {
            var results = (await new AudioFileEncoder(encoderName)
                {
                    EncodedDirectoryName = Path.Combine("Output", "Encode", "Valid"),
                    EncodedFileName = $"{index:000} - {Path.GetFileNameWithoutExtension(sourceFileName)}",
                    Settings = settings,
                    Overwrite = true,
                    UseOptimizations = false
                }.EncodeAsync(new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", sourceFileName)))
                .ConfigureAwait(true)).ToArray();

            Assert.Single(results);

            var resultPath = results[0].Path;
            var resultData = await File.ReadAllBytesAsync(resultPath, CancellationToken.None);

            TestContext.Current.AddAttachment(
                Path.GetFileNameWithoutExtension(resultPath),
                resultData,
                PathUtility.GetMime(resultPath));

            Assert.Contains(HashUtility.CalculateHash(resultData), validHashes);
        }
    }
}
