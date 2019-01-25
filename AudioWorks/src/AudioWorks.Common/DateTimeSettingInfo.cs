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
    /// Describes a date and time setting.
    /// </summary>
    /// <seealso cref="SettingInfo"/>
    [PublicAPI]
    public sealed class DateTimeSettingInfo : SettingInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeSettingInfo"/> class.
        /// </summary>
        public DateTimeSettingInfo()
            : base(typeof(DateTime))
        {
        }
    }
}
