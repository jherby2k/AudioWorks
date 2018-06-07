using System;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NuGet.Configuration;
using NuGet.PackageManagement;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Resolver;

namespace AudioWorks.Extensions
{
    abstract class ExtensionContainerBase
    {
        [NotNull]
        protected static CompositionHost CompositionHost { get; }

        static ExtensionContainerBase()
        {
            UpdateExtensionsAsync().Wait();

            CompositionHost = new ContainerConfiguration().WithAssemblies(
                    new DirectoryInfo(Path.Combine(
                            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) ??
                            string.Empty,
                            "Extensions")).GetDirectories()
                        .SelectMany(extensionDir => extensionDir.GetFiles("AudioWorks.Extensions.*.dll"))
                        .Select(fileInfo => new ExtensionAssemblyResolver(fileInfo.FullName).Assembly))
                .CreateContainer();
        }

        static async Task UpdateExtensionsAsync()
        {
            var sourceRepository = new SourceRepository(
                new PackageSource("https://www.myget.org/F/audioworks-extensions/api/v3/index.json"),
                Repository.Provider.GetCoreV3());

            var packageSearchResource =
                await sourceRepository.GetResourceAsync<PackageSearchResource>().ConfigureAwait(false);
            var searchMetadata = await packageSearchResource.SearchAsync("AudioWorks.Extensions",
                new SearchFilter(true), 0, 100, new NugetLogger(), CancellationToken.None).ConfigureAwait(false);

            var project = new ExtensionNuGetProject("C:\\Project");

            var settings = Settings.LoadDefaultSettings(project.Root);
            var packageManager = new NuGetPackageManager(
                new SourceRepositoryProvider(settings, Repository.Provider.GetCoreV3()),
                settings,
                project.Root);

            // Install any packages not already installed
            var installedPackages = (await packageManager
                .GetInstalledPackagesInDependencyOrder(project, CancellationToken.None).ConfigureAwait(false)).ToArray();
            foreach (var package in searchMetadata
                .Select(item => item.Identity)
                .Except(installedPackages, new PackageIdentityComparer()))
            {
                await packageManager.InstallPackageAsync(
                    project,
                    package,
                    new ResolutionContext(DependencyBehavior.Lowest, true, false, VersionConstraints.None),
                    new ExtensionProjectContext(new NugetLogger()),
                    sourceRepository,
                    new[]
                    {
                        new SourceRepository(
                            new PackageSource("https://api.nuget.org/v3/index.json"),
                            Repository.Provider.GetCoreV3())
                    },
                    CancellationToken.None).ConfigureAwait(false);

                // Create a bin folder for the extension
                var extensionDir = Path.Combine(@"C:\Project\bin", package.Id);
                if (Directory.Exists(extensionDir))
                    Directory.Delete(extensionDir, true);
                Directory.CreateDirectory(extensionDir);

                // Copy newly installed assemblies into the bin folder
                foreach (var dependency in project.InstalledPackages)
                {
                    var sourceDir = project.GetInstalledPath(dependency);
                    CopyDirectory(Path.Combine(sourceDir, "lib", "netstandard2.0"), extensionDir);
                    CopyDirectory(Path.Combine(sourceDir, "contentFiles", "any", "netstandard2.0"), extensionDir);
                }

                project.ClearInstalledPackagesList();

                // Remove obsolete versions by simply deleting the directory
                //foreach (var obsoletePackage in packageManager
                //    .GetInstalledPackagesInDependencyOrder(project, CancellationToken.None).Result
                //    .GroupBy(package => package.Id)
                //    .Select(group => group.OrderByDescending(package => package.Version).Skip(1))
                //    .SelectMany(package => package))
                //{
                //    Directory.Delete(Path.Combine(project.Root, $"{obsoletePackage.Id}.{obsoletePackage.Version}"), true);
                //}
            }
        }

        static void CopyDirectory([NotNull] string source, [NotNull] string destination)
        {
            CopyDirectory(new DirectoryInfo(source), destination);
        }

        static void CopyDirectory([NotNull] DirectoryInfo source, [NotNull] string destination)
        {
            if (!source.Exists) return;

            Directory.CreateDirectory(destination);

            foreach (var file in source.GetFiles())
                file.CopyTo(Path.Combine(destination, file.Name));

            foreach (var subdir in source.GetDirectories())
                CopyDirectory(subdir, Path.Combine(destination, subdir.Name));
        }
    }
}
