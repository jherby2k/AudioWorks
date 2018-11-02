﻿/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Management.Automation;
using AudioWorks.Api;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Get, "AudioAnalyzerInfo"), OutputType(typeof(AudioAnalyzerInfo))]
    public sealed class GetAudioAnalyzerInfoCommand : LoggingCmdlet
    {
        protected override void ProcessRecord()
        {
            var result = AudioAnalyzerManager.GetAnalyzerInfo();
            ProcessLogMessages();
            WriteObject(result, true);
        }
    }
}