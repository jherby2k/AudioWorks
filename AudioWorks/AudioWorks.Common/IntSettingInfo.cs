using JetBrains.Annotations;
using System;

namespace AudioWorks.Common
{
    [PublicAPI]
    public sealed class IntSettingInfo : SettingInfo
    {
        public int MinValue { get; }

        public int MaxValue { get; }

        public IntSettingInfo(int minValue, int maxValue)
            : base(typeof(int))
        {
            if (minValue >= maxValue)
                throw new ArgumentException("maxValue must be larger than minValue.");

            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}