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
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Common
{
    /// <summary>
    /// Manages logging to various destinations.
    /// </summary>
    public static class LoggerManager
    {
        [NotNull] static readonly List<ILoggerProvider> _providerSingletons = new List<ILoggerProvider>();

        /// <summary>
        /// Gets the singleton logger factory.
        /// </summary>
        /// <value>The logger factory.</value>
        [NotNull]
        [CLSCompliant(false)]
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();

        /// <summary>
        /// Adds an instance of type {T} to the <see cref="LoggerFactory"/>, if one of the same type hasn't already
        /// been added.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ILoggerProvider"/> requested.</typeparam>
        /// <param name="createProviderFunc">A function called if a new provider is required.</param>
        /// <returns>The new provider, or an existing one if there is already one of type <typeparamref name="T"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="createProviderFunc"/> is null.</exception>
        [NotNull]
        [CLSCompliant(false)]
        public static T AddSingletonProvider<T>([NotNull] Func<T> createProviderFunc) where T : ILoggerProvider
        {
            if (createProviderFunc == null) throw new ArgumentNullException(nameof(createProviderFunc));

            lock (_providerSingletons)
            {
                var existingProvider = _providerSingletons.OfType<T>().FirstOrDefault();
                if (existingProvider != null)
                    return existingProvider;
                {
                    var newProvider = createProviderFunc();
                    LoggerFactory.AddProvider(newProvider);
                    _providerSingletons.Add(newProvider);
                    return newProvider;
                }
            }
        }
    }
}
