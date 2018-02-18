using System;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using SixLabors.ImageSharp;

namespace AudioWorks.Common
{
    /// <summary>
    /// Provides static methods for creating <see cref="ICoverArt"/> objects.
    /// </summary>
    public static class CoverArtFactory
    {
        [NotNull] static readonly string[] _acceptedExtensions = { ".bmp", ".png", ".jpg", ".jpeg" };

        /// <summary>
        /// Creates a new <see cref="ICoverArt"/> object from an image file.
        /// </summary>
        /// <param name="path">The fully-qualified path to the file.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown if <paramref name="path"/> does not exist.</exception>
        /// <exception cref="ImageUnsupportedException">Thrown if <paramref name="path"/> is not a supported image
        /// format.</exception>
        /// <exception cref="ImageInvalidException">Thrown if <paramref name="path"/> is not a valid image file.
        /// </exception>
        [NotNull]
        public static ICoverArt Create(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path), "Value cannot be null or empty.");
            if (!File.Exists(path))
                throw new FileNotFoundException($"The file '{path}' cannot be found.", path);

            if (!_acceptedExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase))
                throw new ImageUnsupportedException("Not a supported image file format.", path);

            using (var fileStream = File.OpenRead(path))
                return new CoverArt(fileStream);
        }

        /// <summary>
        /// Creates a lossy <see cref="ICoverArt"/> from a lossless one. If <paramref name="coverArt"/> is already
        /// lossless, this method returns it unmodified.
        /// </summary>
        /// <param name="coverArt">The cover art.</param>
        /// <returns>A lossy copy of an <see cref="ICoverArt"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="coverArt"/> is null.</exception>
        [NotNull]
        public static ICoverArt ConvertToLossy([NotNull] ICoverArt coverArt)
        {
            if (coverArt == null) throw new ArgumentNullException(nameof(coverArt));

            if (!coverArt.Lossless) return coverArt;

            using (var tempStream = new MemoryStream())
            using (var image = Image.Load(coverArt.GetData()))
            {
                image.SaveAsJpeg(tempStream);
                tempStream.Position = 0;
                return new CoverArt(tempStream);
            }
        }
    }
}
