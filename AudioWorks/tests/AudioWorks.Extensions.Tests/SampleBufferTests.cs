using System;
using Xunit;

namespace AudioWorks.Extensions.Tests
{
    public sealed class SampleBufferTests
    {
        [Fact(DisplayName = "SampleBuffer.Empty returns an empty SampleBuffer")]
        public void SampleBufferEmptyReturnsEmpty()
        {
            Assert.Equal(0, SampleBuffer.Empty.Frames);
        }

        [Theory(DisplayName = "Integers stay in range after conversion to float")]
        [InlineData(1, -1, 0)]
        [InlineData(2, -2, 1)]
        [InlineData(3, -4, 3)]
        [InlineData(4, -8, 7)]
        [InlineData(5, -0x10, 0xF)]
        [InlineData(6, -0x20, 0x1F)]
        [InlineData(7, -0x40, 0x3F)]
        [InlineData(8, -0x80, 0x7F)]
        [InlineData(9, -0x100, 0xFF)]
        [InlineData(10, -0x200, 0x1FF)]
        [InlineData(11, -0x400, 0x3FF)]
        [InlineData(12, -0x800, 0x7FF)]
        [InlineData(13, -0x1000, 0xFFF)]
        [InlineData(14, -0x2000, 0x1FFF)]
        [InlineData(15, -0x4000, 0x3FFF)]
        [InlineData(16, -0x8000, 0x7FFF)]
        [InlineData(17, -0x1_0000, 0xFFFF)]
        [InlineData(18, -0x2_0000, 0x1_FFFF)]
        [InlineData(19, -0x4_0000, 0x3_FFFF)]
        [InlineData(20, -0x8_0000, 0x7_FFFF)]
        [InlineData(21, -0x10_0000, 0xF_FFFF)]
        [InlineData(22, -0x20_0000, 0x1F_FFFF)]
        [InlineData(23, -0x40_0000, 0x3F_FFFF)]
        [InlineData(24, -0x80_0000, 0x7F_FFFF)]
        [InlineData(25, -0x100_0000, 0xFF_FFFF)]
        [InlineData(26, -0x200_0000, 0x1FF_FFFF)]
        [InlineData(27, -0x400_0000, 0x3FF_FFFF)]
        [InlineData(28, -0x800_0000, 0x7FF_FFFF)]
        [InlineData(29, -0x1000_0000, 0xFFF_FFFF)]
        [InlineData(30, -0x2000_0000, 0x1FFF_FFFF)]
        [InlineData(31, -0x4000_0000, 0x3FFF_FFFF)]
        [InlineData(32, -0x8000_0000, 0x7FFF_FFFF)]
        public void SamplesStayInRange(int bitsPerSample, int minValue, int maxValue)
        {
            var inSamples = new[] { minValue, maxValue };
            var outSamples = new float[inSamples.Length];

            new SampleBuffer(inSamples, bitsPerSample).GetSamples(0).CopyTo(outSamples);

            Assert.All(outSamples, sample =>
                Assert.True(sample >= -1.0 && sample <= 1.0));
        }

        [Theory(DisplayName = "Integers are preserved exactly")]
        [InlineData(1, -1, 0)]
        [InlineData(2, -2, 1)]
        [InlineData(3, -4, 3)]
        [InlineData(4, -8, 7)]
        [InlineData(5, -0x10, 0xF)]
        [InlineData(6, -0x20, 0x1F)]
        [InlineData(7, -0x40, 0x3F)]
        [InlineData(8, -0x80, 0x7F)]
        [InlineData(9, -0x100, 0xFF)]
        [InlineData(10, -0x200, 0x1FF)]
        [InlineData(11, -0x400, 0x3FF)]
        [InlineData(12, -0x800, 0x7FF)]
        [InlineData(13, -0x1000, 0xFFF)]
        [InlineData(14, -0x2000, 0x1FFF)]
        [InlineData(15, -0x4000, 0x3FFF)]
        [InlineData(16, -0x8000, 0x7FFF)]
        [InlineData(17, -0x1_0000, 0xFFFF)]
        [InlineData(18, -0x2_0000, 0x1_FFFF)]
        [InlineData(19, -0x4_0000, 0x3_FFFF)]
        [InlineData(20, -0x8_0000, 0x7_FFFF)]
        [InlineData(21, -0x10_0000, 0xF_FFFF)]
        [InlineData(22, -0x20_0000, 0x1F_FFFF)]
        [InlineData(23, -0x40_0000, 0x3F_FFFF)]
        [InlineData(24, -0x80_0000, 0x7F_FFFF)]
        [InlineData(25, -0x100_0000, 0xFF_FFFF)]
        public void IntegersArePreservedExactly(int bitsPerSample, int minValue, int maxValue)
        {
            var inSamples = new int[4096];
            for (int i = 1, j = minValue, k = maxValue; i < inSamples.Length; i += 2, j++, k--)
            {
                inSamples[i - 1] = Math.Min(j, maxValue);
                inSamples[i] = Math.Max(k, minValue);
            }

            var outSamples = new int[inSamples.Length];
            new SampleBuffer(inSamples, 2, bitsPerSample).CopyToInterleaved(outSamples, bitsPerSample);

            Assert.Equal(inSamples, outSamples);
        }
    }
}
