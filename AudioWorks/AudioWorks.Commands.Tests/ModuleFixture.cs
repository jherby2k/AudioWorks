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

            Publish(
                Path.Combine(workingDir.Parent.Parent.Parent.Parent.FullName, _moduleProject),
                workingDir.Parent.Name,
                workingDir.CreateSubdirectory(_moduleDir).FullName);

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

        void Publish(string projectDir, string configuration, string outputDir)
        {
            using (var publish = new Process())
            {
                publish.StartInfo.FileName = "dotnet";
                publish.StartInfo.Arguments = $"publish -c {configuration} -o \"{outputDir}\"";
                publish.StartInfo.WorkingDirectory = projectDir;
                publish.Start();
                publish.WaitForExit();
            }
        }
    }
}