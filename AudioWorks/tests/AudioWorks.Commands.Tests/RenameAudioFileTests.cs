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
    public sealed class RenameAudioFileTests(ModuleFixture moduleFixture) : IClassFixture<ModuleFixture>
    {
        readonly IMapper _mapper = new MapperConfiguration(config => config.CreateMap<AudioMetadata, AudioMetadata>()).CreateMapper();

        [Fact(DisplayName = "Rename-AudioFile command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Rename-AudioFile");
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

        [Fact(DisplayName = "Rename-AudioFile requires the Name parameter")]
        public void RequiresNameParameter()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
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
                ps.Runspace = moduleFixture.Runspace;
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
                ps.Runspace = moduleFixture.Runspace;
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
                ps.Runspace = moduleFixture.Runspace;
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
                ps.Runspace = moduleFixture.Runspace;
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

        [Fact(DisplayName = "Rename-AudioFile accepts a Force switch")]
        public void AcceptsForceSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Rename-AudioFile")
                    .AddParameter("AudioFile", new Mock<ITaggedAudioFile>().Object)
                    .AddParameter("Name", "Foo")
                    .AddParameter("Force");

                ps.Invoke();

                Assert.True(true);
            }
        }

        [Fact(DisplayName = "Rename-AudioFile accepts a PassThru switch")]
        public void AcceptsPassThruSwitch()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
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
                ps.Runspace = moduleFixture.Runspace;
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
        [MemberData(nameof(RenameValidFileDataSource.FileNames), MemberType = typeof(RenameValidFileDataSource))]
        public void PassThruSwitchReturnsAudioFile(string fileName)
        {
            var path = Path.Combine("Output", "Rename-AudioFile", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.Copy(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName), path, true);
            var audioFile = new TaggedAudioFile(path);
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Rename-AudioFile")
                    .AddParameter("AudioFile", audioFile)
                    .AddParameter("Name", "Foo")
                    .AddParameter("Force")
                    .AddParameter("PassThru");

                Assert.Equal(audioFile, ps.Invoke()[0].BaseObject);
            }
        }

        [Theory(DisplayName = "Rename-AudioFile renames the file")]
        [MemberData(nameof(RenameValidFileDataSource.Data), MemberType = typeof(RenameValidFileDataSource))]
        public void RenamesFile(string fileName, AudioMetadata metadata, string name, string expectedFileName)
        {
            var path = Path.Combine("Output", "Rename-AudioFile", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.Copy(Path.Combine(PathUtility.GetTestFileRoot(), "Valid", fileName), path, true);
            var audioFile = new TaggedAudioFile(path);
            _mapper.Map(metadata, audioFile.Metadata);
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Rename-AudioFile")
                    .AddParameter("AudioFile", audioFile)
                    .AddParameter("Name", name)
                    .AddParameter("Force");

                ps.Invoke();

                Assert.Equal(expectedFileName, Path.GetFileName(audioFile.Path));
            }
        }
    }
}
