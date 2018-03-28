using System;
using System.IO;
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
            state.ImportPSModule(new[]
            {
                Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName, $"{_moduleProject}.dll")
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