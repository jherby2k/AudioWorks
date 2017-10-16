using JetBrains.Annotations;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests.DataTypes
{
    public sealed class TestAudioInfo : IXunitSerializable
    {
        [NotNull]
        public string Description { get; set; } = string.Empty;

        public int Channels { get; set; }

        public int BitsPerSample { get; set; }

        public int SampleRate { get; set; }

        public long SampleCount { get; set; }

        public int BitRate { get; set; }

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
