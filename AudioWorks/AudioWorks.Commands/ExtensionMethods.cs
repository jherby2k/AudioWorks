using JetBrains.Annotations;
using Microsoft.PowerShell.Commands;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    static class ExtensionMethods
    {
        [Pure, NotNull]
        internal static IEnumerable<string> GetFileSystemPaths(this PSCmdlet cmdlet,
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
                var providerPath = cmdlet.SessionState.Path.GetUnresolvedProviderPathFromPSPath(literalPath, out provider, out PSDriveInfo _);
                if (provider.ImplementingType == typeof(FileSystemProvider))
                    return new[] { providerPath };
            }

            return Array.Empty<string>();
        }
    }
}
