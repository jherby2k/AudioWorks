using System;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Threading;
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

namespace AudioWorks.Extensions
{
    abstract class ExtensionContainerBase
    {
        [NotNull] static readonly string _customUrl = ConfigurationManager.Configuration.GetValue("ExtensionRepository",
            "https://www.myget.org/F/audioworks-extensions/api/v3/index.json");

        [NotNull] static readonly string _defaultUrl = ConfigurationManager.Configuration.GetValue("DefaultRepository",
            "https://api.nuget.org/v3/index.json");

        [NotNull] static readonly string _projectRoot = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AudioWorks",
            "Extensions");

        [NotNull]
        protected static CompositionHost CompositionHost { get; }

        static ExtensionContainerBase()
        {
            if (ConfigurationManager.Configuration.GetValue("AutomaticExtensionDownloads", true))
                UpdateExtensions();

            CompositionHost = new ContainerConfiguration().WithAssemblies(
                    new DirectoryInfo(_projectRoot).GetDirectories()
                        .SelectMany(extensionDir => extensionDir.GetFiles("AudioWorks.Extensions.*.dll"))
                        .Select(fileInfo => new ExtensionAssemblyResolver(fileInfo.FullName).Assembly))
                .CreateContainer();
        }

        static void UpdateExtensions()
        {
            Directory.CreateDirectory(_projectRoot);

            var customRepository = new SourceRepository(new PackageSource(_customUrl), Repository.Provider.GetCoreV3());
            var defaultRepository =
                new SourceRepository(new PackageSource(_defaultUrl), Repository.Provider.GetCoreV3());

            var settings = Settings.LoadDefaultSettings(_projectRoot);
            var packageManager = new NuGetPackageManager(
                new SourceRepositoryProvider(settings, Repository.Provider.GetCoreV3()),
                settings,
                _projectRoot);

            var logger = LoggingManager.CreateLogger<ExtensionContainerBase>();

            try
            {
                var packageSearchResource =
                    customRepository.GetResourceAsync<PackageSearchResource>(CancellationToken.None).Result;
                var publishedPackages = packageSearchResource.SearchAsync("AudioWorks.Extensions",
                        new SearchFilter(true), 0, 100, NullLogger.Instance, CancellationToken.None)
                    .Result.ToArray();

                logger.LogInformation($"Discovered {0} packages published at '{1}'.",
                    publishedPackages.Length, _customUrl);

                foreach (var publishedPackage in publishedPackages)
                {
                    var extensionDir =
                        new DirectoryInfo(Path.Combine(_projectRoot, publishedPackage.Identity.ToString()));
                    if (extensionDir.Exists) continue;

                    extensionDir.Create();
                    var stagingRootDir = extensionDir.CreateSubdirectory("Staging");

                    var project = new ExtensionNuGetProject(stagingRootDir.FullName);

                    packageManager.InstallPackageAsync(
                        project,
                        publishedPackage.Identity,
                        new ResolutionContext(DependencyBehavior.Lowest, true, false, VersionConstraints.None),
                        new ExtensionProjectContext(),
                        customRepository,
                        new[] { defaultRepository },
                        CancellationToken.None).Wait();

                    // Move newly installed packages into the extension folder
                    foreach (var installedPackage in project.GetInstalledPackagesAsync(CancellationToken.None).Result)
                    {
                        var packageDir = new DirectoryInfo(project.GetInstalledPath(installedPackage.PackageIdentity));

                        foreach (var subDir in packageDir.GetDirectories())
                            switch (subDir.Name)
                            {
                                case "lib":
                                    MoveContents(subDir
                                            .GetDirectories("netstandard*").OrderByDescending(dir => dir.Name)
                                            .FirstOrDefault(),
                                        extensionDir, logger);
                                    break;
                                case "contentFiles":
                                    MoveContents(subDir
                                            .GetDirectories("any").FirstOrDefault()?
                                            .GetDirectories("netstandard*").OrderByDescending(dir => dir.Name)
                                            .FirstOrDefault(),
                                        extensionDir, logger);
                                    break;
                            }
                    }

                    stagingRootDir.Delete(true);
                }

                // Remove any extensions that aren't published
                foreach (var obsoleteExtension in new DirectoryInfo(_projectRoot).GetDirectories()
                    .Select(dir => dir.Name)
                    .Except(publishedPackages.Select(package => package.Identity.ToString()),
                        StringComparer.OrdinalIgnoreCase))
                {
                    Directory.Delete(Path.Combine(_projectRoot, obsoleteExtension), true);

                    logger.LogInformation("Deleted unlisted extension in '{0}'.",
                        obsoleteExtension);
                }

            }
            catch (Exception e)
            {
                if (e is AggregateException aggregate)
                    foreach (var inner in aggregate.InnerExceptions)
                        logger.LogError(inner, e.Message);
                else
                    logger.LogError(e, e.Message);
            }
        }

        static void MoveContents([CanBeNull] DirectoryInfo source, [NotNull] DirectoryInfo destination, ILogger logger)
        {
            if (source == null || !source.Exists) return;

            foreach (var file in source.GetFiles()
                .Where(file => file.Extension.Equals(".dll", StringComparison.OrdinalIgnoreCase)))
            {
                logger.LogInformation("Moving '{0}' to '{1}'.",
                    file.FullName, destination.FullName);

                file.MoveTo(Path.Combine(destination.FullName, file.Name));
            }

            foreach (var subdir in source.GetDirectories())
                MoveContents(subdir, destination.CreateSubdirectory(subdir.Name), logger);

            source.Delete(true);
        }
    }
}
