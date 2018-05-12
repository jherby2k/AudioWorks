using System;
using System.IO;
using System.Linq;
using AudioWorks.Api;
using AudioWorks.Common;

namespace Perf
{
    class Program
    {
        static void Main(string[] args)
        {
            //Archive();
            ConvertToAac();

            Console.ReadLine();
        }

        static void Archive()
        {
            var audioFiles = new DirectoryInfo(@"C:\Rips\A Perfect Circle\Eat The Elephant").GetFiles("*.flac")
                .Select(file => new TaggedAudioFile(file.FullName)).ToArray<ITaggedAudioFile>();

            new AudioFileAnalyzer(
                    "ReplayGain")
                .Analyze(audioFiles);

            Console.WriteLine("Analysis complete");

            new AudioFileEncoder(
                    "FLAC",
                    new SettingDictionary
                    {
                        ["CompressionLevel"] = 8
                    },
                    @"E:\Users\Jeremy\Music\New FLAC\{Artist}\{Album}",
                    "{TrackNumber} - {Title}",
                    true)
                .Encode(audioFiles);

            Console.WriteLine("Encoding complete");
        }

        static void ConvertToAac()
        {
            var audioFiles = new DirectoryInfo(@"E:\Users\Jeremy\Music\New FLAC\A Perfect Circle\Eat The Elephant").GetFiles("*.flac")
                .Select(file => new TaggedAudioFile(file.FullName)).ToArray<ITaggedAudioFile>();

            var coverArt = CoverArtFactory.Create(@"E:\Users\Jeremy\Music\New FLAC\A Perfect Circle\Eat The Elephant\Folder.png");
            foreach (var audioFile in audioFiles)
                audioFile.Metadata.CoverArt = coverArt;

            Console.WriteLine("Cover art applied");

            new AudioFileEncoder(
                    "AppleAAC",
                    null,
                    @"C:\AAC\{Artist}\{Album}",
                    "{TrackNumber} - {Title}",
                    true)
                .Encode(audioFiles);

            Console.WriteLine("Encoding complete");
        }
    }
}
