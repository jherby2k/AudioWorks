using System;
using System.IO;

namespace AudioWorks.Extensions
{
    /// <summary>
    /// Wraps a temporary file. Suitable as a <see cref="MemoryStream"/> replacement for large data sets.
    /// </summary>
    /// <seealso cref="Stream" />
    public sealed class TempFileStream : Stream
    {
        readonly FileStream _fileStream = File.Open(Path.GetTempFileName(), FileMode.Truncate);

        /// <inheritdoc/>
        public override void Flush() => _fileStream.Flush();

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count) => _fileStream.Read(buffer, offset, count);

        /// <inheritdoc/>
        public override long Seek(long offset, SeekOrigin origin) => _fileStream.Seek(offset, origin);

        /// <inheritdoc/>
        public override void SetLength(long value) => _fileStream.SetLength(value);

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count) => _fileStream.Write(buffer, offset, count);

        /// <inheritdoc/>
        public override bool CanRead => _fileStream.CanRead;

        /// <inheritdoc/>
        public override bool CanSeek => _fileStream.CanSeek;

        /// <inheritdoc/>
        public override bool CanWrite => _fileStream.CanWrite;

        /// <inheritdoc/>
        public override long Length => _fileStream.Length;

        /// <inheritdoc/>
        public override long Position
        {
            get => _fileStream.Position;
            set => _fileStream.Position = value;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            try
            {
                var fileName = _fileStream.Name;
                if (disposing)
                    _fileStream.Dispose();
                File.Delete(fileName);
            }
            catch (Exception)
            {
                // Ignore any errors during disposal
            }

            base.Dispose(disposing);
        }
    }
}
