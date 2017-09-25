using JetBrains.Annotations;
using System;

namespace AudioWorks.Common
{
    [PublicAPI]
    public sealed class SettingInfo
    {
        [NotNull]
        public Type Type { get; }

        public SettingInfo([NotNull] Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}