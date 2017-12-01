using System.Management.Automation;
using AudioWorks.Api;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    /// <summary>
    /// <para type="synopsis">Gets an object that represents an audio file.</para>
    /// <para type="description">The Get-AudioFile cmdlet gets objects that represent audio files. Audio files expose
    /// metadata, and can be manipulated in various ways by the other AudioWorks cmdlets.</para>
    /// </summary>
    [PublicAPI]
    [Cmdlet(VerbsCommon.Get, "AudioFile", DefaultParameterSetName = "ByPath"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class GetAudioFileCommand : PSCmdlet
    {
        /// <summary>
        /// <para type="description">Specifies the path to an item. This cmdlet gets the item at the specified
        /// location. Wildcards are permitted. This parameter is required, but the parameter name ("Path") is optional.
        /// </para>
        /// <para type="description">Use a dot (.) to specify the current location. Use the wildcard character (*) to
        /// specify all the items in the current location.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "ByPath")]
        public string Path { get; set; }

        /// <summary>
        /// <para type="description">Specifies a path to the item. Unlike the Path parameter, the value of LiteralPath
        /// is used exactly as it is typed. No characters are interpreted as wildcards. If the path includes escape
        /// characters, enclose it in single quotation marks. Single quotation marks tell Windows PowerShell not to
        /// interpret any characters as escape sequences.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "ByLiteralPath"), Alias("PSPath")]
        public string LiteralPath { get; set; }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            try
            {
                foreach (var path in this.GetFileSystemPaths(Path, LiteralPath))
                    WriteObject(new TaggedAudioFile(path));
            }
            catch (ItemNotFoundException e)
            {
                WriteError(new ErrorRecord(e, nameof(ItemNotFoundException), ErrorCategory.ObjectNotFound, Path));
            }
            catch (AudioException e)
            {
                WriteError(new ErrorRecord(e, e.GetType().Name, ErrorCategory.InvalidData, Path));
            }
        }
    }
}
