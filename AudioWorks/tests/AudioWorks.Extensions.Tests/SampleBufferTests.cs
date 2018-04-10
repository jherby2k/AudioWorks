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

        [Fact(DisplayName = "Maximum and minimum 16-bit integers stay in range")]
        public void MaxAndMin16BitStayInRange()
        {
            var inSamples = new int[] { short.MinValue, short.MaxValue };
            var outSamples = new float[inSamples.Length];

            var buffer = new SampleBuffer(inSamples, 16);
            buffer.GetSamples(0).CopyTo(outSamples);

            Assert.All(outSamples, f =>
                Assert.True(f >= -1f && f <= 1f));
        }

        [Fact(DisplayName = "Maximum and minimum 32-bit integers stay in range")]
        public void MaxAndMin32BitStayInRange()
        {
            var inSamples = new[] { int.MinValue, int.MaxValue };
            var outSamples = new float[inSamples.Length];

            var buffer = new SampleBuffer(inSamples, 32);
            buffer.GetSamples(0).CopyTo(outSamples);

            Assert.All(outSamples, f =>
                Assert.True(f >= -1f && f <= 1f));
        }

        [Theory(DisplayName = "Integers are preserved exactly")]
        [InlineData(8, -0x80, 0x7F)]
        [InlineData(16, -0x8000, 0x7FFF)]
        [InlineData(24, -0x80_0000, 0x7F_FFFF)]
        [InlineData(31, -0x40_0000, 0x3F_FFFF)]
        public void IntegersArePreservedExactly(int bitsPerSample, int minValue, int maxValue)
        {
            var inSamples = new int[4096];
            for (int i = 1, j = minValue, k = maxValue; i < inSamples.Length; i += 2, j++, k--)
            {
                inSamples[i - 1] = Math.Min(j, maxValue);
                inSamples[i] = Math.Max(k, minValue);
            }

            var outSamples = new int[inSamples.Length];
            var buffer = new SampleBuffer(inSamples, 2, bitsPerSample);
            buffer.CopyToInterleaved(outSamples, bitsPerSample);

            Assert.Equal(inSamples, outSamples);
        }
    }
}
