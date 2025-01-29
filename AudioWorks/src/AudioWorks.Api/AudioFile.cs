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
using AudioWorks.Extensibility;
using Microsoft.Extensions.Logging;
using IO = System.IO;

namespace AudioWorks.Api
{
    /// <inheritdoc/>
    [Serializable]
    public class AudioFile : IAudioFile
    {
        /// <inheritdoc/>
        public string Path { get; private set; }

        /// <inheritdoc/>
        public AudioInfo Info { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFile"/> class.
        /// </summary>
        /// <param name="path">The fully-qualified path to the file.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="path"/> is empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown if <paramref name="path"/> does not exist.</exception>
        /// <exception cref="DirectoryNotFoundException">Throw in the directory does not exist.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown if <paramref name="path"/> cannot be accessed due to
        /// permissions.</exception>
        /// <exception cref="PathTooLongException">Thrown if <paramref name="path"/> is too long.</exception>
        public AudioFile(string path)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(path));

            Path = IO.Path.GetFullPath(path);
            Info = LoadInfo();
        }

        /// <inheritdoc/>
        public virtual void Rename(string name, bool replace)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));

            // If the name isn't changing, do nothing
            if (name.Equals(IO.Path.GetFileNameWithoutExtension(Path), StringComparison.OrdinalIgnoreCase))
                return;

            var newPath = IO.Path.Combine(IO.Path.GetDirectoryName(Path)!, name + IO.Path.GetExtension(Path));

            if (File.Exists(newPath) && replace)
                File.Delete(newPath);
            File.Move(Path, newPath);
            Path = newPath;
        }

        AudioInfo LoadInfo()
        {
            var logger = LoggerManager.LoggerFactory.CreateLogger<AudioFile>();

            // Try each info decoder that supports this file extension
            using (var fileStream = File.OpenRead(Path))
                foreach (var factory in ExtensionProviderWrapper.GetFactories<IAudioInfoDecoder>(
                    "Extension", IO.Path.GetExtension(Path)))
                {
                    var format = string.Empty;
                    try
                    {
                        using (var export = factory.CreateExport())
                        {
                            format = export.Value.Format;
                            logger.LogDebug("Attempting to decode '{path}' as '{format}'.", Path, format);
                            return export.Value.ReadAudioInfo(fileStream);
                        }
                    }
                    catch (AudioUnsupportedException e)
                    {
                        logger.LogDebug(e, "Unable to decode '{path}' as '{format}'.", Path, format);

                        // If a decoder wasn't supported, rewind the stream and try another
                        fileStream.Position = 0;
                    }
                }

            throw new AudioUnsupportedException($"Unable to decode '{Path}' with any loaded extension.");
        }
    }
}
