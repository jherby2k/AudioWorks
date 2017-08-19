using AudioWorks.Api.Tests;
using JetBrains.Annotations;
using System;
using System.IO;
using System.Management.Automation.Runspaces;

namespace AudioWorks.Commands.Tests
{
    [UsedImplicitly]
    public class ModuleFixture : IDisposable
    {
        const string _moduleProject = "AudioWorks.Commands";

        [NotNull]
        internal Runspace Runspace { get; }

        public ModuleFixture()
        {
            var workingDir = new DirectoryInfo(Directory.GetCurrentDirectory());

            DotNetUtility.Publish(
                Path.Combine(workingDir.Parent.Parent.Parent.Parent.FullName, _moduleProject),
                workingDir.Parent.Name,
                workingDir.FullName);

            // Import the module
            var state = InitialSessionState.CreateDefault();
            state.ImportPSModule(new[] { Path.Combine(workingDir.FullName, $"{_moduleProject}.dll") });
            Runspace = RunspaceFactory.CreateRunspace(state);
            Runspace.Open();
        }

        public void Dispose()
        {
            Runspace.Close();
        }
    }
}