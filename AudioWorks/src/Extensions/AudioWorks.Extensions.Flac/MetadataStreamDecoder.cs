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
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using AudioWorks.Common;

namespace AudioWorks.Extensions.Flac
{
    sealed class MetadataStreamDecoder : StreamDecoder
    {
        internal VorbisCommentToMetadataAdapter AudioMetadata { get; } = new();

        internal MetadataStreamDecoder(Stream stream)
            : base(stream)
        {
        }

        internal void SetMetadataRespond(MetadataType type) =>
            LibFlac.StreamDecoderSetMetadataRespond(Handle, type);

        protected override unsafe void MetadataCallback(IntPtr handle, IntPtr metadataBlock, IntPtr userData)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch ((MetadataType) Marshal.ReadInt32(metadataBlock))
            {
                case MetadataType.VorbisComment:
                    var vorbisComment = Marshal.PtrToStructure<VorbisCommentMetadataBlock>(metadataBlock).VorbisComment;
                    for (var commentIndex = 0; commentIndex < vorbisComment.Count; commentIndex++)
                    {
                        var entry = Marshal.PtrToStructure<VorbisCommentEntry>(IntPtr.Add(vorbisComment.Comments,
                            commentIndex * sizeof(VorbisCommentEntry)));

                        var entryString = Utf8StringMarshaller.ConvertToManaged(entry.Entry) ?? string.Empty;
                        var delimiter = entryString.IndexOf('=', StringComparison.OrdinalIgnoreCase);
                        AudioMetadata.Set(entryString[..delimiter], entryString[(delimiter + 1)..]);
                    }

                    break;

                case MetadataType.Picture:
                    var picture = Marshal.PtrToStructure<PictureMetadataBlock>(metadataBlock).Picture;
                    if (picture.Type is PictureType.CoverFront or PictureType.Other)
                        AudioMetadata.CoverArt = CoverArtFactory.GetOrCreate(
                            new Span<byte>(picture.Data.ToPointer(), (int) picture.DataLength));
                    break;
            }
        }
    }
}