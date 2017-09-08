using JetBrains.Annotations;

namespace AudioWorks.Extensions.Flac
{
    enum MetadataType
    {
        StreamInfo
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

    enum DecoderReadStatus
    {
        Continue,
        EndOfStream
    }

    enum DecoderSeekStatus
    {
        Ok
    }

    enum DecoderTellStatus
    {
        Ok
    }

    enum DecoderLengthStatus
    {
        Ok
    }

    enum DecoderWriteStatus
    {
        Continue
    }

    enum DecoderErrorStatus
    {
    }
}
