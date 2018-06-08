using System;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using NuGet.Configuration;
using NuGet.PackageManagement;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Resolver;

namespace AudioWorks.Extensions
{
    abstract class ExtensionContainerBase
    {
        const string _projectRoot = "C:\\Project";
        const string _customUrl = "https://www.myget.org/F/audioworks-extensions/api/v3/index.json";
        const string _defaultUrl = "https://api.nuget.org/v3/index.json";

        [NotNull]
        protected static CompositionHost CompositionHost { get; }

        static ExtensionContainerBase()
        {
            UpdateExtensions();

            CompositionHost = new ContainerConfiguration().WithAssemblies(
                    new DirectoryInfo(_projectRoot).GetDirectories()
                        .SelectMany(extensionDir => extensionDir.GetFiles("AudioWorks.Extensions.*.dll"))
                        .Select(fileInfo => new ExtensionAssemblyResolver(fileInfo.FullName).Assembly))
                .CreateContainer();
        }

        static void UpdateExtensions()
        {
            var customRepository = new SourceRepository(new PackageSource(_customUrl), Repository.Provider.GetCoreV3());
            var defaultRepository = new SourceRepository(new PackageSource(_defaultUrl), Repository.Provider.GetCoreV3());

            var settings = Settings.LoadDefaultSettings(_projectRoot);
            var packageManager = new NuGetPackageManager(
                new SourceRepositoryProvider(settings, Repository.Provider.GetCoreV3()),
                settings,
                _projectRoot);

            var packageSearchResource = customRepository.GetResourceAsync<PackageSearchResource>().Result;
            var publishedPackages = packageSearchResource.SearchAsync("AudioWorks.Extensions",
                new SearchFilter(true), 0, 100, new NugetLogger(), CancellationToken.None).Result.ToArray();

            foreach (var publishedPackage in publishedPackages)
            {
                var extensionDir = new DirectoryInfo(Path.Combine(_projectRoot, publishedPackage.Identity.ToString()));
                if (extensionDir.Exists) continue;

                extensionDir.Create();
                var stagingRootDir = extensionDir.CreateSubdirectory("Staging");

                var project = new ExtensionNuGetProject(stagingRootDir.FullName);

                packageManager.InstallPackageAsync(
                    project,
                    publishedPackage.Identity,
                    new ResolutionContext(DependencyBehavior.Lowest, true, false, VersionConstraints.None),
                    new ExtensionProjectContext(new NugetLogger()),
                    customRepository,
                    new[] { defaultRepository },
                    CancellationToken.None).Wait();

                // Copy newly installed packages into the extension folder
                foreach (var installedPackage in project.InstalledPackages)
                {
                    var packageDir = new DirectoryInfo(project.GetInstalledPath(installedPackage));

                    foreach (var subDir in packageDir.GetDirectories())
                        switch (subDir.Name)
                        {
                            case "lib":
                                CopyDirectory(subDir
                                        .GetDirectories("netstandard*").OrderByDescending(dir => dir.Name)
                                        .FirstOrDefault(),
                                    extensionDir);
                                break;
                            case "contentFiles":
                                CopyDirectory(subDir
                                        .GetDirectories("any").FirstOrDefault()?
                                        .GetDirectories("netstandard*").OrderByDescending(dir => dir.Name)
                                        .FirstOrDefault(),
                                    extensionDir);
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
                Directory.Delete(Path.Combine(_projectRoot, obsoleteExtension), true);
        }

        static void CopyDirectory([CanBeNull] DirectoryInfo source, [NotNull] DirectoryInfo destination)
        {
            if (source == null || !source.Exists) return;

            foreach (var file in source.GetFiles()
                .Where(file => file.Extension.Equals(".dll", StringComparison.OrdinalIgnoreCase)))
                file.CopyTo(Path.Combine(destination.FullName, file.Name));

            foreach (var subdir in source.GetDirectories())
                CopyDirectory(subdir, destination.CreateSubdirectory(subdir.Name));
        }
    }
}
