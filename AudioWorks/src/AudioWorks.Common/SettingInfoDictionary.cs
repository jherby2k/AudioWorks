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

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents a dictionary of strings and <see cref="SettingInfo"/> objects describing the settings which you can
    /// pass to various methods.
    /// </summary>
    /// <seealso cref="Dictionary{String, SettingInfo}"/>
    public sealed class SettingInfoDictionary : Dictionary<string, SettingInfo>
    {
        /// <summary>
        /// Validates a settings dictionary against this <see cref="SettingInfoDictionary"/>.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="settings"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more settings are not valid.</exception>
        public void ValidateSettings(SettingDictionary settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            foreach (var setting in settings)
            {
                if (!TryGetValue(setting.Key, out var settingInfo))
                    throw new ArgumentException($"{setting.Key} is not a supported setting.");
                if (setting.Value.GetType() != settingInfo.ValueType)
                    throw new ArgumentException($"{setting.Key} expects a value of type {settingInfo.ValueType}.");

                switch (settingInfo)
                {
                    case IntSettingInfo intSettingInfo:
                        if ((int) setting.Value > intSettingInfo.MaxValue)
                            throw new ArgumentException(
                                $"{setting.Key} is out of range (maximum is {intSettingInfo.MaxValue}).");
                        if ((int) setting.Value < intSettingInfo.MinValue)
                            throw new ArgumentException(
                                $"{setting.Key} is out of range (minimum is {intSettingInfo.MinValue}).");
                        break;
                    case StringSettingInfo stringSettingInfo:
                        if (!stringSettingInfo.AcceptedValues.Contains(setting.Value))
                            throw new ArgumentException($"{setting.Value} is not a valid {setting.Key} value.");
                        break;
                }
            }
        }
    }
}