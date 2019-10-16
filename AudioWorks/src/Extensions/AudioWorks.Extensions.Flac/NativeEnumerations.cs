/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Diagnostics.CodeAnalysis;

namespace AudioWorks.Extensions.Flac
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    enum MetadataType
    {
        StreamInfo,
        Padding,
        Application,
        SeekTable,
        VorbisComment,
        CueSheet,
        Picture
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
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

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
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

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    enum DecoderErrorStatus
    {
        LostSync,
        BadHeader,
        FrameCrcMismatch,
        UnparseableStream
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

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
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
