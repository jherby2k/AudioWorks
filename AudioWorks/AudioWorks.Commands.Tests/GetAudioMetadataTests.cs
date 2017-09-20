using JetBrains.Annotations;
using System;
using System.IO;
using System.Management.Automation;
using AudioWorks.Api;
using AudioWorks.Api.Tests;
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
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, null));
                    // ReSharper restore AssignNullToNotNullAttribute
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
                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Get-AudioMetadata accepts the AudioFile parameter as the first argument")]
        public void GetAudioMetadataAcceptsAudioFileParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddArgument(new AudioFile(null, null, null));
                    // ReSharper restore AssignNullToNotNullAttribute
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioMetadata accepts the AudioFile parameter from the pipeline")]
        public void GetAudioMetadataAcceptsAudioFileParameterFromPipeline()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddArgument(new AudioFile(null, null, null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("PassThru");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Value");
                ps.AddCommand("Get-AudioMetadata");
                ps.Invoke();
                foreach (var error in ps.Streams.Error)
                    if (error.Exception is ParameterBindingException &&
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound"))
                        throw error.Exception;
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Get-AudioMetadata has an OutputType of AudioMetadata")]
        public void GetAudioMetadataOutputTypeIsAudioMetadata()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Get-AudioMetadata");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");
                Assert.Equal(typeof(AudioMetadata), (Type) ps.Invoke()[0].BaseObject);
            }
        }

        [Theory(DisplayName = "Get-AudioMetadata returns an AudioMetadata")]
        [MemberData(nameof(TestFilesValidDataSource.FileNames), MemberType = typeof(TestFilesValidDataSource))]
        public void GetAudioMetadataReturnsAudioMetadata([NotNull] string fileName)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioMetadata")
                    .AddArgument(AudioFileFactory.Create(Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                        "TestFiles",
                        "Valid",
                        fileName)));
                Assert.IsType<AudioMetadata>(ps.Invoke()[0].BaseObject);
            }
        }
    }
}
