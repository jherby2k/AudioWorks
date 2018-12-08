using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AudioWorks.Api;

namespace PerfTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var audioFiles = new DirectoryInfo(@"C:\Users\jerem\Desktop\And All That Could Have Been\FLAC")
                .GetFiles("*.flac")
                .Select(file => new TaggedAudioFile(file.FullName));

            var encoder = new AudioFileEncoder("FLAC",
                    @"C:\Users\jerem\Desktop\And All That Could Have Been\Output\FLAC\{Album} by {Artist}",
                    @"{TrackNumber} - {Title}")
                { Overwrite = true };

            foreach (var result in await encoder.EncodeAsync(audioFiles))
                Console.WriteLine(result.Path);
        }
    }
}
