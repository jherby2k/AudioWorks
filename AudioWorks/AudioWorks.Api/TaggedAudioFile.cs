using System;
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;
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
            AudioMetadataEncoderManager.GetSettingInfo(extension).ValidateSettings(settings);

            using (var fileStream = File.Open(Path, FileMode.Open))
            {
                // Try each encoder that supports this file extension
                foreach (var factory in ExtensionProvider.GetFactories<IAudioMetadataEncoder>(
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

            base.Rename(new MetadataSubstituter(name, IO.Path.GetInvalidFileNameChars()).Substitute(Metadata), replace);
        }

        [NotNull]
        static AudioMetadata LoadMetadata([NotNull] string path)
        {
            using (var fileStream = File.OpenRead(path))
            {
                // Try each decoder that supports this file extension
                foreach (var decoderFactory in ExtensionProvider.GetFactories<IAudioMetadataDecoder>(
                    "Extension", IO.Path.GetExtension(path)))
                    try
                    {
                        using (var lifetimeContext = decoderFactory.CreateExport())
                            return lifetimeContext.Value.ReadMetadata(fileStream);
                    }
                    catch (AudioUnsupportedException)
                    {
                        // If a decoder wasn't supported, rewind the stream and try another
                        fileStream.Position = 0;
                    }
            }

            return new AudioMetadata();
        }
    }
}