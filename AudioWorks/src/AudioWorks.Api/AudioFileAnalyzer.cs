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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AudioWorks.Common;
using AudioWorks.Extensibility;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    /// <summary>
    /// Performs analysis on one or more audio files.
    /// </summary>
    [PublicAPI]
    public sealed class AudioFileAnalyzer
    {
        [NotNull] readonly ExportFactory<IAudioAnalyzer> _analyzerFactory;
        int _maxDegreeOfParallelism = Environment.ProcessorCount;

        /// <summary>
        /// Gets or sets the maximum degree of parallelism. The default value is equal to
        /// <see cref="Environment.ProcessorCount"/>.
        /// </summary>
        /// <value>The maximum degree of parallelism.</value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is less than 1.</exception>
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
        [NotNull]
        public SettingDictionary Settings { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFileAnalyzer"/> class.
        /// </summary>
        /// <param name="name">The name of the analyzer.</param>
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="name"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <see paramref="name"/> is not the name of an available analyzer.
        /// </exception>
        public AudioFileAnalyzer([NotNull] string name, [CanBeNull] SettingDictionary settings = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            _analyzerFactory = ExtensionProviderWrapper.GetFactories<IAudioAnalyzer>("Name", name).SingleOrDefault() ??
                               throw new ArgumentException($"No '{name}' analyzer is available.", nameof(name));

            using (var export = _analyzerFactory.CreateExport())
                Settings = new ValidatingSettingDictionary(export.Value.SettingInfo, settings);
        }

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The audio files.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task AnalyzeAsync(
            [NotNull, ItemNotNull] IEnumerable<ITaggedAudioFile> audioFiles)
        {
            if (audioFiles == null) throw new ArgumentNullException(nameof(audioFiles));

            await AnalyzeAsync(audioFiles.ToArray()).ConfigureAwait(false);
        }

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The audio files.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="progress">The progress queue, or <c>null</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task AnalyzeAsync(
            [NotNull, ItemNotNull] IEnumerable<ITaggedAudioFile> audioFiles,
            CancellationToken cancellationToken,
            [CanBeNull] IProgress<ProgressToken> progress = null)
        {
            if (audioFiles == null) throw new ArgumentNullException(nameof(audioFiles));

            await AnalyzeAsync(progress, cancellationToken, audioFiles.ToArray()).ConfigureAwait(false);
        }

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The audio files.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task AnalyzeAsync(
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            await AnalyzeAsync(CancellationToken.None, audioFiles).ConfigureAwait(false);
        }

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The audio files.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task AnalyzeAsync(
            CancellationToken cancellationToken,
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
        {
            await AnalyzeAsync(null, cancellationToken, audioFiles).ConfigureAwait(false);
        }

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="progress">The progress queue, or <c>null</c>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The audio files.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        [SuppressMessage("Maintainability", "CA1506:Avoid excessive class coupling", Justification =
            "Method is considered maintainable.")]
        public async Task AnalyzeAsync(
            [CanBeNull] IProgress<ProgressToken> progress,
            CancellationToken cancellationToken,
            [NotNull, ItemNotNull] params ITaggedAudioFile[] audioFiles)
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

            using (var groupToken = new GroupToken())
            {
                var disposableExports = new ConcurrentBag<IDisposable>();
                var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

                // Initialization should be sequential
                var initializeBlock = new TransformBlock<ITaggedAudioFile, (ITaggedAudioFile, IAudioAnalyzer)>(
                    audioFile =>
                    {
                        var analyzerExport = _analyzerFactory.CreateExport();
                        disposableExports.Add(analyzerExport);
                        // ReSharper disable once AccessToDisposedClosure
                        analyzerExport.Value.Initialize(audioFile.Info, Settings, groupToken);
                        return (audioFile, analyzerExport.Value);
                    },
                    new ExecutionDataflowBlockOptions
                    {
                        SingleProducerConstrained = true,
                        CancellationToken = cancellationToken
                    });

                // Analysis can happen in parallel
                var analyzeBlock = new TransformBlock<
                    (ITaggedAudioFile audioFile, IAudioAnalyzer analyzer), (ITaggedAudioFile, IAudioAnalyzer)>(
                    message =>
                    {
                        message.analyzer.ProcessSamples(
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

                        CopyStringProperties(message.analyzer.GetResult(), message.audioFile.Metadata);

                        Interlocked.Increment(ref audioFilesCompleted);

                        return message;
                    },
                    new ExecutionDataflowBlockOptions
                    {
                        MaxDegreeOfParallelism = MaxDegreeOfParallelism,
                        SingleProducerConstrained = true
                    });
                initializeBlock.LinkTo(analyzeBlock, linkOptions);

                var batchBlock = new BatchBlock<(ITaggedAudioFile, IAudioAnalyzer)>(audioFiles.Length);
                analyzeBlock.LinkTo(batchBlock, linkOptions);

                var groupResultBlock = new ActionBlock<(ITaggedAudioFile, IAudioAnalyzer)[]>(group =>
                    {
                        foreach (var (audioFile, analyzer) in group)
                            CopyStringProperties(analyzer.GetGroupResult(), audioFile.Metadata);
                    },
                    new ExecutionDataflowBlockOptions { SingleProducerConstrained = true });
                batchBlock.LinkTo(groupResultBlock, linkOptions);

                try
                {
                    foreach (var audioFile in audioFiles)
                        await initializeBlock.SendAsync(audioFile, cancellationToken).ConfigureAwait(false);
                    initializeBlock.Complete();

                    await groupResultBlock.Completion.ConfigureAwait(false);
                }
                catch (AggregateException e)
                {
                    throw e.GetBaseException();
                }
                finally
                {
                    foreach (var export in disposableExports)
                        export.Dispose();
                }
            }

            progress?.Report(new ProgressToken
            {
                AudioFilesCompleted = audioFilesCompleted,
                FramesCompleted = totalFramesCompleted
            });
        }

        static void CopyStringProperties([NotNull] AudioMetadata source, [NotNull] AudioMetadata destination)
        {
            // Copy every non-blank string property from source to destination
            foreach (var property in typeof(AudioMetadata).GetProperties())
            {
                var value = property.GetValue(source);
                if (value == null || value is string stringValue && string.IsNullOrEmpty(stringValue)) continue;
                property.SetValue(destination, value);
            }
        }
    }
}