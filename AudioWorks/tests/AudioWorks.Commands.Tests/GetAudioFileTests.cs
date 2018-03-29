using System;
using System.IO;
using System.Management.Automation;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Common;
using JetBrains.Annotations;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public sealed class GetAudioFileTests : IClassFixture<ModuleFixture>
    {
        [NotNull] readonly ModuleFixture _moduleFixture;

        public GetAudioFileTests([NotNull] ModuleFixture moduleFixture)
        {
            _moduleFixture = moduleFixture;
        }

        [Fact(DisplayName = "Get-AudioFile command exists")]
        public void CommandExists()
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
        public void AcceptsPathParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioFile")
                    .AddParameter("Path", "Foo");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioFile requires the Path parameter")]
        public void RequiresPathParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioFile");

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Get-AudioFile accepts the Path parameter as the first argument")]
        public void AcceptsPathParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioFile")
                    .AddArgument("Foo");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioFile accepts the Path parameter from the pipeline")]
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
                ps.AddCommand("Get-AudioFile");

                ps.Invoke();
                foreach (var error in ps.Streams.Error)
                    if (error.Exception is ParameterBindingException &&
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound",
                            StringComparison.InvariantCulture))
                        throw error.Exception;

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioFile has an OutputType of ITaggedAudioFile")]
        public void OutputTypeIsITaggedAudioFile()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Get-AudioFile");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");

                var result = ps.Invoke();

                Assert.Equal(typeof(ITaggedAudioFile), (Type) result[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Get-AudioFile returns an error if the Path can't be found")]
        public void PathNotFoundReturnsError()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioFile")
                    .AddArgument("Foo");

                ps.Invoke();

                var errors = ps.Streams.Error.ReadAll();
                Assert.Single(errors);
                Assert.IsType<ItemNotFoundException>(errors[0].Exception);
                Assert.Equal($"{nameof(ItemNotFoundException)},AudioWorks.Commands.GetAudioFileCommand",
                    errors[0].FullyQualifiedErrorId);
                Assert.Equal(ErrorCategory.ObjectNotFound, errors[0].CategoryInfo.Category);
            }
        }

        [Theory(DisplayName = "Get-AudioFile returns an error if the Path is an unsupported file")]
        [MemberData(nameof(UnsupportedFileDataSource.Data), MemberType = typeof(UnsupportedFileDataSource))]
        public void PathUnsupportedReturnsError([NotNull] string fileName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioFile")
                    .AddArgument(Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Unsupported",
                        fileName));

                ps.Invoke();

                var errors = ps.Streams.Error.ReadAll();
                Assert.Single(errors);
                Assert.IsType<AudioUnsupportedException>(errors[0].Exception);
                Assert.Equal($"{nameof(AudioUnsupportedException)},AudioWorks.Commands.GetAudioFileCommand",
                    errors[0].FullyQualifiedErrorId);
                Assert.Equal(ErrorCategory.InvalidData, errors[0].CategoryInfo.Category);
            }
        }

        [Theory(DisplayName = "Get-AudioFile returns an error if the Path is an invalid file")]
        [MemberData(nameof(InvalidFileDataSource.Data), MemberType = typeof(InvalidFileDataSource))]
        public void PathInvalidReturnsError([NotNull] string fileName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioFile")
                    .AddArgument(Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Invalid",
                        fileName));

                ps.Invoke();

                var errors = ps.Streams.Error.ReadAll();
                Assert.Single(errors);
                Assert.IsType<AudioInvalidException>(errors[0].Exception);
                Assert.Equal($"{nameof(AudioInvalidException)},AudioWorks.Commands.GetAudioFileCommand",
                    errors[0].FullyQualifiedErrorId);
                Assert.Equal(ErrorCategory.InvalidData, errors[0].CategoryInfo.Category);
            }
        }

        [Theory(DisplayName = "Get-AudioFile returns an ITaggedAudioFile")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void ReturnsITaggedAudioFile([NotNull] string fileName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioFile")
                    .AddArgument(Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid",
                        fileName));

                Assert.IsAssignableFrom<ITaggedAudioFile>(ps.Invoke()[0].BaseObject);
            }
        }

        [Theory(DisplayName = "Get-AudioFile returns an ITaggedAudioFile using a relative path")]
        [MemberData(nameof(ValidFileDataSource.FileNames), MemberType = typeof(ValidFileDataSource))]
        public void RelativePathReturnsITaggedAudioFile([NotNull] string fileName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Push-Location")
                    .AddArgument(Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                        "TestFiles",
                        "Valid"));
                ps.AddStatement();
                ps.AddCommand("Get-AudioFile")
                    .AddArgument(fileName);
                var result = ps.Invoke();
                ps.Commands.Clear();
                ps.AddCommand("Pop-Location");

                ps.Invoke();

                Assert.IsAssignableFrom<ITaggedAudioFile>(result[0].BaseObject);
            }
        }
    }
}