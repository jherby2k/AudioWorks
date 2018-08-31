using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using JetBrains.Annotations;

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

            // This bypasses the execution policy (InitialSessionState.ExecutionPolicy isn't available with PowerShell 5)
            state.AuthorizationManager = new AuthorizationManager("Microsoft.PowerShell");

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