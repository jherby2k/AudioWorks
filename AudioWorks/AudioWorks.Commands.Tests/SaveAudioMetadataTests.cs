using AudioWorks.Api;
using AudioWorks.Api.Tests;
using AudioWorks.Api.Tests.DataSources;
using AudioWorks.Common;
using JetBrains.Annotations;
using Moq;
using System;
using System.IO;
using System.Management.Automation;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    [Collection("Module")]
    public sealed class SaveAudioMetadataTests
    {
        [NotNull] readonly ModuleFixture _moduleFixture;

        public SaveAudioMetadataTests(
            [NotNull] ModuleFixture moduleFixture)
        {
            _moduleFixture = moduleFixture;
        }

        [Fact(DisplayName = "Save-AudioMetadata command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata");
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

        [Fact(DisplayName = "Save-AudioMetadata accepts an AudioFile parameter")]
        public void AcceptsAudioFileParameter()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.FileInfo).Returns(new FileInfo("Foo"));
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object);

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata requires the AudioFile parameter")]
        public void RequiresAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata");

                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata accepts the AudioFile parameter as the first argument")]
        public void AcceptsAudioFileParameterAsFirstArgument()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.FileInfo).Returns(new FileInfo("Foo"));
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    .AddArgument(mock.Object);

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata accepts the AudioFile parameter from the pipeline")]
        public void AcceptsAudioFileParameterFromPipeline()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.FileInfo).Returns(new FileInfo("Foo"));
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    .AddArgument(mock.Object)
                    .AddParameter("PassThru");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Value");
                ps.AddCommand("Save-AudioMetadata");

                ps.Invoke();
                foreach (var error in ps.Streams.Error)
                    if (error.Exception is ParameterBindingException &&
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound",
                            StringComparison.InvariantCulture))
                        throw error.Exception;

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata accepts a PassThru switch")]
        public void AcceptsPassThruSwitch()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.FileInfo).Returns(new FileInfo("Foo"));
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("PassThru");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata with PassThru switch returns the AudioFile")]
        public void PassThruSwitchReturnsAudioFile()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.FileInfo).Returns(new FileInfo("Foo"));
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("PassThru");

                Assert.Equal(mock.Object, ps.Invoke()[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata has an OutputType of ITaggedAudioFile")]
        public void OutputTypeIsAudioFile()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Save-AudioMetadata");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");

                Assert.Equal(typeof(ITaggedAudioFile), (Type) ps.Invoke()[0].BaseObject);
            }
        }

        [Theory(DisplayName = "Save-AudioMetadata creates the expected output")]
        [MemberData(nameof(SaveMetadataDataValidFileSource.Data), MemberType = typeof(SaveMetadataDataValidFileSource))]
        public void CreatesExpectedOutput(
            int index,
            [NotNull] string fileName,
            [NotNull] AudioMetadata metadata,
            [CanBeNull] SettingDictionary settings,
            [NotNull] string expectedHash)
        {
            var path = Path.Combine("Output", "Save-AudioMetadata", "Valid", $"{index:00} - {fileName}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName), path, true);
            var audioFile = new TaggedAudioFile(path) { Metadata = metadata };
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    .AddArgument(audioFile);
                if (settings != null)
                    foreach (var item in settings)
                        ps.AddParameter(item.Key, item.Value);

                ps.Invoke();
            }

            Assert.Equal(expectedHash, HashUtility.CalculateHash(audioFile));
        }

        [Theory(DisplayName = "Save-AudioMetadata method returns an error if the file is unsupported")]
        [MemberData(nameof(SaveMetadataUnsupportedFileDataSource.Data), MemberType = typeof(SaveMetadataUnsupportedFileDataSource))]
        public void UnsupportedFileReturnsError(int index, [NotNull] string fileName)
        {
            var path = Path.Combine("Output", "Save-AudioMetadata", "Unsupported", $"{index:00} - {fileName}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Copy(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "TestFiles",
                "Valid",
                fileName), path, true);
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    .AddArgument(new TaggedAudioFile(path));

                ps.Invoke();

                var errors = ps.Streams.Error.ReadAll();
                Assert.Single(errors);
                Assert.IsType<AudioUnsupportedException>(errors[0].Exception);
                Assert.Equal($"{nameof(AudioUnsupportedException)},AudioWorks.Commands.SaveAudioMetadataCommand",
                    errors[0].FullyQualifiedErrorId);
                Assert.Equal(ErrorCategory.InvalidData, errors[0].CategoryInfo.Category);
            }
        }
    }
}
