using AudioWorks.Api;
using AudioWorks.Common;
using JetBrains.Annotations;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Get, "AudioFile", DefaultParameterSetName = "ByPath"), OutputType(typeof(AudioFile))]
    public class GetAudioFileCommand : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "ByPath")]
        public string Path { get; set; }

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "ByLiteralPath"), Alias("PSPath")]
        public string LiteralPath { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                foreach (var path in this.GetFileSystemPaths(Path, LiteralPath))
                    WriteObject(AudioFileFactory.Create(path));
            }
            catch (ItemNotFoundException e)
            {
                WriteError(new ErrorRecord(e, nameof(ItemNotFoundException), ErrorCategory.ObjectNotFound, Path));
            }
            catch (AudioUnsupportedException e)
            {
                WriteError(new ErrorRecord(e, nameof(AudioUnsupportedException), ErrorCategory.InvalidData, Path));
            }
            catch (AudioInvalidException e)
            {
                WriteError(new ErrorRecord(e, nameof(AudioInvalidException), ErrorCategory.InvalidData, Path));
            }
        }
    }
}
