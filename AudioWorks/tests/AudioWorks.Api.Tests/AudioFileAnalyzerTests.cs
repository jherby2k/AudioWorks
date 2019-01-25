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
using ObjectsComparer;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public sealed class AudioFileAnalyzerTests
    {
        public AudioFileAnalyzerTests([NotNull] ITestOutputHelper outputHelper)
        {
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;
        }

        [Fact(DisplayName = "AudioFileAnalyzer's constructor throws an exception if the name is null")]
        public void ConstructorNameNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new AudioFileAnalyzer(null));
        }

        [Fact(DisplayName = "AudioFileAnalyzer's constructor throws an exception if the name is unsupported")]
        public void ConstructorNameUnsupportedThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new AudioFileAnalyzer("Foo"));
        }

        [Fact(DisplayName = "AudioFileAnalyzer's MaxDegreeOfParallelism property throws an exception if it is less than 1")]
        public void MaxDegreeOfParallelismTooLowThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new AudioFileAnalyzer("ReplayGain").MaxDegreeOfParallelism = 0);
        }

        [Fact(DisplayName = "AudioFileEncoder's Encode method throws an exception if an audio file is null")]
        public async void AnalyzeAsyncNullAudioFileThrowsException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                new AudioFileAnalyzer("ReplayGain").AnalyzeAsync(null)).ConfigureAwait(true);
        }

        [Theory(DisplayName = "AudioFileAnalyzer's Analyze method creates the expected metadata")]
        [MemberData(nameof(AnalyzeValidFileDataSource.Data), MemberType = typeof(AnalyzeValidFileDataSource))]
        public async void AnalyzeAsyncCreatesExpectedMetadata(
            [NotNull] string fileName,
            [NotNull] string analyzerName,
            [CanBeNull] TestSettingDictionary settings,
#if LINUX
            [NotNull] TestAudioMetadata expectedUbuntu1604Metadata,
            [NotNull] TestAudioMetadata expectedUbuntu1804Metadata)
#else
            [NotNull] TestAudioMetadata expectedMetadata)
#endif
        {
            var audioFile = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName));

            await new AudioFileAnalyzer(analyzerName, settings).AnalyzeAsync(audioFile).ConfigureAwait(true);

            Assert.True(
#if LINUX
                new Comparer().Compare(LinuxUtility.GetRelease().StartsWith("Ubuntu 16.04", StringComparison.Ordinal)
                        ? expectedUbuntu1604Metadata
                        : expectedUbuntu1804Metadata,
                    audioFile.Metadata, out var differences),
#else
                new Comparer().Compare(expectedMetadata, audioFile.Metadata, out var differences),
#endif
                string.Join(" ", differences));
        }

        [Theory(DisplayName = "AudioFileAnalyzer's Analyze method creates the expected metadata for a group")]
        [MemberData(nameof(AnalyzeGroupDataSource.Data), MemberType = typeof(AnalyzeGroupDataSource))]
        public async void AnalyzeAsyncCreatesExpectedMetadataForGroup(
            [NotNull] string[] fileNames,
            [NotNull] string analyzerName,
            [CanBeNull] TestSettingDictionary settings,
#if LINUX
            [NotNull] TestAudioMetadata[] expectedUbuntu1604Metadata,
            [NotNull] TestAudioMetadata[] expectedUbuntu1804Metadata)
#else
            [NotNull] TestAudioMetadata[] expectedMetadata)
#endif
        {
            var audioFiles = fileNames.Select(fileName => new TaggedAudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                    "TestFiles",
                    "Valid",
                    fileName)))
                .ToArray<ITaggedAudioFile>();

            await new AudioFileAnalyzer(analyzerName, settings).AnalyzeAsync(audioFiles).ConfigureAwait(true);

            var i = 0;
            var comparer = new Comparer();
            Assert.All(audioFiles, audioFile =>
#if LINUX
                Assert.True(comparer.Compare(
                        LinuxUtility.GetRelease().StartsWith("Ubuntu 16.04", StringComparison.Ordinal)
                            ? expectedUbuntu1604Metadata[i++]
                            : expectedUbuntu1804Metadata[i++],
                        audioFile.Metadata, out var differences),
#else
                Assert.True(comparer.Compare(expectedMetadata[i++], audioFile.Metadata, out var differences),
#endif
                    string.Join(" ", differences)));
        }
    }
}
