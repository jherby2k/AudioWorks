using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if !NETCOREAPP2_1
using System.Reflection;
#endif
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using AudioWorks.Common;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.PackageManagement;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Resolver;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AudioWorks.Api
{
    static class ExtensionInstaller
    {
        [NotNull] static readonly string _projectRoot = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AudioWorks",
            "Extensions",
#if NETCOREAPP2_1
            "netcoreapp2.1"
#else
            "netstandard2.0"
#endif
            );

        [NotNull] static readonly string _customUrl = ConfigurationManager.Configuration.GetValue("ExtensionRepository",
            "https://www.myget.org/F/audioworks-extensions/api/v3/index.json");

        [NotNull] static readonly string _defaultUrl = ConfigurationManager.Configuration.GetValue("DefaultRepository",
            "https://api.nuget.org/v3/index.json");

        [NotNull] static readonly List<string> _compatibleTargets = new List<string>(new[]
        {
#if NETCOREAPP2_1
            "netcoreapp2.1",
            "netcoreapp2.0",
            "netcoreapp1.1",
            "netcoreapp1.0",
#endif
            "netstandard2.0",
            "netstandard1.6",
            "netstandard1.5",
            "netstandard1.4",
            "netstandard1.3",
            "netstandard1.2",
            "netstandard1.1",
            "netstandard1.0"
        });

        internal static void Download()
        {
#if !NETCOREAPP2_1
            if (RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework",
                StringComparison.Ordinal))
                ApplyRedirects();
#endif

            var logger = LoggingManager.LoggerFactory.CreateLogger(typeof(ExtensionInstaller).FullName);

            if (!ConfigurationManager.Configuration.GetValue("AutomaticExtensionDownloads", true))
                logger.LogInformation("Automatic extension downloads are disabled.");
            else
            {
                logger.LogInformation("Beginning automatic extension updates.");

                Directory.CreateDirectory(_projectRoot);

                var customRepository =
                    new SourceRepository(new PackageSource(_customUrl), Repository.Provider.GetCoreV3());
                var defaultRepository =
                    new SourceRepository(new PackageSource(_defaultUrl), Repository.Provider.GetCoreV3());

                var settings = Settings.LoadDefaultSettings(_projectRoot);
                var packageManager = new NuGetPackageManager(
                    new SourceRepositoryProvider(settings, Repository.Provider.GetCoreV3()), settings, _projectRoot);

                try
                {
                    // Search on the thread pool to avoid deadlocks
                    // ReSharper disable once ImplicitlyCapturedClosure
                    var publishedPackages = Task.Run(async () =>
                        {
                            var cancellationTokenSource = GetCancellationTokenSource();
                            return await (await customRepository
                                    .GetResourceAsync<PackageSearchResource>(cancellationTokenSource.Token)
                                    .ConfigureAwait(false))
                                .SearchAsync("AudioWorks.Extensions", new SearchFilter(
                                        ConfigurationManager.Configuration.GetValue("IncludePreReleaseExtensions",
                                            false)), 0, 100, NullLogger.Instance,
                                    cancellationTokenSource.Token)
                                .ConfigureAwait(false);
                        }).Result
#if NETCOREAPP2_1
                        .Where(package => package.Tags.Contains(GetOSTag(), StringComparison.OrdinalIgnoreCase))
#else
                        .Where(package => package.Tags.Contains(GetOSTag()))
#endif
                        .ToArray();

                    logger.LogDebug("Discovered {0} packages published at '{1}'.",
                        publishedPackages.Length, _customUrl);

                    foreach (var publishedPackage in publishedPackages)
                    {
                        var extensionDir =
                            new DirectoryInfo(Path.Combine(_projectRoot, publishedPackage.Identity.ToString()));
                        if (extensionDir.Exists)
                        {
                            logger.LogDebug("Package '{0}' is already installed.",
                                publishedPackage.Identity.ToString());

                            continue;
                        }

                        logger.LogInformation("Installing package '{0}'.",
                            publishedPackage.Identity.ToString());

                        extensionDir.Create();
                        var stagingDir = extensionDir.CreateSubdirectory("Staging");

                        var project = new ExtensionNuGetProject(stagingDir.FullName);

                        try
                        {
                            // Download on the thread pool to avoid deadlocks
                            Task.Run(async () =>
                            {
                                using (var cancellationTokenSource = GetCancellationTokenSource())
                                    await packageManager.InstallPackageAsync(
                                            project,
                                            publishedPackage.Identity,
                                            new ResolutionContext(DependencyBehavior.Lowest, true, false,
                                                VersionConstraints.None),
                                            new ExtensionProjectContext(),
                                            customRepository,
                                            new[] { defaultRepository },
                                            cancellationTokenSource.Token)
                                        .ConfigureAwait(false);
                            }).Wait();

                            // Move newly installed packages into the extension folder
                            foreach (var installedPackage in project
                                .GetInstalledPackagesAsync(CancellationToken.None)
                                .Result)
                            {
                                var packageDir = new DirectoryInfo(
                                    project.GetInstalledPath(installedPackage.PackageIdentity));

                                foreach (var subDir in packageDir.GetDirectories())
                                    // ReSharper disable once SwitchStatementMissingSomeCases
                                    switch (subDir.Name)
                                    {
                                        case "lib":
                                            MoveContents(
                                                SelectDirectory(subDir.GetDirectories()),
                                                extensionDir,
                                                logger);
                                            break;

                                        case "contentFiles":
                                            MoveContents(
                                                SelectDirectory(subDir.GetDirectories("any").FirstOrDefault()
                                                    ?.GetDirectories()),
                                                extensionDir,
                                                logger);
                                            break;
                                    }
                            }
                        }
                        finally
                        {
                            stagingDir.Delete(true);

                            // If the download was cancelled, clean up an empty extension directory
                            if (!extensionDir.EnumerateFileSystemInfos().Any())
                                extensionDir.Delete();
                        }
                    }

                    // Remove any extensions that aren't published
                    foreach (var obsoleteExtension in new DirectoryInfo(_projectRoot).GetDirectories()
                        .Select(dir => dir.Name)
                        .Except(publishedPackages.Select(package => package.Identity.ToString()),
                            StringComparer.OrdinalIgnoreCase))
                    {
                        Directory.Delete(Path.Combine(_projectRoot, obsoleteExtension), true);

                        logger.LogDebug("Deleted unlisted or obsolete extension in '{0}'.",
                            obsoleteExtension);
                    }

                    logger.LogInformation("Completed automatic extension updates.");
                }
                catch (Exception e)
                {
                    // Timeout on search throws a TaskCanceled inside an Aggregate
                    // Timeout on install throws an OperationCanceled inside an InvalidOperation inside an Aggregate
                    if (e is AggregateException aggregate)
                        foreach (var inner in aggregate.InnerExceptions)
                            if (inner is OperationCanceledException ||
                                inner is InvalidOperationException invalidInner &&
                                invalidInner.InnerException is OperationCanceledException)
                                logger.LogWarning("The configured timeout was exceeded.");
                            else
                                logger.LogError(inner, e.Message);
                    else
                        logger.LogError(e, e.Message);
                }
            }
        }

#if !NETCOREAPP2_1
        static void ApplyRedirects()
        {
            // Workaround for binding issue under Windows PowerShell
            AppDomain.CurrentDomain.AssemblyResolve += (context, args) =>
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
            };
        }
#endif

        [Pure, NotNull]
        static string GetOSTag() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? "Windows"
            : (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? "Linux"
                : "OSX");

        [Pure, NotNull]
        static CancellationTokenSource GetCancellationTokenSource() => new CancellationTokenSource(
            ConfigurationManager.Configuration.GetValue("AutomaticExtensionDownloadTimeout", 30) *
            1000);

        [CanBeNull]
        static DirectoryInfo SelectDirectory([CanBeNull, ItemNotNull] IEnumerable<DirectoryInfo> directories)
        {
            // Select the first directory in the list of compatible TFMs
            return directories?
                .Where(dir => _compatibleTargets.Contains(dir.Name, StringComparer.OrdinalIgnoreCase))
                .OrderBy(dir => _compatibleTargets
                    .FindIndex(target => target.Equals(dir.Name, StringComparison.OrdinalIgnoreCase)))
                .FirstOrDefault();
        }

        static void MoveContents(
            [CanBeNull] DirectoryInfo source,
            [NotNull] DirectoryInfo destination,
            [NotNull] ILogger logger)
        {
            if (source == null || !source.Exists) return;

            foreach (var file in source.GetFiles()
                .Where(file => file.Extension.Equals(".dll", StringComparison.OrdinalIgnoreCase)))
            {
                logger.LogDebug("Moving '{0}' to '{1}'.",
                    file.FullName, destination.FullName);

                file.MoveTo(Path.Combine(destination.FullName, file.Name));
            }

            foreach (var subDir in source.GetDirectories())
                MoveContents(subDir, destination.CreateSubdirectory(subDir.Name), logger);
        }
    }
}