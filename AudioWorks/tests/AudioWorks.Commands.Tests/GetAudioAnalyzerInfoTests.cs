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
using JetBrains.Annotations;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    public sealed class GetAudioAnalyzerInfoTests : IClassFixture<ModuleFixture>
    {
        [NotNull] readonly ModuleFixture _moduleFixture;

        public GetAudioAnalyzerInfoTests([NotNull] ModuleFixture moduleFixture) => _moduleFixture = moduleFixture;

        [Fact(DisplayName = "Get-AudioAnalyzerInfo command exists")]
        public void CommandExists()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioAnalyzerInfo");
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

        [Fact(DisplayName = "Get-AudioAnalyzerInfo has an OutputType of AudioAnalyzerInfo")]
        public void OutputTypeIsAudioAnalyzerInfo()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-Command")
                    .AddArgument("Get-AudioAnalyzerInfo");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "OutputType");
                ps.AddCommand("Select-Object")
                    .AddParameter("ExpandProperty", "Type");

                Assert.Equal(typeof(AudioAnalyzerInfo), (Type) ps.Invoke()[0].BaseObject);
            }
        }

        [Fact(DisplayName = "Get-AudioAnalyzerInfo returns an AudioAnalyzerInfo")]
        public void ReturnsAudioAnalyzerInfo()
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _moduleFixture.Runspace;
                ps.AddCommand("Get-AudioAnalyzerInfo");

                Assert.IsAssignableFrom<AudioAnalyzerInfo>(ps.Invoke()[0].BaseObject);
            }
        }
    }
}
