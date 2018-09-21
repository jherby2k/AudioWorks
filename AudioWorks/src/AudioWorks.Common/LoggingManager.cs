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

namespace AudioWorks.Common
{
    /// <summary>
    /// Manages logging to various destinations.
    /// </summary>
    public static class LoggingManager
    {
        /// <summary>
        /// Gets the singleton logger factory.
        /// </summary>
        /// <value>The logger factory.</value>
        [NotNull]
        [CLSCompliant(false)]
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();
    }
}
