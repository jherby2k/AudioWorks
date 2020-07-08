﻿/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Management.Automation;
using AudioWorks.Common;

namespace AudioWorks.Commands
{
    public abstract class LoggingCmdlet : Cmdlet
    {
        private protected CmdletLoggerProvider LoggerProvider { get; } =
            LoggerManager.AddSingletonProvider(() => new CmdletLoggerProvider());

        private protected void ProcessLogMessages()
        {
            while (LoggerProvider.TryDequeueMessage(out var logMessage))
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