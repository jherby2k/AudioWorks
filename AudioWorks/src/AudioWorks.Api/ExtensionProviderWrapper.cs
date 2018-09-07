using System.Collections.Generic;
using System.Composition;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    static class ExtensionProviderWrapper
    {
        static ExtensionProviderWrapper()
        {
            // Ensure this is only called once via the static constructor
            ExtensionInstaller.Download();
        }

        [NotNull]
        internal static IEnumerable<ExportFactory<T, IDictionary<string, object>>> GetFactories<T>()
            where T : class
        {
            return ExtensionProvider.GetFactories<T>();
        }

        [NotNull]
        internal static IEnumerable<ExportFactory<T>> GetFactories<T>([NotNull] string key, [NotNull] string value)
            where T : class
        {
            return ExtensionProvider.GetFactories<T>(key, value);
        }
    }
}