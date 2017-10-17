using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace AudioWorks.Api
{
    /// <summary>
    /// The primary base type for working with AudioWorks. Represents a single track of audio within the filesystem.
    /// </summary>
    /// <seealso cref="IAudioFile"/>
    [PublicAPI]
    [Serializable]
    public class AudioFile : IAudioFile
    {
        [NotNull, NonSerialized] FileInfo _fileInfo;
        [NotNull] string _serializedPath;

        /// <inheritdoc />
        public FileInfo FileInfo => _fileInfo;

        /// <inheritdoc />
        public AudioInfo AudioInfo { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFile"/> class.
        /// </summary>
        /// <param name="path">The fully-qualified path to the file.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown if <paramref name="path"/> does not exist.</exception>
        public AudioFile([NotNull] string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path), "Value cannot be null or empty.");
            if (!File.Exists(path))
                throw new FileNotFoundException($"The file '{path}' cannot be found.", path);

            _fileInfo = new FileInfo(path);
            _serializedPath = _fileInfo.FullName;

            AudioInfo = LoadAudioInfo();
        }

        [NotNull]
        AudioInfo LoadAudioInfo()
        {
            using (var fileStream = _fileInfo.OpenRead())
            {
                // Try each info decoder that supports this file extension
                foreach (var decoderFactory in ExtensionProvider.GetFactories<IAudioInfoDecoder>(
                    "Extension", _fileInfo.Extension))
                    try
                    {
                        using (var lifetimeContext = decoderFactory.CreateExport())
                            return lifetimeContext.Value.ReadAudioInfo(fileStream);
                    }
                    catch (AudioUnsupportedException)
                    {
                        // If a decoder wasn't supported, rewind the stream and try another
                        fileStream.Position = 0;
                    }
            }

            throw new AudioUnsupportedException("No supporting extensions are available.");
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            _fileInfo = new FileInfo(_serializedPath);
        }
    }
}
