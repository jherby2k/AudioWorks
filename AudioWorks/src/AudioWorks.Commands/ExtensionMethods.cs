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

            // ReSharper disable once InvertIf
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
