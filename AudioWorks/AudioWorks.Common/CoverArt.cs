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

        internal CoverArt([NotNull] Stream stream)
        {
            // TODO use Image.Identify in next release to get an IImageInfo instead
            var format = Image.DetectFormat(stream) ??
                         throw new ImageInvalidException("Not a valid image.");

            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    using (var image = Image.Load(stream))
                    {
                        Width = image.Width;
                        Height = image.Height;

                        if (format.Name.Equals("BMP", StringComparison.OrdinalIgnoreCase))
                        {
                            // Bitmaps should be converted to PNG format to save memory
                            image.SaveAsPng(memoryStream);
                            memoryStream.Position = 0;
                            format = Image.DetectFormat(memoryStream);
                        }

                        // JPEG and PNG should be stored verbatim
                        else
                        {
                            stream.Position = 0;
                            stream.CopyTo(memoryStream);
                        }
                    }
                }
                catch (NotSupportedException)
                {
                    throw new ImageInvalidException("Not a valid image.");
                }

                _data = memoryStream.ToArray();
            }

            ColorDepth = 24; //TODO can read this from IImageInfo in next release
            Lossless = format.Name.Equals("PNG", StringComparison.OrdinalIgnoreCase);
            MimeType = format.DefaultMimeType;
            FileExtension = $".{format.FileExtensions.First()}";
        }

        /// <inheritdoc/>
        public byte[] GetData()
        {
            return (byte[]) _data.Clone();
        }
    }
}
