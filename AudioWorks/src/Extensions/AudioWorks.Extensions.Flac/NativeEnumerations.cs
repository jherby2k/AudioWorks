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
