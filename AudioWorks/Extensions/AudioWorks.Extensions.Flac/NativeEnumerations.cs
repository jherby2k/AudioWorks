namespace AudioWorks.Extensions.Flac
{
    enum MetadataType
    {
        StreamInfo
    }

    enum DecoderState
    {
        SearchForMetadata,
        ReadMetadata,
        SearchForFrameSync,
        ReadFrame,
        EndOfStream,
        OggError,
        SeekError,
        Aborted,
        MemoryAllocationError,
        Uninitialized
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
}
