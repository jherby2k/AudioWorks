using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [StructLayout(LayoutKind.Sequential)]
    struct IoCallbacks
    {
        internal NativeCallbacks.IoCallbacksReadCallback Read;

        internal NativeCallbacks.IoCallbacksWriteCallback Write;

        internal NativeCallbacks.IoCallbacksSeekCallback Seek;

        internal NativeCallbacks.IoCallbacksTellCallback Tell;

        internal NativeCallbacks.IoCallbacksEofCallback Eof;

        readonly NativeCallbacks.IoCallbacksCloseCallback Close;
    }
}