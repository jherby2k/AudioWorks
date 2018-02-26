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

        protected override void MetadataCallback(IntPtr handle, IntPtr metadataBlock, IntPtr userData)
        {
            switch ((MetadataType) Marshal.ReadInt32(metadataBlock))
            {
                case MetadataType.VorbisComment:
                    var vorbisComment = Marshal.PtrToStructure<VorbisCommentMetadataBlock>(metadataBlock).VorbisComment;
                    for (var commentIndex = 0; commentIndex < vorbisComment.Count; commentIndex++)
                    {
                        var entry = Marshal.PtrToStructure<VorbisCommentEntry>(IntPtr.Add(vorbisComment.Comments,
                            commentIndex * Marshal.SizeOf<VorbisCommentEntry>()));
                        var commentBytes = new byte[entry.Length];
                        Marshal.Copy(entry.Entry, commentBytes, 0, commentBytes.Length);
                        var comment = Encoding.UTF8.GetString(commentBytes).Split(new[] { '=' }, 2);
                        AudioMetadata.Set(comment[0], comment[1]);
                    }
                    break;

                case MetadataType.Picture:
                    var picture = Marshal.PtrToStructure<PictureMetadataBlock>(metadataBlock).Picture;
                    if (picture.Type == PictureType.CoverFront || picture.Type == PictureType.Other)
                    {
                        var coverBytes = new byte[picture.DataLength];
                        Marshal.Copy(picture.Data, coverBytes, 0, coverBytes.Length);
                        AudioMetadata.CoverArt = CoverArtFactory.Create(coverBytes);
                    }
                    break;
            }
        }
    }
}