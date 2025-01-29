/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.IO;
using System.Management.Automation;
using AudioWorks.Api;
using AudioWorks.Common;
using AudioWorks.TestUtilities;
using AudioWorks.TestUtilities.DataSources;
using AutoMapper;
using Moq;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public sealed class SaveAudioMetadataTests(ModuleFixture moduleFixture) : IClassFixture<ModuleFixture>
    {
        readonly IMapper _mapper = new MapperConfiguration(
            config => config.CreateMap<AudioMetadata, AudioMetadata>()).CreateMapper();

        [Fact(DisplayName = "Save-AudioMetadata command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata");
                try
                {
                    ps.Invoke();
                }
                catch (Exception e) when (e is not CommandNotFoundException)
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
            mock.SetupGet(audioFile => audioFile.Path).Returns("Foo");
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());

            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
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
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata");

                // ReSharper disable once AccessToDisposedClosure
                Assert.Throws<ParameterBindingException>(() => ps.Invoke());
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata accepts the AudioFile parameter as the first argument")]
        public void AcceptsAudioFileParameterAsFirstArgument()
        {
            var mock = new Mock<ITaggedAudioFile>();
            mock.SetupGet(audioFile => audioFile.Path).Returns("Foo");
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
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
            mock.SetupGet(audioFile => audioFile.Path).Returns("Foo");
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
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
            mock.SetupGet(audioFile => audioFile.Path).Returns("Foo");
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
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
            mock.SetupGet(audioFile => audioFile.Path).Returns("Foo");
            mock.SetupGet(audioFile => audioFile.Metadata).Returns(new AudioMetadata());
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    .AddParameter("AudioFile", mock.Object)
                    .AddParameter("PassThru");

                Assert.Equal(mock.Object, ps.Invoke()[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Save-AudioMetadata has an OutputType of ITaggedAudioFile")]
        public void OutputTypeIsITaggedAudioFile()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
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
        [MemberData(nameof(SaveMetadataValidFileSource.Data), MemberType = typeof(SaveMetadataValidFileSource))]
        public void CreatesExpectedOutput(
            int index,
            string fileName,
            AudioMetadata metadata,
            string imageFileName,
            SettingDictionary settings,
            string[] validHashes)
        {
            var sourceDirectory = Path.Combine(PathUtility.GetTestFileRoot(), "Valid");
            var path = Path.Combine("Output", "Save-AudioMetadata", "Valid", $"{index:000} - {fileName}");
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.Copy(Path.Combine(sourceDirectory, fileName), path, true);
            var audioFile = new TaggedAudioFile(path);
            _mapper.Map(metadata, audioFile.Metadata);
            if (!string.IsNullOrEmpty(imageFileName))
                audioFile.Metadata.CoverArt = CoverArtFactory.GetOrCreate(Path.Combine(sourceDirectory, imageFileName));
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Save-AudioMetadata")
                    .AddArgument(audioFile);
                foreach (var item in settings)
                    if (item.Value is bool boolValue)
                    {
                        if (boolValue)
                            ps.AddParameter(item.Key);
                    }
                    else
                        ps.AddParameter(item.Key, item.Value);

                ps.Invoke();
            }

            Assert.Contains(HashUtility.CalculateHash(audioFile.Path), validHashes);
        }

        [Theory(DisplayName = "Save-AudioMetadata method returns an error if the file is unsupported")]
        [MemberData(nameof(SaveMetadataUnsupportedFileDataSource.Data), MemberType = typeof(SaveMetadataUnsupportedFileDataSource))]
        public void UnsupportedFileReturnsError(int index, string fileName)
        {
            var path = Path.Combine("Output", "Save-AudioMetadata", "Unsupported", $"{index:000} - {fileName}");
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.Copy(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName), path, true);
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
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
