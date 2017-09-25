using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.IO;
using System.Management.Automation;
using System.Security.Cryptography;
using AudioWorks.Api;
using AudioWorks.Api.Tests;
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
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(
                        new FileInfo("Foo"),
                        null,
                        fileInfo => new AudioMetadata(),
                        (fileInfo, metadata, settings) => { }));
                    // ReSharper restore AssignNullToNotNullAttribute
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
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddArgument(new AudioFile(
                        new FileInfo("Foo"),
                        null,
                        fileInfo => new AudioMetadata(),
                        (fileInfo, metadata, settings) => { }));
                    // ReSharper restore AssignNullToNotNullAttribute
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata accepts the AudioFile parameter from the pipeline")]
        public void AcceptsAudioFileParameterFromPipeline()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddArgument(new AudioFile(
                        new FileInfo("Foo"),
                        null,
                        fileInfo => new AudioMetadata(),
                        (fileInfo, metadata, settings) => { }))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("PassThru");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Value");
                ps.AddCommand("Save-AudioMetadata");
                ps.Invoke();
                foreach (var error in ps.Streams.Error)
                    if (error.Exception is ParameterBindingException &&
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound"))
                        throw error.Exception;
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata accepts a PassThru switch")]
        public void AcceptsPassThruSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(
                        new FileInfo("Foo"),
                        null,
                        fileInfo => new AudioMetadata(),
                        (fileInfo, metadata, settings) => { }))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("PassThru");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata with PassThru switch returns the AudioFile")]
        public void PassThruSwitchReturnsAudioFile()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            var audioFile = new AudioFile(
                new FileInfo("Foo"),
                null,
                fileInfo => new AudioMetadata(),
                (fileInfo, metadata, settings) => { });
            // ReSharper restore AssignNullToNotNullAttribute
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    .AddParameter("AudioFile", audioFile)
                    .AddParameter("PassThru");
                Assert.Equal(audioFile, ps.Invoke()[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata has an OutputType of AudioFile")]
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
                Assert.Equal(typeof(AudioFile), (Type)ps.Invoke()[0].BaseObject);
            }
        }

        [Theory(DisplayName = "Save-AudioMetadata creates the expected output")]
        [MemberData(nameof(TestFilesValidSaveMetadataDataSource.Data), MemberType = typeof(TestFilesValidSaveMetadataDataSource))]
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
            var audioFile = AudioFileFactory.Create(path);
            audioFile.Metadata = metadata;
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
            Assert.Equal(expectedHash, CalculateHash(audioFile));
        }

        [Theory(DisplayName = "Save-AudioMetadata method returns an error if the file is unsupported")]
        [MemberData(nameof(TestFilesUnsupportedSaveMetadataDataSource.Data), MemberType = typeof(TestFilesUnsupportedSaveMetadataDataSource))]
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
                    .AddArgument(AudioFileFactory.Create(path));
                ps.Invoke();
                var errors = ps.Streams.Error.ReadAll();
                Assert.True(
                    errors.Count == 1 &&
                    errors[0].Exception is AudioUnsupportedException &&
                    errors[0].FullyQualifiedErrorId == $"{nameof(AudioUnsupportedException)},AudioWorks.Commands.SaveAudioMetadataCommand" &&
                    errors[0].CategoryInfo.Category == ErrorCategory.InvalidData);
            }
        }

        [Pure, NotNull]
        static string CalculateHash([NotNull] AudioFile audioFile)
        {
            using (var md5 = MD5.Create())
            using (var fileStream = audioFile.FileInfo.OpenRead())
                return BitConverter.ToString(md5.ComputeHash(fileStream))
                    .Replace("-", string.Empty);
        }
    }
}
