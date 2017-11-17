using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using JetBrains.Annotations;
using ObjectsComparer;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AudioWorks.Api.Tests
{
    public sealed class AnalyzableAudioFileTests
    {
        [Fact(DisplayName = "AnalyzableAudioFile's constructor throws an exception if the path is null")]
        public void ConstructorPathNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new AnalyzableAudioFile(null));
        }

        [Fact(DisplayName = "AnalyzableAudioFile's constructor throws an exception if the path cannot be found")]
        public void ConstructorPathNotFoundThrowsException()
        {
            Assert.Throws<FileNotFoundException>(() => new AnalyzableAudioFile("Foo"));
        }

        [Theory(DisplayName = "AnalyzableAudioFile's constructor throws an exception if the path is an unsupported file")]
        [MemberData(nameof(UnsupportedFileDataSource.Data), MemberType = typeof(UnsupportedFileDataSource))]
        public void ConstructorPathUnsupportedThrowsException([NotNull] string fileName)
        {
            Assert.Throws<AudioUnsupportedException>(() =>
                new AnalyzableAudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                    "TestFiles",
                    "Unsupported",
                    fileName)));
        }

        [Theory(DisplayName = "AnalyzableAudioFile's constructor throws an exception if the Path is an invalid file")]
        [MemberData(nameof(InvalidFileDataSource.Data), MemberType = typeof(InvalidFileDataSource))]
        public void ConstructorPathInvalidThrowsException([NotNull] string fileName)
        {
            Assert.Throws<AudioInvalidException>(() =>
                new AnalyzableAudioFile(Path.Combine(
                    new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                    "TestFiles",
                    "Invalid",
                    fileName)));
        }

        [Theory(DisplayName = "AnalyzableAudioFile's Analyze method creates the expected metadata")]
        [MemberData(nameof(AnalyzeValidFileDataSource.Data), MemberType = typeof(AnalyzeValidFileDataSource))]
        public void AnalyzeCreatesExpectedMetadata(
            [NotNull] string fileName,
            [NotNull] string analyzer,
            [NotNull] TestSettingDictionary settings,
            [NotNull] TestAudioMetadata expectedMetadata)
        {
            var audioFile = new AnalyzableAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName));

            using (var groupToken = new GroupToken())
                audioFile.AnalyzeAsync(analyzer, settings, groupToken, CancellationToken.None).Wait();

            Assert.True(new Comparer().Compare(expectedMetadata, audioFile.Metadata, out var differences), string.Join(' ', differences));
        }

        [Theory(DisplayName = "AnalyzableAudioFile's Analyze method creates the expected metadata for a group")]
        [MemberData(nameof(AnalyzeGroupDataSource.Data), MemberType = typeof(AnalyzeGroupDataSource))]
        public void AnalyzeCreatesExpectedMetadataForGroup(
            [NotNull] string fileName1,
            [NotNull] string fileName2,
            [NotNull] string fileName3,
            [NotNull] string analyzer,
            [NotNull] TestSettingDictionary settings,
            [NotNull] TestAudioMetadata expectedMetadata1,
            [NotNull] TestAudioMetadata expectedMetadata2,
            [NotNull] TestAudioMetadata expectedMetadata3
            )
        {
            var audioFile1 = new AnalyzableAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName1));
            var audioFile2 = new AnalyzableAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName2));
            var audioFile3 = new AnalyzableAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName3));

            using (var groupToken = new GroupToken(3))
            {
                var task1 = audioFile1.AnalyzeAsync(analyzer, settings, groupToken, CancellationToken.None);
                var task2 = audioFile2.AnalyzeAsync(analyzer, settings, groupToken, CancellationToken.None);
                var task3 = audioFile3.AnalyzeAsync(analyzer, settings, groupToken, CancellationToken.None);
                Task.WaitAll(task1, task2, task3);
            }

            Assert.True(new Comparer().Compare(expectedMetadata1, audioFile1.Metadata, out var differences), string.Join(' ', differences));
            Assert.True(new Comparer().Compare(expectedMetadata2, audioFile2.Metadata, out differences), string.Join(' ', differences));
            Assert.True(new Comparer().Compare(expectedMetadata3, audioFile3.Metadata, out differences), string.Join(' ', differences));
        }
    }
}
