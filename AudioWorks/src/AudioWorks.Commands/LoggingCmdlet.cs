#if !NETCOREAPP2_1
using System;
using System.IO;
#endif
using System.Management.Automation;
#if !NETCOREAPP2_1
using System.Reflection;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
#endif

namespace AudioWorks.Commands
{
    /// <summary>
    /// A <see cref="Cmdlet"/> that can process log messages.
    /// </summary>
    /// <seealso cref="Cmdlet"/>
    public abstract class LoggingCmdlet : Cmdlet
    {
        static LoggingCmdlet()
        {
#if !NETCOREAPP2_1
            // Workaround for binding issue under Windows PowerShell
            if (RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework",
                StringComparison.Ordinal))
                AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

#endif
            CmdletLoggerProvider.Instance.Enable();
        }

        /// <inheritdoc/>
        protected override void BeginProcessing()
        {
            Telemetry.TrackFirstLaunch();
        }

        private protected void ProcessLogMessages()
        {
            while (CmdletLoggerProvider.Instance.TryDequeueMessage(out var logMessage))
                switch (logMessage)
                {
                    case DebugRecord debugRecord:
                        WriteDebug(debugRecord.Message);
                        break;
                    case InformationRecord informationRecord:
                        WriteInformation(informationRecord);
                        break;
                    case WarningRecord warningRecord:
                        WriteWarning(warningRecord.Message);
                        break;
                }
        }
#if !NETCOREAPP2_1

        [CanBeNull]
        static Assembly AssemblyResolve(object sender, [NotNull] ResolveEventArgs args)
        {
            // Load the available version of Newtonsoft.Json, regardless of the requested version
            if (args.Name.StartsWith("Newtonsoft.Json", StringComparison.Ordinal))
            {
                var jsonAssembly = Path.Combine(
                    // ReSharper disable once AssignNullToNotNullAttribute
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Newtonsoft.Json.dll");

                if (!File.Exists(jsonAssembly)) return null;

                // Make sure the assembly is really Newtonsoft.Json
                var assemblyName = AssemblyName.GetAssemblyName(jsonAssembly);
                if (assemblyName.Name.Equals("Newtonsoft.Json", StringComparison.Ordinal) &&
                    assemblyName.FullName.EndsWith("PublicKeyToken=30ad4fe6b2a6aeed", StringComparison.Ordinal))
                    return Assembly.LoadFrom(jsonAssembly);
            }

            return null;
        }
#endif
    }
}