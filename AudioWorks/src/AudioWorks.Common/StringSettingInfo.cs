﻿/* Copyright © 2018 Jeremy Herbison

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

namespace AudioWorks.Common
{
    /// <summary>
    /// Describes a string setting which has a limited number of accepted values.
    /// </summary>
    /// <seealso cref="SettingInfo"/>
    /// <param name="acceptedValues">The accepted values.</param>
    public sealed class StringSettingInfo(params string[] acceptedValues) : SettingInfo(typeof(string))
    {
        /// <summary>
        /// Gets the accepted values.
        /// </summary>
        /// <value>The accepted values.</value>
        public IEnumerable<string> AcceptedValues { get; } = acceptedValues;

        /// <inheritdoc/>
        public override void Validate(object value)
        {
            base.Validate(value);

            if (!AcceptedValues.Contains(value))
                throw new ArgumentException($"{value} is not one of the accepted values.", nameof(value));
        }
    }
}
