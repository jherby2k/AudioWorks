﻿using JetBrains.Annotations;
using System.Management.Automation;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Get, "AudioFile")]
    public class GetAudioFileCommand : Cmdlet
    {
        [Parameter]
        public string Path { get; set; }
    }
}
