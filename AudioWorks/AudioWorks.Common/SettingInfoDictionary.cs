using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AudioWorks.Common
{
    [PublicAPI]
    public sealed class SettingInfoDictionary : Dictionary<string, SettingInfo>
    {
        public void ValidateSettings([NotNull] SettingDictionary settings)
        {
            foreach (var setting in settings)
            {
                if (!TryGetValue(setting.Key, out var settingInfo))
                    throw new ArgumentException($"{setting.Key} is not a supported setting.");
                if (setting.Value.GetType() != settingInfo.SettingType)
                    throw new ArgumentException($"{setting.Key} expects a value of type {settingInfo.SettingType}.");

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
                        if (!stringSettingInfo.ValidSettings.Contains(setting.Value))
                            throw new ArgumentException($"{setting.Value} is not a valid {setting.Key} value.");
                        break;
                }
            }
        }
    }
}