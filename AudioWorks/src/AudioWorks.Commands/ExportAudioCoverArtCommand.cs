using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Management.Automation;
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
    [Cmdlet(VerbsData.Export, "AudioCoverArt"), OutputType(typeof(FileInfo))]
    public sealed class ExportAudioCoverArtCommand : LoggingPSCmdlet
    {
        [CanBeNull] CoverArtExtractor _extractor;

        /// <summary>
        /// <para type="description">Specifies the output directory path.</para>
        /// </summary>
        [NotNull]
        [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized",
            Justification = "Mandatory properties cannot be null by default")]
        [Parameter(Mandatory = true, Position = 0)]
        public string Path { get; set; }

        /// <summary>
        /// <para type="description">Specifies the source audio file.</para>
        /// </summary>
        [NotNull]
        [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized",
            Justification = "Mandatory properties cannot be null by default")]
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true)]
        public ITaggedAudioFile AudioFile { get; set; }

        /// <summary>
        /// <para type="description">Specifies the output file name.</para>
        /// <para type="description">The file extension will be selected automatically and should not be included. If
        /// this parameter is omitted, the name will be the same as the source audio file.</para>
        /// </summary>
        [CanBeNull]
        [Parameter]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Indicates that existing files should be replaced.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Replace { get; set; }

        /// <inheritdoc/>
        protected override void BeginProcessing()
        {
            Telemetry.TrackFirstLaunch();

            _extractor = new CoverArtExtractor(
                SessionState.Path.GetUnresolvedProviderPathFromPSPath(Path),
                Name,
                Replace);
        }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            // ReSharper disable once PossibleNullReferenceException
            var result = _extractor.Extract(AudioFile);

            ProcessLogMessages();

            WriteObject(result);
        }
    }
}
