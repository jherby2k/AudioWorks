using System;
using System.IO;
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
                        var comment = Encoding.UTF8.GetString((byte*) entry.Entry.ToPointer(), (int) entry.Length)
                            .Split(new[] { '=' }, 2);
                        AudioMetadata.Set(comment[0], comment[1]);
                    }
                    break;

                case MetadataType.Picture:
                    var picture = Marshal.PtrToStructure<PictureMetadataBlock>(metadataBlock).Picture;
                    if (picture.Type == PictureType.CoverFront || picture.Type == PictureType.Other)
                        AudioMetadata.CoverArt = CoverArtFactory.Create(
                            new Span<byte>(picture.Data.ToPointer(), (int) picture.DataLength).ToArray());
                    break;
            }
        }
    }
}