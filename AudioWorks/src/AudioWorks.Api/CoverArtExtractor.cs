/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.IO;
using AudioWorks.Common;

namespace AudioWorks.Api
{
    /// <summary>
    /// Extracts cover art from audio files.
    /// </summary>
    public sealed class CoverArtExtractor
    {
        readonly EncodedPath? _encodedFileName;
        readonly EncodedPath? _encodedDirectoryName;

        /// <summary>
        /// Gets or sets a value indicating whether existing files should be overwritten.
        /// </summary>
        /// <value><c>true</c> if files should be overwritten; otherwise, <c>false</c>.</value>
        // ReSharper disable once PropertyCanBeMadeInitOnly.Global
        public bool Overwrite { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoverArtExtractor"/> class.
        /// </summary>
        /// <param name="encodedDirectoryName">The encoded directory name, or null.</param>
        /// <param name="encodedFileName">The encode file name, or null.</param>
        public CoverArtExtractor(string? encodedDirectoryName = null, string? encodedFileName = null)
        {
            if (encodedDirectoryName != null)
                _encodedDirectoryName = new(encodedDirectoryName);
            if (encodedFileName != null)
                _encodedFileName = new(encodedFileName);
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
        public FileInfo? Extract(ITaggedAudioFile audioFile)
        {
            if (audioFile == null) throw new ArgumentNullException(nameof(audioFile));

            if (audioFile.Metadata.CoverArt == null) return null;

            // The output directory defaults to the AudioFile's current directory
            var outputDirectory = _encodedDirectoryName?.ReplaceWith(audioFile.Metadata) ??
                                  Path.GetDirectoryName(audioFile.Path)!;

            var outputDirectoryInfo = Directory.CreateDirectory(outputDirectory);

            // The output file names default to the input file names
            var outputFileName = _encodedFileName?.ReplaceWith(audioFile.Metadata) ??
                                 Path.GetFileNameWithoutExtension(audioFile.Path);

            var result = new FileInfo(Path.Combine(outputDirectoryInfo.FullName,
                outputFileName + audioFile.Metadata.CoverArt.FileExtension));

            using (var fileStream = result.Open(Overwrite ? FileMode.Create : FileMode.CreateNew, FileAccess.Write))
                fileStream.Write(audioFile.Metadata.CoverArt.Data);

            return result;
        }
    }
}