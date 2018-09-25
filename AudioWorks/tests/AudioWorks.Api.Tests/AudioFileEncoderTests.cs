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
        public AudioFileEncoderTests([NotNull] ITestOutputHelper outputHelper)
        {
            LoggingManager.LoggerFactory.AddProvider(new XUnitLoggerProvider(outputHelper));
        }

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
            //TODO move this into a SettingDictionary test class
            Assert.Throws<ArgumentException>(() =>
                new AudioFileEncoder("Wave", null, null, new SettingDictionary { ["Foo"] = "Bar" }));
        }

        [Fact(DisplayName = "AudioFileEncoder's Settings property throws an exception if an unexpected setting is provided")]
        public void SettingsUnexpectedSettingThrowsException()
        {
            //TODO move this into a SettingDictionary test class
            var encoder = new AudioFileEncoder("Wave");
            Assert.Throws<ArgumentException>(() => encoder.Settings["Foo"] = "Bar");
        }

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
            var path = Path.Combine("Output", "Encode", "Valid");
            Directory.CreateDirectory(path);
            var audioFile = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                sourceFileName));

            var results = (await new AudioFileEncoder(
                        encoderName,
                        path,
                        $"{index:00} - {Path.GetFileNameWithoutExtension(sourceFileName)}",
                        settings)
                    { Overwrite = true }
                .EncodeAsync(audioFile).ConfigureAwait(false)).ToArray();

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
                HashUtility.CalculateHash(results[0]));
        }
    }
}
