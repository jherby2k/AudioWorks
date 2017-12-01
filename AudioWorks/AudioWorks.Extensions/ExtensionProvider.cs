using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using JetBrains.Annotations;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Provides methods for accessing extensions of various types.
    /// </summary>
    [PublicAPI]
    public static class ExtensionProvider
    {
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of extensions wrapped in <see cref="ExportFactory{T}"/> objects to
        /// control their lifetime.
        /// </summary>
        /// <typeparam name="T">The type of extension.</typeparam>
        /// <returns>The extension factories.</returns>
        [NotNull]
        public static IEnumerable<ExportFactory<T>> GetFactories<T>()
            where T : class
        {
            return ExtensionContainer<T>.Instance.Factories;
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of extensions wrapped in <see cref="ExportFactory{T}"/> objects to
        /// control their lifetime, filtered by metadata.
        /// </summary>
        /// <typeparam name="T">The type of extension.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>The extension factories.</returns>
        [NotNull]
        public static IEnumerable<ExportFactory<T>> GetFactories<T>([NotNull] string key, [NotNull] string value)
            where T : class
        {
            return ExtensionContainer<T>.Instance.Factories.Where(factory =>
                string.Compare((string) factory.Metadata[key], value, StringComparison.OrdinalIgnoreCase) == 0);
        }
    }
}
