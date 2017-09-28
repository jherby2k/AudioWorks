using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Composition;

namespace AudioWorks.Extensions
{
    sealed class ExtensionContainer<T> : ExtensionContainerBase
    {
        [NotNull] static readonly Lazy<ExtensionContainer<T>> _lazyInstance =
            new Lazy<ExtensionContainer<T>>(() => new ExtensionContainer<T>());

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
            CompositionHost.SatisfyImports(this);
        }
    }
}
