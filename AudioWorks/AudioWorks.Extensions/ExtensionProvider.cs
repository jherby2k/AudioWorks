using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;

namespace AudioWorks.Extensions
{
    [PublicAPI]
    public static class ExtensionProvider
    {
        [NotNull]
        public static IEnumerable<ExportFactory<T>> GetFactories<T>()
            where T : class
        {
            return ExtensionContainer<T>.Instance.Factories;
        }

        [NotNull]
        public static IEnumerable<ExportFactory<T>> GetFactories<T>([NotNull] string key, [NotNull] string value)
            where T : class
        {
            return ExtensionContainer<T>.Instance.Factories.Where(factory =>
                string.Compare((string) factory.Metadata[key], value, StringComparison.OrdinalIgnoreCase) == 0);
        }
    }
}
