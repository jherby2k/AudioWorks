using JetBrains.Annotations;
using System;
using System.IO;
using System.Management.Automation;
using AudioWorks.Common;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    [Collection("Module")]
    public sealed class GetAudioMetadataTests
    {
        [NotNull] readonly ModuleFixture _moduleFixture;

        public GetAudioMetadataTests(
            [NotNull] ModuleFixture moduleFixture)
        {
            _moduleFixture = moduleFixture;
        }

        [Fact(DisplayName = "Get-AudioMetadata command exists")]
        public void GetAudioMetadataCommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioMetadata");
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

        [Fact(DisplayName = "Get-AudioMetadata accepts an AudioFile parameter")]
        public void GetAudioMetadataAcceptsAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        new FileInfo("Foo"),
                        AudioInfo.CreateForLossless("Test", 2, 16, 44100),
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null));
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioMetadata requires the AudioFile parameter")]
        public void GetAudioMetadataRequiresAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioMetadata");
                Assert.Throws<ParameterBindingException>(() =>
                    ps.Invoke());
            }
        }

        [Fact(DisplayName = "Get-AudioMetadata accepts the AudioFile parameter as the first argument")]
        public void GetAudioMetadataAcceptsAudioFileParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioMetadata")
                    .AddArgument(new AudioFile(
                        new FileInfo("Foo"),
                        AudioInfo.CreateForLossless("Test", 2, 16, 44100),
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null));
                ps.Invoke();
                Assert.True(true);
            }
        }
    }
}
