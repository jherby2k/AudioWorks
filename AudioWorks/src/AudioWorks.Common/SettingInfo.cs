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
using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Describes a setting of a specific <see cref="Type"/>.
    /// </summary>
    [PublicAPI]
    public abstract class SettingInfo
    {
        /// <summary>
        /// Gets the type of the setting value.
        /// </summary>
        /// <value>The type of the setting value.</value>
        [NotNull]
        public Type ValueType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingInfo"/> class.
        /// </summary>
        /// <param name="valueType">Type of the setting value.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="valueType"/> is null.</exception>
        protected SettingInfo([NotNull] Type valueType) =>
            ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));

        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public virtual void Validate([NotNull] object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.GetType() != ValueType)
                throw new ArgumentException($"{nameof(value)} is not of type {ValueType}.", nameof(value));
        }
    }
}