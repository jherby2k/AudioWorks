/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    abstract class StreamDecoder : IDisposable
    {
        [NotNull] readonly NativeCallbacks.StreamDecoderReadCallback _readCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderSeekCallback _seekCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderTellCallback _tellCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderLengthCallback _lengthCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderEofCallback _eofCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderWriteCallback _writeCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderMetadataCallback _metadataCallback;
        [NotNull] readonly NativeCallbacks.StreamDecoderErrorCallback _errorCallback;
        [NotNull] readonly Stream _stream;

        [NotNull]
        protected StreamDecoderHandle Handle { get; } = SafeNativeMethods.StreamDecoderNew();

        internal StreamDecoder([NotNull] Stream stream)
        {
            // Need a reference to the callbacks for the lifetime of the decoder
            _readCallback = ReadCallback;
            _seekCallback = SeekCallback;
            _tellCallback = TellCallback;
            _lengthCallback = LengthCallback;
            _eofCallback = EofCallback;
            _writeCallback = WriteCallback;
            _metadataCallback = MetadataCallback;
            _errorCallback = ErrorCallback;

            _stream = stream;
        }

        [SuppressMessage("Performance", "CA1806:Do not ignore method results",
            Justification = "Native method is always expected to return 0")]
        internal void Initialize()
        {
            SafeNativeMethods.StreamDecoderInitStream(Handle,
                _readCallback,
                _seekCallback,
                _tellCallback,
                _lengthCallback,
                _eofCallback,
                _writeCallback,
                _metadataCallback,
                _errorCallback,
                IntPtr.Zero);
        }

        internal bool ProcessMetadata()
        {
            return SafeNativeMethods.StreamDecoderProcessUntilEndOfMetadata(Handle);
        }

        internal void Finish()
        {
            SafeNativeMethods.StreamDecoderFinish(Handle);
        }

        [Pure]
        internal DecoderState GetState()
        {
            return SafeNativeMethods.StreamDecoderGetState(Handle);
        }

        public void Dispose()
        {
            Handle.Dispose();
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        DecoderReadStatus ReadCallback(IntPtr handle, [NotNull] byte[] buffer, ref int bytes, IntPtr userData)
        {
            bytes = _stream.Read(buffer, 0, bytes);
            return bytes == 0 ? DecoderReadStatus.EndOfStream : DecoderReadStatus.Continue;
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        DecoderSeekStatus SeekCallback(IntPtr handle, ulong absoluteOffset, IntPtr userData)
        {
            _stream.Position = (long) absoluteOffset;
            return DecoderSeekStatus.Ok;
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        DecoderTellStatus TellCallback(IntPtr handle, out ulong absoluteOffset, IntPtr userData)
        {
            absoluteOffset = (ulong) _stream.Position;
            return DecoderTellStatus.Ok;
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        DecoderLengthStatus LengthCallback(IntPtr handle, out ulong streamLength, IntPtr userData)
        {
            streamLength = (ulong) _stream.Length;
            return DecoderLengthStatus.Ok;
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        bool EofCallback(IntPtr handle, IntPtr userData)
        {
            return _stream.Position >= _stream.Length;
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        protected virtual DecoderWriteStatus WriteCallback(IntPtr handle, ref Frame frame, IntPtr buffer, IntPtr userData)
        {
            return DecoderWriteStatus.Continue;
        }

        protected virtual void MetadataCallback(IntPtr handle, IntPtr metadataBlock, IntPtr userData)
        {
        }

        [SuppressMessage("Performance", "CA1801:Review unused parameters",
            Justification = "Part of FLAC API")]
        static void ErrorCallback(IntPtr handle, DecoderErrorStatus error, IntPtr userData)
        {
        }
    }
}