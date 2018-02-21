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
        [CanBeNull] readonly MetadataSubstituter _fileNameSubstituter;
        [CanBeNull] readonly MetadataSubstituter _directoryNameSubstituter;
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
            if (encodedDirectoryName != null)
                _directoryNameSubstituter = new DirectoryNameSubstituter(encodedDirectoryName);
            if (encodedFileName != null)
                _fileNameSubstituter = new FileNameSubstituter(encodedFileName);
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
            var outputDirectory = _directoryNameSubstituter?.Substitute(audioFile.Metadata) ??
                                  Path.GetDirectoryName(audioFile.Path);

            Directory.CreateDirectory(outputDirectory);

            // The output file names default to the input file names
            var outputFileName = _fileNameSubstituter?.Substitute(audioFile.Metadata) ??
                                 Path.GetFileNameWithoutExtension(audioFile.Path);

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