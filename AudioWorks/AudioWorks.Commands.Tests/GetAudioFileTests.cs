using JetBrains.Annotations;
using System;
using System.Management.Automation;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public class GetAudioFileTests :
        IClassFixture<PowerShellFixture>
    {
        [NotNull] readonly PowerShellFixture _psFixture;

        public GetAudioFileTests(
            [NotNull] PowerShellFixture psFixture)
        {
            _psFixture = psFixture;
        }

        [Fact(DisplayName = "Get-AudioFile command exists")]
        public void GetAudioFileCommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _psFixture.Runspace;
                ps.AddCommand("Get-AudioFile");
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
    }
}
