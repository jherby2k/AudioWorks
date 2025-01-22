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
using System.Management.Automation;
using AudioWorks.Api;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public sealed class GetAudioMetadataEncoderInfoTests(ModuleFixture moduleFixture) : IClassFixture<ModuleFixture>
    {
        [Fact(DisplayName = "Get-AudioMetadataEncoderInfo command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-AudioMetadataEncoderInfo");
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

        [Fact(DisplayName = "Get-AudioMetadataEncoderInfo has an OutputType of AudioMetadataEncoderInfo")]
        public void OutputTypeIsAudioMetadataEncoderInfo()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Get-AudioMetadataEncoderInfo");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");

                Assert.Equal(typeof(AudioMetadataEncoderInfo), (Type) ps.Invoke()[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Get-AudioMetadataEncoderInfo returns an AudioMetadataEncoderInfo")]
        public void ReturnsAudioEncoderInfo()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = moduleFixture.Runspace;
                ps.AddCommand("Get-AudioMetadataEncoderInfo");

                Assert.IsType<AudioMetadataEncoderInfo>(ps.Invoke()[0].BaseObject, false);
            }
        }
    }
}
