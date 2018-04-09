using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    abstract class VorbisCommentBlock : MetadataBlock
    {
        internal VorbisCommentBlock()
            : base(MetadataType.VorbisComment)
        {
        }

        internal unsafe void Append([NotNull] string key, [NotNull] string value)
        {
            // Optimization - avoid allocating on the heap
            Span<byte> keySpan = stackalloc byte[Encoding.ASCII.GetByteCount(key) + 1];
            Encoding.ASCII.GetBytes(
                (char*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(key.AsReadOnlySpan())), key.Length,
                (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(keySpan)), keySpan.Length);

            Span<byte> valueSpan = stackalloc byte[Encoding.UTF8.GetByteCount(value) + 1];
            Encoding.UTF8.GetBytes(
                (char*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(value.AsReadOnlySpan())), value.Length,
                (byte*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(valueSpan)), valueSpan.Length);

            SafeNativeMethods.MetadataObjectVorbisCommentEntryFromNameValuePair(
                out var entry,
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(keySpan))),
                new IntPtr(Unsafe.AsPointer(ref MemoryMarshal.GetReference(valueSpan))));

            // The comment takes ownership of the new entry if 'copy' is false
            SafeNativeMethods.MetadataObjectVorbisCommentAppendComment(Handle, entry, false);
        }
    }
}