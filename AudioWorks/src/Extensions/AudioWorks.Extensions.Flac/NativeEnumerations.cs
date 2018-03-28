using JetBrains.Annotations;

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

    enum PictureType : uint
    {
        Other,
        PngIcon,
        OtherIcon,
        CoverFront,
        CoverBack,
        Leaflet,
        Media,
        LeadArtist,
        Artist,
        Conductor,
        Band,
        Composer,
        Lyricist,
        Location,
        DuringRecording,
        DuringPerformance,
        ScreenCapture,
        BrightFish,
        Illustration,
        ArtistLogo,
        PublisherLogo
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
        [UsedImplicitly] LostSync,
        [UsedImplicitly] BadHeader,
        [UsedImplicitly] FrameCrcMismatch,
        [UsedImplicitly] UnparseableStream
    }

    enum EncoderWriteStatus
    {
        Ok
    }

    enum EncoderSeekStatus
    {
        Ok
    }

    enum EncoderTellStatus
    {
        Ok
    }

    enum EncoderState
    {
        Ok,
        Uninitialized,
        OggError,
        DecoderError,
        AudioDataMismatch,
        ClientError,
        IoError,
        FramingError,
        MemoryAllocationError
    }
}
