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
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Api
{
    /// <summary>
    /// Encodes one or more audio files in a new audio format.
    /// </summary>
    public sealed class AudioFileEncoder
    {
        readonly EncodedPath? _encodedFileName;
        readonly EncodedPath? _encodedDirectoryName;
        readonly ExportFactory<IAudioEncoder> _encoderFactory;
        readonly int _maxDegreeOfParallelism = (int) Math.Round(Environment.ProcessorCount * 1.5);
        readonly SettingDictionary _settings;

        /// <summary>
        /// Gets or sets the encoded file name.
        /// </summary>
        /// <value>The encoded file name, if set; otherwise, an empty string.</value>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is not properly formatted.
        /// </exception>
        public string EncodedFileName
        {
            get => _encodedFileName?.ToString() ?? string.Empty;
            init => _encodedFileName = string.IsNullOrEmpty(value) ? null : new(value);
        }

        /// <summary>
        /// Gets or sets the encoded directory name.
        /// </summary>
        /// <value>The encoded directory name, if set; otherwise, an empty string.</value>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is not properly formatted.
        /// </exception>
        public string EncodedDirectoryName
        {
            get => _encodedDirectoryName?.ToString() ?? string.Empty;
            init => _encodedDirectoryName = new(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether existing files should be overwritten.
        /// </summary>
        /// <value><c>true</c> if files should be overwritten; otherwise, <c>false</c>.</value>
        public bool Overwrite { get; init; }

        /// <summary>
        /// Gets or sets the maximum degree of parallelism. The default value is equal to
        /// <see cref="Environment.ProcessorCount"/> * 1.5.
        /// </summary>
        /// <value>The maximum degree of parallelism.</value>
        /// <exception cref="ArgumentOutOfRangeException">Throw in <paramref name="value"/> is less than 1.</exception>
        public int MaxDegreeOfParallelism
        {
            get => _maxDegreeOfParallelism;
            init
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);

                _maxDegreeOfParallelism = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether various performance optimizations will be applied. This defaults to
        /// true, and should only be disabled for testing or troubleshooting purposes.
        /// </summary>
        /// <value><c>false</c> if optimizations should be skipped; otherwise, <c>true</c>.</value>
        public bool UseOptimizations { get; init; } = true;

        /// <summary>
        /// Gets or sets the encoder settings.
        /// </summary>
        /// <value>The settings.</value>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="value"/> is null.</exception>
        public SettingDictionary Settings
        {
            get => _settings;
            init
            {
                ArgumentNullException.ThrowIfNull(value);

                using (var export = _encoderFactory.CreateExport())
                    _settings = new ValidatingSettingDictionary(export.Value.SettingInfo, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFileEncoder"/> class.
        /// </summary>
        /// <param name="name">The name of the encoder.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="name"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <see paramref="name"/> is not the name of an available encoder.
        /// </exception>
        public AudioFileEncoder(
            string name)
        {
            ArgumentNullException.ThrowIfNull(name);

            _encoderFactory = ExtensionProviderWrapper.GetFactories<IAudioEncoder>("Name", name).SingleOrDefault() ??
                              throw new ArgumentException($"No '{name}' encoder is available.", nameof(name));

            using (var export = _encoderFactory.CreateExport())
                _settings = new ValidatingSettingDictionary(export.Value.SettingInfo, []);
        }

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The source audio files.</param>
        /// <returns>The new audio files.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task<IEnumerable<ITaggedAudioFile>> EncodeAsync(IEnumerable<ITaggedAudioFile> audioFiles)
        {
            ArgumentNullException.ThrowIfNull(audioFiles);

            return await EncodeAsync([.. audioFiles]).ConfigureAwait(false);
        }

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The audio files.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="progress">The progress queue, or <c>null</c>.</param>
        /// <returns>The new audio files.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task<IEnumerable<ITaggedAudioFile>> EncodeAsync(
            IEnumerable<ITaggedAudioFile> audioFiles,
            CancellationToken cancellationToken,
            IProgress<ProgressToken>? progress = null)
        {
            ArgumentNullException.ThrowIfNull(audioFiles);

            return await EncodeAsync(progress, cancellationToken, [.. audioFiles]).ConfigureAwait(false);
        }

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The source audio files.</param>
        /// <returns>The new audio files.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task<IEnumerable<ITaggedAudioFile>> EncodeAsync(params ITaggedAudioFile[] audioFiles) =>
            await EncodeAsync(null, CancellationToken.None, audioFiles).ConfigureAwait(false);

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="progress">The progress queue, or <c>null</c>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The source audio files.</param>
        /// <returns>The new audio files.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task<IEnumerable<ITaggedAudioFile>> EncodeAsync(
            IProgress<ProgressToken>? progress,
            CancellationToken cancellationToken,
            params ITaggedAudioFile[] audioFiles)
        {
            ArgumentNullException.ThrowIfNull(audioFiles);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (audioFiles.Any(audioFile => audioFile == null))
                throw new ArgumentException("One or more audio files are null.", nameof(audioFiles));

            SampleBuffer.UseOptimizations = UseOptimizations;

            var logger = LoggerManager.LoggerFactory.CreateLogger<AudioFileEncoder>();
            logger.LogDebug("Preparing to encode {count} audio file(s).", audioFiles.Length);
            logger.LogDebug("Optimizations are {optimized}.", UseOptimizations ? "enabled" : "disabled");
            logger.LogDebug("Encoding up to {maxParallel} file(s) in parallel.",
                Math.Min(_maxDegreeOfParallelism, audioFiles.Length));

            progress?.Report(new() { AudioFilesCompleted = 0, FramesCompleted = 0 });

            var audioFilesCompleted = 0;
            var totalFramesCompleted = 0L;

            var outputPaths = GenerateOutputPaths(audioFiles);
            var results = new TaggedAudioFile[audioFiles.Length];

            await Parallel.ForAsync(0, audioFiles.Length,
                new ParallelOptions
                {
                    CancellationToken = cancellationToken,
                    MaxDegreeOfParallelism = MaxDegreeOfParallelism
                },
                async (i, c) =>
                {
                    var tempOutputPath = Path.Combine(
                        Path.GetDirectoryName(outputPaths[i])!,
                        Path.GetRandomFileName());

                    try
                    {
                        FileStream? outputStream = null;
                        var encoderExport = _encoderFactory.CreateExport();

                        try
                        {
                            outputStream = File.Open(tempOutputPath, FileMode.OpenOrCreate);

                            // Copy the source metadata, so it can't be modified
                            encoderExport.Value.Initialize(
                                outputStream,
                                audioFiles[i].Info,
                                new(audioFiles[i].Metadata),
                                Settings);

                            await encoderExport.Value.ProcessSamples(
                                audioFiles[i].Path,
                                progress == null
                                    ? null
                                    : new SimpleProgress<int>(framesCompleted => progress.Report(new()
                                    {
                                        // ReSharper disable once AccessToModifiedClosure
                                        AudioFilesCompleted = audioFilesCompleted,
                                        FramesCompleted = Interlocked.Add(ref totalFramesCompleted, framesCompleted)
                                    })),
                                c).ConfigureAwait(false);

                            encoderExport.Value.Finish();
                        }
                        finally
                        {
                            // Dispose the encoder before closing the stream
                            encoderExport.Dispose();
                            if (outputStream != null)
                                await outputStream.DisposeAsync().ConfigureAwait(false);
                        }
                    }
                    catch (Exception)
                    {
                        // Clean up output
                        if (File.Exists(tempOutputPath))
                            File.Delete(tempOutputPath);
                        throw;
                    }

                    // Rename the temporary file to the final name
                    File.Delete(outputPaths[i]);
                    File.Move(tempOutputPath, outputPaths[i]);

                    results[i] = new(outputPaths[i]);

                    Interlocked.Increment(ref audioFilesCompleted);
                }).ConfigureAwait(false);

            logger.LogDebug("Finished encoding {count} audio file(s).", audioFiles.Length);

            progress?.Report(new()
            {
                AudioFilesCompleted = audioFilesCompleted,
                FramesCompleted = totalFramesCompleted
            });

            return results;
        }

        string[] GenerateOutputPaths(ITaggedAudioFile[] audioFiles)
        {
            string fileExtension;
            using (var export = _encoderFactory.CreateExport())
                fileExtension = export.Value.FileExtension;

            var result = new List<string>(audioFiles.Length);

            // File names need to be worked out sequentially, in case of conflicts
            foreach (var audioFile in audioFiles)
            {
                var outputPath = GetUniquePath(Path.Combine(
                    Directory.CreateDirectory(_encodedDirectoryName?.ReplaceWith(audioFile.Metadata) ??
                                              Path.GetDirectoryName(audioFile.Path)!).FullName,
                    (_encodedFileName?.ReplaceWith(audioFile.Metadata) ??
                     Path.GetFileNameWithoutExtension(audioFile.Path)) + fileExtension), result);

                result.Add(outputPath);

                if (File.Exists(outputPath) && !Overwrite)
                    throw new IOException($"The file '{outputPath}' already exists.");
            }

            return [.. result];
        }

        static string GetUniquePath(string path, List<string> existingPaths) =>
            existingPaths.Contains(path, StringComparer.OrdinalIgnoreCase)
                ? $"{Path.ChangeExtension(path, null)}~{existingPaths.Count}{Path.GetExtension(path)}"
                : path;
    }
}
