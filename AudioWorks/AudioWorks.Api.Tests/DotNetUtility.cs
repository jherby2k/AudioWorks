using JetBrains.Annotations;
using System.Diagnostics;

namespace AudioWorks.Api.Tests
{
    static class DotNetUtility
    {
        internal static void Publish([NotNull] string projectDir, [NotNull] string configuration, [NotNull] string outputDir)
        {
            using (var publish = new Process())
            {
                publish.StartInfo.FileName = "dotnet";
                publish.StartInfo.Arguments = $"publish -c {configuration} -o \"{outputDir}\"";
                publish.StartInfo.WorkingDirectory = projectDir;
                publish.Start();
                publish.WaitForExit(15000);
            }
        }
    }
}
