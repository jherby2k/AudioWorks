using System.Management.Automation;

namespace AudioWorks.Commands
{
    /// <summary>
    /// A <see cref="Cmdlet"/> that can process log messages.
    /// </summary>
    /// <seealso cref="Cmdlet"/>
    public abstract class LoggingCmdlet : Cmdlet
    {
        static LoggingCmdlet()
        {
            CmdletLoggerProvider.Instance.Enable();
        }
    }
}