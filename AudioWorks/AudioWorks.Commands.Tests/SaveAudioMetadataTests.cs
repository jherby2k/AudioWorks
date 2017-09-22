using AudioWorks.Common;
using JetBrains.Annotations;
using System;
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
        public void SaveAudioMetadataCommandExists()
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
        public void SaveAudioMetadataAcceptsAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null));
                    // ReSharper restore AssignNullToNotNullAttribute
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata requires the AudioFile parameter")]
        public void SaveAudioMetadataRequiresAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata");
                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata accepts the AudioFile parameter as the first argument")]
        public void SaveAudioMetadataAcceptsAudioFileParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddArgument(new AudioFile(null, null, fileInfo => new AudioMetadata(), null));
                    // ReSharper restore AssignNullToNotNullAttribute
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata accepts the AudioFile parameter from the pipeline")]
        public void SaveAudioMetadataAcceptsAudioFileParameterFromPipeline()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddArgument(new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
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
        public void SaveAudioMetadataAcceptsPassThruSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    // ReSharper disable AssignNullToNotNullAttribute
                    .AddParameter("AudioFile", new AudioFile(null, null, fileInfo => new AudioMetadata(), null))
                    // ReSharper restore AssignNullToNotNullAttribute
                    .AddParameter("PassThru");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata with PassThru switch returns the AudioFile")]
        public void SaveAudioMetadataPassThruSwitchReturnsAudioFile()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            var audioFile = new AudioFile(null, null, fileInfo => new AudioMetadata(), null);
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
        public void SaveAudioMetadataOutputTypeIsAudioFile()
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
    }
}
