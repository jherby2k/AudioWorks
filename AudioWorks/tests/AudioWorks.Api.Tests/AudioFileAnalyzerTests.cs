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
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests
{
    public sealed class AudioFileAnalyzerTests
    {
        public AudioFileAnalyzerTests(ITestOutputHelper outputHelper) =>
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;

        [Fact(DisplayName = "AudioFileAnalyzer's constructor throws an exception if the name is null")]
        [SuppressMessage("Performance", "CS8625:Cannot convert null literal to non-nullable reference type")]
        public void ConstructorNameNullThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => new AudioFileAnalyzer(null));

        [Fact(DisplayName = "AudioFileAnalyzer's constructor throws an exception if the name is unsupported")]
        public void ConstructorNameUnsupportedThrowsException() =>
            Assert.Throws<ArgumentException>(() => new AudioFileAnalyzer("Foo"));

        [Fact(DisplayName = "AudioFileAnalyzer's MaxDegreeOfParallelism property throws an exception if it is less than 1")]
        public void MaxDegreeOfParallelismTooLowThrowsException() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new AudioFileAnalyzer("ReplayGain").MaxDegreeOfParallelism = 0);

        [Fact(DisplayName = "AudioFileEncoder's Encode method throws an exception if an audio file is null")]
        [SuppressMessage("Performance", "CS8625:Cannot convert null literal to non-nullable reference type")]
        public async void AnalyzeAsyncNullAudioFileThrowsException() =>
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                new AudioFileAnalyzer("ReplayGain").AnalyzeAsync(null)).ConfigureAwait(true);

        [Theory(DisplayName = "AudioFileAnalyzer's Analyze method creates the expected metadata")]
        [MemberData(nameof(AnalyzeValidFileDataSource.Data), MemberType = typeof(AnalyzeValidFileDataSource))]
        public async void AnalyzeAsyncCreatesExpectedMetadata(
            string fileName,
            string analyzerName,
            TestSettingDictionary settings,
            TestAudioMetadata[] validMetadata)
        {
            var audioFile = new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName));

            await new AudioFileAnalyzer(analyzerName, settings).AnalyzeAsync(audioFile).ConfigureAwait(true);

            Assert.Contains(audioFile.Metadata, validMetadata, new MetadataComparer());
        }

        [Theory(DisplayName = "AudioFileAnalyzer's Analyze method creates the expected metadata for a group")]
        [MemberData(nameof(AnalyzeGroupDataSource.Data), MemberType = typeof(AnalyzeGroupDataSource))]
        public async void AnalyzeAsyncCreatesExpectedMetadataForGroup(
            string[] fileNames,
            string analyzerName,
            TestSettingDictionary settings,
            TestAudioMetadata[][] validMetadata)
        {
            var audioFiles = fileNames.Select(fileName =>
                    new TaggedAudioFile(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName)))
                .ToArray<ITaggedAudioFile>();

            await new AudioFileAnalyzer(analyzerName, settings).AnalyzeAsync(audioFiles).ConfigureAwait(true);

            var i = 0;
            var comparer = new MetadataComparer();
            Assert.All(audioFiles, audioFile =>
            {
                Assert.Contains(audioFile.Metadata, validMetadata.Select(group => group[i]), comparer);
                i++;
            });
        }
    }
}
