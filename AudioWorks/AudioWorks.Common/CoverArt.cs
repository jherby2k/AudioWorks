using System;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using SixLabors.ImageSharp;

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents a cover art image for one or more audio files.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public sealed class CoverArt
    {
        [NotNull] static readonly string[] _acceptedExtensions = { ".bmp", ".png", ".jpg" };

        [NotNull] readonly byte[] _data;

        /// <summary>
        /// Gets the image format.
        /// </summary>
        /// <value>The image format.</value>
        public string Format { get; set; }

        /// <summary>
        /// Gets the MIME type.
        /// </summary>
        /// <value>The MIME type.</value>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; }

        /// <summary>
        /// Gets the color depth.
        /// </summary>
        /// <value>The color depth.</value>
        public int ColorDepth { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoverArt"/> class.
        /// </summary>
        /// <param name="path">The fully-qualified path to the file.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown if <paramref name="path"/> does not exist.</exception>
        public CoverArt([NotNull] string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path), "Value cannot be null or empty.");
            if (!File.Exists(path))
                throw new FileNotFoundException($"The file '{path}' cannot be found.", path);

            if (!_acceptedExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase))
                throw new ImageUnsupportedException("Not a supported image file format.", path);

            using (var fileStream = File.OpenRead(path))
            {
                // TODO use Image.Identify in next release to get an IImageInfo instead
                var format = Image.DetectFormat(fileStream) ??
                             throw new ImageInvalidException("Not a valid image file.", path);

                using (var memoryStream = new MemoryStream())
                {
                    try
                    {
                        using (var image = Image.Load(fileStream))
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
                                fileStream.Position = 0;
                                fileStream.CopyTo(memoryStream);
                            }
                        }
                    }
                    catch (NotSupportedException)
                    {
                        throw new ImageInvalidException("Not a valid image file.", path);
                    }

                    _data = memoryStream.ToArray();
                }

                Format = format.Name;
                MimeType = format.DefaultMimeType;
                ColorDepth = 24; //TODO can read this from IImageInfo in next release
            }
        }

        /// <summary>
        /// Gets the raw image data.
        /// </summary>
        /// <returns>The raw image data.</returns>
        /// <exception cref="NotImplementedException"></exception>
        [NotNull]
        public byte[] GetData()
        {
            return _data;
        }
    }
}
