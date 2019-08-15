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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using AudioWorks.TestUtilities;
using JetBrains.Annotations;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public sealed class AudioFileEncoderTests
    {
        public AudioFileEncoderTests([NotNull] ITestOutputHelper outputHelper) =>
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;

        [Fact(DisplayName = "AudioFileEncoder's constructor throws an exception if the name is null")]
        public void ConstructorNameNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioFileEncoder(null));

        [Fact(DisplayName = "AudioFileEncoder's constructor throws an exception if the name is unsupported")]
        public void ConstructorNameUnsupportedThrowsException() =>
            Assert.Throws<ArgumentException>(() => new AudioFileEncoder("Foo"));

        [Fact(DisplayName =
            "AudioFileEncoder's constructor throws an exception if encodedDirectoryName references an invalid metadata field")]
        public void ConstructorEncodedDirectoryNameInvalidThrowsException() =>
            Assert.Throws<ArgumentException>(() => new AudioFileEncoder("Wave", null, "{Invalid}"));

        [Fact(DisplayName = "AudioFileEncoder's constructor throws an exception if an unexpected setting is provided")]
        public void ConstructorUnexpectedSettingThrowsException() =>
            Assert.Throws<ArgumentException>(() =>
                new AudioFileEncoder("Wave", null, null, new SettingDictionary { ["Foo"] = "Bar" }));

        [Fact(DisplayName = "AudioFileEncoder's MaxDegreeOfParallelism property throws an exception if it is less than 1")]
        public void MaxDegreeOfParallelismTooLowThrowsException() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new AudioFileEncoder("Wave").MaxDegreeOfParallelism = 0);

        [Fact(DisplayName = "AudioFileEncoder's Settings property throws an exception if an unexpected setting is provided")]
        public void SettingsUnexpectedSettingThrowsException() =>
            Assert.Throws<ArgumentException>(() => new AudioFileEncoder("Wave").Settings["Foo"] = "Bar");

        [Fact(DisplayName = "AudioFileEncoder's Encode method throws an exception if an audio file is null")]
        public async void EncodeAsyncNullAudioFileThrowsException() =>
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                new AudioFileEncoder("Wave").EncodeAsync(null)).ConfigureAwait(true);

        [Theory(DisplayName = "AudioFileEncoder's Encode method creates the expected audio file")]
        [MemberData(nameof(EncodeValidFileDataSource.Data), MemberType = typeof(EncodeValidFileDataSource))]
        public async void EncodeAsyncCreatesExpectedAudioFile(
            int index,
            [NotNull] string sourceFileName,
            [NotNull] string encoderName,
            [CanBeNull] TestSettingDictionary settings,
#if LINUX
            [NotNull] string expectedUbuntu1604Hash,
            [NotNull] string expectedUbuntu1804Hash)
#elif OSX
            [NotNull] string expectedHash)
#else
            [NotNull] string expected32BitHash,
            [NotNull] string expected64BitHash)
#endif
        {
            var results = (await new AudioFileEncoder(
                        encoderName,
                        Path.Combine("Output", "Encode", "Valid"),
                        $"{index:00} - {Path.GetFileNameWithoutExtension(sourceFileName)}",
                        settings)
                    { Overwrite = true }
                .EncodeAsync(new TaggedAudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                    "TestFiles",
                    "Valid",
                    sourceFileName))).ConfigureAwait(true)).ToArray();

            Assert.Single(results);
#if LINUX
            Assert.Equal(LinuxUtility.GetRelease().StartsWith("Ubuntu 16.04", StringComparison.Ordinal)
                ? expectedUbuntu1604Hash
                : expectedUbuntu1804Hash,
#elif OSX
            Assert.Equal(expectedHash,
#else
            Assert.Equal(Environment.Is64BitProcess ? expected64BitHash : expected32BitHash,
#endif
                HashUtility.CalculateHash(results[0].Path));
        }
    }
}
