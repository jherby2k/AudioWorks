using System;
using System.Collections.Generic;
using System.Composition;
using JetBrains.Annotations;

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
