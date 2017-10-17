using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace AudioWorks.Api
{
    /// <summary>
    /// Represents a single track of audio on the filesystem that may or may not contain a metadata "tag".
    /// </summary>
    /// <seealso cref="AudioFile"/>
    /// <seealso cref="ITaggedAudioFile"/>
    [PublicAPI]
    [Serializable]
    public sealed class TaggedAudioFile : AudioFile, ITaggedAudioFile
    {
        [CanBeNull] AudioMetadata _metadata;

        /// <inheritdoc />
        public AudioMetadata Metadata
        {
            get => _metadata ?? (_metadata = LoadMetadata(FileInfo));
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

        TaggedAudioFile([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <inheritdoc />
        public void LoadMetadata()
        {
            _metadata = LoadMetadata(FileInfo);
        }

        /// <inheritdoc />
        public void SaveMetadata(SettingDictionary settings = null)
        {
            if (settings == null)
                settings = new SettingDictionary();

            // Make sure the provided settings are clean
            AudioMetadataEncoderManager.GetSettingInfo(FileInfo.Extension).ValidateSettings(settings);

            using (var fileStream = FileInfo.Open(FileMode.Open, FileAccess.ReadWrite))
            {
                // Try each encoder that supports this file extension
                foreach (var factory in ExtensionProvider.GetFactories<IAudioMetadataEncoder>(
                    "Extension", FileInfo.Extension))
                    using (var export = factory.CreateExport())
                    {
                        export.Value.WriteMetadata(fileStream, Metadata, settings);
                        return;
                    }
            }

            throw new AudioUnsupportedException("No supporting extensions are available.");
        }

        [NotNull]
        static AudioMetadata LoadMetadata([NotNull] FileInfo fileInfo)
        {
            using (var fileStream = fileInfo.OpenRead())
            {
                // Try each decoder that supports this file extension
                foreach (var decoderFactory in ExtensionProvider.GetFactories<IAudioMetadataDecoder>(
                    "Extension", fileInfo.Extension))
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