using System;
using System.Collections.Generic;
using System.Linq;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    sealed class ValidatingSettingDictionary : SettingDictionary
    {
        [NotNull] readonly SettingInfoDictionary _settingInfos;

        internal ValidatingSettingDictionary([NotNull] SettingInfoDictionary settingInfos, [CanBeNull] SettingDictionary settings = null)
        {
            _settingInfos = settingInfos;

            if (settings == null) return;

            foreach (var setting in settings)
                Add(setting);
        }

        public override void Add(KeyValuePair<string, object> item)
        {
            Validate(item.Key, item.Value);
            base.Add(item);
        }

        public override void Add(string key, object value)
        {
            Validate(key, value);
            base.Add(key, value);
        }

        public override object this[string key]
        {
            get => base[key];
            set
            {
                Validate(key, value);
                base[key] = value;
            }
        }

        void Validate([NotNull] string key, [NotNull] object value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (!_settingInfos.TryGetValue(key, out var settingInfo))
                throw new ArgumentException($"{key} is not a supported setting.");
            if (value.GetType() != settingInfo.ValueType)
                throw new ArgumentException($"{key} expects a value of type {settingInfo.ValueType}.");

            switch (settingInfo)
            {
                case IntSettingInfo intSettingInfo:
                    if ((int) value > intSettingInfo.MaxValue)
                        throw new ArgumentException(
                            $"{key} is out of range (maximum is {intSettingInfo.MaxValue}).");
                    if ((int) value < intSettingInfo.MinValue)
                        throw new ArgumentException(
                            $"{key} is out of range (minimum is {intSettingInfo.MinValue}).");
                    break;

                case StringSettingInfo stringSettingInfo:
                    if (!stringSettingInfo.AcceptedValues.Contains(value))
                        throw new ArgumentException($"{value} is not a valid {key} value.");
                    break;
            }
        }
    }
}