/* Copyright � 2018 Jeremy Herbison

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
using System.Management.Automation.Runspaces;
#if !NET472
using Microsoft.PowerShell;
#endif

namespace AudioWorks.Commands.Tests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class ModuleFixture : IDisposable
    {
        const string _moduleProject = "AudioWorks.Commands";

        internal Runspace Runspace { get; }

        public ModuleFixture()
        {
            var state = InitialSessionState.CreateDefault();

#if NET472
            // This bypasses the execution policy (InitialSessionState.ExecutionPolicy isn't available with PowerShell 5)
            state.AuthorizationManager = new("Microsoft.PowerShell");

            state.ImportPSModule([
                Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName, _moduleProject)
            ]);
#else
            state.ExecutionPolicy = ExecutionPolicy.Bypass;

            state.ImportPSModule(
                Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName, _moduleProject)
            );
#endif

            Runspace = RunspaceFactory.CreateRunspace(state);
            Runspace.Open();
        }

        public void Dispose() => Runspace.Close();
    }
}