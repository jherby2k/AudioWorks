using JetBrains.Annotations;
using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation.Runspaces;

namespace AudioWorks.Commands.Tests
{
    [UsedImplicitly]
    public class ModuleFixture : IDisposable
    {
        const string _moduleProject = "AudioWorks.Commands";
        const string _moduleDir = "Module";

        [NotNull]
        internal Runspace Runspace { get; }

        public ModuleFixture()
        {
            var workingDir = new DirectoryInfo(Directory.GetCurrentDirectory());

            // Publish the module project to the output folder
            using (var publish = new Process())
            {
                publish.StartInfo.FileName = "dotnet";
                publish.StartInfo.Arguments =
                    $"publish -c {workingDir.Parent.Name} -o \"{workingDir.CreateSubdirectory(_moduleDir).FullName}\"";
                publish.StartInfo.WorkingDirectory = Path.Combine(
                    workingDir.Parent.Parent.Parent.Parent.FullName, _moduleProject);
                publish.Start();
                publish.WaitForExit();
            }

            // Import the module
            var state = InitialSessionState.CreateDefault();
            state.ImportPSModule(new[] { Path.Combine(workingDir.FullName, _moduleDir, $"{_moduleProject}.dll") });
            Runspace = RunspaceFactory.CreateRunspace(state);
            Runspace.Open();
        }

        public void Dispose()
        {
            Runspace.Close();
        }
    }
}