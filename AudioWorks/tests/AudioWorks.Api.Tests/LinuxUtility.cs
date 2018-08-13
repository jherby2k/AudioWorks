#if LINUX
using System.Diagnostics;
using JetBrains.Annotations;

namespace AudioWorks.Api.Tests
{
    public static class LinuxUtility
    {
        [NotNull]
        public static string GetRelease()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo("lsb_release", "-d -s")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result.Trim();
        }
    }
}
#endif
