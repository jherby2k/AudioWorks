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
            Description = info.GetValue<string>("Description");
            Channels = info.GetValue<int>("Channels");
            BitsPerSample = info.GetValue<int>("BitsPerSample");
            SampleRate = info.GetValue<int>("SampleRate");
            SampleCount = info.GetValue<long>("SampleCount");
            BitRate = info.GetValue<int>("BitRate");
        }

        public void Serialize([NotNull] IXunitSerializationInfo info)
        {
            info.AddValue("Description", Description);
            info.AddValue("Channels", Channels);
            info.AddValue("BitsPerSample", BitsPerSample);
            info.AddValue("SampleRate", SampleRate);
            info.AddValue("SampleCount", SampleCount);
            info.AddValue("BitRate", BitRate);
        }
    }
}
