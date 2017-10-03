using AudioWorks.Common;
using JetBrains.Annotations;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests.DataTypes
{
    sealed class SerializableAudioMetadata : AudioMetadata, IXunitSerializable
    {
        public void Deserialize([NotNull] IXunitSerializationInfo info)
        {
            Title = info.GetValue<string>("Title");
            Artist = info.GetValue<string>("Artist");
            Album = info.GetValue<string>("Album");
            Genre = info.GetValue<string>("Genre");
            Comment = info.GetValue<string>("Comment");
            Day = info.GetValue<string>("Day");
            Month = info.GetValue<string>("Month");
            Year = info.GetValue<string>("Year");
            TrackNumber = info.GetValue<string>("TrackNumber");
            TrackCount = info.GetValue<string>("TrackCount");
        }

        public void Serialize([NotNull] IXunitSerializationInfo info)
        {
            info.AddValue("Title", Title);
            info.AddValue("Artist", Artist);
            info.AddValue("Album", Album);
            info.AddValue("Genre", Genre);
            info.AddValue("Comment", Comment);
            info.AddValue("Day", Day);
            info.AddValue("Month", Month);
            info.AddValue("Year", Year);
            info.AddValue("TrackNumber", TrackNumber);
            info.AddValue("TrackCount", TrackCount);
        }
    }
}
