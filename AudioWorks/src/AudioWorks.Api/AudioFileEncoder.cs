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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AudioWorks.Common;
using AudioWorks.Extensibility;

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
        int _maxDegreeOfParallelism = Environment.ProcessorCount;

        /// <summary>
        /// Gets or sets a value indicating whether existing files should be overwritten.
        /// </summary>
        /// <value><c>true</c> if files should be overwritten; otherwise, <c>false</c>.</value>
        public bool Overwrite { get; set; }

        /// <summary>
        /// Gets or sets the maximum degree of parallelism. The default value is equal to
        /// <see cref="Environment.ProcessorCount"/>.
        /// </summary>
        /// <value>The maximum degree of parallelism.</value>
        /// <exception cref="ArgumentOutOfRangeException">Throw in <paramref name="value"/> is less than 1.</exception>
        public int MaxDegreeOfParallelism
        {
            get => _maxDegreeOfParallelism;
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Minimum value is 1.");

                _maxDegreeOfParallelism = value;
            }
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public SettingDictionary Settings { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFileEncoder"/> class.
        /// </summary>
        /// <param name="name">The name of the encoder.</param>
        /// <param name="encodedDirectoryName">The encoded directory name, or null.</param>
        /// <param name="encodedFileName">The encode file name, or null.</param>
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="name"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <see paramref="name"/> is not the name of an available encoder.
        /// </exception>
        public AudioFileEncoder(
            string name,
            string? encodedDirectoryName = null,
            string? encodedFileName = null,
            SettingDictionary? settings = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            _encoderFactory = ExtensionProviderWrapper.GetFactories<IAudioEncoder>("Name", name).SingleOrDefault() ??
                              throw new ArgumentException($"No '{name}' encoder is available.", nameof(name));

            if (encodedDirectoryName != null)
                _encodedDirectoryName = new EncodedPath(encodedDirectoryName);
            if (encodedFileName != null)
                _encodedFileName = new EncodedPath(encodedFileName);

            using (var export = _encoderFactory.CreateExport())
                Settings = new ValidatingSettingDictionary(export.Value.SettingInfo, settings);
        }

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The audio files.</param>
        /// <returns>A new audio file.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task<IEnumerable<ITaggedAudioFile>> EncodeAsync(IEnumerable<ITaggedAudioFile> audioFiles)
        {
            if (audioFiles == null) throw new ArgumentNullException(nameof(audioFiles));

            return await EncodeAsync(audioFiles.ToArray()).ConfigureAwait(false);
        }

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The audio files.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="progress">The progress queue, or <c>null</c>.</param>
        /// <returns>A new audio file.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task<IEnumerable<ITaggedAudioFile>> EncodeAsync(
            IEnumerable<ITaggedAudioFile> audioFiles,
            CancellationToken cancellationToken,
            IProgress<ProgressToken>? progress = null)
        {
            if (audioFiles == null) throw new ArgumentNullException(nameof(audioFiles));

            return await EncodeAsync(progress, cancellationToken, audioFiles.ToArray()).ConfigureAwait(false);
        }

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The audio files.</param>
        /// <returns>A new audio file.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task<IEnumerable<ITaggedAudioFile>> EncodeAsync(params ITaggedAudioFile[] audioFiles) =>
            await EncodeAsync(null, CancellationToken.None, audioFiles).ConfigureAwait(false);

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The audio files.</param>
        /// <returns>A new audio file.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task<IEnumerable<ITaggedAudioFile>> EncodeAsync(
            CancellationToken cancellationToken,
            params ITaggedAudioFile[] audioFiles) =>
            await EncodeAsync(null, cancellationToken, audioFiles).ConfigureAwait(false);

        /// <summary>
        /// Encodes the specified audio files.
        /// </summary>
        /// <param name="progress">The progress queue, or <c>null</c>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The audio files.</param>
        /// <returns>A new audio file.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        [SuppressMessage("Maintainability", "CA1506:Avoid excessive class coupling", Justification =
            "Method is considered maintainable.")]
        public async Task<IEnumerable<ITaggedAudioFile>> EncodeAsync(
            IProgress<ProgressToken>? progress,
            CancellationToken cancellationToken,
            params ITaggedAudioFile[] audioFiles)
        {
            if (audioFiles == null) throw new ArgumentNullException(nameof(audioFiles));
            if (audioFiles.Any(audioFile => audioFile == null))
                throw new ArgumentException("One or more audio files are null.", nameof(audioFiles));

            progress?.Report(new ProgressToken
            {
                AudioFilesCompleted = 0,
                FramesCompleted = 0
            });

            var audioFilesCompleted = 0;
            var totalFramesCompleted = 0L;

            // Encoding can happen in parallel
            var encodeBlock = new TransformBlock<(ITaggedAudioFile audioFile, string outputPath), ITaggedAudioFile>(
                message =>
                {
                    var tempOutputPath = Path.Combine(
                        Path.GetDirectoryName(message.outputPath)!,
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
                                message.audioFile.Info,
                                new AudioMetadata(message.audioFile.Metadata),
                                Settings);

                            encoderExport.Value.ProcessSamples(
                                message.audioFile.Path,
                                progress == null
                                    ? null
                                    : new SimpleProgress<int>(framesCompleted => progress.Report(new ProgressToken
                                    {
                                        // ReSharper disable once AccessToModifiedClosure
                                        AudioFilesCompleted = audioFilesCompleted,
                                        FramesCompleted = Interlocked.Add(ref totalFramesCompleted, framesCompleted)
                                    })),
                                cancellationToken);

                            encoderExport.Value.Finish();

                            Interlocked.Increment(ref audioFilesCompleted);
                        }
                        finally
                        {
                            // Dispose the encoder before closing the stream
                            encoderExport.Dispose();
                            outputStream?.Dispose();
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
                    File.Delete(message.outputPath);
                    File.Move(tempOutputPath, message.outputPath);

                    return new TaggedAudioFile(message.outputPath);
                },
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = MaxDegreeOfParallelism,
                    SingleProducerConstrained = true,
                    CancellationToken = cancellationToken
                });

            var batchBlock = new BatchBlock<ITaggedAudioFile>(audioFiles.Length);
            encodeBlock.LinkTo(batchBlock, new DataflowLinkOptions { PropagateCompletion = true });

            string outputExtension;
            using (var export = _encoderFactory.CreateExport())
                outputExtension = export.Value.FileExtension;
            var uniqueOutputPaths = new List<string>(audioFiles.Length);

            // File names need to be worked out sequentially, in case of conflicts
            foreach (var audioFile in audioFiles)
            {
                var outputPath = GetUniquePath(Path.Combine(
                    Directory.CreateDirectory(_encodedDirectoryName?.ReplaceWith(audioFile.Metadata) ??
                                              Path.GetDirectoryName(audioFile.Path)!).FullName,
                    (_encodedFileName?.ReplaceWith(audioFile.Metadata) ??
                     Path.GetFileNameWithoutExtension(audioFile.Path)) + outputExtension), uniqueOutputPaths);

                if (File.Exists(outputPath) && !Overwrite)
                    throw new IOException($"The file '{outputPath}' already exists.");

                await encodeBlock.SendAsync((audioFile, outputPath), cancellationToken).ConfigureAwait(false);
            }

            try
            {
                var result = await batchBlock.ReceiveAsync(cancellationToken).ConfigureAwait(false);

                progress?.Report(new ProgressToken
                {
                    AudioFilesCompleted = audioFilesCompleted,
                    FramesCompleted = totalFramesCompleted
                });

                return result;
            }
            catch (InvalidOperationException)
            {
                // Throw the real exception that caused the pipeline to cancel
                if (batchBlock.Completion.Exception != null)
                    throw batchBlock.Completion.Exception.GetBaseException();
                throw;
            }
        }

        static string GetUniquePath(string path, ICollection<string> uniquePaths)
        {
            if (uniquePaths.Contains(path, StringComparer.OrdinalIgnoreCase))
                path = $"{Path.ChangeExtension(path, null)}~{uniquePaths.Count}{Path.GetExtension(path)}";

            uniquePaths.Add(path);
            return path;
        }
    }
}
