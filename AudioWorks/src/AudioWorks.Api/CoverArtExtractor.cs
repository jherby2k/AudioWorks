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
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Extracts cover art from audio files.
    /// </summary>
    public sealed class CoverArtExtractor
    {
        [CanBeNull] readonly EncodedPath _encodedFileName;
        [CanBeNull] readonly EncodedPath _encodedDirectoryName;
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
                _encodedDirectoryName = new EncodedPath(encodedDirectoryName);
            if (encodedFileName != null)
                _encodedFileName = new EncodedPath(encodedFileName);
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

            // The output directory defaults to the AudioFile's current directory
            var outputDirectory = _encodedDirectoryName?.ReplaceWith(audioFile.Metadata) ??
                                  Path.GetDirectoryName(audioFile.Path);

            // ReSharper disable once AssignNullToNotNullAttribute
            var outputDirectoryInfo = Directory.CreateDirectory(outputDirectory);

            // The output file names default to the input file names
            var outputFileName = _encodedFileName?.ReplaceWith(audioFile.Metadata) ??
                                 Path.GetFileNameWithoutExtension(audioFile.Path);

            var result = new FileInfo(Path.Combine(outputDirectoryInfo.FullName,
                outputFileName + audioFile.Metadata.CoverArt.FileExtension));

            using (var fileStream = result.Open(_overwrite ? FileMode.Create : FileMode.CreateNew, FileAccess.Write))
            {
#if NETCOREAPP2_1
                fileStream.Write(audioFile.Metadata.CoverArt.Data);
#else
                var data = audioFile.Metadata.CoverArt.Data;
                fileStream.Write(data.ToArray(), 0, data.Length);
#endif
            }

            return result;
        }
    }
}