using System;
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
            using (var stream = new MemoryStream(Convert.FromBase64String(comment)))
            using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
            {
                // If the image isn't a "Front Cover" or "Other", return null
                var imageType = reader.ReadUInt32BigEndian();
                if (imageType != 3 && imageType != 0) return null;

                // Seek past the mime type and description
                reader.BaseStream.Seek(reader.ReadUInt32BigEndian(), SeekOrigin.Current);
                reader.BaseStream.Seek(reader.ReadUInt32BigEndian(), SeekOrigin.Current);

                // Seek past the width, height, color depth and type
                reader.BaseStream.Seek(16, SeekOrigin.Current);

                return CoverArtFactory.GetOrCreate(reader.ReadBytes((int) reader.ReadUInt32BigEndian()));
            }
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