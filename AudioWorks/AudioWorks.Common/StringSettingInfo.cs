using JetBrains.Annotations;

namespace AudioWorks.Common
{
    [PublicAPI]
    public sealed class StringSettingInfo : SettingInfo
    {
        [NotNull, ItemNotNull]
        public string[] AcceptedValues { get; set; }

        public StringSettingInfo([NotNull, ItemNotNull] params string[] acceptedValues)
            : base(typeof(string))
        {
            AcceptedValues = acceptedValues;
        }
    }
}
