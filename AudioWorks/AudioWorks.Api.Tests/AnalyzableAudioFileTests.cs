using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using JetBrains.Annotations;
using ObjectsComparer;
using System;
using System.IO;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
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
            [NotNull] TestAudioMetadata expectedMetadata)
        {
            var audioFile = new AnalyzableAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName));

            audioFile.Analyze(analyzer);

            Assert.True(new Comparer().Compare(expectedMetadata, audioFile.Metadata));
        }
    }
}
