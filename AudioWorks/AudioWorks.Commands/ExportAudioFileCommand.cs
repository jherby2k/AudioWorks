using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        [CanBeNull] ConcurrentDictionary<int, ITaggedAudioFile> _outputAudioFiles = new ConcurrentDictionary<int, ITaggedAudioFile>();
        [CanBeNull] RuntimeDefinedParameterDictionary _parameters;

        /// <summary>
        /// <para type="description">Specifies the encoder to use.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string Encoder { get; set; }

        /// <summary>
        /// <para type="description">Specifies the path to the output directory. This parameter is required, but the
        /// parameter name ("Path") is optional.</para>
        /// <para type="description">Use a dot (.) to specify the current location.</para>
        /// </summary>
        [Parameter(ParameterSetName = "ByPath", Mandatory = true, Position = 1)]
        public string Path { get; set; }

        /// <summary>
        /// <para type="description">Specifies the path to the output directory. Unlike the Path parameter, the value
        /// of LiteralPath is used exactly as it is typed. If the path includes escape characters, enclose it in single
        /// quotation marks. Single quotation marks tell Windows PowerShell not to interpret any characters as escape
        /// sequences.</para>
        /// </summary>
        [Parameter(ParameterSetName = "ByLiteralPath", Mandatory = true, Position = 1), Alias("PSPath")]
        public string LiteralPath { get; set; }

        /// <summary>
        /// <para type="description">Specifies the source audio file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2, ValueFromPipeline = true)]
        public ITaggedAudioFile AudioFile { get; set; }

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
            _outputAudioFiles = new ConcurrentDictionary<int, ITaggedAudioFile>();
            var substituter = new MetadataSubstituter(System.IO.Path.GetInvalidFileNameChars());
            var encoder = new AudioFileEncoder(Encoder, SettingAdapter.ParametersToSettings(_parameters));

            Parallel.For(0, _sourceAudioFiles.Count,
                new ParallelOptions { CancellationToken = _cancellationSource.Token },
                i =>
                {
                    DirectoryInfo outputDirectory;
                    try
                    {
                        outputDirectory = new DirectoryInfo(this.GetFileSystemPaths(
                                substituter.Substitute(Path ?? string.Empty, _sourceAudioFiles[i].Metadata),
                                substituter.Substitute(LiteralPath ?? string.Empty, _sourceAudioFiles[i].Metadata))
                            .First());
                    }
                    catch (ItemNotFoundException e)
                    {
                        outputDirectory = new DirectoryInfo(e.ItemName);
                    }

                    _outputAudioFiles.TryAdd(i, encoder.Export(_sourceAudioFiles[i], _cancellationSource.Token, outputDirectory, substituter.Substitute(Name, _sourceAudioFiles[i].Metadata), Replace));
                });

            if (!_cancellationSource.IsCancellationRequested)
                WriteObject(_outputAudioFiles.OrderBy(item => item.Key).Select(kvp => kvp.Value), true);
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

        /// <inheritdoc />
        public void Dispose()
        {
            _cancellationSource.Dispose();
        }
    }
}
