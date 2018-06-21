using System;
using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Describes an integer setting which has maximum and minimum values.
    /// </summary>
    /// <seealso cref="SettingInfo"/>
    [PublicAPI]
    public sealed class IntSettingInfo : SettingInfo
    {
        /// <summary>
        /// Gets the minimum value for the integer.
        /// </summary>
        /// <value>The minimum value.</value>
        public int MinValue { get; }

        /// <summary>
        /// Gets the maximum value for the integer.
        /// </summary>
        /// <value>The maximum value.</value>
        public int MaxValue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntSettingInfo"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value for the integer.</param>
        /// <param name="maxValue">The maximum value for the integer.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="minValue"/> is larger than
        /// <paramref name="maxValue"/>.</exception>
        public IntSettingInfo(int minValue, int maxValue)
            : base(typeof(int))
        {
            if (minValue >= maxValue)
                throw new ArgumentException($"{nameof(maxValue)} must be larger than {nameof(minValue)}.");

            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <inheritdoc/>
        public override void Validate(object value)
        {
            base.Validate(value);

            if ((int) value > MaxValue)
                throw new ArgumentOutOfRangeException(nameof(value), $"Maximum value is {MaxValue}.");
            if ((int) value < MinValue)
                throw new ArgumentOutOfRangeException(nameof(value), $"Minimum value is {MinValue}.");
        }
    }
}