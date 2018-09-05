using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.ProjectManagement;

namespace AudioWorks.Api
{
    sealed class ExtensionNuGetProject : FolderNuGetProject
    {
        internal ExtensionNuGetProject([NotNull] string root) : base(root)
        {
        }

        [NotNull]
        public override Task<IEnumerable<PackageReference>> GetInstalledPackagesAsync(CancellationToken token)
        {
            var result = new List<PackageReference>();
            foreach (var specFile in new DirectoryInfo(Root).GetDirectories()
                .SelectMany(dir => dir.GetFiles("*.nuspec")))
                using (var stream = specFile.OpenRead())
                    result.Add(new PackageReference(new NuspecReader(stream).GetIdentity(),
                        NuGetFramework.AnyFramework));
            return Task.FromResult(result.AsEnumerable());
        }
    }
}