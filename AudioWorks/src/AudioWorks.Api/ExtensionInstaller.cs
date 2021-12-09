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
    /// <summary>
    /// Responsible for downloading and installing extensions.
    /// </summary>
    public static class ExtensionInstaller
    {
        static readonly NuGetFramework _framework = NuGetFramework.ParseFolder(RuntimeChecker.GetShortFolderName());

        static readonly SourceRepository _customRepository = new(new(
                ConfigurationManager.Configuration.GetValue(
                    "ExtensionRepository",
                    "https://www.myget.org/F/audioworks-extensions-v5/api/v3/index.json")),
            Repository.Provider.GetCoreV3());

        static readonly SourceRepository _defaultRepository = new(new(
                ConfigurationManager.Configuration.GetValue(
                    "DefaultRepository",
                    "https://api.nuget.org/v3/index.json")),
            Repository.Provider.GetCoreV3());

        static readonly List<string> _fileTypesToInstall = new(new[]
        {
            ".dll",
            ".dylib",
            ".so"
        });

        static readonly string[] _rootAssemblyNames = GetRootAssemblyNames().ToArray();

        /// <summary>
        /// Gets the framework-specific extension root directory.
        /// </summary>
        /// <value>The extension root.</value>
        public static string ExtensionRoot { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AudioWorks",
            "Extensions",
            _framework.GetShortFolderName());

        /// <summary>
        /// Gets a value indicating whether extensions have already been loaded for this session and can no longer be installed.
        /// </summary>
        /// <value><c>true</c> if extension loading is already complete; otherwise, <c>false</c>.</value>
        public static bool LoadComplete { get; private set; }

        internal static void SetLoadComplete() => LoadComplete = true;

        /// <summary>
        /// Downloads and installs all available extensions.
        /// </summary>
        public static async Task InstallAsync()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger(typeof(ExtensionInstaller).FullName);

            if (LoadComplete)
            {
                logger.LogWarning("Extensions have already been loaded for this session.");
                return;
            }

            try
            {
                await DownloadAsync(logger).ConfigureAwait(false);
            }
            catch (AggregateException e)
            {
                // Log any connection errors and fail silently
                if (e.InnerException is FatalProtocolException)
                    logger.LogError(e.InnerException, e.InnerException.Message);
                else
                    throw;
            }
        }

        static async Task DownloadAsync(ILogger logger)
        {
            Directory.CreateDirectory(ExtensionRoot);

            try
            {
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
                    if (Directory.Exists(ExtensionRoot))
                        foreach (var installedExtension in new DirectoryInfo(ExtensionRoot).GetDirectories()
                            .Select(dir => dir.Name)
                            .Except(publishedPackages.Select(package => package.Identity.ToString()),
                                StringComparer.OrdinalIgnoreCase))
                        {
                            Directory.Delete(Path.Combine(ExtensionRoot, installedExtension), true);

                            logger.LogDebug("Deleted unlisted or obsolete extension in '{path}'.", installedExtension);
                        }

                    logger.LogInformation(!packagesInstalled
                        ? "Extensions are already up to date."
                        : "Extensions successfully updated.");
                }
            }
            catch (FatalProtocolException e)
            {
                if (e.InnerException is OperationCanceledException)
                    logger.LogWarning("Timed out enumerating the published extensions.");
                else
                    throw;
            }
            catch (OperationCanceledException e)
            {
                logger.LogWarning(e, e.Message);
            }
        }

        static async Task<IPackageSearchMetadata[]> GetPublishedPackagesAsync(
            ILogger logger,
            CancellationToken cancellationToken)
        {
            var result = (await _customRepository.GetResource<PackageSearchResource>(cancellationToken)
                    .SearchAsync("AudioWorks.Extensions",
                        new(ConfigurationManager.Configuration.GetValue<bool>("UsePreReleaseExtensions")),
                        0, 100, NullLogger.Instance, cancellationToken).ConfigureAwait(false))
#if NETSTANDARD2_0
                .Where(package => package.Tags.Contains(GetOSTag()))
#else
                .Where(package => package.Tags.Contains(GetOSTag(), StringComparison.OrdinalIgnoreCase))
#endif
                .ToArray();

            logger.LogDebug("Discovered {count} extension packages published at '{source}'.",
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
                new DirectoryInfo(Path.Combine(ExtensionRoot, packageMetadata.Identity.ToString()));
            if (extensionDir.Exists)
            {
                logger.LogDebug("'{id}' version {version} is already installed. Skipping.",
                    packageMetadata.Identity.Id, packageMetadata.Identity.Version.ToString());

                return false;
            }

            logger.LogInformation("Installing '{id}' version {version}.",
                packageMetadata.Identity.Id, packageMetadata.Identity.Version.ToString());

            try
            {
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
                                new(cacheContext),
                                SettingsUtility.GetGlobalPackagesFolder(settings),
                                NullLogger.Instance,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (downloadResult.Status != DownloadResourceResultStatus.Available ||
                            downloadResult.PackageReader == null ||
                            downloadResult.PackageStream is not FileStream packageStream)
                            continue;

                        var libGroups = (await downloadResult.PackageReader.GetLibItemsAsync(cancellationToken)
                            .ConfigureAwait(false)).ToArray();
                        var nearestLibFramework =
                            frameworkReducer.GetNearest(_framework, libGroups.Select(l => l.TargetFramework));

                        // Copy the relevant libraries directly from the NuGet cache
                        foreach (var item in libGroups
                            .First(l => l.TargetFramework.Equals(nearestLibFramework)).Items)
                            CopyLibFiles(
                                Path.Combine(Path.GetDirectoryName(packageStream.Name)!,
                                    item),
                                Path.Combine(extensionDir.FullName, Path.GetFileName(item)),
                                logger);

                        if (!packageToInstall.Id.StartsWith("AudioWorks.Extensions",
                            StringComparison.OrdinalIgnoreCase))
                            continue;

                        // For AudioWorks extension packages only, copy the native library content files as well
                        var contentGroups = (await downloadResult.PackageReader
                            .GetItemsAsync("contentFiles", cancellationToken).ConfigureAwait(false)).ToArray();
                        if (contentGroups.Length <= 0) continue;

                        var nearestContentFramework =
                            frameworkReducer.GetNearest(_framework, contentGroups.Select(c => c.TargetFramework));

                        foreach (var item in contentGroups
                            .First(c => c.TargetFramework.Equals(nearestContentFramework)).Items)
                        {
                            var sourceFileName = Path.Combine(
                                Path.GetDirectoryName(packageStream.Name)!, item);

                            CopyContentFiles(
                                sourceFileName,
                                Path.Combine(extensionDir.FullName, new DirectoryInfo(sourceFileName).Parent!.Name,
                                    Path.GetFileName(item)),
                                logger);
                        }
                    }
                }

                cancellationToken.ThrowIfCancellationRequested();
            }
            catch (Exception)
            {
                // ReSharper disable once InvertIf
                if (Directory.Exists(extensionDir.FullName))
                {
                    logger.LogDebug("Deleting partially-installed extension '{name}'.", extensionDir.Name);
                    extensionDir.Delete(true);
                }

                throw;
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
                            new(dependency.Id, dependency.VersionRange.MinVersion),
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
            logger.LogDebug("Copying '{source}' to '{destination}'.", source, destination);
            Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
            File.Copy(source, destination);
        }

        static void CopyContentFiles(string source, string destination, ILogger logger)
        {
            var extension = Path.GetExtension(source);

            if (!_fileTypesToInstall.Contains(extension, StringComparer.OrdinalIgnoreCase))
                return;

            if (File.Exists(destination)) return;
            logger.LogDebug("Copying '{source}' to '{destination}'.", source, destination);
            Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
            File.Copy(source, destination);
        }

        static IEnumerable<string> GetRootAssemblyNames() =>
            new DirectoryInfo(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!)
                .EnumerateFiles("*.dll")
                .Select(file =>
                {
                    try
                    {
                        return AssemblyName.GetAssemblyName(file.FullName).Name ?? string.Empty;
                    }
                    catch (BadImageFormatException)
                    {
                        // The DLL might not be managed
                        return Path.GetFileNameWithoutExtension(file.Name);
                    }
                });
    }
}