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
    public sealed class XunitLoggerProvider : ILoggerProvider
    {
        [NotNull] static readonly Lazy<XunitLoggerProvider> _lazyInstance =
            new Lazy<XunitLoggerProvider>(() => new XunitLoggerProvider());

        [NotNull]
        public static XunitLoggerProvider Instance => _lazyInstance.Value;

        bool _enabled;

        [CanBeNull]
        public ITestOutputHelper OutputHelper { get; set; }

        public LogLevel LogLevel { get; set; } = LogLevel.Debug;

        XunitLoggerProvider()
        {
        }

        [NotNull]
        public ILogger CreateLogger([CanBeNull] string categoryName)
        {
            return new XunitLogger(this, categoryName);
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
