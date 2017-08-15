using JetBrains.Annotations;
using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace AudioWorks.Commands.Tests
{
    [UsedImplicitly]
    public class PowerShellFixture : IDisposable
    {
        [NotNull]
        internal Runspace Runspace { get; } = RunspaceFactory.CreateRunspace();

        public PowerShellFixture()
        {
            var workingDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            foreach (var filePath in Directory.GetFiles(Path.Combine(
                workingDir.Parent.Parent.Parent.Parent.FullName,
                "AudioWorks.Commands",
                "bin",
                workingDir.Parent.Name,
                workingDir.Name)))
            {
                try
                {
                    var fileName = Path.GetFileName(filePath);
                    if (fileName == null) continue;
                    File.Copy(filePath, fileName, true);
                }
                catch (IOException)
                {
                    //HACK This file gets locked sometimes by the test runner
                }
            }

            Runspace.Open();
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = Runspace;
                ps.AddCommand("Import-Module").AddArgument(@".\AudioWorks.Commands.dll");
                ps.Invoke();
            }
        }

        public void Dispose()
        {
            Runspace.Close();
        }
    }
}