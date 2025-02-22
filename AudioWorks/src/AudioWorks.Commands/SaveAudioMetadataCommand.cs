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

using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using AudioWorks.Api;
using AudioWorks.Common;

namespace AudioWorks.Commands
{
    [Cmdlet(VerbsData.Save, "AudioMetadata", SupportsShouldProcess = true), OutputType(typeof(ITaggedAudioFile))]
    public sealed class SaveAudioMetadataCommand : LoggingCmdlet, IDynamicParameters
    {
        RuntimeDefinedParameterDictionary? _parameters;
        string? _expectedExtension;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ITaggedAudioFile? AudioFile { get; set; }

        [Parameter]
        [ArgumentCompleter(typeof(MetadataFormatCompleter))]
        public string? Format { get; set; }

        [Parameter]
        public SwitchParameter PassThru { get; set; }

        protected override void BeginProcessing()
        {
            if (Format != null)
                _expectedExtension = AudioMetadataEncoderManager.GetEncoderInfo()
                    .FirstOrDefault(info => info.Format.Equals(Format, StringComparison.OrdinalIgnoreCase))?.Extension;
        }

        protected override void ProcessRecord()
        {
            if (_expectedExtension != null)
            {
                var fileExtension = Path.GetExtension(AudioFile!.Path);
                if (!fileExtension.Equals(_expectedExtension, StringComparison.OrdinalIgnoreCase))
                {
                    WriteError(new(new ArgumentException(
                            $"The '{Format}' metadata encoder cannot be used with '{fileExtension}' files."),
                        nameof(ArgumentException), ErrorCategory.InvalidArgument, AudioFile));
                    return;
                }
            }

            try
            {
                if (ShouldProcess(AudioFile!.Path))
                {
                    AudioFile.SaveMetadata(SettingAdapter.ParametersToSettings(_parameters));
                    ProcessLogMessages();
                }
            }
            catch (AudioUnsupportedException e)
            {
                WriteError(new(e, e.GetType().Name, ErrorCategory.InvalidData, AudioFile));
            }

            if (PassThru)
                WriteObject(AudioFile);
        }

        public object? GetDynamicParameters()
        {
            try
            {
                if (Format != null)
                    return _parameters = SettingAdapter.SettingInfoToParameters(
                        AudioMetadataEncoderManager.GetSettingInfoByFormat(Format));

                // AudioFile parameter may not be bound yet
                if (AudioFile == null) return null;

                return _parameters = SettingAdapter.SettingInfoToParameters(
                    AudioMetadataEncoderManager.GetSettingInfoByExtension(Path.GetExtension(AudioFile.Path)));
            }
            catch (AudioUnsupportedException)
            {
                // Some formats (Wave) don't support metadata
                return null;
            }
        }
    }
}
