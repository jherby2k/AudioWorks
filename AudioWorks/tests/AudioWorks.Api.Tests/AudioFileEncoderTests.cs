using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using JetBrains.Annotations;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public sealed class AudioFileEncoderTests
    {
        [Fact(DisplayName = "AudioFileEncoder's constructor throws an exception if the name is null")]
        public void ConstructorNameNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new AudioFileEncoder(null));
        }

        [Fact(DisplayName = "AudioFileEncoder's constructor throws an exception if the name is unsupported")]
        public void ConstructorNameUnsupportedThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new AudioFileEncoder("Foo"));
        }

        [Fact(DisplayName = "AudioFileEncoder's constructor throws an exception if encodedDirectoryName references an invalid metadata field")]
        public void ConstructorEncodedDirectoryNameInvalidThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new AudioFileEncoder("Wave", null, "{Invalid}"));
        }

        [Fact(DisplayName = "AudioFileEncoder's constructor throws an exception if an unexpected setting is provided")]
        public void ConstructorUnexpectedSettingThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new AudioFileEncoder("Wave", new SettingDictionary { ["Foo"] = "Bar" }));
        }

        [Theory(DisplayName = "AudioFileEncoder's Encode method creates the expected audio file")]
        [MemberData(nameof(EncodeValidFileDataSource.Data), MemberType = typeof(EncodeValidFileDataSource))]
        public void EncodeCreatesExpectedAudioFile(
            int index,
            [NotNull] string sourceFileName,
            [NotNull] string encoderName,
            [CanBeNull] TestSettingDictionary settings,
            [NotNull] string expected32BitHash,
            [NotNull] string expected64BitHash)
        {
            var path = Path.Combine("Output", "Encode", "Valid");
            Directory.CreateDirectory(path);
            var audioFile = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                sourceFileName));

            var results = new AudioFileEncoder(
                    encoderName,
                    settings,
                    path,
                    $"{index:00} - {Path.GetFileNameWithoutExtension(sourceFileName)}",
                    true)
                .Encode(audioFile).ToArray();

            Assert.Single(results);
            Assert.Equal(Environment.Is64BitProcess ? expected64BitHash : expected32BitHash,
                HashUtility.CalculateHash(results[0]));
        }
    }
}
