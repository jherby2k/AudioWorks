/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using IO = System.IO;

namespace AudioWorks.Api
{
    /// <inheritdoc cref="ITaggedAudioFile"/>
    [PublicAPI]
    [Serializable]
    public sealed class TaggedAudioFile : AudioFile, ITaggedAudioFile
    {
        [CanBeNull] AudioMetadata _metadata;

        /// <inheritdoc/>
        public AudioMetadata Metadata
        {
            get => _metadata ?? (_metadata = LoadMetadata(Path));
            set => _metadata = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaggedAudioFile"/> class.
        /// </summary>
        /// <param name="path">The fully-qualified path to the file.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown if <paramref name="path"/> does not exist.</exception>
        public TaggedAudioFile([NotNull] string path)
            : base(path)
        {
        }

        /// <inheritdoc/>
        public void LoadMetadata()
        {
            _metadata = LoadMetadata(Path);
        }

        /// <inheritdoc/>
        public void SaveMetadata(SettingDictionary settings = null)
        {
            if (settings == null)
                settings = new SettingDictionary();
            var extension = IO.Path.GetExtension(Path);

            // Make sure the provided settings are clean
            AudioMetadataEncoderManager.GetSettingInfoByExtension(extension).ValidateSettings(settings);

            using (var fileStream = File.Open(Path, FileMode.Open))
            {
                // Try each encoder that supports this file extension
                foreach (var factory in ExtensionProviderWrapper.GetFactories<IAudioMetadataEncoder>(
                    "Extension", extension))
                    using (var export = factory.CreateExport())
                    {
                        export.Value.WriteMetadata(fileStream, Metadata, settings);
                        return;
                    }
            }

            throw new AudioUnsupportedException("No supporting extensions are available.");
        }

        /// <inheritdoc/>
        public override void Rename(string name, bool replace)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Value cannot be null or empty.");

            base.Rename(new EncodedPath(name).ReplaceWith(Metadata), replace);
        }

        [NotNull]
        static AudioMetadata LoadMetadata([NotNull] string path)
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger<TaggedAudioFile>();

            using (var fileStream = File.OpenRead(path))
            {
                // Try each decoder that supports this file extension
                foreach (var decoderFactory in ExtensionProviderWrapper.GetFactories<IAudioMetadataDecoder>(
                    "Extension", IO.Path.GetExtension(path)))
                {
                    var format = string.Empty;
                    try
                    {
                        using (var export = decoderFactory.CreateExport())
                        {
                            format = export.Value.Format;
                            logger.LogDebug("Attempting to read '{0}' metadata from '{1}'.", format, path);
                            return export.Value.ReadMetadata(fileStream);
                        }
                    }
                    catch (AudioUnsupportedException e)
                    {
                        logger.LogDebug(e, "Unable to read '{0}' metadata from '{1}'.", format, path);

                        // If a decoder wasn't supported, rewind the stream and try another
                        fileStream.Position = 0;
                    }
                }
            }

            logger.LogDebug("Unable to read any metadata from '{0}'.", path);
            return new AudioMetadata();
        }
    }
}