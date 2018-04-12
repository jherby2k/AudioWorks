using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    /// <summary>
    /// <para type="synopsis">Gets metadata describing an audio file.</para>
    /// <para type="description">The Get-AudioMetadata cmdlet gets information about audio files. This consists of
    /// fields that can be modified and optionally persisted to disk using other cmdlets.</para>
    /// </summary>
    [PublicAPI]
    [Cmdlet(VerbsCommon.Get, "AudioMetadata"), OutputType(typeof(AudioMetadata))]
    public sealed class GetAudioMetadataCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Specifies the audio file.</para>
        /// </summary>
        [NotNull, SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ITaggedAudioFile AudioFile { get; set; }

        /// <inheritdoc/>
        protected override void BeginProcessing()
        {
            Telemetry.TrackFirstLaunch();
        }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            WriteObject(AudioFile.Metadata);
        }
    }
}
