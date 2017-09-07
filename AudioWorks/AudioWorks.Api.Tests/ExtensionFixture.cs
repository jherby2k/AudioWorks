using JetBrains.Annotations;
using System;
using System.IO;
using System.Reflection;

namespace AudioWorks.Api.Tests
{
    [UsedImplicitly]
    public sealed class ExtensionFixture
    {
        public ExtensionFixture()
        {
            var configuration = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Name;
            var extensionsInstallDir = new DirectoryInfo(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath))
                .CreateSubdirectory("Extensions");

            foreach (var extensionProjectDir in new DirectoryInfo(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName,
                "Extensions")).GetDirectories())
                DotNetUtility.Publish(
                    extensionProjectDir.FullName,
                    configuration,
                    extensionsInstallDir.CreateSubdirectory(extensionProjectDir.Name).FullName);
        }
    }
}
