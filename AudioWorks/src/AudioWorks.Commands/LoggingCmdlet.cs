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

        private protected void ProcessLogMessages()
        {
            while (CmdletLoggerProvider.Instance.TryDequeueMessage(out var logMessage))
                switch (logMessage)
                {
                    case DebugRecord debugRecord:
                        WriteDebug(debugRecord.Message);
                        break;
                    case InformationRecord informationRecord:
                        WriteInformation(informationRecord);
                        break;
                    case WarningRecord warningRecord:
                        WriteWarning(warningRecord.Message);
                        break;
                }
        }
    }
}