/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

#if NETSTANDARD2_0
using System;
using System.IO;
#endif
using System.Management.Automation;
#if NETSTANDARD2_0
using System.Reflection;
using System.Runtime.InteropServices;
#endif
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    public abstract class LoggingCmdlet : Cmdlet
    {
        [NotNull]
        private protected CmdletLoggerProvider LoggerProvider { get; } =
            LoggerManager.AddSingletonProvider(() => new CmdletLoggerProvider());

#if NETSTANDARD2_0
        static LoggingCmdlet()
        {
            // Workaround for binding issue under Windows PowerShell
            if (RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework",
                StringComparison.Ordinal))
                AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;
        }

#endif
        protected override void BeginProcessing() => Telemetry.TrackFirstLaunch();

        private protected void ProcessLogMessages()
        {
            while (LoggerProvider.TryDequeueMessage(out var logMessage))
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
#if NETSTANDARD2_0

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
                    // ReSharper disable once StringLiteralTypo
                    assemblyName.FullName.EndsWith("PublicKeyToken=30ad4fe6b2a6aeed", StringComparison.Ordinal))
                    return Assembly.LoadFrom(jsonAssembly);
            }

            return null;
        }
#endif
    }
}