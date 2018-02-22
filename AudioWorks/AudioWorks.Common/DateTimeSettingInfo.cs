using System;
using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Describes a date and time setting.
    /// </summary>
    /// <seealso cref="SettingInfo"/>
    [PublicAPI]
    public sealed class DateTimeSettingInfo : SettingInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeSettingInfo"/> class.
        /// </summary>
        public DateTimeSettingInfo()
            : base(typeof(DateTime))
        {
        }
    }
}
