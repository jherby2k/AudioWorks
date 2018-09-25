/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

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