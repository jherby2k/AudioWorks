using JetBrains.Annotations;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Get, "AudioInfo")]
    public class GetAudioInfoCommand : Cmdlet
    {
    }
}
