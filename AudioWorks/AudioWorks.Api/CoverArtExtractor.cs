using System;
using System.IO;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Extracts cover art from audio files.
    /// </summary>
    public sealed class CoverArtExtractor
    {
        [NotNull] readonly MetadataSubstituter _fileNameSubstituter =
            new MetadataSubstituter(Path.GetInvalidFileNameChars());
        [NotNull] readonly MetadataSubstituter _directoryNameSubstituter =
            new MetadataSubstituter(Path.GetInvalidPathChars());

        [CanBeNull] readonly string _encodedDirectoryName;
        [CanBeNull] readonly string _encodedFileName;
        readonly bool _overwrite;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoverArtExtractor"/> class.
        /// </summary>
        /// <param name="encodedDirectoryName">The encoded directory name, or null.</param>
        /// <param name="encodedFileName">The encode file name, or null.</param>
        /// <param name="overwrite">if set to <c>true</c>, any existing file will be overwritten.</param>
        public CoverArtExtractor(
            [CanBeNull] string encodedDirectoryName = null,
            [CanBeNull] string encodedFileName = null,
            bool overwrite = false)
        {
            _encodedDirectoryName = encodedDirectoryName;
            _encodedFileName = encodedFileName;
            _overwrite = overwrite;
        }

        /// <summary>
        /// Extracts the specified audio file's cover art to an image file. This method returns null if cover art is
        /// not present.
        /// </summary>
        /// <param name="audioFile">The audio file.</param>
        /// <returns>An image file.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="audioFile"/> is null.</exception>
        /// <exception cref="IOException">Throw if the image file already exists, and overwrite was not enabled in the
        /// constructor.</exception>
        [CanBeNull]
        public FileInfo Extract([NotNull] ITaggedAudioFile audioFile)
        {
            if (audioFile == null) throw new ArgumentNullException(nameof(audioFile));

            if (audioFile.Metadata.CoverArt == null) return null;

            // The output directory defaults to the audiofile's current directory
            var outputDirectory = _encodedDirectoryName == null
                ? Path.GetDirectoryName(audioFile.Path)
                : _directoryNameSubstituter.Substitute(_encodedDirectoryName, audioFile.Metadata);

            Directory.CreateDirectory(outputDirectory);

            // The output file names default to the input file names
            var outputFileName = _encodedFileName == null
                ? Path.GetFileNameWithoutExtension(audioFile.Path)
                : _fileNameSubstituter.Substitute(_encodedFileName, audioFile.Metadata);

            var result = new FileInfo(Path.Combine(
                outputDirectory,
                outputFileName + audioFile.Metadata.CoverArt.FileExtension));

            using (var fileStream = result.Open(_overwrite ? FileMode.Create : FileMode.CreateNew, FileAccess.Write))
            {
                var data = audioFile.Metadata.CoverArt.GetData();
                fileStream.Write(data, 0, data.Length);
            }

            return result;
        }
    }
}