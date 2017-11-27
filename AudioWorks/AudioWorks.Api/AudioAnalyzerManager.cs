using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;
using System;

namespace AudioWorks.Api
{
    /// <summary>
    /// Provides information about the available analyzers, which are used by an <see cref="AudioFileAnalyzer"/>'s
    /// Analyze method.
    /// </summary>
    [PublicAPI]
    public static class AudioAnalyzerManager
    {
        /// <summary>
        /// Gets information about the available settings that can be passed to an <see cref="AudioFileAnalyzer"/>'s
        /// Analyze method, for a given analyzer.
        /// </summary>
        /// <param name="name">The name of the analyzer.</param>
        /// <returns>Information about the available settings.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is null.</exception>
        [NotNull]
        public static SettingInfoDictionary GetSettingInfo([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            // Try each encoder that supports this file extension:
            foreach (var factory in ExtensionProvider.GetFactories<IAudioAnalyzer>(
                "Name", name))
                using (var export = factory.CreateExport())
                    return export.Value.SettingInfo;

            return new SettingInfoDictionary();
        }
    }
}
