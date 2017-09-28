using JetBrains.Annotations;
using System;

namespace AudioWorks.Common
{
    [PublicAPI]
    public abstract class SettingInfo
    {
        [NotNull]
        public Type ValueType { get; }

        protected SettingInfo([NotNull] Type valueType)
        {
            ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));
        }
    }
}