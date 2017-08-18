using AudioWorks.Api;
using AudioWorks.Common;
using JetBrains.Annotations;
using System.IO;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Get, "AudioFile")]
    public class GetAudioFileCommand : Cmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        public string Path { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                AudioFileFactory.Create(Path);
            }
            catch (FileNotFoundException e)
            {
                WriteError(new ErrorRecord(e, nameof(FileNotFoundException), ErrorCategory.InvalidArgument, Path));
            }
            catch (UnsupportedFileException e)
            {
                WriteError(new ErrorRecord(e, nameof(UnsupportedFileException), ErrorCategory.InvalidData, Path));
            }
        }
    }
}
