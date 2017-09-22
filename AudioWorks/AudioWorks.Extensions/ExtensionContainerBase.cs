using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace AudioWorks.Extensions
{
    abstract class ExtensionContainerBase
    {
        protected static readonly DirectoryInfo ExtensionRoot = new DirectoryInfo(Path.Combine(
            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) ??
            string.Empty, "Extensions"));

        static ExtensionContainerBase()
        {
            // Search all extension directories for any references that can't be resolved
            AssemblyLoadContext.Default.Resolving += (context, name) =>
                AssemblyLoadContext.Default.LoadFromAssemblyPath(ExtensionRoot
                    .EnumerateFiles($"{name.Name}.dll", SearchOption.AllDirectories)
                    .FirstOrDefault()?
                    .FullName);
        }
    }
}
