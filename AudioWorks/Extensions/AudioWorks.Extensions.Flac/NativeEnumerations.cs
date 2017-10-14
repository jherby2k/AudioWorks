using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace AudioWorks.Extensions.Flac
{
    enum MetadataType
    {
        StreamInfo,
        Padding,
        [UsedImplicitly] Application,
        SeekTable,
        VorbisComment,
        [UsedImplicitly] CueSheet,
        Picture
    }

    enum DecoderState
    {
        [UsedImplicitly] SearchForMetadata,
        [UsedImplicitly] ReadMetadata,
        [UsedImplicitly] SearchForFrameSync,
        [UsedImplicitly] ReadFrame,
        [UsedImplicitly] EndOfStream,
        [UsedImplicitly] OggError,
        [UsedImplicitly] SeekError,
        [UsedImplicitly] Aborted,
        [UsedImplicitly] MemoryAllocationError,
        [UsedImplicitly] Uninitialized
    }

    [SuppressMessage("Naming", "CA1717:Only FlagsAttribute enums should have plural names",
        Justification = "'Status' is plural")]
    enum DecoderReadStatus
    {
        Continue,
        EndOfStream
    }

    [SuppressMessage("Naming", "CA1717:Only FlagsAttribute enums should have plural names",
        Justification = "'Status' is plural")]
    enum DecoderSeekStatus
    {
        Ok
    }

    [SuppressMessage("Naming", "CA1717:Only FlagsAttribute enums should have plural names",
        Justification = "'Status' is plural")]
    enum DecoderTellStatus
    {
        Ok
    }

    [SuppressMessage("Naming", "CA1717:Only FlagsAttribute enums should have plural names",
        Justification = "'Status' is plural")]
    enum DecoderLengthStatus
    {
        Ok
    }

    [SuppressMessage("Naming", "CA1717:Only FlagsAttribute enums should have plural names",
        Justification = "'Status' is plural")]
    enum DecoderWriteStatus
    {
        Continue
    }

    [SuppressMessage("Naming", "CA1717:Only FlagsAttribute enums should have plural names",
        Justification = "'Status' is plural")]
    enum DecoderErrorStatus
    {
        [UsedImplicitly] LostSync,
        [UsedImplicitly] BadHeader,
        [UsedImplicitly] FrameCrcMismatch,
        [UsedImplicitly] UnparseableStream
    }
}
