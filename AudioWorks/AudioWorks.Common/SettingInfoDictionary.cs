using JetBrains.Annotations;
using System;
using System.Collections.Generic;

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
                if (setting.Value.GetType() != settingInfo.Type)
                    throw new ArgumentException($"{setting.Key} expects a value of type {settingInfo.Type}.");
            }
        }
    }
}