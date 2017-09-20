using AudioWorks.Common;
using JetBrains.Annotations;
using System;
using System.Management.Automation;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    [Collection("Module")]
    public sealed class SetAudioMetadataTests
    {
        [NotNull] readonly ModuleFixture _moduleFixture;

        public SetAudioMetadataTests(
            [NotNull] ModuleFixture moduleFixture)
        {
            _moduleFixture = moduleFixture;
        }

        [Fact(DisplayName = "Set-AudioMetadata command exists")]
        public void SetAudioMetadataCommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata");
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

        [Fact(DisplayName = "Set-AudioMetadata accepts an AudioFile parameter")]
        public void SetAudioMetadataAcceptsAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()));
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata requires the AudioFile parameter")]
        public void SetAudioMetadataRequiresAudioFileParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata");
                Assert.Throws<ParameterBindingException>(() =>
                    ps.Invoke());
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts the AudioFile parameter as the first argument")]
        public void SetAudioMetadataAcceptsAudioFileParameterAsFirstArgument()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddArgument(new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()));
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts the AudioFile parameter from the pipeline")]
        public void SetAudioMetadataAcceptsAudioFileParameterFromPipeline()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-Variable")
                    .AddArgument("audioFile")
                    .AddArgument(new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()))
                    .AddParameter("PassThru");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Value");
                ps.AddCommand("Set-AudioMetadata");
                ps.Invoke();
                foreach (var error in ps.Streams.Error)
                    if (error.Exception is ParameterBindingException &&
                        error.FullyQualifiedErrorId.StartsWith("InputObjectNotBound"))
                        throw error.Exception;
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata accepts a Title parameter")]
        public void SetAudioMetadataAcceptsTitleParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => new AudioMetadata()))
                    .AddParameter("Title", "Foo");
                ps.Invoke();
                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Set-AudioMetadata's Title Parameter sets the Title")]
        public void SetAudioMetadataTitleParameterSetsTitle()
        {
            var metadata = new AudioMetadata();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Set-AudioMetadata")
                    .AddParameter("AudioFile", new AudioFile(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        // ReSharper disable once AssignNullToNotNullAttribute
                        null,
                        fileInfo => metadata))
                    .AddParameter("Title", "Foo");
                ps.Invoke();
                Assert.Equal("Foo", metadata.Title);
            }
        }
    }
}
