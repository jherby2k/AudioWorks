using AudioWorks.Common;
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    /// <summary>
    /// <para type="synopsis">Gets information about an audio file.</para>
    /// <para type="description">The Get-AudioInfo cmdlet gets information about audio files. This consists of
    /// immutable information that can't be changed without re-encoding the file.</para>
    /// </summary>
    [PublicAPI]
    [Cmdlet(VerbsCommon.Get, "AudioInfo"), OutputType(typeof(AudioInfo))]
    public sealed class GetAudioInfoCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Specifies the audio file.</para>
        /// </summary>
        [NotNull, SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public IAudioFile AudioFile { get; set; }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            WriteObject(AudioFile.Info);
        }
    }
}
