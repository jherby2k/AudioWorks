## About

This is the public API for AudioWorks, a cross-platform, multi-format audio conversion and tagging suite.

You should also consume one or more "Extensions" packages (see Related Packages below) for full functionality.

More documentation is available on [the GitHub repository](https://github.com/jherby2k/AudioWorks).

## How to Use

### Encode some files

```csharp
var flacFiles = new DirectoryInfo("<path to an album>")
    .GetFiles("*.flac")
    .Select(file => new TaggedAudioFile(file.FullName));

// Export the files to Opus format, organized into directories and named according to metadata
var opusEncoder = new AudioFileEncoder("Opus")
{
    EncodedDirectoryName = @"C:\Output\{Album} by {Artist}",
    EncodedFileName = "{TrackNumber} - {Title}",
    Overwrite = true,
    Settings = new()
    {
        ["SignalType"] = "Speech",
        ["BitRate"] = 32
    }
};
await opusEncoder.EncodeAsync(flacFiles);
```

### Set metadata (tags)

```csharp
var flacFiles = new DirectoryInfo("<path to an album>")
    .GetFiles("*.flac")
    .Select(file => new TaggedAudioFile(file.FullName));

// Add ReplayGain tags, analyzing the files as a single album
var replayGainAnalyzer = new AudioFileAnalyzer("ReplayGain");
await replayGainAnalyzer.AnalyzeAsync(flacFiles);

// Set the artist as well, then persist the new tags to disk
foreach (var flacFile in flacFiles)
{
    flacFile.Metadata.Artist = "Iron Butterfly";
    flacFile.SaveMetadata();
}
```

## Key Features

* Supports MP3, MP4 AAC, FLAC, Apple Lossless (ALAC), Opus, Ogg Vorbis and Wave formats
* High performance architecture
* Runs on Windows, Linux and MacOS

## Main Types

The main types provided by this library are:

* `TaggedAudioFile`
* `AudioFileEncoder`
* `AudioFileAnalyzer`
* `CoverArtExtractor`

## Related Packages

### Extensions providing audio formats

* AAC and Apple Lossless: [AudioWorks.Extensions.Apple](https://www.nuget.org/packages/AudioWorks.Extensions.Apple/)
* FLAC: [AudioWorks.Extensions.Flac](https://www.nuget.org/packages/AudioWorks.Extensions.Flac/)
* MP3: [AudioWorks.Extensions.Lame](https://www.nuget.org/packages/AudioWorks.Extensions.Lame/)
* Opus: [AudioWorks.Extensions.Opus](https://www.nuget.org/packages/AudioWorks.Extensions.Opus/)
* Ogg Vorbis: [AudioWorks.Extensions.Vorbis](https://www.nuget.org/packages/AudioWorks.Extensions.Vorbis/)
* Wave: [AudioWorks.Extensions.Wave](https://www.nuget.org/packages/AudioWorks.Extensions.Wave/)

### Extensions providing analysis

* ReplayGain: [AudioWorks.Extensions.ReplayGain](https://www.nuget.org/packages/AudioWorks.Extensions.ReplayGain/)

## Feedback

AudioWorks is released as open source under the [GNU Affero General Public License](https://github.com/jherby2k/AudioWorks/blob/main/LICENSE). Bug reports and contributions are welcome at [the GitHub repository](https://github.com/jherby2k/AudioWorks).