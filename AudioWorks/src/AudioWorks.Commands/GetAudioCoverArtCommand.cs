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
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Get, "AudioCoverArt", DefaultParameterSetName = "ByPath"), OutputType(typeof(ICoverArt))]
    public sealed class GetAudioCoverArtCommand : LoggingPSCmdlet
    {
        [CanBeNull]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "ByPath")]
        public string Path { get; set; }

        [CanBeNull]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "ByLiteralPath"), Alias("PSPath")]
        public string LiteralPath { get; set; }

        [CanBeNull]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "ByFileInfo")]
        public FileInfo FileInfo { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                if (FileInfo != null)
                    ProcessPath(FileInfo.FullName);
                else
                    foreach (var path in this.GetFileSystemPaths(Path, LiteralPath))
                        ProcessPath(path);
            }
            catch (ItemNotFoundException e)
            {
                WriteError(new ErrorRecord(e, nameof(ItemNotFoundException), ErrorCategory.ObjectNotFound, Path));
            }
        }

        void ProcessPath([NotNull] string path)
        {
            try
            {
                ICoverArt result;

                try
                {
                    result = CoverArtFactory.GetOrCreate(path);
                }
                finally
                {
                    ProcessLogMessages();
                }

                WriteObject(result);
            }
            catch (AudioException e)
            {
                WriteError(new ErrorRecord(e, e.GetType().Name, ErrorCategory.InvalidData, Path));
            }
        }
    }
}
