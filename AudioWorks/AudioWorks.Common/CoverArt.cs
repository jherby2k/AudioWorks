using System;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using SixLabors.ImageSharp;

namespace AudioWorks.Common
{
    /// <inheritdoc/>
    [PublicAPI]
    [Serializable]
    public sealed class CoverArt : ICoverArt
    {
        [NotNull] readonly byte[] _data;

        /// <inheritdoc/>
        public int Width { get; }

        /// <inheritdoc/>
        public int Height { get; }

        /// <inheritdoc/>
        public int ColorDepth { get; }

        /// <inheritdoc/>
        public bool Lossless { get; }

        /// <inheritdoc/>
        public string MimeType { get; set; }

        /// <inheritdoc/>
        public string FileExtension { get; set; }

        /// <inheritdoc/>
        public ReadOnlySpan<byte> Data => _data;

        internal CoverArt([NotNull] Stream stream)
        {
            var format = Image.DetectFormat(stream) ??
                         throw new ImageInvalidException("Not a valid image.");

            var info = Image.Identify(stream);
            Width = info.Width;
            Height = info.Height;
            ColorDepth = info.PixelType.BitsPerPixel;

            stream.Position = 0;

            // Bitmaps should be compressed in PNG format
            if (format.Name.Equals("BMP", StringComparison.OrdinalIgnoreCase))
                using (var memoryStream = new MemoryStream())
                {
                    using (var image = Image.Load(stream))
                        image.SaveAsPng(memoryStream);
                    _data = memoryStream.ToArray();
                    memoryStream.Position = 0;
                    format = Image.DetectFormat(memoryStream);
                }

            // JPEG and PNG images should be stored verbatim
            else if (stream is MemoryStream currentStream)
                _data = currentStream.ToArray();
            else
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    _data = memoryStream.ToArray();
                }

            Lossless = format.Name.Equals("PNG", StringComparison.OrdinalIgnoreCase);
            MimeType = format.DefaultMimeType;
            FileExtension = $".{format.FileExtensions.First()}";
        }
    }
}
