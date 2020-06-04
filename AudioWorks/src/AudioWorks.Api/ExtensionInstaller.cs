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

using AudioWorks.Common;
using AudioWorks.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Protocol.Core.Types;
using NuGet.Resolver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AudioWorks.Api
{
    static class ExtensionInstaller
    {
        static readonly NuGetFramework _framework = NuGetFramework.ParseFolder(RuntimeChecker.GetShortFolderName());

        static readonly string _extensionRoot = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AudioWorks",
            "Extensions",
            _framework.GetShortFolderName());

        static readonly SourceRepository _customRepository = new SourceRepository(
            new PackageSource(
                ConfigurationManager.Configuration.GetValue(
                    "ExtensionRepository",
                    "https://www.myget.org/F/audioworks-extensions-v5/api/v3/index.json")),
            Repository.Provider.GetCoreV3());

        static readonly SourceRepository _defaultRepository = new SourceRepository(
            new PackageSource(
                ConfigurationManager.Configuration.GetValue(
                    "DefaultRepository",
                    "https://api.nuget.org/v3/index.json")),
            Repository.Provider.GetCoreV3());

        static readonly List<string> _fileTypesToInstall = new List<string>(new[]
        {
            ".dll",
            ".dylib",
            ".pdb",
            ".so"
        });

        static readonly string[] _rootAssemblyNames = GetRootAssemblyNames().ToArray();

        internal static async Task TryDownloadAsync()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger(typeof(ExtensionInstaller).FullName);

            if (!ConfigurationManager.Configuration.GetValue("AutomaticExtensionDownloads", true))
                logger.LogInformation("Automatic extension downloads are disabled.");
            else
            {
                logger.LogInformation("Beginning automatic extension updates.");

                try
                {
                    await DownloadAsync(logger).ConfigureAwait(false);
                }
                catch (AggregateException e)
                {
                    // Log any connection errors and otherwise fail silently
                    if (e.InnerException is FatalProtocolException)
                        logger.LogError(e.InnerException.Message);
                    else
                        throw;
                }
            }
        }

        static async Task DownloadAsync(ILogger logger)
        {
            Directory.CreateDirectory(_extensionRoot);

            using (var tokenSource = new CancellationTokenSource(
                ConfigurationManager.Configuration.GetValue("AutomaticExtensionDownloadTimeout", 30) * 1000))
            {
                // Hack - do this on the thread pool to avoid deadlocks
                var publishedPackages = await Task.Run(() =>
                    // ReSharper disable once AccessToDisposedClosure
                    GetPublishedPackagesAsync(logger, tokenSource.Token), tokenSource.Token).ConfigureAwait(false);

                var packagesInstalled = false;
                foreach (var packageMetadata in publishedPackages)
                    if (await InstallPackageAsync(packageMetadata, logger, tokenSource.Token).ConfigureAwait(false))
                        packagesInstalled = true;

                // Remove any extensions that aren't published
                if (Directory.Exists(_extensionRoot))
                    foreach (var installedExtension in new DirectoryInfo(_extensionRoot).GetDirectories()
                        .Select(dir => dir.Name)
                        .Except(publishedPackages.Select(package => package.Identity.ToString()),
                            StringComparer.OrdinalIgnoreCase))
                    {
                        Directory.Delete(Path.Combine(_extensionRoot, installedExtension), true);

                        logger.LogDebug("Deleted unlisted or obsolete extension in '{0}'.", installedExtension);
                    }

                logger.LogInformation(!packagesInstalled
                    ? "Extensions are already up to date."
                    : "Extensions successfully updated.");
            }
        }

        static async Task<IPackageSearchMetadata[]> GetPublishedPackagesAsync(
            ILogger logger,
            CancellationToken cancellationToken)
        {
            var result = (await _customRepository.GetResource<PackageSearchResource>(cancellationToken)
                    .SearchAsync("AudioWorks.Extensions",
                        new SearchFilter(ConfigurationManager.Configuration.GetValue<bool>("UsePreReleaseExtensions")),
                        0, 100, NullLogger.Instance, cancellationToken).ConfigureAwait(false))
#if NETSTANDARD2_0
                .Where(package => package.Tags.Contains(GetOSTag()))
#else
                .Where(package => package.Tags.Contains(GetOSTag(), StringComparison.OrdinalIgnoreCase))
#endif
                .ToArray();

            logger.LogDebug("Discovered {0} extension packages published at '{1}'.",
                result.Length, _customRepository.PackageSource.SourceUri);

            return result;
        }

        static string GetOSTag() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? "Windows"
            : RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? "Linux"
                : "MacOS";

        static async Task<bool> InstallPackageAsync(
            IPackageSearchMetadata packageMetadata,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            var extensionDir =
                new DirectoryInfo(Path.Combine(_extensionRoot, packageMetadata.Identity.ToString()));
            if (extensionDir.Exists)
            {
                logger.LogDebug("'{0}' version {1} is already installed. Skipping.",
                    packageMetadata.Identity.Id, packageMetadata.Identity.Version.ToString());

                return false;
            }

            logger.LogInformation("Installing '{0}' version {1}.",
                packageMetadata.Identity.Id, packageMetadata.Identity.Version.ToString());

            using (var cacheContext = new SourceCacheContext())
            {
                var packagesToInstall =
                    await ResolvePackagesAsync(packageMetadata.Identity, cacheContext, cancellationToken)
                        .ConfigureAwait(false);

                var settings = Settings.LoadDefaultSettings(null);
                var frameworkReducer = new FrameworkReducer();

                // Download and install the package and its dependencies
                foreach (var packageToInstall in packagesToInstall)
                {
                    var downloadResource = await packageToInstall.Source
                        .GetResourceAsync<DownloadResource>(cancellationToken)
                        .ConfigureAwait(false);
                    var downloadResult = await downloadResource.GetDownloadResourceResultAsync(
                            packageToInstall,
                            new PackageDownloadContext(cacheContext),
                            SettingsUtility.GetGlobalPackagesFolder(settings),
                            NullLogger.Instance,
                            cancellationToken)
                        .ConfigureAwait(false);

                    var libGroups = downloadResult.PackageReader.GetLibItems().ToArray();
                    var nearestLibFramework =
                        frameworkReducer.GetNearest(_framework, libGroups.Select(l => l.TargetFramework));

                    // Copy the relevant libraries directly from the NuGet cache
                    foreach (var item in libGroups
                        .First(l => l.TargetFramework.Equals(nearestLibFramework)).Items)
                        CopyLibFiles(
                            Path.Combine(Path.GetDirectoryName(((FileStream) downloadResult.PackageStream).Name), item),
                            Path.Combine(extensionDir.FullName, Path.GetFileName(item)),
                            logger);

                    if (!packageToInstall.Id.StartsWith("AudioWorks.Extensions", StringComparison.OrdinalIgnoreCase))
                        continue;

                    // For AudioWorks extension packages only, copy the native library content files as well
                    var contentGroups = downloadResult.PackageReader.GetItems("contentFiles").ToArray();
                    if (contentGroups.Length <= 0) continue;

                    var nearestContentFramework =
                        frameworkReducer.GetNearest(_framework, contentGroups.Select(c => c.TargetFramework));

                    foreach (var item in contentGroups
                        .First(c => c.TargetFramework.Equals(nearestContentFramework)).Items)
                    {
                        var sourceFileName = Path.Combine(
                            Path.GetDirectoryName(((FileStream) downloadResult.PackageStream).Name), item);

                        CopyContentFiles(
                            sourceFileName,
                            Path.Combine(extensionDir.FullName, new DirectoryInfo(sourceFileName).Parent?.Name,
                            Path.GetFileName(item)),
                            logger);
                    }
                }
            }

            return true;
        }

        static async Task<IEnumerable<SourcePackageDependencyInfo>> ResolvePackagesAsync(
            PackageIdentity packageIdentity,
            SourceCacheContext cacheContext,
            CancellationToken cancellationToken)
        {
            // Recursively collect all dependencies
            var dependencies = new HashSet<SourcePackageDependencyInfo>(PackageIdentityComparer.Default);
            await CollectDependenciesAsync(packageIdentity, cacheContext, dependencies, cancellationToken)
                .ConfigureAwait(false);

            var resolverContext = new PackageResolverContext(
                DependencyBehavior.Lowest,
                new[] { packageIdentity.Id },
                Enumerable.Empty<string>(),
                Enumerable.Empty<PackageReference>(),
                Enumerable.Empty<PackageIdentity>(),
                dependencies,
                new[] { _customRepository.PackageSource, _defaultRepository.PackageSource },
                NullLogger.Instance);

            // Resolve the dependency graph
            var resolver = new PackageResolver();
            return resolver.Resolve(resolverContext, CancellationToken.None)
                .Select(p => dependencies.Single(x => PackageIdentityComparer.Default.Equals(x, p)));
        }

        static async Task CollectDependenciesAsync(
            PackageIdentity packageIdentity,
            SourceCacheContext cacheContext,
            ISet<SourcePackageDependencyInfo> dependencies,
            CancellationToken cancellationToken)
        {
            if (dependencies.Contains(packageIdentity)) return;

            foreach (var repository in new[] { _customRepository, _defaultRepository })
            {
                var dependencyInfoResource = await repository.GetResourceAsync<DependencyInfoResource>(cancellationToken)
                    .ConfigureAwait(false);
                var dependencyInfo = await dependencyInfoResource.ResolvePackage(
                        packageIdentity,
                        _framework,
                        cacheContext,
                        NullLogger.Instance,
                        cancellationToken)
                    .ConfigureAwait(false);

                if (dependencyInfo == null) continue;

                dependencies.Add(dependencyInfo);

                foreach (var dependency in dependencyInfo.Dependencies)
                    await CollectDependenciesAsync(
                            new PackageIdentity(dependency.Id, dependency.VersionRange.MinVersion),
                            cacheContext,
                            dependencies,
                            cancellationToken)
                        .ConfigureAwait(false);
            }
        }

        static void CopyLibFiles(string source, string destination, ILogger logger)
        {
            var extension = Path.GetExtension(source);

            if (!_fileTypesToInstall.Contains(extension, StringComparer.OrdinalIgnoreCase))
                return;

            // Skip any 3rd party symbols
            if (extension.Equals(".pdb", StringComparison.OrdinalIgnoreCase) &&
                !Path.GetFileName(source).StartsWith("AudioWorks", StringComparison.OrdinalIgnoreCase))
                return;

            try
            {
                // Skip any assemblies already used by AudioWorks
                if (extension.Equals(".dll", StringComparison.OrdinalIgnoreCase) &&
                    _rootAssemblyNames.Contains(
                        AssemblyName.GetAssemblyName(Path.GetFullPath(source)).Name, StringComparer.OrdinalIgnoreCase))
                    return;
            }
            catch (BadImageFormatException)
            {
                return;
            }

            if (File.Exists(destination)) return;
            logger.LogDebug("Copying '{0}' to '{1}'.", source, destination);
            Directory.CreateDirectory(Path.GetDirectoryName(destination));
            File.Copy(source, destination);
        }

        static void CopyContentFiles(string source, string destination, ILogger logger)
        {
            var extension = Path.GetExtension(source);

            if (!_fileTypesToInstall.Contains(extension, StringComparer.OrdinalIgnoreCase))
                return;

            if (File.Exists(destination)) return;
            logger.LogDebug("Copying '{0}' to '{1}'.", source, destination);
            Directory.CreateDirectory(Path.GetDirectoryName(destination));
            File.Copy(source, destination);
        }

        static IEnumerable<string> GetRootAssemblyNames() =>
            new DirectoryInfo(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath))
                .EnumerateFiles("*.dll")
                .Select(file =>
                {
                    try
                    {
                        return AssemblyName.GetAssemblyName(file.FullName).Name;
                    }
                    catch (BadImageFormatException)
                    {
                        // The DLL might not be managed
                        return Path.GetFileNameWithoutExtension(file.Name);
                    }
                });
    }
}