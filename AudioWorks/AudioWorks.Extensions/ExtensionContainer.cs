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
    class ExtensionContainer<T>
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
            // Add a catalog for each subdirectory under Extensions:
            new ContainerConfiguration().WithAssemblies(
                    new DirectoryInfo(Path.Combine(
                            Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) ??
                            string.Empty, "Extensions")).GetDirectories()
                        .SelectMany(d => d.GetFiles("AudioWorks.Extensions.*.dll")).Select(f => f.FullName)
                        .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath))
                .CreateContainer().SatisfyImports(this);
        }
    }
}
