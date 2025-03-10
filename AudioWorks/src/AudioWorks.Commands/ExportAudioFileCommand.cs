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
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using AudioWorks.Api;
using AudioWorks.Common;

namespace AudioWorks.Commands
{
    [Cmdlet(VerbsData.Export, "AudioFile"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class ExportAudioFileCommand : LoggingPSCmdlet, IDynamicParameters, IDisposable
    {
        readonly CancellationTokenSource _cancellationSource = new();
        readonly List<ITaggedAudioFile> _sourceAudioFiles = [];
        RuntimeDefinedParameterDictionary? _parameters;

        [Parameter(Mandatory = true, Position = 0)]
        [ArgumentCompleter(typeof(EncoderCompleter))]
        public string? Encoder { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        public string? Path { get; set; }

        [Parameter(Mandatory = true, Position = 2, ValueFromPipeline = true)]
        public ITaggedAudioFile? AudioFile { get; set; }

        [Parameter]
        public string? Name { get; set; }

        [Parameter]
        public SwitchParameter Force { get; set; }

        [Parameter, ValidateRange(1, int.MaxValue)]
        public int MaxDegreeOfParallelism { get; set; } = (int) Math.Round(Environment.ProcessorCount * 1.5);

        protected override void ProcessRecord() => _sourceAudioFiles.Add(AudioFile!);

        protected override void EndProcessing()
        {
            var encoder = new AudioFileEncoder(Encoder!) {
                EncodedDirectoryName = SessionState.Path.GetUnresolvedProviderPathFromPSPath(Path),
                EncodedFileName = Name ?? string.Empty,
                Settings = SettingAdapter.ParametersToSettings(_parameters),
                Overwrite = Force,
                MaxDegreeOfParallelism = MaxDegreeOfParallelism
            };

            var activityId = RandomNumberGenerator.GetInt32(20, int.MaxValue);
            var activity = $"Encoding {_sourceAudioFiles.Count} audio files in {Encoder} format";
            var totalFrames = (double) _sourceAudioFiles.Sum(audioFile => audioFile.Info.FrameCount);
            var lastAudioFilesCompleted = 0;
            var lastPercentComplete = 0;

            using (var messageQueue = new BlockingCollection<object>())
            {
                var progress = new SimpleProgress<ProgressToken>(token =>
                {
                    var percentComplete = (int) Math.Round(token.FramesCompleted / totalFrames * 100);

                    // Only report progress if something has changed
                    if (percentComplete <= lastPercentComplete && token.AudioFilesCompleted <= lastAudioFilesCompleted)
                        return;

                    lastAudioFilesCompleted = token.AudioFilesCompleted;
                    lastPercentComplete = percentComplete;

                    // ReSharper disable once AccessToDisposedClosure
                    messageQueue.Add(new ProgressRecord(activityId, activity,
                        $"{token.AudioFilesCompleted} of {_sourceAudioFiles.Count} audio files encoded")
                    {
                        // If the audio files have estimated frame counts, make sure this doesn't go over 100%
                        PercentComplete = Math.Min(percentComplete, 100)
                    });

                    // Send any new log messages to the output queue
                    while (LoggerProvider.TryDequeueMessage(out var logMessage))
                        // ReSharper disable once AccessToDisposedClosure
                        messageQueue.Add(logMessage);
                });

                var encodeTask = encoder.EncodeAsync(_sourceAudioFiles, _cancellationSource.Token, progress);
                // ReSharper disable once AccessToDisposedClosure
                encodeTask.ContinueWith(_ => messageQueue.CompleteAdding(), TaskScheduler.Current);

                this.OutputMessages(messageQueue, _cancellationSource.Token);

                // Remove the progress bar
                WriteProgress(new ProgressRecord(activityId) { RecordType = ProgressRecordType.Completed });

                try
                {
                    WriteObject(encodeTask.Result, true);
                }
                catch (AggregateException e)
                {
                    throw e.GetBaseException();
                }
            }
        }

        protected override void StopProcessing() => _cancellationSource.Cancel();

        public object? GetDynamicParameters()
        {
            // AudioFile parameter may not be bound yet
            if (Encoder == null) return null;

            return _parameters = SettingAdapter.SettingInfoToParameters(
                AudioEncoderManager.GetSettingInfo(Encoder));
        }

        public void Dispose() => _cancellationSource.Dispose();
    }
}
