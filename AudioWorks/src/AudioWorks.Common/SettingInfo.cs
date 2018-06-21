using System;
using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Describes a setting of a specific <see cref="Type"/>.
    /// </summary>
    [PublicAPI]
    public abstract class SettingInfo
    {
        /// <summary>
        /// Gets the type of the setting value.
        /// </summary>
        /// <value>The type of the setting value.</value>
        [NotNull]
        public Type ValueType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingInfo"/> class.
        /// </summary>
        /// <param name="valueType">Type of the setting value.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="valueType"/> is null.</exception>
        protected SettingInfo([NotNull] Type valueType)
        {
            ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));
        }

        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public virtual void Validate([NotNull] object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.GetType() != ValueType)
                throw new ArgumentException($"{nameof(value)} is not of type {ValueType}.", nameof(value));
        }
    }
}