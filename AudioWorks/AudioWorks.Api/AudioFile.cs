using System;
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;
using IO = System.IO;

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
        /// <inheritdoc/>
        public string Path { get; private set; }

        /// <inheritdoc/>
        public AudioInfo Info { get; }

        /// <inheritdoc/>
        public virtual void Rename(string name, bool replace)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Value cannot be null or empty.");

            var newPath = IO.Path.Combine(IO.Path.GetDirectoryName(Path), name + IO.Path.GetExtension(Path));
            if (File.Exists(newPath) && replace)
                File.Delete(newPath);
            File.Move(Path, newPath);
            Path = newPath;
        }

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

            Path = IO.Path.GetFullPath(path);
            Info = LoadInfo();
        }

        [NotNull]
        AudioInfo LoadInfo()
        {
            using (var fileStream = File.OpenRead(Path))
            {
                // Try each info decoder that supports this file extension
                foreach (var factory in ExtensionProvider.GetFactories<IAudioInfoDecoder>(
                    "Extension", IO.Path.GetExtension(Path)))
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
    }
}
