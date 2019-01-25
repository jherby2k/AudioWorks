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
#if NETSTANDARD2_0
using System.Runtime.CompilerServices;
#endif
using System.Runtime.InteropServices;
using System.Text;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    sealed class MetadataStreamDecoder : StreamDecoder
    {
        [NotNull]
        internal VorbisCommentToMetadataAdapter AudioMetadata { get; } = new VorbisCommentToMetadataAdapter();

        internal MetadataStreamDecoder([NotNull] Stream stream)
            : base(stream)
        {
        }

        internal void SetMetadataRespond(MetadataType type)
        {
            SafeNativeMethods.StreamDecoderSetMetadataRespond(Handle, type);
        }

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
                            commentIndex * Marshal.SizeOf<VorbisCommentEntry>()));

                        var commentBytes = new Span<byte>(entry.Entry.ToPointer(), (int) entry.Length);
                        var delimiter = commentBytes.IndexOf((byte) 0x3D); // '='
#if NETSTANDARD2_0
                        var keyBytes = commentBytes.Slice(0, delimiter);
                        var valueBytes = commentBytes.Slice(delimiter + 1);
                        AudioMetadata.Set(
                            Encoding.ASCII.GetString(
                                (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(keyBytes)),
                                keyBytes.Length),
                            Encoding.UTF8.GetString(
                                (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(valueBytes)),
                                valueBytes.Length));
#else
                        AudioMetadata.Set(
                            Encoding.ASCII.GetString(commentBytes.Slice(0, delimiter)),
                            Encoding.UTF8.GetString(commentBytes.Slice(delimiter + 1)));
#endif
                    }

                    break;

                case MetadataType.Picture:
                    var picture = Marshal.PtrToStructure<PictureMetadataBlock>(metadataBlock).Picture;
                    if (picture.Type == PictureType.CoverFront || picture.Type == PictureType.Other)
                        AudioMetadata.CoverArt = CoverArtFactory.GetOrCreate(
                            new Span<byte>(picture.Data.ToPointer(), (int) picture.DataLength));
                    break;
            }
        }
    }
}