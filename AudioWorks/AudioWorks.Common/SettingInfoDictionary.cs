using JetBrains.Annotations;
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
    [PublicAPI]
    public sealed class SettingInfoDictionary : Dictionary<string, SettingInfo>
    {
        /// <summary>
        /// Validates a settings dictionary against this <see cref="SettingInfoDictionary"/>.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentException">Thrown if one or more settings are not valid.</exception>
        public void ValidateSettings([NotNull] SettingDictionary settings)
        {
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