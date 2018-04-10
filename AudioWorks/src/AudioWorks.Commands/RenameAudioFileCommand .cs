using System.Management.Automation;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    /// <summary>
    /// <para type="synopsis">Renames an audio file.</para>
    /// <para type="description">The Rename-AudioFile cmdlet renames audio files.
    /// </para>
    /// </summary>
    [PublicAPI]
    [Cmdlet(VerbsCommon.Rename, "AudioFile"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class RenameAudioFileCommand : Cmdlet
    {
        /// <summary>
        /// <para type="description">Specifies the new file name.</para>
        /// <para type="description">The file extension will be selected automatically and should not be included.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Specifies the audio file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true)]
        public ITaggedAudioFile AudioFile { get; set; }

        /// <summary>
        /// <para type="description">Indicates that existing files should be replaced.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Replace { get; set; }

        /// <summary>
        /// <para type="description">Returns an object representing the item with which you are working. By default,
        /// this cmdlet does not generate any output.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            AudioFile.Rename(Name, Replace);
            if (PassThru)
                WriteObject(AudioFile);
        }
    }
}
