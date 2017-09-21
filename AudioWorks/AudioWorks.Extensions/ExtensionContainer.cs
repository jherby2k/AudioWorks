using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace AudioWorks.Extensions
{
    sealed class ExtensionContainer<T>
    {
        [NotNull] static readonly Lazy<ExtensionContainer<T>> _lazyInstance = new Lazy<ExtensionContainer<T>>(() => new ExtensionContainer<T>());

        [NotNull]
        internal static ExtensionContainer<T> Instance => _lazyInstance.Value;

        [UsedImplicitly]
        [ImportMany]
        internal IEnumerable<ExportFactory<T, IDictionary<string, object>>> Factories { get; private set; }

        ExtensionContainer()
        {
            Initialize();
        }

        void Initialize()
        {
            var extensionRoot = new DirectoryInfo(Path.Combine(
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) ??
                string.Empty, "Extensions"));

            // Search all extension directories for any references that can't be resolved
            AssemblyLoadContext.Default.Resolving += (context, name) =>
                AssemblyLoadContext.Default.LoadFromAssemblyPath(extensionRoot
                    .EnumerateFiles($"{name.Name}.dll", SearchOption.AllDirectories)
                    .FirstOrDefault()?
                    .FullName);

            new ContainerConfiguration()
                .WithAssemblies(extensionRoot
                    .EnumerateFiles("AudioWorks.Extensions.*.dll", SearchOption.AllDirectories)
                    .Select(f => f.FullName)
                    .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath))
                .CreateContainer()
                .SatisfyImports(this);
        }
    }
}
