using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents a cover art image.
    /// </summary>
    [PublicAPI]
    public interface ICoverArt
    {
        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>The width.</value>
        int Width { get; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        int Height { get; }

        /// <summary>
        /// Gets the color depth.
        /// </summary>
        /// <value>The color depth.</value>
        int ColorDepth { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="CoverArt"/> is lossless (stored in PNG format).
        /// </summary>
        /// <value><c>true</c> if the image is lossless; otherwise, <c>false</c>.</value>
        bool Lossless { get; }

        /// <summary>
        /// Gets the MIME type.
        /// </summary>
        /// <value>The MIME type.</value>
        [NotNull]
        string MimeType { get; }

        /// <summary>
        /// Gets the raw image data.
        /// </summary>
        /// <returns>The raw image data.</returns>
        [NotNull]
        byte[] GetData();
    }
}