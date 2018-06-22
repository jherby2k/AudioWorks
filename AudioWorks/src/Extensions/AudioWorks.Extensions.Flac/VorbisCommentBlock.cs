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
            Span<byte> keySpan = stackalloc byte[Encoding.ASCII.GetMaxByteCount(key.Length) + 1];
            Span<byte> valueSpan = stackalloc byte[Encoding.UTF8.GetMaxByteCount(value.Length) + 1];

#if NETCOREAPP2_1
            var keyLength = Encoding.ASCII.GetBytes(key, keySpan) + 1;
            var valueLength = Encoding.UTF8.GetBytes(value, valueSpan) + 1;
#else
            var keyLength = Encoding.ASCII.GetBytes(
                                (char*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(key.AsSpan())),
                                key.Length,
                                (byte*) Unsafe.AsPointer(ref keySpan.GetPinnableReference()),
                                keySpan.Length)
                            + 1;

            var valueLength = Encoding.UTF8.GetBytes(
                                  (char*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(value.AsSpan())),
                                  value.Length,
                                  (byte*) Unsafe.AsPointer(ref valueSpan.GetPinnableReference()),
                                  valueSpan.Length)
                              + 1;
#endif

            SafeNativeMethods.MetadataObjectVorbisCommentEntryFromNameValuePair(
                out var entry,
                new IntPtr(Unsafe.AsPointer(ref keySpan.Slice(0, keyLength).GetPinnableReference())),
                new IntPtr(Unsafe.AsPointer(ref valueSpan.Slice(0, valueLength).GetPinnableReference())));

            // The comment takes ownership of the new entry if 'copy' is false
            SafeNativeMethods.MetadataObjectVorbisCommentAppendComment(Handle, entry, false);
        }
    }
}