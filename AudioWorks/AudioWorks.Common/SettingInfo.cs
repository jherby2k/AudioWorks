using JetBrains.Annotations;
using System;

namespace AudioWorks.Common
{
    [PublicAPI]
    public abstract class SettingInfo
    {
        [NotNull]
        public Type Type { get; }

        protected SettingInfo([NotNull] Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}