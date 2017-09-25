using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;
using System;

namespace AudioWorks.Api
{
    [PublicAPI]
    public static class AudioMetadataEncoderManager
    {
        [NotNull]
        public static SettingInfoDictionary GetSettingInfo([NotNull] string extension)
        {
            if (extension == null) throw new ArgumentNullException(nameof(extension));

            // Try each encoder that supports this file extension:
            foreach (var encoderFactory in ExtensionProvider.GetFactories<IAudioMetadataEncoder>(
                "Extension", extension))
                using (var lifetimeContext = encoderFactory.CreateExport())
                    return lifetimeContext.Value.GetSettingInfo();

            return new SettingInfoDictionary();
        }
    }
}
