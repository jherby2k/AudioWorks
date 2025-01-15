﻿/* Copyright © 2019 Jeremy Herbison

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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Extensions.Opus
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification =
        "Instances are created via MEF.")]
    [AudioMetadataDecoderExport(".opus")]
    sealed class OpusAudioMetadataDecoder : IAudioMetadataDecoder
    {
        const string _format = "Opus Comment";

        public string Format => _format;

        public unsafe AudioMetadata ReadMetadata(Stream stream)
        {
            OggStream? oggStream = null;
            Span<byte> buffer = stackalloc byte[4096];

            try
            {
                using (var sync = new OggSync())
                {
                    OggPage page;

                    do
                    {
                        // Read from the buffer into a page
                        while (!sync.PageOut(out page))
                        {
                            var bytesRead = stream.Read(buffer);
                            if (bytesRead == 0)
                                throw new AudioInvalidException("No Ogg stream was found.");

                            var nativeBuffer = new Span<byte>(sync.Buffer(bytesRead).ToPointer(), bytesRead);
                            buffer[..bytesRead].CopyTo(nativeBuffer);
                            sync.Wrote(bytesRead);
                        }

                        oggStream ??= new(SafeNativeMethods.OggPageSerialNo(page));
                        oggStream.PageIn(page);

                        while (oggStream.PacketOut(out var packet))
                            if (packet.PacketNumber == 1)
                                return new CommentHeaderToMetadataAdapter(packet);
                    } while (!SafeNativeMethods.OggPageEos(page));

                    throw new AudioInvalidException("The end of the Ogg stream was reached without finding a header.");
                }
            }
            finally
            {
                oggStream?.Dispose();
            }
        }
    }
}
