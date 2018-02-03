using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using AudioWorks.Api;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    /// <summary>
    /// <para type="synopsis">Exports an audio file.</para>
    /// <para type="description">The Export-AudioFile cmdlet creates a new audio file using the specified encoder.
    /// </para>
    /// </summary>
    [PublicAPI]
    [Cmdlet(VerbsData.Export, "AudioFile", DefaultParameterSetName = "ByPath"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class ExportAudioFileCommand : PSCmdlet, IDynamicParameters, IDisposable
    {
        [NotNull] readonly CancellationTokenSource _cancellationSource = new CancellationTokenSource();
        [NotNull] readonly List<ITaggedAudioFile> _sourceAudioFiles = new List<ITaggedAudioFile>();
        [CanBeNull] RuntimeDefinedParameterDictionary _parameters;

        /// <summary>
        /// <para type="description">Specifies the encoder to use.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string Encoder { get; set; }

        /// <summary>
        /// <para type="description">Specifies the source audio file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true)]
        public ITaggedAudioFile AudioFile { get; set; }

        /// <summary>
        /// <para type="description">Specifies the output directory path.</para>
        /// </summary>
        [Parameter]
        public string Path { get; set; }

        /// <summary>
        /// <para type="description">Specifies the output file name.</para>
        /// <para type="description">The file extension will be selected automatically and should not be included. If
        /// this parameter is omitted, the name will be the same as the source audio file.</para>
        /// </summary>
        [Parameter]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Indicates that existing files should be replaced.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Replace { get; set; }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            _sourceAudioFiles.Add(AudioFile);
        }

        /// <inheritdoc/>
        protected override void EndProcessing()
        {
            using (var outputQueue = new BlockingCollection<ProgressToken>())
            {
                var encodeTask = Task<IEnumerable<ITaggedAudioFile>>.Factory.StartNew(q =>
                        new AudioFileEncoder(
                                Encoder,
                                SettingAdapter.ParametersToSettings(_parameters),
                                SessionState.Path.GetUnresolvedProviderPathFromPSPath(Path, out var _, out var _),
                                Name,
                                Replace)
                            .Encode(
                                (BlockingCollection<ProgressToken>) q,
                                _cancellationSource.Token,
                                _sourceAudioFiles.ToArray()),
                    outputQueue,
                    _cancellationSource.Token,
                    TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning,
                    TaskScheduler.Default);

                encodeTask.ContinueWith((t, q) =>
                        ((BlockingCollection<ProgressToken>) q).CompleteAdding(),
                    outputQueue,
                    TaskScheduler.Default);

                // Process output on the main thread
                foreach (var progressToken in outputQueue.GetConsumingEnumerable(_cancellationSource.Token))
                    WriteProgress(ProgressAdapter.TokenToRecord(progressToken));

                if (!encodeTask.IsCanceled)
                    WriteObject(encodeTask.Result, true);
            }
        }

        /// <inheritdoc/>
        protected override void StopProcessing()
        {
            _cancellationSource.Cancel();
        }

        /// <inheritdoc/>
        [CanBeNull]
        public object GetDynamicParameters()
        {
            // AudioFile parameter may not be bound yet
            if (Encoder == null) return null;

            return _parameters = SettingAdapter.SettingInfoToParameters(
                AudioEncoderManager.GetSettingInfo(Encoder));
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _cancellationSource.Dispose();
        }
    }
}
