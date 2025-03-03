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
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Api
{
    /// <summary>
    /// Performs analysis on one or more audio files.
    /// </summary>
    public sealed class AudioFileAnalyzer
    {
        readonly ExportFactory<IAudioAnalyzer> _analyzerFactory;
        readonly int _maxDegreeOfParallelism = (int) Math.Round(Environment.ProcessorCount * 1.5);
        readonly SettingDictionary _settings;

        /// <summary>
        /// Gets or sets the maximum degree of parallelism. The default value is equal to
        /// <see cref="Environment.ProcessorCount"/> * 1.5.
        /// </summary>
        /// <value>The maximum degree of parallelism.</value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is less than 1.</exception>
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
        /// Gets or sets the analyzer settings.
        /// </summary>
        /// <value>The settings.</value>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="value"/> is null.</exception>
        public SettingDictionary Settings
        {
            get => _settings;
            init
            {
                ArgumentNullException.ThrowIfNull(value);

                using (var export = _analyzerFactory.CreateExport())
                    _settings = new ValidatingSettingDictionary(export.Value.SettingInfo, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFileAnalyzer"/> class.
        /// </summary>
        /// <param name="name">The name of the analyzer.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="name"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <see paramref="name"/> is not the name of an available analyzer.
        /// </exception>
        public AudioFileAnalyzer(string name)
        {
            ArgumentNullException.ThrowIfNull(name);

            _analyzerFactory = ExtensionProviderWrapper.GetFactories<IAudioAnalyzer>("Name", name).SingleOrDefault() ??
                               throw new ArgumentException($"No '{name}' analyzer is available.", nameof(name));

            using (var export = _analyzerFactory.CreateExport())
                _settings = new ValidatingSettingDictionary(export.Value.SettingInfo, []);
        }

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The audio files.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task AnalyzeAsync(IEnumerable<ITaggedAudioFile> audioFiles)
        {
            ArgumentNullException.ThrowIfNull(audioFiles);

            await AnalyzeAsync([.. audioFiles]).ConfigureAwait(false);
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
            IEnumerable<ITaggedAudioFile> audioFiles,
            CancellationToken cancellationToken,
            IProgress<ProgressToken>? progress = null)
        {
            ArgumentNullException.ThrowIfNull(audioFiles);

            await AnalyzeAsync(progress, cancellationToken, [.. audioFiles]).ConfigureAwait(false);
        }

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="audioFiles">The audio files.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task AnalyzeAsync(params ITaggedAudioFile[] audioFiles) =>
            await AnalyzeAsync(null, CancellationToken.None, audioFiles).ConfigureAwait(false);

        /// <summary>
        /// Analyzes the specified audio files.
        /// </summary>
        /// <param name="progress">The progress queue, or <c>null</c>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="audioFiles">The audio files.</param>
        /// <exception cref="ArgumentNullException">Thrown if <see paramref="audioFiles"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one or more audio files are null.</exception>
        public async Task AnalyzeAsync(
            IProgress<ProgressToken>? progress,
            CancellationToken cancellationToken,
            params ITaggedAudioFile[] audioFiles)
        {
            ArgumentNullException.ThrowIfNull(audioFiles);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (audioFiles.Any(audioFile => audioFile == null))
                throw new ArgumentException("One or more audio files are null.", nameof(audioFiles));

            progress?.Report(new() { AudioFilesCompleted = 0, FramesCompleted = 0 });

            var audioFilesCompleted = 0;
            var totalFramesCompleted = 0L;

            using (var groupToken = new GroupToken())
            {
                var analyzerExports = new Export<IAudioAnalyzer>[audioFiles.Length];

                try
                {
                    // Initialization should be sequential
                    for (var i = 0; i < audioFiles.Length; i++)
                    {
                        var export = _analyzerFactory.CreateExport();
                        export.Value.Initialize(audioFiles[i].Info, Settings, groupToken);
                        analyzerExports[i] = export;
                    }

                    // Analysis can happen in parallel
                    await Parallel.ForAsync(0, audioFiles.Length,
                        new ParallelOptions
                        {
                            CancellationToken = cancellationToken,
                            MaxDegreeOfParallelism = MaxDegreeOfParallelism
                        },
                        async (i, c) =>
                        {
                            var analyzer = analyzerExports[i].Value;

                            await analyzer.ProcessSamples(
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

                            CopyStringProperties(analyzer.GetResult(), audioFiles[i].Metadata);

                            Interlocked.Increment(ref audioFilesCompleted);
                        }).ConfigureAwait(false);

                    for (var i = 0; i < audioFiles.Length; i++)
                        CopyStringProperties(analyzerExports[i].Value.GetGroupResult(), audioFiles[i].Metadata);
                }
                finally
                {
                    foreach (var export in analyzerExports)
                        export.Dispose();
                }
            }

            progress?.Report(new()
            {
                AudioFilesCompleted = audioFilesCompleted,
                FramesCompleted = totalFramesCompleted
            });
        }

        static void CopyStringProperties(AudioMetadata source, AudioMetadata destination)
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