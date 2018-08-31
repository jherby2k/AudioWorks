using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Management.Automation;
using AudioWorks.Api;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Api.Tests.DataTypes;
using AudioWorks.Common;
using JetBrains.Annotations;
using Moq;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public sealed class RenameAudioFileTests : IClassFixture<ModuleFixture>
    {
        [NotNull] readonly ModuleFixture _moduleFixture;

        public RenameAudioFileTests([NotNull] ModuleFixture moduleFixture)
        {
            _moduleFixture = moduleFixture;
        }

        [Fact(DisplayName = "Rename-AudioFile command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Rename-AudioFile");
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

        [Fact(DisplayName = "Rename-AudioFile requires the Name parameter")]
        public void RequiresNameParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Rename-AudioFile")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object);

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Rename-AudioFile accepts the Name parameter as the first argument")]
        public void AcceptsNameParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Rename-AudioFile")
                    .AddArgument("Foo")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object);

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Rename-AudioFile requires the AudioFile parameter")]
        public void RequiresAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Rename-AudioFile")
                    .AddParameter("Name", "Foo");

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Rename-AudioFile accepts the AudioFile parameter as the second argument")]
        public void AcceptsAudioFileParameterAsSecondArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Rename-AudioFile")
                    .AddParameter("Name", "Foo")
                    .AddArgument(new Mock<ITaggedAudioFile>().Object);

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Rename-AudioFile accepts the AudioFile parameter from the pipeline")]
        public void AcceptsAudioFileParameterFromPipeline()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    .AddArgument(new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("PassThru");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Value");
                ps.AddCommand("Rename-AudioFile")
                    .AddParameter("Name", "Foo");

                ps.Invoke();
                foreach (var error in ps.Streams.Error)
                    if (error.Exception is ParameterBindingException &&
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound",
                            StringComparison.InvariantCulture))
                        throw error.Exception;

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Rename-AudioFile accepts a Replace switch")]
        public void AcceptsReplaceSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Rename-AudioFile")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Name", "Foo")
                    .AddParameter("Replace");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Rename-AudioFile accepts a PassThru switch")]
        public void AcceptsPassThruSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Rename-AudioFile")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Name", "Foo")
                    .AddParameter("PassThru");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Rename-AudioFile has an OutputType of ITaggedAudioFile")]
        public void OutputTypeIsITaggedAudioFile()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Rename-AudioFile");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");

                Assert.Equal(typeof(ITaggedAudioFile), (Type) ps.Invoke()[0].BaseObject);
            }
        }

        [Theory(DisplayName = "Rename-AudioFile with PassThru switch returns the AudioFile")]
        [MemberData(nameof(RenameValidFileDataSource.FileNamesMetadataAndNames), MemberType = typeof(RenameValidFileDataSource))]
        public void PassThruSwitchReturnsAudioFile(
            [NotNull] string fileName,
            [NotNull] TestAudioMetadata metadata,
            [NotNull] string name)
        {
            var path = Path.Combine("Output", "Rename-AudioFile", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName), path, true);
            var audioFile = new TaggedAudioFile(path) { Metadata = metadata };
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Rename-AudioFile")
                    .AddParameter("AudioFile", audioFile)
                    .AddParameter("Name", name)
                    .AddParameter("Replace")
                    .AddParameter("PassThru");

                Assert.Equal(audioFile, ps.Invoke()[0].BaseObject);
            }
        }

        [Theory(DisplayName = "Rename-AudioFile renames the file")]
        [MemberData(nameof(RenameValidFileDataSource.Data), MemberType = typeof(RenameValidFileDataSource))]
        public void RenamesFile(
            [NotNull] string fileName,
            [NotNull] TestAudioMetadata metadata,
            [NotNull] string name,
            [NotNull] string expectedFileName)
        {
            var path = Path.Combine("Output", "Rename-AudioFile", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName,
                "TestFiles",
                "Valid",
                fileName), path, true);
            var audioFile = new TaggedAudioFile(path) { Metadata = metadata };
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Rename-AudioFile")
                    .AddParameter("AudioFile", audioFile)
                    .AddParameter("Name", name)
                    .AddParameter("Replace");

                ps.Invoke();

                Assert.Equal(expectedFileName, Path.GetFileName(audioFile.Path));
            }
        }
    }
}
