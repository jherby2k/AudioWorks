using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;
using System;

namespace AudioWorks.Api
{
    /// <summary>
    /// Provides information about the available metadata encoders, which are used by an
    /// <see cref="ITaggedAudioFile"/>'s SaveMetadata method.
    /// </summary>
    [PublicAPI]
    public static class AudioMetadataEncoderManager
    {
        /// <summary>
        /// Gets information about the available settings that can be passed to an <see cref="ITaggedAudioFile"/>'s
        /// SaveMetadata method, for a given file extension.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <returns>Information about the available settings.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="extension"/> is null.</exception>
        [NotNull]
        public static SettingInfoDictionary GetSettingInfo([NotNull] string extension)
        {
            if (extension == null) throw new ArgumentNullException(nameof(extension));

            // Try each encoder that supports this file extension:
            foreach (var factory in ExtensionProvider.GetFactories<IAudioMetadataEncoder>(
                "Extension", extension))
                using (var export = factory.CreateExport())
                    return export.Value.SettingInfo;

            return new SettingInfoDictionary();
        }
    }
}
