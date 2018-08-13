using System;
using System.IO;
using System.Management.Automation;
using AudioWorks.Api;
using AudioWorks.Api.Tests;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using JetBrains.Annotations;
using Moq;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public sealed class ExportAudioFileTests : IClassFixture<ModuleFixture>
    {
        [NotNull] readonly ModuleFixture _moduleFixture;

        public ExportAudioFileTests([NotNull] ModuleFixture moduleFixture)
        {
            _moduleFixture = moduleFixture;
        }

        [Fact(DisplayName = "Export-AudioFile command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Export-AudioFile");
                try
                {
                    ps.Invoke();
                }
                catch (Exception e) when (!(e is CommandNotFoundException))
                {
                    // CommandNotFoundException is the only type we are testing for
                }

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Export-AudioFile requires the Encoder parameter")]
        public void RequiresEncoderParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Export-AudioFile")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Path", "Bar");

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Export-AudioFile requires the AudioFile parameter")]
        public void RequiresAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Export-AudioFile")
                    .AddParameter("Encoder", "Foo")
                    .AddParameter("Path", "Bar");

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Export-AudioFile requires the Path parameter")]
        public void RequiresPathParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Export-AudioFile")
                    .AddParameter("Encoder", "Foo")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object);

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Export-AudioFile throws an exception if an encoded path references an invalid metadata field")]
        public void PathInvalidEncodingThrowsException()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Info)
                .Returns(AudioInfo.CreateForLossless("Test", 2, 16, 44100, 100));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Export-AudioFile")
                    .AddParameter("Encoder", "Wave")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("Path", "{Invalid}");

                Assert.IsType<ArgumentException>(
                    Assert.Throws<CmdletInvocationException>(() => ps.Invoke())
                        .InnerException);
            }
        }

        [Fact(DisplayName = "Export-AudioFile has an OutputType of ITaggedAudioFile")]
        public void OutputTypeIsITaggedAudioFile()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Export-AudioFile");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");

                Assert.Equal(typeof(ITaggedAudioFile), (Type) ps.Invoke()[0].BaseObject);
            }
        }

        [Theory(DisplayName = "Export-AudioFile creates the expected audio file")]
        [MemberData(nameof(EncodeValidFileDataSource.Data), MemberType = typeof(EncodeValidFileDataSource))]
        public void CreatesExpectedAudioFile(
            int index,
            [NotNull] string sourceFileName,
            [NotNull] string encoderName,
            [CanBeNull] TestSettingDictionary settings,
#if LINUX
            [NotNull] string expectedUbuntu1604Hash,
            [NotNull] string expectedUbuntu1804Hash)
#else
            [NotNull] string expected32BitHash,
            [NotNull] string expected64BitHash)
#endif
        {
            var path = Path.Combine("Output", "Export-AudioFile", "Valid");
            Directory.CreateDirectory(path);
            var sourceAudioFile = new TaggedAudioFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                sourceFileName));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Export-AudioFile")
                    .AddArgument(encoderName)
                    .AddArgument(sourceAudioFile)
                    .AddParameter("Path", path)
                    .AddParameter("Name", $"{index:00} - {Path.GetFileNameWithoutExtension(sourceFileName)}")
                    .AddParameter("Replace");
                if (settings != null)
                    foreach (var item in settings)
                        if (item.Value is bool boolValue)
                        {
                            if (boolValue)
                                ps.AddParameter(item.Key);
                        }
                        else
                            ps.AddParameter(item.Key, item.Value);

                var results = ps.Invoke();

                Assert.Single(results);
#if LINUX
                Assert.Equal(LinuxUtility.GetRelease().StartsWith("Ubuntu 16.04", StringComparison.Ordinal)
                    ? expectedUbuntu1604Hash
                    : expectedUbuntu1804Hash,
#else
                Assert.Equal(Environment.Is64BitProcess ? expected64BitHash : expected32BitHash,
#endif
                    HashUtility.CalculateHash((ITaggedAudioFile) results[0].BaseObject));
            }
        }
    }
}
