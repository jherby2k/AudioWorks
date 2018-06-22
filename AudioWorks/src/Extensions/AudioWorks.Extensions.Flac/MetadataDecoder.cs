using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    sealed class MetadataDecoder : StreamDecoder
    {
        [NotNull]
        internal VorbisCommentToMetadataAdapter AudioMetadata { get; } = new VorbisCommentToMetadataAdapter();

        internal MetadataDecoder([NotNull] Stream stream)
            : base(stream)
        {
        }

        internal void SetMetadataRespond(MetadataType type)
        {
            SafeNativeMethods.StreamDecoderSetMetadataRespond(Handle, type);
        }

        protected override unsafe void MetadataCallback(IntPtr handle, IntPtr metadataBlock, IntPtr userData)
        {
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
#if NETCOREAPP2_1
                        AudioMetadata.Set(
                            Encoding.ASCII.GetString(commentBytes.Slice(0, delimiter)),
                            Encoding.UTF8.GetString(commentBytes.Slice(delimiter + 1)));
#else
                        var keyBytes = commentBytes.Slice(0, delimiter);
                        var valueBytes = commentBytes.Slice(delimiter + 1);
                        AudioMetadata.Set(
                            Encoding.ASCII.GetString(
                                (byte*) Unsafe.AsPointer(ref keyBytes.GetPinnableReference()),
                                keyBytes.Length),
                            Encoding.UTF8.GetString(
                                (byte*) Unsafe.AsPointer(ref valueBytes.GetPinnableReference()),
                                valueBytes.Length));
#endif
                    }

                    break;

                case MetadataType.Picture:
                    var picture = Marshal.PtrToStructure<PictureMetadataBlock>(metadataBlock).Picture;
                    if (picture.Type == PictureType.CoverFront || picture.Type == PictureType.Other)
                        AudioMetadata.CoverArt = CoverArtFactory.GetOrCreate(
                            new Span<byte>(picture.Data.ToPointer(), (int) picture.DataLength).ToArray());
                    break;
            }
        }
    }
}