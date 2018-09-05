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
        [UsedImplicitly] PngIcon,
        [UsedImplicitly] OtherIcon,
        CoverFront,
        [UsedImplicitly] CoverBack,
        [UsedImplicitly] Leaflet,
        [UsedImplicitly] Media,
        [UsedImplicitly] LeadArtist,
        [UsedImplicitly] Artist,
        [UsedImplicitly] Conductor,
        [UsedImplicitly] Band,
        [UsedImplicitly] Composer,
        [UsedImplicitly] Lyricist,
        [UsedImplicitly] Location,
        [UsedImplicitly] DuringRecording,
        [UsedImplicitly] DuringPerformance,
        [UsedImplicitly] ScreenCapture,
        [UsedImplicitly] BrightFish,
        [UsedImplicitly] Illustration,
        [UsedImplicitly] ArtistLogo,
        [UsedImplicitly] PublisherLogo
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
        [UsedImplicitly] Ok,
        [UsedImplicitly] Uninitialized,
        [UsedImplicitly] OggError,
        [UsedImplicitly] DecoderError,
        [UsedImplicitly] AudioDataMismatch,
        [UsedImplicitly] ClientError,
        [UsedImplicitly] IoError,
        [UsedImplicitly] FramingError,
        [UsedImplicitly] MemoryAllocationError
    }
}
