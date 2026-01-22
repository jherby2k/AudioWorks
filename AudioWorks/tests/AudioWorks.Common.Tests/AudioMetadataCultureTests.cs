using System;
using System.Globalization;
using Xunit;

namespace AudioWorks.Common.Tests
{
    public sealed class AudioMetadataCultureTests
    {
        [Fact(DisplayName = "AudioMetadata normalizes numeric strings under different cultures")]
        public void NumericStringsAreCultureInvariant()
        {
            var original = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("de-DE"); // uses ',' as decimal separator and '.' as grouping

                var m = new AudioMetadata { TrackPeak = "0.5", AlbumPeak = "1.234567", TrackGain = "-9.75", AlbumGain = "-9.75" };

                Assert.Equal("0.500000", m.TrackPeak);
                Assert.Equal("1.234567", m.AlbumPeak);
                Assert.Equal("-9.75", m.TrackGain);
                Assert.Equal("-9.75", m.AlbumGain);
            }
            finally
            {
                CultureInfo.CurrentCulture = original;
            }
        }
    }
}
