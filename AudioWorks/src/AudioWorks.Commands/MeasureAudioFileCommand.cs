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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using AudioWorks.Api;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsDiagnostic.Measure, "AudioFile"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class MeasureAudioFileCommand : LoggingCmdlet, IDynamicParameters, IDisposable
    {
        [NotNull] readonly CancellationTokenSource _cancellationSource = new CancellationTokenSource();
        [NotNull] readonly List<ITaggedAudioFile> _audioFiles = new List<ITaggedAudioFile>();
        [CanBeNull] RuntimeDefinedParameterDictionary _parameters;

        [CanBeNull]
        [Parameter(Mandatory = true, Position = 0)]
        [ArgumentCompleter(typeof(AnalyzerCompleter))]
        public string Analyzer { get; set; }

        [CanBeNull]
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true)]
        public ITaggedAudioFile AudioFile { get; set; }

        [Parameter]
        public SwitchParameter PassThru { get; set; }

        protected override void ProcessRecord()
        {
            _audioFiles.Add(AudioFile);
        }

        protected override void EndProcessing()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var analyzer = new AudioFileAnalyzer(Analyzer, SettingAdapter.ParametersToSettings(_parameters));

            var activity = $"Performing {Analyzer} analysis on {_audioFiles.Count} audio files";
            var totalFrames = (double) _audioFiles.Sum(audioFile => audioFile.Info.FrameCount);
            var lastAudioFilesCompleted = 0;
            var lastPercentComplete = 0;

            using (var messageQueue = new BlockingCollection<object>())
            {
                var progress = new SimpleProgress<ProgressToken>(token =>
                {
                    var percentComplete = (int) Math.Round(token.FramesCompleted / totalFrames * 100);

                    // Avoid reporting progress when nothing has changed
                    if (percentComplete <= lastPercentComplete && token.AudioFilesCompleted <= lastAudioFilesCompleted)
                        return;

                    lastAudioFilesCompleted = token.AudioFilesCompleted;
                    lastPercentComplete = percentComplete;

                    // ReSharper disable once AccessToDisposedClosure
                    messageQueue.Add(new ProgressRecord(0, activity,
                        $"{token.AudioFilesCompleted} of {_audioFiles.Count} audio files analyzed")
                    {
                        // If the audio files have estimated frame counts, make sure this doesn't go over 100%
                        PercentComplete = Math.Min(percentComplete, 100)
                    });

                    // Send any new log messages to the output queue
                    while (LoggerProvider.TryDequeueMessage(out var logMessage))
                        // ReSharper disable once AccessToDisposedClosure
                        messageQueue.Add(logMessage);
                });

                analyzer.AnalyzeAsync(progress, _cancellationSource.Token, _audioFiles.ToArray())
                    // ReSharper disable once AccessToDisposedClosure
                    .ContinueWith(task => messageQueue.CompleteAdding(), TaskScheduler.Current);

                this.OutputMessages(messageQueue, _cancellationSource.Token);
            }

            if (PassThru)
                WriteObject(_audioFiles, true);
        }

        protected override void StopProcessing()
        {
            _cancellationSource.Cancel();
        }

        [CanBeNull]
        public object GetDynamicParameters()
        {
            // AudioFile parameter may not be bound yet
            if (Analyzer == null) return null;

            return _parameters = SettingAdapter.SettingInfoToParameters(
                AudioAnalyzerManager.GetSettingInfo(Analyzer));
        }

        public void Dispose()
        {
            _cancellationSource.Dispose();
        }
    }
}
