using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;
using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Vorbis
{
    static class CoverArtAdapter
    {
        [Pure, CanBeNull]
        internal static ICoverArt FromComment([NotNull] string comment)
        {
            Span<byte> commentSpan = Convert.FromBase64String(comment);

            // If the image isn't a "Front Cover" or "Other", return null
            var imageType = BinaryPrimitives.ReadUInt32BigEndian(commentSpan);
            if (imageType != 3 && imageType != 0) return null;

            var offset = 4;

            // Seek past the mime type and description
            offset += (int) BinaryPrimitives.ReadUInt32BigEndian(commentSpan.Slice(offset)) + 4;
            offset += (int) BinaryPrimitives.ReadUInt32BigEndian(commentSpan.Slice(offset)) + 4;

            // Seek past the width, height, color depth and type
            offset += 16;

            return CoverArtFactory.GetOrCreate(
                commentSpan.Slice(offset + 4, (int) BinaryPrimitives.ReadUInt32BigEndian(commentSpan.Slice(offset))));
        }

        [Pure, NotNull]
        internal static string ToComment([NotNull] ICoverArt coverArt)
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                // Set the picture type as "Front Cover"
                writer.WriteBigEndian(3);

                var mimeBytes = Encoding.ASCII.GetBytes(coverArt.MimeType);
                writer.WriteBigEndian((uint) mimeBytes.Length);
                writer.Write(mimeBytes);

                var descriptionBytes = Encoding.UTF8.GetBytes(string.Empty);
                writer.WriteBigEndian((uint) descriptionBytes.Length);
                writer.Write(descriptionBytes);

                writer.WriteBigEndian((uint) coverArt.Width);
                writer.WriteBigEndian((uint) coverArt.Height);
                writer.WriteBigEndian((uint) coverArt.ColorDepth);
                writer.WriteBigEndian(0); // Always 0 for PNG and JPEG

                writer.WriteBigEndian((uint) coverArt.Data.Length);
                writer.Write(coverArt.Data.ToArray());

                return Convert.ToBase64String(stream.GetBuffer());
            }
        }
    }
}