using JetBrains.Annotations;
using System;

namespace AudioWorks.Common
{
    /// <summary>
    /// Describes a setting of a specific <see cref="Type"/>.
    /// </summary>
    [PublicAPI]
    public abstract class SettingInfo
    {
        /// <summary>
        /// Gets the type of the setting.
        /// </summary>
        /// <value>
        /// The type of the setting.
        /// </value>
        [NotNull]
        public Type ValueType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingInfo"/> class.
        /// </summary>
        /// <param name="valueType">Type of the setting.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="valueType"/> is null.</exception>
        protected SettingInfo([NotNull] Type valueType)
        {
            ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));
        }
    }
}