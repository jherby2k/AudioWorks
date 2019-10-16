/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using AudioWorks.Common;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensibility
{
    sealed class ExtensionContainer<T> : ExtensionContainerBase
    {
        static readonly Lazy<ExtensionContainer<T>> _lazyInstance =
            new Lazy<ExtensionContainer<T>>(() => new ExtensionContainer<T>());

        internal static ExtensionContainer<T> Instance => _lazyInstance.Value;

        [ImportMany]
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        internal IEnumerable<ExportFactory<T, IDictionary<string, object>>> Factories { get; private set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        ExtensionContainer()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            CompositionHost.SatisfyImports(this);

            LoggerManager.LoggerFactory.CreateLogger<ExtensionContainer<T>>()
                .LogDebug("Composed {0} part(s) of type '{1}'.", Factories.Count(), typeof(T).Name);
        }
    }
}
