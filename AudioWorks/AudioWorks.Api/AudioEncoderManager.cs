using System;
using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Provides information about the available encoders, which are used by an <see cref="AudioFileEncoder"/>'s
    /// Encode method.
    /// </summary>
    [PublicAPI]
    public static class AudioEncoderManager
    {
        /// <summary>
        /// Gets information about the available settings that can be passed to an <see cref="AudioFileEncoder"/>'s
        /// Encode method, for a given encoder.
        /// </summary>
        /// <param name="name">The name of the encoder.</param>
        /// <returns>Information about the available settings.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is null.</exception>
        [NotNull]
        public static SettingInfoDictionary GetSettingInfo([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            // Try each encoder that supports this file extension:
            foreach (var factory in ExtensionProvider.GetFactories<IAudioEncoder>(
                "Name", name))
                using (var export = factory.CreateExport())
                    return export.Value.SettingInfo;

            return new SettingInfoDictionary();
        }
    }
}
