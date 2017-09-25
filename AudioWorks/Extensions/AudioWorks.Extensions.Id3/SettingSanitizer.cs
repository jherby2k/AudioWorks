using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Id3
{
    sealed class SettingSanitizer
    {
        internal bool ConfiguresPadding { get; }

        internal uint Padding { get; }

        internal SettingSanitizer([CanBeNull] SettingDictionary settings)
        {
            if (settings == null) return;

            settings.TryGetValue("Padding", out var padding);
            if (padding is int newPadding)
            {
                ConfiguresPadding = true;
                Padding = (uint)newPadding;
            }
        }
    }
}