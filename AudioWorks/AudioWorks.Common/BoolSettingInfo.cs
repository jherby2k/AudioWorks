using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Describes a setting which can be either true or false.
    /// </summary>
    /// <seealso cref="SettingInfo"/>
    [PublicAPI]
    public sealed class BoolSettingInfo : SettingInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoolSettingInfo"/> class.
        /// </summary>
        public BoolSettingInfo()
            : base(typeof(bool))
        {
        }
    }
}
