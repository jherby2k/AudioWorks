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
            UpdateExtensions();

            CompositionHost = new ContainerConfiguration().WithAssemblies(
                    new DirectoryInfo(Path.Combine(
                            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) ??
                            string.Empty,
                            "Extensions")).GetDirectories()
                        .SelectMany(extensionDir => extensionDir.GetFiles("AudioWorks.Extensions.*.dll"))
                        .Select(fileInfo => new ExtensionAssemblyResolver(fileInfo.FullName).Assembly))
                .CreateContainer();
        }

        static void UpdateExtensions()
        {
            var customRepository = new SourceRepository(
                new PackageSource("https://www.myget.org/F/audioworks-extensions/api/v3/index.json"),
                Repository.Provider.GetCoreV3());
            var defaultRepository = new SourceRepository(
                new PackageSource("https://api.nuget.org/v3/index.json"),
                Repository.Provider.GetCoreV3());

            var projectRoot = "C:\\Project";
            var settings = Settings.LoadDefaultSettings(projectRoot);
            var packageManager = new NuGetPackageManager(
                new SourceRepositoryProvider(settings, Repository.Provider.GetCoreV3()),
                settings,
                projectRoot);

            var packageSearchResource = customRepository.GetResourceAsync<PackageSearchResource>().Result;
            var publishedPackages = packageSearchResource.SearchAsync("AudioWorks.Extensions",
                new SearchFilter(true), 0, 100, new NugetLogger(), CancellationToken.None).Result.ToArray();

            foreach (var publishedPackage in publishedPackages)
            {
                var extensionDir = new DirectoryInfo(Path.Combine(projectRoot, publishedPackage.Identity.ToString()));
                if (extensionDir.Exists) break;

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
                    var stagingDir = project.GetInstalledPath(installedPackage);

                    var runtimesDir = new DirectoryInfo(Path.Combine(stagingDir, "runtimes"));
                    if (runtimesDir.Exists)
                        CopyDirectory(runtimesDir, extensionDir.CreateSubdirectory("runtimes"));

                    var libDir = new DirectoryInfo(Path.Combine(stagingDir, "lib"));
                    if (libDir.Exists)
                        CopyDirectory(
                            libDir.GetDirectories("netstandard*").OrderByDescending(dir => dir.Name).First(),
                            extensionDir);

                    var contentDir = new DirectoryInfo(Path.Combine(stagingDir, "contentFiles", "any"));
                    if (contentDir.Exists)
                        CopyDirectory(
                            contentDir.GetDirectories("netstandard*").OrderByDescending(dir => dir.Name).First(),
                            extensionDir);
                }

                stagingRootDir.Delete(true);
            }

            // Remove any extensions that aren't published
            foreach (var obsoleteExtension in new DirectoryInfo(projectRoot).GetDirectories()
                .Select(dir => dir.Name)
                .Except(publishedPackages.Select(package => package.Identity.ToString()),
                    StringComparer.OrdinalIgnoreCase))
                Directory.Delete(Path.Combine(projectRoot, obsoleteExtension), true);
        }

        static void CopyDirectory([NotNull] DirectoryInfo source, [NotNull] DirectoryInfo destination)
        {
            if (!source.Exists) return;

            foreach (var file in source.GetFiles()
                .Where(file => !file.Extension.Equals(".xml", StringComparison.OrdinalIgnoreCase)))
                file.CopyTo(Path.Combine(destination.FullName, file.Name));

            foreach (var subdir in source.GetDirectories())
                CopyDirectory(subdir, destination.CreateSubdirectory(subdir.Name));
        }
    }
}
