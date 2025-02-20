/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.IO;
using System.Management.Automation;
using AudioWorks.Api;
using AudioWorks.Common;

namespace AudioWorks.Commands
{
    [Cmdlet(VerbsData.Export, "AudioCoverArt"), OutputType(typeof(FileInfo))]
    public sealed class ExportAudioCoverArtCommand : LoggingPSCmdlet
    {
        CoverArtExtractor? _extractor;

        [Parameter(Mandatory = true, Position = 0)]
        public string? Path { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true)]
        public ITaggedAudioFile? AudioFile { get; set; }

        [Parameter]
        public string? Name { get; set; }

        [Parameter]
        public SwitchParameter Force { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            _extractor = new()
            {
                EncodedDirectoryName = SessionState.Path.GetUnresolvedProviderPathFromPSPath(Path),
                EncodedFileName = Name ?? string.Empty,
                Overwrite = Force
            };
        }

        protected override void ProcessRecord()
        {
            var result = _extractor!.Extract(AudioFile!);

            ProcessLogMessages();

            if (result != null)
                WriteObject(result);
        }
    }
}
