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
using System.Linq;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using AudioWorks.Api;
using AudioWorks.Common;

namespace AudioWorks.Commands
{
    [Cmdlet(VerbsDiagnostic.Measure, "AudioFile"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class MeasureAudioFileCommand : LoggingCmdlet, IDynamicParameters, IDisposable
    {
        readonly CancellationTokenSource _cancellationSource = new();
        readonly List<ITaggedAudioFile> _audioFiles = [];
        RuntimeDefinedParameterDictionary? _parameters;

        [Parameter(Mandatory = true, Position = 0)]
        [ArgumentCompleter(typeof(AnalyzerCompleter))]
        public string? Analyzer { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true)]
        public ITaggedAudioFile? AudioFile { get; set; }

        [Parameter, ValidateRange(1, int.MaxValue)]
        public int MaxDegreeOfParallelism { get; set; } = Environment.ProcessorCount;

        [Parameter]
        public SwitchParameter PassThru { get; set; }

        protected override void ProcessRecord() => _audioFiles.Add(AudioFile!);

        protected override void EndProcessing()
        {
            var analyzer = new AudioFileAnalyzer(Analyzer!, SettingAdapter.ParametersToSettings(_parameters))
                { MaxDegreeOfParallelism = MaxDegreeOfParallelism };

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

                var analyzeTask = analyzer.AnalyzeAsync(_audioFiles, _cancellationSource.Token, progress);
                // ReSharper disable once AccessToDisposedClosure
                analyzeTask.ContinueWith(_ => messageQueue.CompleteAdding(), TaskScheduler.Current);

                this.OutputMessages(messageQueue, _cancellationSource.Token);

                if (analyzeTask.Exception != null)
                    throw analyzeTask.Exception.GetBaseException();
            }

            if (PassThru)
                WriteObject(_audioFiles, true);
        }

        protected override void StopProcessing() => _cancellationSource.Cancel();

        public object? GetDynamicParameters()
        {
            // AudioFile parameter may not be bound yet
            if (Analyzer == null) return null;

            return _parameters = SettingAdapter.SettingInfoToParameters(
                AudioAnalyzerManager.GetSettingInfo(Analyzer));
        }

        public void Dispose() => _cancellationSource.Dispose();
    }
}
