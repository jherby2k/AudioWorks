using JetBrains.Annotations;
using System;

namespace AudioWorks.Common
{
    [PublicAPI]
    public abstract class SettingInfo
    {
        [NotNull]
        public Type SettingType { get; }

        protected SettingInfo([NotNull] Type settingType)
        {
            SettingType = settingType ?? throw new ArgumentNullException(nameof(settingType));
        }
    }
}