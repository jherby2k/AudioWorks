using JetBrains.Annotations;
using System;
using System.IO;
using System.Management.Automation;
using AudioWorks.Common;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    [Collection("Module")]
    public class GetAudioInfoTests
    {
        [NotNull] readonly ModuleFixture _moduleFixture;

        public GetAudioInfoTests(
            [NotNull] ModuleFixture moduleFixture)
        {
            _moduleFixture = moduleFixture;
        }

        [Fact(DisplayName = "Get-AudioInfo command exists")]
        public void GetAudioInfoCommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioInfo");
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

        [Fact(DisplayName = "Get-AudioInfo accepts an AudioFile parameter")]
        public void GetAudioInfoAcceptsAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioInfo")
                    .AddParameter("AudioFile", new AudioFile(
                        new FileInfo("Foo"),
                        new AudioInfo("Test", 2, 16, 44100, 0)));
                ps.Invoke();
                Assert.True(true);
            }
        }
    }
}
