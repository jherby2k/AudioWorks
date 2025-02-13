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

namespace AudioWorks.Common
{
    /// <summary>
    /// Describes an integer setting which has maximum and minimum values.
    /// </summary>
    /// <seealso cref="SettingInfo"/>
    public sealed class IntSettingInfo : SettingInfo
    {
        /// <summary>
        /// Gets the minimum value for the integer.
        /// </summary>
        /// <value>The minimum value.</value>
        public int MinValue { get; }

        /// <summary>
        /// Gets the maximum value for the integer.
        /// </summary>
        /// <value>The maximum value.</value>
        public int MaxValue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntSettingInfo"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value for the integer.</param>
        /// <param name="maxValue">The maximum value for the integer.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="minValue"/> is greater than or
        /// equal to <paramref name="maxValue"/>.</exception>
        public IntSettingInfo(int minValue, int maxValue)
            : base(typeof(int))
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minValue, maxValue);

            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <inheritdoc/>
        public override void Validate(object value)
        {
            base.Validate(value);

            ArgumentOutOfRangeException.ThrowIfGreaterThan((int) value, MaxValue);
            ArgumentOutOfRangeException.ThrowIfLessThan((int) value, MinValue);
        }
    }
}