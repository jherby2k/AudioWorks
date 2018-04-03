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

        [Fact(DisplayName = "Maximum and minimum 16-bit integers are preserved")]
        public void MaxAndMin16BitArePreserved()
        {
            var inSamples = new int[] { short.MinValue, short.MaxValue };
            var outSamples = new int[inSamples.Length];

            var buffer = new SampleBuffer(inSamples, 16);
            buffer.CopyToInterleaved(outSamples, 16);

            Assert.Equal(inSamples, outSamples);
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

        [Fact(DisplayName = "Maximum and minimum 32-bit integers are preserved")]
        public void MaxAndMin32BitArePreserved()
        {
            var inSamples = new[] { int.MinValue, int.MaxValue };
            var outSamples = new int[inSamples.Length];

            var buffer = new SampleBuffer(inSamples, 32);
            buffer.CopyToInterleaved(outSamples, 32);

            Assert.Equal(inSamples, outSamples);
        }
    }
}
