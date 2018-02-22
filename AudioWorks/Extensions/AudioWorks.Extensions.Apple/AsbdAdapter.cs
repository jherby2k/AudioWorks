using AudioWorks.Common;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Apple
{
    static class AsbdAdapter
    {
        internal static AudioStreamBasicDescription GetInputDescription([NotNull] AudioInfo info)
        {
            return new AudioStreamBasicDescription
            {
                SampleRate = info.SampleRate,
                AudioFormat = AudioFormat.LinearPcm,
                Flags = AudioFormatFlags.PcmIsSignedInteger,
                BytesPerPacket = 4 * (uint) info.Channels,
                FramesPerPacket = 1,
                BytesPerFrame = 4 * (uint) info.Channels,
                ChannelsPerFrame = (uint) info.Channels,
                BitsPerChannel = (uint) info.BitsPerSample
            };
        }
    }
}