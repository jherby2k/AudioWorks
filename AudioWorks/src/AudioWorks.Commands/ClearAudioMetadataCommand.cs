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

using AudioWorks.Common;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace AudioWorks.Commands
{
    [Cmdlet(VerbsCommon.Clear, "AudioMetadata"), OutputType(typeof(ITaggedAudioFile))]
    public sealed class ClearAudioMetadataCommand : LoggingCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ITaggedAudioFile? AudioFile { get; set; }

        [Parameter]
        public SwitchParameter Title { get; set; }

        [Parameter]
        public SwitchParameter Artist { get; set; }

        [Parameter]
        public SwitchParameter Album { get; set; }

        [Parameter]
        public SwitchParameter AlbumArtist { get; set; }

        [Parameter]
        public SwitchParameter Composer { get; set; }

        [Parameter]
        public SwitchParameter Genre { get; set; }

        [Parameter]
        public SwitchParameter Comment { get; set; }

        [Parameter]
        public SwitchParameter Day { get; set; }

        [Parameter]
        public SwitchParameter Month { get; set; }

        [Parameter]
        public SwitchParameter Year { get; set; }

        [Parameter]
        public SwitchParameter TrackNumber { get; set; }

        [Parameter]
        public SwitchParameter TrackCount { get; set; }

        [Parameter]
        public SwitchParameter Loudness { get; set; }

        [Parameter]
        public SwitchParameter CoverArt { get; set; }

        [Parameter]
        public SwitchParameter PassThru { get; set; }

        protected override void ProcessRecord()
        {
            // Get all switches that are set (excluding PassThru)
            var definedSwitches = typeof(ClearAudioMetadataCommand).GetProperties(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(p =>
                    p.Name != nameof(PassThru) && p.GetValue(this) is SwitchParameter paramValue && paramValue)
                .ToList();

            // If no switches are set, clear all metadata
            if (definedSwitches.Count == 0)
                AudioFile!.Metadata.Clear();
            else
                foreach (var switchItem in definedSwitches)
                    switch (switchItem.Name)
                    {
                        case nameof(Loudness):
                            AudioFile!.Metadata.TrackPeak = string.Empty;
                            AudioFile.Metadata.AlbumPeak = string.Empty;
                            AudioFile.Metadata.TrackGain = string.Empty;
                            AudioFile.Metadata.AlbumGain = string.Empty;
                            break;

                        case nameof(CoverArt):
                            AudioFile!.Metadata.CoverArt = null;
                            break;

                        // All other fields are strings
                        default:
                            typeof(AudioMetadata).GetProperty(switchItem.Name)
                                ?.SetValue(AudioFile!.Metadata, string.Empty);
                            break;
                    }

            ProcessLogMessages();

            if (PassThru)
                WriteObject(AudioFile);
        }
    }
}
