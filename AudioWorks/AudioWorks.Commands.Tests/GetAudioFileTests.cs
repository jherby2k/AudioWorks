using JetBrains.Annotations;
using System;
using System.IO;
using System.Management.Automation;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public class GetAudioFileTests :
        IClassFixture<ModuleFixture>
    {
        [NotNull] readonly ModuleFixture _moduleFixture;

        public GetAudioFileTests(
            [NotNull] ModuleFixture moduleFixture)
        {
            _moduleFixture = moduleFixture;
        }

        [Fact(DisplayName = "Get-AudioFile command exists")]
        public void GetAudioFileCommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
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

        [Fact(DisplayName = "Get-AudioFile accepts a Path parameter")]
        public void GetAudioFileAcceptsPathParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioFile").AddParameter("Path", "Foo");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioFile accepts the Path parameter as the first argument")]
        public void GetAudioFileAcceptsPathParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioFile").AddArgument("Foo");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioFile accepts the Path parameter from the pipeline")]
        public void GetAudioFileAcceptsPathParameterFromPipeline()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("New-Variable").AddArgument("Path").AddParameter("Value", "Foo");
                ps.AddCommand("Get-AudioFile");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioFile requires the Path parameter")]
        public void GetAudioFileRequiresPathParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioFile");
                Assert.Throws(typeof(ParameterBindingException), () => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Get-AudioFile returns an error if the Path can't be found")]
        public void GetAudioFilePathNotFoundReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioFile").AddArgument("Foo");
                ps.Invoke();
                var errors = ps.Streams.Error.ReadAll();
                Assert.True(
                    errors.Count == 1 &&
                    errors[0].Exception is FileNotFoundException &&
                    errors[0].FullyQualifiedErrorId == $"{nameof(FileNotFoundException)},AudioWorks.Commands.GetAudioFileCommand" &&
                    errors[0].CategoryInfo.Category == ErrorCategory.InvalidArgument);
            }
        }
    }
}
