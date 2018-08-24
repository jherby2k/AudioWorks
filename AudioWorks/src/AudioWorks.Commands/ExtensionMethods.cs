using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.PowerShell.Commands;

namespace AudioWorks.Commands
{
    static class ExtensionMethods
    {
        [Pure, NotNull]
        internal static IEnumerable<string> GetFileSystemPaths(
            [NotNull] this PSCmdlet cmdlet,
            [CanBeNull] string path,
            [CanBeNull] string literalPath)
        {
            ProviderInfo provider;
            if (!string.IsNullOrEmpty(path))
            {
                var providerPaths = cmdlet.GetResolvedProviderPathFromPSPath(path, out provider);
                if (provider.ImplementingType == typeof(FileSystemProvider))
                    return providerPaths;
            }

            if (!string.IsNullOrEmpty(literalPath))
            {
                var providerPath = cmdlet.SessionState.Path.GetUnresolvedProviderPathFromPSPath(literalPath, out provider, out _);
                if (provider.ImplementingType == typeof(FileSystemProvider))
                    return new[] { providerPath };
            }

            return Array.Empty<string>();
        }

        internal static void OutputMessages(
            [NotNull] this Cmdlet cmdlet,
            [NotNull] BlockingCollection<object> messageQueue,
            CancellationToken cancellationToken)
        {
            foreach (var message in messageQueue.GetConsumingEnumerable(cancellationToken))
                switch (message)
                {
                    case ProgressRecord progressRecord:
                        cmdlet.WriteProgress(progressRecord);
                        break;
                    case DebugRecord debugRecord:
                        cmdlet.WriteDebug(debugRecord.Message);
                        break;
                    case InformationRecord informationRecord:
                        cmdlet.WriteInformation(informationRecord);
                        break;
                    case WarningRecord warningRecord:
                        cmdlet.WriteWarning(warningRecord.Message);
                        break;
                }
        }
    }
}
