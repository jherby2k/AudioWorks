using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using JetBrains.Annotations;
using ObjectsComparer;
using System.IO;
using Xunit;

namespace AudioWorks.Api.Tests
{
    public sealed class AudioFileAnalyzerTests
    {
        [Theory(DisplayName = "AudioFileAnalyzer's Analyze method creates the expected metadata")]
        [MemberData(nameof(AnalyzeValidFileDataSource.Data), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void AnalyzeCreatesExpectedMetadata(
            [NotNull] string fileName,
            [NotNull] string analyzerName,
            [NotNull] TestSettingDictionary settings,
            [NotNull] TestAudioMetadata expectedMetadata)
        {
            var analyzer = new AudioFileAnalyzer(analyzerName, settings);
            var audioFile = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName));

            analyzer.Analyze(audioFile);

            Assert.True(new Comparer().Compare(expectedMetadata, audioFile.Metadata, out var differences), string.Join(' ', differences));
        }

        [Theory(DisplayName = "AnalyzableAudioFile's Analyze method creates the expected metadata for a group")]
        [MemberData(nameof(AnalyzeGroupDataSource.Data), MemberType = typeof(AnalyzeGroupDataSource))]
        public void AnalyzeCreatesExpectedMetadataForGroup(
            [NotNull] string fileName1,
            [NotNull] string fileName2,
            [NotNull] string fileName3,
            [NotNull] string analyzerName,
            [NotNull] TestSettingDictionary settings,
            [NotNull] TestAudioMetadata expectedMetadata1,
            [NotNull] TestAudioMetadata expectedMetadata2,
            [NotNull] TestAudioMetadata expectedMetadata3
            )
        {
            var analyzer = new AudioFileAnalyzer(analyzerName, settings);
            var audioFile1 = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName1));
            var audioFile2 = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName2));
            var audioFile3 = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName3));

            analyzer.Analyze(audioFile1, audioFile2, audioFile3);

            Assert.True(new Comparer().Compare(expectedMetadata1, audioFile1.Metadata, out var differences), string.Join(' ', differences));
            Assert.True(new Comparer().Compare(expectedMetadata2, audioFile2.Metadata, out differences), string.Join(' ', differences));
            Assert.True(new Comparer().Compare(expectedMetadata3, audioFile3.Metadata, out differences), string.Join(' ', differences));
        }
    }
}
