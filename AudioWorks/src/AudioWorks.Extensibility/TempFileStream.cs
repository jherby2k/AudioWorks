/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.IO;
using JetBrains.Annotations;

namespace AudioWorks.Extensibility
{
    /// <summary>
    /// Wraps a temporary file. Suitable as a <see cref="MemoryStream"/> replacement for large data sets.
    /// </summary>
    /// <seealso cref="Stream" />
    public sealed class TempFileStream : Stream
    {
        [NotNull]
        readonly FileStream _fileStream = File.Create(Path.GetTempFileName(), 4096, FileOptions.DeleteOnClose);

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
            if (disposing)
                _fileStream.Dispose();

            base.Dispose(disposing);
        }
    }
}
