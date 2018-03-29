﻿using System.Collections.Generic;
using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Describes a string setting which has a limited number of accepted values.
    /// </summary>
    /// <seealso cref="SettingInfo"/>
    [PublicAPI]
    public sealed class StringSettingInfo : SettingInfo
    {
        /// <summary>
        /// Gets the accepted values.
        /// </summary>
        /// <value>The accepted values.</value>
        [NotNull, ItemNotNull]
        public IEnumerable<string> AcceptedValues { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringSettingInfo"/> class.
        /// </summary>
        /// <param name="acceptedValues">The accepted values.</param>
        public StringSettingInfo([NotNull, ItemNotNull] params string[] acceptedValues)
            : base(typeof(string))
        {
            AcceptedValues = acceptedValues;
        }
    }
}