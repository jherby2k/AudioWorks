using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using JetBrains.Annotations;
using ObjectsComparer;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public sealed class AudioFileAnalyzerTests
    {
        [Fact(DisplayName = "AudioFileAnalyzer's constructor throws an exception if the name is null")]
        public void ConstructorNameNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new AudioFileAnalyzer(null));
        }

        [Fact(DisplayName = "AudioFileAnalyzer's constructor throws an exception if the name is unsupported")]
        public void ConstructorNameUnsupportedThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new AudioFileAnalyzer("Foo"));
        }

        [Theory(DisplayName = "AudioFileAnalyzer's Analyze method creates the expected metadata")]
        [MemberData(nameof(AnalyzeValidFileDataSource.Data), MemberType = typeof(AnalyzeValidFileDataSource))]
        public async void AnalyzeAsyncCreatesExpectedMetadata(
            [NotNull] string fileName,
            [NotNull] string analyzerName,
            [CanBeNull] TestSettingDictionary settings,
            [NotNull] TestAudioMetadata expectedMetadata)
        {
            var audioFile = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName));

            await new AudioFileAnalyzer(analyzerName, settings).AnalyzeAsync(audioFile).ConfigureAwait(false);

            Assert.True(
                new Comparer().Compare(expectedMetadata, audioFile.Metadata, out var differences),
                string.Join(" ", differences));
        }

        [Theory(DisplayName = "AudioFileAnalyzer's Analyze method creates the expected metadata for a group")]
        [MemberData(nameof(AnalyzeGroupDataSource.Data), MemberType = typeof(AnalyzeGroupDataSource))]
        public async void AnalyzeAsyncCreatesExpectedMetadataForGroup(
            [NotNull] string[] fileNames,
            [NotNull] string analyzerName,
            [CanBeNull] TestSettingDictionary settings,
            [NotNull] TestAudioMetadata[] expectedMetadata)
        {
            var audioFiles = fileNames.Select(fileName => new TaggedAudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                    "TestFiles",
                    "Valid",
                    fileName)))
                .ToArray<ITaggedAudioFile>();

            await new AudioFileAnalyzer(analyzerName, settings).AnalyzeAsync(audioFiles).ConfigureAwait(false);

            var i = 0;
            var comparer = new Comparer();
            Assert.All(audioFiles, audioFile =>
                Assert.True(comparer.Compare(expectedMetadata[i++], audioFile.Metadata, out var differences),
                    string.Join(" ", differences)));
        }
    }
}
