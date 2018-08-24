using System.Management.Automation;

namespace AudioWorks.Commands
{
    /// <summary>
    /// A <see cref="PSCmdlet"/> that can process log messages.
    /// </summary>
    /// <seealso cref="PSCmdlet"/>
    public abstract class LoggingPSCmdlet : PSCmdlet
    {
        static LoggingPSCmdlet()
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