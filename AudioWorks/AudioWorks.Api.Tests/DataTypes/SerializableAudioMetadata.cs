using AudioWorks.Common;
using JetBrains.Annotations;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests.DataTypes
{
    sealed class SerializableAudioMetadata : AudioMetadata, IXunitSerializable
    {
        public void Deserialize([NotNull] IXunitSerializationInfo info)
        {
            foreach (var property in GetType().GetProperties())
                property.SetValue(this, info.GetValue(property.Name, property.PropertyType));
        }

        public void Serialize([NotNull] IXunitSerializationInfo info)
        {
            foreach (var property in GetType().GetProperties())
                info.AddValue(property.Name, property.GetValue(this));
        }
    }
}
