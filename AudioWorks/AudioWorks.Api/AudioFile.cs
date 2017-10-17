using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;
using System;
using System.Diagnostics.CodeAnalysis;
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
    public class AudioFile : IAudioFile, ISerializable
    {
        /// <inheritdoc />
        [SuppressMessage("Usage", "CA2235:Mark all non-serializable fields",
            Justification = "Properties are serialized manually")]
        public FileInfo FileInfo { get; }

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

            FileInfo = new FileInfo(path);
            AudioInfo = LoadAudioInfo();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFile"/> class.
        /// </summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Required for ISerializable implementation")]
        protected AudioFile([NotNull] SerializationInfo info, StreamingContext context)
        {
            FileInfo = new FileInfo(info.GetString("fileName"));
            AudioInfo = (AudioInfo) info.GetValue("audioInfo", typeof(AudioInfo));
        }

        [NotNull]
        AudioInfo LoadAudioInfo()
        {
            using (var fileStream = FileInfo.OpenRead())
            {
                // Try each info decoder that supports this file extension
                foreach (var factory in ExtensionProvider.GetFactories<IAudioInfoDecoder>(
                    "Extension", FileInfo.Extension))
                    try
                    {
                        using (var export = factory.CreateExport())
                            return export.Value.ReadAudioInfo(fileStream);
                    }
                    catch (AudioUnsupportedException)
                    {
                        // If a decoder wasn't supported, rewind the stream and try another
                        fileStream.Position = 0;
                    }
            }

            throw new AudioUnsupportedException("No supporting extensions are available.");
        }

        /// <inheritdoc />
        public virtual void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
        {
            info.AddValue("fileName", FileInfo.FullName);
            info.AddValue("audioInfo", AudioInfo);
        }
    }
}
