using JetBrains.Annotations;

namespace AudioWorks.Common
{
    public sealed class StringSettingInfo : SettingInfo
    {
        [NotNull, ItemNotNull]
        public string[] ValidSettings { get; set; }

        public StringSettingInfo([NotNull, ItemNotNull] params string[] validSettings)
            : base(typeof(string))
        {
            ValidSettings = validSettings;
        }
    }
}
