using AudioWorks.Common;
using System;
using Xunit;

namespace AudioWorks.Api.Tests
{
    [Collection("Extensions")]
    public sealed class AudioMetadataTests
    {
        [Fact(DisplayName = "AudioMetadata throws an exception if the Title is null")]
        public void AudioMetadataTitleNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata{ Title = null });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Artist is null")]
        public void AudioMetadataArtistNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Artist = null });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Album is null")]
        public void AudioMetadataAlbumNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Album = null });
        }

        [Fact(DisplayName = "AudioMetadata throws an exception if the Comment is null")]
        public void AudioMetadataCommentNullThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() =>
                new AudioMetadata { Comment = null });
        }
    }
}
