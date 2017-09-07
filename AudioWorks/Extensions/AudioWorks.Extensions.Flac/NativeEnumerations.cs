namespace AudioWorks.Extensions.Flac
{
    enum MetadataType
    {
        StreamInfo
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
