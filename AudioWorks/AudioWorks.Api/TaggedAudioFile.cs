using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;
using System;
using System.IO;

namespace AudioWorks.Api
{
    [PublicAPI]
    [Serializable]
    public sealed class TaggedAudioFile : AudioFile, ITaggedAudioFile
    {
        [CanBeNull] AudioMetadata _metadata;

        public AudioMetadata Metadata
        {
            get => _metadata ?? (_metadata = LoadMetadata(FileInfo));
            set => _metadata = value ?? throw new ArgumentNullException(nameof(value));
        }

        public TaggedAudioFile([NotNull] string path)
            : base(path)
        {
        }

        public void LoadMetadata()
        {
            _metadata = LoadMetadata(FileInfo);
        }

        public void SaveMetadata(SettingDictionary settings = null)
        {
            if (settings == null)
                settings = new SettingDictionary();

            // Make sure the provided settings are clean
            AudioMetadataEncoderManager.GetSettingInfo(FileInfo.Extension).ValidateSettings(settings);

            using (var fileStream = FileInfo.Open(FileMode.Open, FileAccess.ReadWrite))
            {
                // Try each encoder that supports this file extension
                foreach (var encoderFactory in ExtensionProvider.GetFactories<IAudioMetadataEncoder>(
                    "Extension", FileInfo.Extension))
                    using (var lifetimeContext = encoderFactory.CreateExport())
                    {
                        lifetimeContext.Value.WriteMetadata(fileStream, Metadata, settings);
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