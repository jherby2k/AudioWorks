/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace AudioWorks.TestUtilities
{
    public sealed class XUnitLoggerProvider : ILoggerProvider
    {
        [NotNull] static readonly Lazy<XUnitLoggerProvider> _lazyInstance =
            new Lazy<XUnitLoggerProvider>(() => new XUnitLoggerProvider());

        [NotNull]
        public static XUnitLoggerProvider Instance => _lazyInstance.Value;

        bool _enabled;

        [CanBeNull]
        public ITestOutputHelper OutputHelper { get; set; }

        public LogLevel LogLevel { get; set; } = LogLevel.Debug;

        XUnitLoggerProvider()
        {
        }

        [NotNull]
        public ILogger CreateLogger([CanBeNull] string categoryName)
        {
            return new XUnitLogger(this, categoryName);
        }

        public void Dispose()
        {
        }

        public void Enable(ILoggerFactory factory)
        {
            // Ensure this provider is only added once
            if (_enabled) return;
            factory.AddProvider(this);
            _enabled = true;
        }
    }
}
