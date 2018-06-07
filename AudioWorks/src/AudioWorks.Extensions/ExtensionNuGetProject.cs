using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.ProjectManagement;
using NuGet.Protocol.Core.Types;

namespace AudioWorks.Extensions
{
    sealed class ExtensionNuGetProject : FolderNuGetProject
    {
        [NotNull, ItemNotNull] readonly List<PackageIdentity> _installedPackages = new List<PackageIdentity>();

        [NotNull, ItemNotNull]
        internal IEnumerable<PackageIdentity> InstalledPackages => _installedPackages;

        internal ExtensionNuGetProject([NotNull] string root) : base(root)
        {
        }

        [NotNull]
        public override async Task<IEnumerable<PackageReference>> GetInstalledPackagesAsync(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                var result = new List<PackageReference>();
                foreach (var nuspecFile in new DirectoryInfo(Root).GetDirectories()
                    .SelectMany(dir => dir.GetFiles("*.nuspec")))
                    using (var stream = nuspecFile.OpenRead())
                        result.Add(new PackageReference(new NuspecReader(stream).GetIdentity(),
                            NuGetFramework.AnyFramework));
                return result;
            }, token).ConfigureAwait(false);
        }

        [NotNull]
        public override async Task<bool> InstallPackageAsync(
            [NotNull] PackageIdentity packageIdentity,
            [NotNull] DownloadResourceResult downloadResourceResult,
            [NotNull] INuGetProjectContext nuGetProjectContext,
            CancellationToken token)
        {
            if (!await base.InstallPackageAsync(packageIdentity, downloadResourceResult, nuGetProjectContext, token)
                .ConfigureAwait(false))
                return false;

            _installedPackages.Add(packageIdentity);
            return true;
        }

        internal void ClearInstalledPackagesList()
        {
            _installedPackages.Clear();
        }
    }
}