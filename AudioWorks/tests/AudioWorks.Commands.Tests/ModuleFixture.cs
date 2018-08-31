using System;
using System.IO;
using System.Management.Automation.Runspaces;
using JetBrains.Annotations;
using Microsoft.PowerShell;

namespace AudioWorks.Commands.Tests
{
    [UsedImplicitly]
    public sealed class ModuleFixture : IDisposable
    {
        const string _moduleProject = "AudioWorks.Commands";

        [NotNull]
        internal Runspace Runspace { get; }

        public ModuleFixture()
        {
            var state = InitialSessionState.CreateDefault();
            state.ExecutionPolicy = ExecutionPolicy.Bypass;
            state.ImportPSModule(new[]
            {
                Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName, _moduleProject)
            });
            Runspace = RunspaceFactory.CreateRunspace(state);
            Runspace.Open();
        }

        public void Dispose()
        {
            Runspace.Close();
        }
    }
}