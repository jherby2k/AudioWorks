using System;
using System.Management.Automation;
using AudioWorks.Api;
using JetBrains.Annotations;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public sealed class GetAudioCoverArtTests : IClassFixture<ModuleFixture>
    {
        [NotNull] readonly ModuleFixture _moduleFixture;

        public GetAudioCoverArtTests([NotNull] ModuleFixture moduleFixture)
        {
            _moduleFixture = moduleFixture;
        }

        [Fact(DisplayName = "Get-AudioCoverArt command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioCoverArt");
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

        [Fact(DisplayName = "Get-AudioCoverArt accepts a Path parameter")]
        public void AcceptsPathParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioCoverArt")
                    .AddParameter("Path", "Foo");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioCoverArt requires the Path parameter")]
        public void RequiresPathParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioCoverArt");

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Get-AudioCoverArt accepts the Path parameter as the first argument")]
        public void AcceptsPathParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioCoverArt")
                    .AddArgument("Foo");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioCoverArt accepts the Path parameter from the pipeline")]
        public void AcceptsPathParameterFromPipeline()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("path")
                    .AddArgument("Foo")
                    .AddParameter("PassThru");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Value");
                ps.AddCommand("Get-AudioCoverArt");

                ps.Invoke();
                foreach (var error in ps.Streams.Error)
                    if (error.Exception is ParameterBindingException &&
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound",
                            StringComparison.InvariantCulture))
                        throw error.Exception;

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioCoverArt has an OutputType of CoverArt")]
        public void OutputTypeIsCoverArt()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Get-AudioCoverArt");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");

                var result = ps.Invoke();

                Assert.Equal(typeof(CoverArt), (Type) result[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Get-AudioCoverArt returns an error if the Path can't be found")]
        public void PathNotFoundReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioCoverArt")
                    .AddArgument("Foo");

                ps.Invoke();

                var errors = ps.Streams.Error.ReadAll();
                Assert.Single(errors);
                Assert.IsType<ItemNotFoundException>(errors[0].Exception);
                Assert.Equal($"{nameof(ItemNotFoundException)},AudioWorks.Commands.GetAudioCoverArtCommand",
                    errors[0].FullyQualifiedErrorId);
                Assert.Equal(ErrorCategory.ObjectNotFound, errors[0].CategoryInfo.Category);
            }
        }
    }
}